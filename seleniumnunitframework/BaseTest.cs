using OpenQA.Selenium;
using SeleniumNUnitFramework.Utils;
using System.Configuration;

namespace SeleniumNUnitFramework
{
    [TestFixture]
    internal class BaseTest
    {
        private ThreadLocal<IWebDriver> ThreadDriver = new ThreadLocal<IWebDriver>();
        private readonly string BaseUrl = ConfigurationManager.AppSettings["baseurl"];


        [OneTimeSetUp]
        public void SetupThreads() {
            ThreadManager.SetupThreads();            
        }

        [SetUp]
        public void SetupAndLaunchBrowser() {
            ThreadManager.GetSemaphore().WaitOne();

            TestContext.Out.WriteLine($"Starting test: \"{TestContext.CurrentContext.Test.Name}\"\n");

            string browser = ConfigurationManager.AppSettings.Get("browser");

            ThreadDriver.Value = SetDriver(browser);

            ManageBrowserSettings(GetDriver());

            GetDriver().Url = BaseUrl;
        }

        [TearDown]
        public void Teardown() {
            TestContext.Out.WriteLine($"\nResult: {TestContext.CurrentContext.Result.Outcome}");
            ThreadDriver.Value.Quit();
            ThreadDriver.Value.Dispose();
            ThreadManager.GetSemaphore().Release();
        }

        [OneTimeTearDown]
        public void ThreadCleanup() {
            ThreadDriver.Dispose();
            ThreadManager.GetSemaphore().Dispose();
        }

        public IWebDriver SetDriver(string browserName) {
            IWebDriver driver;

            switch (browserName) {
                case "firefox" : 
                        return driver = BrowserManager.GetFirefoxDriver();
                case "edge" :
                        return driver = BrowserManager.GetEdgeDriver();
                default :
                        return driver = BrowserManager.GetChromeDriver();
            }
        }

        public IWebDriver GetDriver() {
            return ThreadDriver.Value;
        }

        public void ManageBrowserSettings(IWebDriver driver) {
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public static JsonReader GetJson() {
            return new JsonReader();
        }

        public static JsonReader GetJson(string jsonFilePath) {
            return new JsonReader(jsonFilePath);
        }
    }
}
