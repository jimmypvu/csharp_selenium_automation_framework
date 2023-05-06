using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class YahooHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#ybar-sbq")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#ybar-sbq");


        public YahooHomePage(IWebDriver driver) : base(driver) { }
    }
}
