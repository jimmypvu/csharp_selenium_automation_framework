using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages
{
    internal class RedditHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#header-search-bar")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#header-search-bar");


        public RedditHomePage(IWebDriver driver) : base(driver) { }
    }
}
