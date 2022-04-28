using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

            //Navigates to the log in page
            string BaseUrl = Environment.GetEnvironmentVariable("BaseURL");
            driver.Url = BaseUrl;
        }

        [After]
        public void TearDown()
        {
            driver.FindElement(By.Id("menu-item-46")).Click();
            driver.FindElement(By.PartialLinkText("Log out")).Click();

            //IWebDriver sharedDriver = (IWebDriver)_scenarioContext["webdriver"];
           // sharedDriver.Quit();
        }
    }
}