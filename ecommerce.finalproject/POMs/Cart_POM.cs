using NUnit.Framework;
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
        public IWebElement couponValue => driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        public IWebElement subtotalValue => driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        public IWebElement totalValue => driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount"));
        public IWebElement shippingValue => driver.FindElement(By.CssSelector(".shipping > td > .amount.woocommerce-Price-amount"));

        //Service Method
        public void AddHoodie()
        {
            shop.Click();
            clickItem.Click();
            addCart.Click();
            clickCart.Click();
        }

        public void CheckTotal()
        {
            Decimal expectedTotal = Decimal.Parse(subtotalValue.Text.Substring(1)) - Decimal.Parse(couponValue.Text.Substring(1)) + Decimal.Parse(shippingValue.Text.Substring(1));
            Assert.That(Decimal.Parse(totalValue.Text.Substring(1)), Is.EqualTo(expectedTotal), String.Format("Total should be {0} but the Total was {1}", expectedTotal, totalValue.Text.Substring(1)));
        }

    }
}