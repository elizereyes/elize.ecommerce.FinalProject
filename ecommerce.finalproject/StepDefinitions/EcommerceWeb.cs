﻿using ecommerce.finalproject.POMs;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;

namespace ecommerce.finalproject.StepDefinitions

{

    [Binding]
    public class StepDefinitions
    {

        IWebDriver driver;
        Helper helper;

        private readonly ScenarioContext _scenarioContext;

        private UserDetails _userDetails;

        public StepDefinitions(ScenarioContext scenarioContext, UserDetails userDetails)
        {
            _scenarioContext = scenarioContext;
            _userDetails = userDetails;
            driver = (IWebDriver)_scenarioContext["webdriver"]; //gets driver
            helper = new Helper(driver);

        }

        //Background used as a POCO
        [Given(@"these details")]
        public void GivenTheseDetails(Table table)
        {
            _userDetails = table.CreateInstance<UserDetails>(); //customer details 
        }
        
        [Given(@"I am logged in")]
        public void GivenThatIAmLoggedIn()
        {
            LoginPass_POM login = new LoginPass_POM(driver);
            login.Notice(); //dismisses the notice which states that the website is for demo purposes
            login.Login(); //log in with username
            login.Pass(); //finds and inputs the password

            //waits so that the dashboard is displayed before moving on
            helper.WaitForElement(By.PartialLinkText("Dashboard"));
        }
        
        [When(@"I add an '([^']*)' into my cart")]
        public void WhenIAddAnItemIntoMyCart(string item)
        {           
            //After logging in, will navigate to the shop page
            Navigate_POM nav = new Navigate_POM(driver);
            Shop_POM add = new Shop_POM(driver);
            nav.Navigate("Shop");
            add.AddItem(item); //adds the item stated in the scenario to cart and views the cart
            nav.Navigate("Cart");
        }


        //Test case 1
        [When(@"provide '([^']*)' discount code")]
        public void WhenProvideADiscountCode(string discountCode)
        {
            Cart_POM discount = new Cart_POM(driver);
            discount.EnterDiscount(discountCode); //enters the coupon code which is named in the scenario

            //waits so that the discount is added before continuing (using partial link text Remove)
            helper.WaitForElement(By.PartialLinkText("[Remove]"));
        }

        [Then(@"my total should update correctly with a discount of '([^']*)'%")]
        public void ThenMyTotalShouldUpdateCorrectly(string discountPercent)
        {
            Cart_POM discount = new Cart_POM(driver);
           
            //this asserts that the discount value is correct, if not then will show a message
            Decimal percent = Decimal.Parse(discountPercent), couponValue = discount.GetCouponPercentValue();

            //if the couponvalue isn't equals to the expected discount percent then take a screenshot
            if (!couponValue.Equals(percent))
            {
                helper.Screenshot("Discount");
                Assert.Fail(String.Format("Discount should be {0}% off but the discount was {1}% off", percent, couponValue));
            }

            Decimal[] values = discount.GetTotalValues();//checks the total of order
                                                         //working out if the total value shown(values[0]) is the same as the expected total(values[1]), if not will show an error message

            //if the total isn't equals to the expected total then take a screenshot
            if (!values[0].Equals(values[1]))
            {
                helper.Scroll(By.LinkText("Proceed to checkout"));
                helper.Screenshot("Total");
                Assert.Fail(String.Format("Total should be {0} but the Total was {1}", values[1], values[0]));
            }
        }

        //Test case 2
        [When(@"I provide valid billing details")]
        public void WhenIProvideValidBillingDetails()
        {
            Cart_POM Cart = new Cart_POM(driver);
            Checkout_POM Checkout = new Checkout_POM(driver);
            Cart.ProceedCheckout();
            Checkout.BillingDetails(_userDetails); //fills in the billing details and places the order

        }

        [Then(@"my order should show up in the order history")]
        public void OrderinHistory()
        {
            //waits for the driver url to change so that the Order number can be returned
            helper.WaitToNav("order-received");

            Checkout_POM Checkout = new Checkout_POM(driver);
            int checkoutOrderNo = Checkout.GetOrderNo(); //finds the order number and writes out the results in the test

            Navigate_POM nav = new Navigate_POM(driver);
            nav.Navigate("My account");
            nav.Navigate("Orders");//navigates to the order history 
           
            //waits until the url is in orders 
            helper.WaitToNav("orders");

            OrderHistory_POM History = new OrderHistory_POM(driver);
            //if the latest order isn't in the order history then take a screenshot
            if (!History.IsOrderInHistory(checkoutOrderNo))//checks the order history to see if it matches the order no provided at checkout
            {
                helper.Screenshot("History");
                Assert.Fail("Latest Order isnt in Order History");
            }

        }
    }
}