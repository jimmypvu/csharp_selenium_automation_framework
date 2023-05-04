using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace seleniumnunitframework.pages
{
    internal class GoogleHomePage : BasePage
    {
        [FindsBy(How.CssSelector, "textarea[title='Search']")]
        public IWebElement SearchBar;
        public By LocSearchBar = By.CssSelector("textarea[title='Search']");


        public GoogleHomePage(IWebDriver driver) : base(driver) { }


    }
}
