using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class Discount_POM
    {
        IWebDriver driver;
        public Discount_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement enterDiscount => driver.FindElement(By.CssSelector("input#coupon_code"));
        public IWebElement couponValue => driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        public IWebElement subtotalValue => driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));

        //Service Method
        public Discount_POM EnterDiscount(String discount)
        {
            enterDiscount.Click();
            enterDiscount.SendKeys("edgewords");
            enterDiscount.SendKeys(Keys.Enter);
            return this;
        }

        public Discount_POM CheckCouponPercentIsCorrect(Decimal percent)
        {

            //checks if the correct discount % is taken off 
            Decimal discount = Decimal.Parse(subtotalValue.Text.Substring(1)) / Decimal.Parse(couponValue.Text.Substring(1)); //takes the subtotal value and gets rid of the £ symbol and then dividing it by the discount value
            Assert.That(discount, Is.EqualTo(percent), String.Format("Discount should be {0}% off but the discount was {1}% off", percent, discount)); //this asserts that the discount value is correct, if not then will show a message
            return this;
        }


    }
}
