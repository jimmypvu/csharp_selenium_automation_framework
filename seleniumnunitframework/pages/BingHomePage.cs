using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnitFramework.Pages
{
    internal class BingHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "textarea[id='sb_form_q']")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("textarea[id='sb_form_q']");


        public BingHomePage(IWebDriver driver) : base(driver) { }
    }
}
