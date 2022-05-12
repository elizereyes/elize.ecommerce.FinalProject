using OpenQA.Selenium;


namespace ecommerce.finalproject.POMs
{
    public class TopNav_POM
    {
        IWebDriver driver;
        public TopNav_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Service Method
        public void Navigate(string webpage)
        {
            driver.FindElement(By.LinkText(webpage)).Click();
        }

    }
}