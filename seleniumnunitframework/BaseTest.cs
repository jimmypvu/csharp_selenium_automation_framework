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

        private ThreadLocal<WebDriverWait> ThreadWait = new ThreadLocal<WebDriverWait>();

        protected Actions act;

        protected IJavaScriptExecutor jse;

        private readonly string BaseUrl = ConfigurationManager.AppSettings["baseurl"];


        [SetUp]
        public void SetupAndLaunchBrowser()
        {
            string browser = ConfigurationManager.AppSettings.Get("browser");

            ThreadDriver.Value = SetDriver(browser);

            ManageBrowser(GetDriver());

            ThreadWait.Value = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10));
            ThreadWait.Value.PollingInterval = TimeSpan.FromMilliseconds(250);

            GetDriver().Url = BaseUrl;

            //act = new Actions(GetDriver());

            //jse = (IJavaScriptExecutor)GetDriver();
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
            ThreadWait.Dispose();
        }

        public IWebDriver SetDriver(string browserName)
        {
            IWebDriver driver;

            switch (browserName)
            {
                case "firefox":
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                        FirefoxOptions fo = new FirefoxOptions();
                        fo.AddArguments("-width=1920");
                        fo.AddArguments("-height=1080");
                        //fo.AddArgument("--headless");
                        return driver = new FirefoxDriver(fo);
                case "edge":
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                        EdgeOptions eo = new EdgeOptions();
                        eo.AddArguments("--window-size=1920,1080");
                        //eo.AddArgument("--headless");
                        return driver = new EdgeDriver(eo);
                default:
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        ChromeOptions co = new ChromeOptions();
                        co.AddArguments("--window-size=1920,1080");
                        co.AddArgument("--headless");
                        return driver = new ChromeDriver(co);
            }
        }

        public IWebDriver GetDriver()
        {
            return ThreadDriver.Value;
        }

        public WebDriverWait Wait()
        {
            return ThreadWait.Value;
        }

        public void ManageBrowser(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
