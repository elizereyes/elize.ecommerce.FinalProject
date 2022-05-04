using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.finalproject.POMs
{
    public class LoginPass_POM
    {
        IWebDriver driver;
        public LoginPass_POM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement userName => driver.FindElement(By.Id("username"));
        public IWebElement passWord => driver.FindElement(By.Id("password"));
        public IWebElement logIn => driver.FindElement(By.CssSelector("[name=login]"));
        public IWebElement demoNotice => driver.FindElement(By.ClassName("woocommerce-store-notice__dismiss-link"));

        //Service Methods
        public void Notice()
        {
            demoNotice.Click(); //dismisses the demo notice banner
        }

        public void Login()
        {
            userName.Click();
            userName.SendKeys(Environment.GetEnvironmentVariable("Username")); //using runsettings to get the username
        }

        public void Pass()
        {
            passWord.Click();
            passWord.SendKeys(Environment.GetEnvironmentVariable("Password")); //using runsettings to get the password
            logIn.Click();
        }

    }
}
