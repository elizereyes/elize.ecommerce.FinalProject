using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.finalproject.POMs;


namespace ecommerce.finalproject
{
    [Binding]
    public class Hooks
    {
        public IWebDriver driver;

        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp()
        {
           driver = new ChromeDriver();
           _scenarioContext["webdriver"] = driver;
        }
        [After]
        public void TearDown()
        {

            TopNav_POM account = new TopNav_POM(driver);
            account.MyAccount();
            driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--customer-logout > a"))



            IWebDriver sharedDriver = (IWebDriver)_scenarioContext["webdriver"];
           // sharedDriver.Quit();
        }
    }
}