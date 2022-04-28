using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class TopNav_POM
    {
        IWebDriver driver;
        public TopNav_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement home => driver.FindElement(By.LinkText("Home"));
        public IWebElement shop => driver.FindElement(By.LinkText("Shop"));
        public IWebElement cart => driver.FindElement(By.LinkText("Cart"));
        public IWebElement checkout => driver.FindElement(By.LinkText("Checkout"));
        public IWebElement myAccount => driver.FindElement(By.LinkText("My account"));
        public IWebElement blog => driver.FindElement(By.LinkText("Blog"));


        //Service Methods
        public TopNav_POM Home()
        {
            home.Click();
            return this;
        }

        public TopNav_POM Shop()
        {
            shop.Click();
            return this;
        }

        public TopNav_POM Cart()
        {
            cart.Click();
            return this;
        }

        public TopNav_POM Checkout()
        {
            checkout.Click();
            return this;
        }

        public TopNav_POM MyAccount()
        {
            myAccount.Click();
            return this;
        }

        public TopNav_POM Blog()
        {
            blog.Click();
            return this;
        }

    }
}
