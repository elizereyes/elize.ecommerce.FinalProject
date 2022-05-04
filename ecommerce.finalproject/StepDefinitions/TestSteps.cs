using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using TechTalk.SpecFlow;
using ecommerce.finalproject.POMs;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow.Assist;

namespace ecommerce.finalproject.StepDefinitions

{

    [Binding]
    public class StepDefinitions
    {

        IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        private UserDetails _userDetails;


        public StepDefinitions(ScenarioContext scenarioContext, UserDetails userDetails)
        {
            _scenarioContext = scenarioContext;
            _userDetails = userDetails;
            driver = (IWebDriver)_scenarioContext["webdriver"]; //gets driver

        }

        [Given(@"these details")]
        public void GivenTheseDetails(Table table)
        {
            _userDetails = table.CreateInstance<UserDetails>(); //customer details using POCO
        }

        //Test case 1
        [Given(@"that I am logged in")]
        public void GivenThatIAmLoggedIn()
        {

            LoginPass_POM Login = new LoginPass_POM(driver);
            Login.Notice(); //dismisses the notice which states that the website is for demo purposes
            Login.Login(); //log in with username
            Login.Pass(); //finds and inputs the password


            Thread.Sleep(1000); //used a thread sleep so that the website has time to successfully log in before going to next step

        }

        [When(@"I add an item into my cart")]
        public void WhenIAddAnItemIntoMyCart()
        {
            //After logging in, will navigate to the shop page
            Cart_POM Add = new Cart_POM(driver);
            Add.AddHoodie(); //adds the hoodie with logo to cart and views the cart
        }

        [When(@"provide a discount code")]
        public void WhenProvideADiscountCode()
        {
            Discount_POM discount = new Discount_POM(driver);
            discount.EnterDiscount("edgewords"); //enters the coupon code which is named edgewords

            Thread.Sleep(2000);

            discount.CheckCouponPercentIsCorrect(15); //checks if 15% is applied
        }

        [Then(@"my total should update correctly")]
        public void ThenMyTotalShouldUpdateCorrectly()
        {
            Cart_POM Cart = new Cart_POM(driver);
            Cart.CheckTotal(); //checks the total of order
        }


        //Test case 2
        [When(@"I provide valid billing details")]
        public void WhenIProvideValidBillingDetails()
        {
            Checkout_POM Checkout = new Checkout_POM(driver);
            Checkout.BillingDetails(_userDetails); //fills in the billing details and places the order

        }

        [Then(@"my order should show up in the order history")]
        public void OrderinHistory()
        {
            Checkout_POM Checkout = new Checkout_POM(driver);
            int checkoutOrderNo = Checkout.GetOrderNo(); //finds the order number and writes out the results in the test

            OrderHistory_POM History = new OrderHistory_POM(driver);
            History.Navigate(); //navigates to the order history 
            History.CheckNewOrder(checkoutOrderNo); //checks the order history to see if it matches the order no provided at checkout
        }
    }
}