using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace seleniumnunitframework.pages
{
    internal class BasePage
    {
        private IWebDriver driver;

        public BasePage(IWebDriver givenDriver) 
        {
            this.driver = givenDriver;
            PageFactory.InitElements(driver, this);
        }
    }
}
