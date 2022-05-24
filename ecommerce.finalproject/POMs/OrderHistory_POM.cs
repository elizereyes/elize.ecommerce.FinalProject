using OpenQA.Selenium;

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

        //Stores latest order number element
        public IWebElement latestOrderNo => driver.FindElement(By.CssSelector("tr:nth-of-type(1) > .woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));


        //Service Method


        public Boolean IsOrderInHistory(int checkoutOrderNo)
        {
            //Writes in the Console that the latestOrder number is X and that the checkoutOrderNo is X
            Console.WriteLine(String.Format("IsOrderInHistory: latestOrderNo: {0} and checkoutOrderNo: {1}", latestOrderNo.Text, checkoutOrderNo));
            
            //Checks that the latest order number from the Order History is the same as the checkout order number from when the order was placed
            if (int.Parse(latestOrderNo.Text.Substring(1)).Equals(checkoutOrderNo))
                return true;
            else return false;
            //uses .Text to get the order number from the selected element, then uses the substring to get everything after the #
            //Then compares that the 2 ints are the same value so therefore the order was created successfully
            //uses ints as the order number won't be a decimal
            
           
        }
    }
}