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

namespace ecommerce.finalproject.StepDefinitions

{

    [Binding]
    public class StepDefinitions
    {

        IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public StepDefinitions(ScenarioContext scenarioContext)
        {

            _scenarioContext = scenarioContext;
            driver = (IWebDriver)_scenarioContext["webdriver"];

        }

        //Test case 1
        [Given(@"that I am logged in")]
        public void GivenThatIAmLoggedIn()
        {

            LoginPass_POM Login = new LoginPass_POM(driver);
            Login.Notice();
            Login.Login(Environment.GetEnvironmentVariable("userName"));
            Login.Pass(Environment.GetEnvironmentVariable("passWord"));


            Thread.Sleep(1000);

        }

        [When(@"I add an item into my cart")]
        public void WhenIAddAnItemIntoMyCart()
        {
            //After logging in, will navigate to the shop page
            Cart_POM Add = new Cart_POM(driver);
            Add.AddHoodie();
        }

        [When(@"provide a discount code")]
        public void WhenProvideADiscountCode()
        {
            Discount_POM discount = new Discount_POM(driver);
            discount.EnterDiscount("edgewords");

            Thread.Sleep(2000);

            discount.CheckCouponPercentIsCorrect(15); //checks if 15% is applied
        }

        [Then(@"my total should update correctly")]
        public void ThenMyTotalShouldUpdateCorrectly()
        {

            Cart_POM Cart = new Cart_POM(driver);
            Cart.CheckTotal();

        }


        //Test case 2
        [When(@"I provide valid billing details")]
        public void WhenIProvideValidBillingDetails()
        {
            Checkout_POM Checkout = new Checkout_POM(driver);
            Checkout.BillingDetails();
            Checkout.OrderNo();

        }

        [Then(@"my order should show up in the order history")]
        public void OrderinHistory()
        {
            OrderHistory_POM History = new OrderHistory_POM(driver);
            History.Navigate();
            History.CheckOrder();
        }


    }
}