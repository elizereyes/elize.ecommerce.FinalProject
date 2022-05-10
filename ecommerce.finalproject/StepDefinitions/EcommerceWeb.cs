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
        
        [When(@"I am logged in")]
        public void GivenThatIAmLoggedIn()
        {
            LoginPass_POM Login = new LoginPass_POM(driver);
            Login.Notice(); //dismisses the notice which states that the website is for demo purposes
            Login.Login(); //log in with username
            Login.Pass(); //finds and inputs the password

            //waits so that the dashboard is displayed before moving on
            helper.WaitForElement("Dashboard");
        }
        
        [When(@"I add an '([^']*)' into my cart")]
        public void WhenIAddAnItemIntoMyCart(string item)
        {
            //After logging in, will navigate to the shop page
            Cart_POM Add = new Cart_POM(driver);
            Add.AddItem(item); //adds the item stated in the scenario to cart and views the cart
        }


        //Test case 1
        [When(@"provide '([^']*)' discount code")]
        public void WhenProvideADiscountCode(string discountCode)
        {
            Discount_POM discount = new Discount_POM(driver);
            discount.EnterDiscount(discountCode); //enters the coupon code which is named edgewords

            //waits so that the dashboard is added before continuing (using partial link text Remove)
            helper.WaitForElement("[Remove]");
        }

        [Then(@"my total should update correctly with a discount of '([^']*)'%")]
        public void ThenMyTotalShouldUpdateCorrectly(string discountPercent)
        {
            Discount_POM discount = new Discount_POM(driver);
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
                helper.Screenshot("Total");
                Assert.Fail(String.Format("Total should be {0} but the Total was {1}", values[1], values[0]));
            }
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
            //waits for the driver url to change so that the Order number can be returned
            helper.WaitToNav("order-received");

            Checkout_POM Checkout = new Checkout_POM(driver);
            int checkoutOrderNo = Checkout.GetOrderNo(); //finds the order number and writes out the results in the test

            OrderHistory_POM History = new OrderHistory_POM(driver);
            History.Navigate(); //navigates to the order history 

            //waits until the url is in orders 
            helper.WaitToNav("orders");

            //if the latest order isn't in the order history then take a screenshot
            if (!History.IsOrderInHistory(checkoutOrderNo))//checks the order history to see if it matches the order no provided at checkout
            {
                helper.Screenshot("History");
                Assert.Fail("Latest Order isnt in Order History");
            }


        }
    }
}