using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace seleniumnunitframework.pages
{
    internal class YoutubeHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#search")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#search");


        public YoutubeHomePage(IWebDriver driver) : base(driver) { }
    }
}
