using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitFramework.Pages.SearchEnginePages
{
    internal class BingResultsPage : BasePage
    {
        [FindsBy(How.CssSelector, "h1[class='b_logo'][title='Back to Bing search']")]
        public IWebElement ResultsPageLogo;
        public By LocResultsPageLogo = By.CssSelector("h1[class='b_logo'][title='Back to Bing search']");

        public By LocResultsPageLogoLink = By.CssSelector("a.b_logoArea");

        public By LocSearchResultsDescriptions = By.CssSelector("li.b_algo div.b_caption p.b_algoSlug");

        public By LocSearchResultsLinkTitles = By.CssSelector("ol#b_results>li.b_algo h2");

        public BingResultsPage(IWebDriver driver) : base(driver) { }
    }
}
