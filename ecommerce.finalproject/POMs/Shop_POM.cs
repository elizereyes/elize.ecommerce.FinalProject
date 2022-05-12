using OpenQA.Selenium;


namespace ecommerce.finalproject.POMs
{
    public class Shop_POM
    {
        IWebDriver driver;
        public Shop_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement addCart => driver.FindElement(By.CssSelector("[name=add-to-cart]"));
        public IWebElement clickCart => driver.FindElement(By.LinkText("Cart"));

        //Service Method
        public void AddItem(string item)
        {
            //adding item into Cart 
            driver.FindElement(By.PartialLinkText(item)).Click();
            addCart.Click();
        }

    }
}