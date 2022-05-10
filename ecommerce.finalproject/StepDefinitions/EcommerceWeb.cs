using ecommerce.finalproject.POMs;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;

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

        //Background used as a POCO
        [Given(@"these details")]
        public void GivenTheseDetails(Table table)
        {
            _userDetails = table.CreateInstance<UserDetails>(); //customer details 
        }
        
        [When(@"I am logged in")]
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


        //Test case 1
        [When(@"provide a discount code")]
        public void WhenProvideADiscountCode()
        {
            Discount_POM discount = new Discount_POM(driver);
            discount.EnterDiscount("edgewords"); //enters the coupon code which is named edgewords
            Thread.Sleep(2000);
        }

        [Then(@"my total should update correctly")]
        public void ThenMyTotalShouldUpdateCorrectly()
        {
            Discount_POM discount = new Discount_POM(driver);
            //this asserts that the discount value is correct, if not then will show a message
            Decimal percent = 15, couponValue = discount.GetCouponPercentValue();
            Assert.That(couponValue, Is.EqualTo(percent), String.Format("Discount should be {0}% off but the discount was {1}% off", percent, couponValue));//checks if 15% is applied

            Decimal[] values = discount.GetTotalValues();//checks the total of order
            //working out if the total value shown(values[0]) is the same as the expected total(values[1]), if not will show an error message
            Assert.That(values[0], Is.EqualTo(values[1]), String.Format("Total should be {0} but the Total was {1}", values[1], values[0]));
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
            Thread.Sleep(1000);
            Assert.That(History.IsOrderInHistory(checkoutOrderNo), "Latest Order isnt in Order History");//checks the order history to see if it matches the order no provided at checkout
        }
    }
}