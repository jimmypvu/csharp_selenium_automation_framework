using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class BingHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "textarea[id='sb_form_q']")]
        public IWebElement SearchBar;

        public By LocSearchBar = By.CssSelector("textarea[id='sb_form_q']");

        public By LocSearchBtn = By.CssSelector("label#search_icon svg");


        public BingHomePage(IWebDriver driver) : base(driver) { }

        public BingResultsPage SearchFor(string searchTerm) {
            IWebElement searchBar = WaitForVis(LocSearchBar);
            ClickWhenReady(searchBar);
            EnterText(searchBar, searchTerm);
            ClickWhenReady(LocSearchBtn);

            return new BingResultsPage(Driver);
        }
    }
}
