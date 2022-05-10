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
        public IWebElement shop => driver.FindElement(By.LinkText("Shop"));
        public IWebElement addCart => driver.FindElement(By.CssSelector("[name=add-to-cart]"));
        public IWebElement clickCart => driver.FindElement(By.LinkText("Cart"));

        //Service Method
        public void AddItem(string item)
        {
            //adding Hoodie with Logo into Cart 
            shop.Click();
            driver.FindElement(By.PartialLinkText(item)).Click();
            addCart.Click();
            clickCart.Click();
        }

    }
}