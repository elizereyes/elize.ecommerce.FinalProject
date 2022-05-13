using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ecommerce.finalproject.POMs;


namespace ecommerce.finalproject
{
    [Binding]
    public class Hooks
    {
        public IWebDriver driver;
        Helper helper;

        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            helper = new Helper(driver);
        }

        [Before]
        public void SetUp()
        {
            driver = new ChromeDriver();
            _scenarioContext["webdriver"] = driver; //gets the driver

            //Navigates to the log in page
            string BaseUrl = Environment.GetEnvironmentVariable("BaseURL"); //uses the runsettings baseurl
            driver.Url = BaseUrl;
        }

        [After]
        public void TearDown()
        {
            //removes any coupons and items from the cart so it's ready for the next test, in order to not restack
            Navigate_POM Nav = new Navigate_POM(driver);
            Nav.Navigate("Cart"); //clicks on cart
            helper.WaitForElement(By.CssSelector(".entry-title")); //waits for Cart

            Cart_POM cart = new Cart_POM(driver);
            cart.ClearCart();

            Nav.Navigate("My account");
            helper.WaitForElement(By.CssSelector(".woocommerce-MyAccount-content > p:nth-of-type(1) > a")); //waits for log out button to be on the page

            Nav.Navigate("Log out");  //logs out

            driver.Quit(); //Quits the WebDriver
        }

    }

} 
