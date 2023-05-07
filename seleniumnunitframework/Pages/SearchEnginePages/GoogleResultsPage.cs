using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class GoogleResultsPage : BasePage
    {
        [FindsBy(How.CssSelector, "div#result-stats")]
        public IWebElement SearchResultsStats;
        public By LocSearchResultsStats = By.CssSelector("div#result-stats");

        [FindsBy(How.CssSelector, "div[class='VwiC3b yXK7lf MUxGbd yDYNvb lyLwlc lEBKkf']")]
        public IList<IWebElement> SearchResultsDescriptions;
        public By LocSearchResultsDescriptions = By.CssSelector("div[class='VwiC3b yXK7lf MUxGbd yDYNvb lyLwlc lEBKkf']");

        public GoogleResultsPage(IWebDriver driver) : base(driver) { }
    }
}
