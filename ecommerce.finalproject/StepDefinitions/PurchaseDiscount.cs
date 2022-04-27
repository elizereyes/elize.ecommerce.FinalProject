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

namespace ecommerce.finalproject.StepDefinitions

{

    [Binding]
    public class StepDefinitions
    {

        IWebDriver driver;         
        
        private readonly ScenarioContext _scenarioContext;

        string baseURL = "https://www.edgewordstraining.co.uk/"; //baseURL for easier referencing


        public StepDefinitions(ScenarioContext scenarioContext)
        {
           
            _scenarioContext = scenarioContext;
            driver = (IWebDriver)_scenarioContext["webdriver"];
        }
        
        [Given(@"that I am logged in")]
        public void GivenThatIAmLoggedIn()
        {
            //Navigates to the log in page
            driver.Url = baseURL + "demo-site/my-account/";

            //Login with username and password
            driver.FindElement(By.Id("username")).SendKeys("elize.reyes@nfocus.co.uk");
            driver.FindElement(By.Id("password")).SendKeys("Pr0j3c7PW0rd");
            //Clicks on the login button
            driver.FindElement(By.CssSelector("[name=login]")).Click();

        }

        [When(@"I add an item into my cart")]
        public void WhenIAddAnItemIntoMyCart()
        {
            //After logging in, will navigate to the shop page
            driver.FindElement(By.LinkText("Shop")).Click();
            driver.FindElement(By.PartialLinkText("Hoodie with Logo")).Click();
            driver.FindElement(By.CssSelector("[name=add-to-cart]")).Click();
            driver.FindElement(By.LinkText("Cart")).Click(); //Goes to the cart


        }

        [When(@"provide a discount code")]
        public void WhenProvideADiscountCode()
        {
            IWebElement discount = driver.FindElement(By.CssSelector("input#coupon_code"));
            discount.SendKeys("edgewords");
            discount.SendKeys(Keys.Enter);
        }

        [Then(@"my total should update correctly")]
        public void ThenMyTotalShouldUpdateCorrectly()
        {
            Assert.That(driver.FindElement(By.ClassName("woocommerce-message")).Displayed);
            Console.WriteLine("Discount code applied");
        }
    }
}