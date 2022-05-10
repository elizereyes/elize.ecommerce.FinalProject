using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
          
            String location = String.Format(@"C:\Users\ElizeReyes\Documents\Training\Ecommerce Project\FailedOn{0}{1}.png", error, DateTime.Now.ToString("yyyy-MM-dd-h.mm.ss-tt"));
            screenshot.SaveAsFile(@location, ScreenshotImageFormat.Png);
        }

        public void WaitToNav(string urlLocation)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.Url.Contains(urlLocation));

        }

        public void WaitForElement(string textToFind)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(By.PartialLinkText(textToFind)).Displayed);
        }

       
    }
}



