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
            string BaseUrl = Environment.GetEnvironmentVariable("BaseURL"); //uses the runsettings baseurl
            driver.Url = BaseUrl;
        }


   
        IWebElement removeCoupon => driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
        IWebElement removeItem => driver.FindElement(By.CssSelector(".remove"));

        [After]
        public void TearDown()
        {
            //removes any coupons and items from the cart so it's ready for the next test, in order to not restack
            driver.FindElement(By.Id("menu-item-44")).Click(); //clicks on cart

            try
            {
                //if cart is empty there will be no exception
                //bool is checking if it's displayed as a true or false. (if false exception is thrown, if true, then no exception thrown then the code is skipped over)
                bool isCartEmpty = driver.FindElement(By.CssSelector(".cart-empty")).Displayed;
            }
            catch (NoSuchElementException) //but if the cart is populated an element not such element exception is thrown
            {

               // Console.WriteLine("Cart Isn't Empty");
                try 
                {
                    //checks if a coupon is used by locating the element
                    bool isCouponUsed = driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > th")).Displayed;
                    if (isCouponUsed) 
                    {
                        //if a coupon is used then it will click the remove button (removing the coupon first, so that coupon doesn't restack on the next test)
                        removeCoupon.Click();
                        //and also clicks the delete button on the item in the cart
                        removeItem.Click();
                    }
                }
                catch (NoSuchElementException) //if a coupon isn't used then a no such element exception is thrown
                {
                   // Console.WriteLine("Coupon isn't Used");
                    removeItem.Click(); //so item is removed 
                }
            }

            driver.FindElement(By.Id("menu-item-46")).Click(); //clicks on my account on the top nav
            driver.FindElement(By.PartialLinkText("Log out")).Click(); //clicks on the logout 

            IWebDriver sharedDriver = (IWebDriver)_scenarioContext["webdriver"];
            sharedDriver.Quit(); //Quits the WebDriver
        }

    }

} 
