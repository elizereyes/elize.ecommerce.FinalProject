using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
        public void BillingDetails(UserDetails userDetails)
        {
            proceedCheckout.Click();
            firstName.Clear(); //makes sure the box is empty before continuing
            firstName.SendKeys(userDetails.firstName);
            lastName.Clear();
            lastName.SendKeys(userDetails.lastName);
            streetAddress.Clear();
            streetAddress.SendKeys(userDetails.streetAddress);
            city.Clear();
            city.SendKeys(userDetails.city);
            postCode.Clear();
            postCode.SendKeys(userDetails.postCode);
            phoneNo.Clear();
            phoneNo.SendKeys(userDetails.phoneNo);
            Thread.Sleep(1000); //waits for the check payments icon to be selected before continuing to place the order
            placeOrder.Click();
        }

        public int GetOrderNo()
        {
           //waits for the driver url to change so that the Order number can be returned
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.Url.Contains("order-received"));


            Console.WriteLine("Order number: " + orderNo.Text); //writes in the test what the order number is
            return int.Parse(orderNo.Text); //parses the order as text
        }


    }
}
