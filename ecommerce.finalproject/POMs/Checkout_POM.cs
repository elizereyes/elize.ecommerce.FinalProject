using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class Checkout_POM
    {
        IWebDriver driver;
        public Checkout_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement proceedCheckout => driver.FindElement(By.LinkText("Proceed to checkout"));
        public IWebElement firstName => driver.FindElement(By.CssSelector("input#billing_first_name"));
        public IWebElement lastName => driver.FindElement(By.CssSelector("input#billing_last_name"));
        public IWebElement streetAddress => driver.FindElement(By.CssSelector("input[name='billing_address_1']"));
        public IWebElement city => driver.FindElement(By.CssSelector("input#billing_city"));
        public IWebElement postCode => driver.FindElement(By.CssSelector("input#billing_postcode"));
        public IWebElement phoneNo => driver.FindElement(By.CssSelector("input#billing_phone"));
        public IWebElement placeOrder => driver.FindElement(By.CssSelector("button#place_order"));
        public IWebElement orderNo => driver.FindElement(By.CssSelector(".order > strong"));


        //Service Method
        public Checkout_POM BillingDetails()
        {
            proceedCheckout.Click();
            firstName.Clear();
            firstName.SendKeys("Elize");
            lastName.Clear();
            lastName.SendKeys("Reyes");
            streetAddress.Clear();
            streetAddress.SendKeys("123 Nfocus Street");
            city.Clear();
            city.SendKeys("Telford");
            postCode.Clear();
            postCode.SendKeys("TF2 9FT");
            phoneNo.Clear();
            phoneNo.SendKeys("0712345678");
            Thread.Sleep(1000);
            placeOrder.Click();
            return this;
        }

        public Decimal OrderNo()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.Url.Contains("order-received"));

            
            Console.WriteLine("Order number: " + orderNo.Text);
            return Decimal.Parse(orderNo.Text);
        }

    }
}
