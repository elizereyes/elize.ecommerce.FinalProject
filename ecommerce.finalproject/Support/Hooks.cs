using NUnit.Framework;
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
        Helper helper;

        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            helper = new Helper(driver);
        }

        [Before]
        public void SetUp()
        {
            driver = new ChromeDriver();
            _scenarioContext["webdriver"] = driver; //gets the driver

            //Navigates to the log in page
            string BaseUrl = Environment.GetEnvironmentVariable("BaseURL"); //uses the runsettings baseurl
            driver.Url = BaseUrl;
        }


        IWebElement removeCoupon => driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
        IWebElement removeItem => driver.FindElement(By.CssSelector(".remove"));

        [After]
        public void TearDown()
        {
            //removes any coupons and items from the cart so it's ready for the next test, in order to not restack
            driver.FindElement(By.LinkText("Cart")).Click(); //clicks on cart
           //helper.WaitForElement("Calculate shipping");

            if (driver.FindElements(By.CssSelector(".cart-empty")).Count == 0)//If cart isnt empty, .count to see if there is anything in the cart empty element
            {
                if (driver.FindElements(By.CssSelector(".cart-discount.coupon-edgewords > th")).Count != 0)//If coupon used
                {
                    //if a coupon is used then it will click the remove button (removing the coupon first, so that coupon doesn't restack on the next test)
                    removeCoupon.Click();
                    //and also clicks the delete button on the item in the cart
                    removeItem.Click();
                }
                else //just removes the item if theres no coupon used
                    //clicks the delete button on the item in the cart
                    removeItem.Click();
            }

            driver.FindElement(By.Id("menu-item-46")).Click(); //clicks on my account on the top nav
            driver.FindElement(By.PartialLinkText("Log out")).Click(); //clicks on the logout 

            driver.Quit(); //Quits the WebDriver
        }

    }

} 
