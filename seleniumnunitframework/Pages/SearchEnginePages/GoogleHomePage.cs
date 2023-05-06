using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class GoogleHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "textarea[title='Search']")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("textarea[title='Search']");


        public GoogleHomePage(IWebDriver driver) : base(driver) { }

        public GoogleResultsPage SearchForTerm(string searchTerm) {
            SearchBar.SendKeys(searchTerm + Keys.Enter);

            return new GoogleResultsPage(Driver);
        }
    }
}
