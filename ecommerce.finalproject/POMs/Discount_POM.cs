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


        //Service Method
        public Discount_POM EnterDiscount(String discount)
        {
            enterDiscount.Click();
            enterDiscount.SendKeys("edgewords");
            enterDiscount.SendKeys(Keys.Enter);
            return this;
        }
       
    }
}
