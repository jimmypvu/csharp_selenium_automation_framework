using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework
{
    internal class BasePage
    {
        public IWebDriver Driver;
        public WebDriverWait Wait;
        public Actions Acts;
        public IJavaScriptExecutor Jse;

        public BasePage(IWebDriver givenDriver)
        {
            Driver = givenDriver;

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Acts = new Actions(Driver);

            Jse = (IJavaScriptExecutor)Driver;

            PageFactory.InitElements(Driver, this);
        }

        public bool IsPageLoadComplete()
        {
            return Jse.ExecuteScript("return document.readyState").Equals("complete");
        }

        public void WaitForPageLoad()
        {
            bool pageLoadComplete = false;

            do
            {
                pageLoadComplete = IsPageLoadComplete();
            } while (!pageLoadComplete);
        }

        public void ScrollToEnd()
        {
            Jse.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
        }

        public void ScrollToTop()
        {
            Jse.ExecuteScript("window.scrollTo(0,0)");
        }
    }
}
