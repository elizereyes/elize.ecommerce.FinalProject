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

        [Given(@"that I am logged in")]
        public void GivenThatIAmLoggedIn()
        {
            
            LoginPass_POM Login = new LoginPass_POM(driver);
            Login.Login(Environment.GetEnvironmentVariable("userName"));

            LoginPass_POM Pass = new LoginPass_POM(driver);
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
        }

        [Then(@"my total should update correctly")]
        public void ThenMyTotalShouldUpdateCorrectly()
        {
           
        }
    }
}