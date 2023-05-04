using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace seleniumnunitframework
{
    [TestFixture]
    internal class BaseTest
    {
        private ThreadLocal<IWebDriver> ThreadDriver = new ThreadLocal<IWebDriver>();

        private readonly string BaseUrl = ConfigurationManager.AppSettings["baseurl"];


        [SetUp]
        public void SetupAndLaunchBrowser()
        {
            string browser = ConfigurationManager.AppSettings.Get("browser");

            ThreadDriver.Value = SetDriver(browser);

            ManageBrowser(GetDriver());

            GetDriver().Url = BaseUrl;
        }

        [TearDown]
        public void Teardown() 
        {
            ThreadDriver.Value.Quit();
            ThreadDriver.Value.Dispose();
        }

        [OneTimeTearDown]
        public void ThreadCleanup()
        {
            ThreadDriver.Dispose();
        }

        public IWebDriver SetDriver(string browserName)
        {
            IWebDriver driver;

            switch (browserName)
            {
                case "firefox":
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                        FirefoxOptions fo = new FirefoxOptions();
                        fo.AddArgument("-width=1920");
                        fo.AddArgument("-height=1080");
                        fo.AddArgument("--headless");
                        return driver = new FirefoxDriver(fo);
                case "edge":
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                        EdgeOptions eo = new EdgeOptions();
                        eo.AddArgument("--window-size=1920,1080");
                        eo.AddArgument("--headless");
                        return driver = new EdgeDriver(eo);
                default:
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        ChromeOptions co = new ChromeOptions();
                        co.AddArgument("--window-size=1920,1080");
                        //co.AddArgument("--headless");
                        co.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
                        return driver = new ChromeDriver(co);
            }
        }

        public IWebDriver GetDriver()
        {
            return ThreadDriver.Value;
        }

        public void ManageBrowser(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
