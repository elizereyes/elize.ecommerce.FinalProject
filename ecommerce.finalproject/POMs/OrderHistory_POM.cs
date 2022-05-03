using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class OrderHistory_POM
    {
        IWebDriver driver;
        public OrderHistory_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement myAccount => driver.FindElement(By.LinkText("My account"));
        public IWebElement orders => driver.FindElement(By.LinkText("Orders"));
        public IWebElement latestOrderNo => driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));


        //Service Method
        public OrderHistory_POM Navigate()
        {
            myAccount.Click();
            orders.Click();
            return this;
        }

        public OrderHistory_POM CheckOrder()
        {
            Console.WriteLine(latestOrderNo.Text.Substring(1));
           
            
            Assert.That()
            return this;
        }
    }
}
