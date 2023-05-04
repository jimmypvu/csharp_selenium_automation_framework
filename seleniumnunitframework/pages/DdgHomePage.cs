using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages
{
    internal class DdgHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "input#searchbox_input")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("input#searchbox_input");


        public DdgHomePage(IWebDriver driver) : base(driver) { }
    }
}
