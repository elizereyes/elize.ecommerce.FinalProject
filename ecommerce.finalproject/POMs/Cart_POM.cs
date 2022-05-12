using OpenQA.Selenium;

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
        public IWebElement enterDiscount => driver.FindElement(By.CssSelector("input#coupon_code"));
        public IWebElement couponValue => driver.FindElement(By.CssSelector(".cart-discount.coupon-edgewords > td > .amount.woocommerce-Price-amount"));
        public IWebElement subtotalValue => driver.FindElement(By.CssSelector(".cart-subtotal > td > .amount.woocommerce-Price-amount"));
        public IWebElement totalValue => driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount"));
        public IWebElement shippingValue => driver.FindElement(By.CssSelector(".shipping > td > .amount.woocommerce-Price-amount"));
        public IWebElement proceedCheckout => driver.FindElement(By.LinkText("Proceed to checkout"));

        //Service Method
        public void EnterDiscount(String discount)
        {
            //enters the discount 
            enterDiscount.Click();
            enterDiscount.SendKeys(discount);
            enterDiscount.SendKeys(Keys.Enter);
        }


        public void ProceedCheckout()
        {
           proceedCheckout.Click();

        }

        public Decimal GetCouponPercentValue()
        {

            //checks if the correct discount % is taken off 
            //takes the coupon value(and * it by 100 to get the percentage) and gets everything after the £ symbol and then dividing it by the subtotal(by parsing the string as a decimal it can do division)
            Decimal discount = (Decimal.Parse(couponValue.Text.Substring(1)) * 100) / Decimal.Parse(subtotalValue.Text.Substring(1));

            //Testing if it works
            //String test15 = "£6.75";
            //Decimal discount = (Decimal.Parse(test15.Substring(1)) * 100) / Decimal.Parse(subtotalValue.Text.Substring(1));
            return discount;

        }

         
        public Decimal[] GetTotalValues()
        {
            //working out the expected total of the whole order
            Decimal expectedTotal = Decimal.Parse(subtotalValue.Text.Substring(1)) - Decimal.Parse(couponValue.Text.Substring(1)) + Decimal.Parse(shippingValue.Text.Substring(1));
            //return both the total displayed value and the expected total, so that we can display extra information on the assert. 
            Decimal[] returnValues = { Decimal.Parse(totalValue.Text.Substring(1)), expectedTotal };
            return returnValues;
        }
    }
}