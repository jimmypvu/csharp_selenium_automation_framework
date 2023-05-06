using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class RedditHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#header-search-bar")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#header-search-bar");

        [FindsBy(How.CssSelector, "div[class='_3ioMyxiI-wWgZFqBDVBh6r _1fauFKjg428h9E1m4B2Vr-']")]
        public IWebElement PopularPostsHdr;
        public By LocPopularPostsHdr = By.CssSelector("div.wBtTDilkW_rtT2k5x3eie");

        public By LocPostTitles = By.XPath("//h3[@class='_eYtD2XCVieq6emjKBH3m']");


        public RedditHomePage(IWebDriver driver) : base(driver) { }
    }
}
