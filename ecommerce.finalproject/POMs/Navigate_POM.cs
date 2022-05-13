using OpenQA.Selenium;


namespace ecommerce.finalproject.POMs
{
    public class Navigate_POM
    {
        IWebDriver driver;
        public Navigate_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Service Method
        public void Navigate(string webpage)
        {
            Console.WriteLine(String.Format("Navigated to : {0}", webpage));
            driver.FindElement(By.LinkText(webpage)).Click();
        }

    }
}