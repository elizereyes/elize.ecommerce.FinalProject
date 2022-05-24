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
        public IWebElement removeCoupon => driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
        public IWebElement removeItem => driver.FindElement(By.CssSelector(".remove"));

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
            //takes the coupon value(and * it by 100 to get the percentage) and gets everything after the £ symbol
            //and then dividing it by the subtotal(by parsing the string as a decimal it can do division)
            Decimal discount = (Decimal.Parse(couponValue.Text.Substring(1)) * 100) / Decimal.Parse(subtotalValue.Text.Substring(1));
           
            Console.WriteLine(String.Format("GetCouponPercentValue: returned {0}%", discount)); //The method returns this discount

            //Testing if the maths works
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
           
            //prints it in the console, returnvalues prints the actual total
            Console.WriteLine(String.Format("GetTotalValues: expectedTotal: £{0} and returnValues[0] was: £{1}", expectedTotal, returnValues[0])); 
            return returnValues;
        }

        public void ClearCart()
        {
            Helper helper = new Helper(driver);
            if (driver.FindElements(By.CssSelector(".cart-empty")).Count == 0)//If cart isnt empty, .count to see if there is anything in the cart empty element
            {
                Console.WriteLine(driver.FindElements(By.CssSelector(".cart-empty")).Count);
                Console.WriteLine("Cart isn't empty");
                if (driver.FindElements(By.CssSelector(".cart-discount.coupon-edgewords > th")).Count != 0)//If coupon used
                {
                    Console.WriteLine("Coupon is used");
                    //if a coupon is used then it will click the remove button (removing the coupon first, so that coupon doesn't restack on the next test)
                    removeCoupon.Click();
                    //and also clicks the delete button on the item in the cart
                    removeItem.Click();
                    Thread.Sleep(1000);
                }
                else
                {   //just removes the item if theres no coupon used
                    //clicks the delete button on the item in the cart
                    Console.WriteLine("Coupon isn't used");
                    removeItem.Click();
                }
            }
            Console.WriteLine("Cart has been cleared"); //writes in the console that the Clear Cart has worked 

        }
    }
}