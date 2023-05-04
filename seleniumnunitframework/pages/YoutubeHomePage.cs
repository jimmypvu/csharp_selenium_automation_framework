using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages
{
    internal class YoutubeHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#search")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#search");


        public YoutubeHomePage(IWebDriver driver) : base(driver) { }
    }
}
