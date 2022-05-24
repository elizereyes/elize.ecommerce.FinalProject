using OpenQA.Selenium;


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
            string username = Environment.GetEnvironmentVariable("Username");
           
            userName.Click();
            userName.SendKeys(username); //using runsettings to get the username
          
            //writes in the console the login that's used but splits it to maintain privacy 
            string[] emailSplit = username.Split('@'); //the split takes before or after the @
            int Characters = emailSplit[0].Count() / 2; //this splits the first half of the email (before the @) so that we can * the other half
            //Writes in the console the login email that has been provided, for privacy reasons I decided to split the email to only show the first half and then * the rest
            Console.WriteLine(String.Format("Login: entered: {0}@{1}", emailSplit[0].Substring(0, Characters) + new String('*', Characters), emailSplit[1]));
        }

        public void Pass()
        {
            passWord.Click(); //clicks on the password field
            passWord.SendKeys(Environment.GetEnvironmentVariable("Password")); //using runsettings to get the password
                        
            logIn.Click(); //clicks on the login button
        }

    }
}
