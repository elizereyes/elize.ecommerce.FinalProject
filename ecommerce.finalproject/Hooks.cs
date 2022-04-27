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
        }
        [After]
        public void TearDown()
        {
            //driver.Quit();

            IWebDriver sharedDriver = (IWebDriver)_scenarioContext["webdriver"];
            sharedDriver.Quit();
        }
    }
}