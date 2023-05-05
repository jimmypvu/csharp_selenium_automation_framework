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

            TestContext.Progress.WriteLine($"Starting test: \"{TestContext.CurrentContext.Test.Name}\"");

            string browser = ConfigurationManager.AppSettings.Get("browser");

            ThreadDriver.Value = SetDriver(browser);

            ManageBrowserSettings(GetDriver());

            GetDriver().Url = BaseUrl;
        }

        [TearDown]
        public void Teardown() {
            TestContext.Progress.WriteLine($"\nResult: {TestContext.CurrentContext.Result.Outcome}");
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

        public void Pause() {
            Thread.Sleep(2000);
        }

        public void Pause(int millis) {
            Thread.Sleep(millis);
        }
    }
}
