using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class BingHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "textarea[id='sb_form_q']")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("textarea[id='sb_form_q']");


        public BingHomePage(IWebDriver driver) : base(driver) { }

        public BingResultsPage SearchFor(string searchTerm) {
            ClickWhenReady(LocSearchBar);
            SearchBar.SendKeys(searchTerm);
            SearchBar.SendKeys(Keys.Enter);

            return new BingResultsPage(Driver);
        }
    }
}
