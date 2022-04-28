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

        //Service Methods
        public LoginPass_POM Login(String username)
        {
            userName.Click();
            userName.SendKeys(username);
            return this;
        }

        public LoginPass_POM Pass(String password)
        {
            passWord.Click();
            passWord.SendKeys(password);
            logIn.Click();
            return this;
        }

    }
}
