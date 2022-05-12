using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace ecommerce.finalproject
{
    public class Helper
    {
        IWebDriver driver;

        public Helper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Screenshot(string error)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
          
            //puts the screenshot in this directory, adding the date and time as well as where it is
            String location = String.Format(@"C:\Users\ElizeReyes\Documents\Training\Ecommerce Project\FailedOn{0} {1}.png", error, DateTime.Now.ToString("yyyy-MM-dd h.mm.ss-tt"));
            screenshot.SaveAsFile(@location, ScreenshotImageFormat.Png);
        }

        public void Scroll(string textToScroll)

        {
            //scrolls down to the LinkText 
            var actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.LinkText(textToScroll)));
            actions.Perform();
        }

        public void WaitToNav(string urlLocation)
        {
            //does a wait till the Url is shown
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.Url.Contains(urlLocation));

        }

        public void WaitForElement(string textToFind)
        {
            //does a wait till the PartialLinkText is shown on the page
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(By.PartialLinkText(textToFind)).Displayed);
        }

       
    }
}



