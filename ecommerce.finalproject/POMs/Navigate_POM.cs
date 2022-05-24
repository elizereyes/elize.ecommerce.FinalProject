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
            //Writes in the Console where the page has been navigated to
            Console.WriteLine(String.Format("Navigated to : {0}", webpage));
            
            //uses the LinkText to navigate to a specific page
            driver.FindElement(By.LinkText(webpage)).Click();
        }

    }
}