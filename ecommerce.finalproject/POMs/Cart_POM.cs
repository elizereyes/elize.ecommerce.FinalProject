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
        public IWebElement clickItem => driver.FindElement(By.PartialLinkText("Hoodie with Logo"));
        public IWebElement addCart => driver.FindElement(By.CssSelector("[name=add-to-cart]"));
        public IWebElement clickCart => driver.FindElement(By.LinkText("Cart"));

        //Service Method
        public void AddHoodie()
        {
            //adding Hoodie with Logo into Cart 
            shop.Click();
            clickItem.Click();
            addCart.Click();
            clickCart.Click();
        }

    }
}