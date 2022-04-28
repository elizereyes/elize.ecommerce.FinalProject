using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class Cart_POM
    {
        IWebDriver driver;
        public Cart_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement shop => driver.FindElement(By.LinkText("Shop"));
        public IWebElement clickItem => driver.FindElement(By.PartialLinkText("Hoodie with Logo"));
        public IWebElement addCart => driver.FindElement(By.CssSelector("[name=add-to-cart]"));
        public IWebElement clickCart => driver.FindElement(By.LinkText("Cart"));


        //Service Method
        public Cart_POM AddHoodie()
        {
            shop.Click();
            clickItem.Click();
            addCart.Click();
            clickCart.Click();
            return this;
        }

    }
}
