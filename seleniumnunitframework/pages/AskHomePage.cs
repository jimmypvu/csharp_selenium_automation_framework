using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages
{
    internal class AskHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input[aria-label='Search']")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input[aria-label='Search']");


        public AskHomePage(IWebDriver driver) : base(driver) { }
    }
}
