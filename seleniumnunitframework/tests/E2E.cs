using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;

namespace seleniumnunitframework.tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class E2E : BaseTest
    {
        [Test]
        public void GoToGoogle()
        {
            GetDriver().Url = "https://google.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[title='Search']")));
            Wait().Until(ExpectedConditions.UrlContains("google"));
        }

        [Test]
        public void GoToBing()
        {
            GetDriver().Url = "https://www.bing.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("textarea[id='sb_form_q']")));
            Wait().Until(ExpectedConditions.UrlContains("bing"));
        }

        [Test]
        public void GoToDDG()
        {
            GetDriver().Url = "https://duckduckgo.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input#searchbox_input")));
            Wait().Until(ExpectedConditions.UrlContains("duck"));
        }

        [Test]
        public void GoToYahoo()
        {
            GetDriver().Url = "https://www.yahoo.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input#ybar-sbq")));
            Wait().Until(ExpectedConditions.UrlContains("yahoo"));
        }

        [Test]
        public void GoToReddit()
        {
            GetDriver().Url = "https://www.reddit.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input#header-search-bar")));
            Wait().Until(ExpectedConditions.UrlContains("reddit"));
        }

        [Test]
        public void GoToAsk()
        {
            GetDriver().Url = "https://www.ask.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[aria-label='Search']")));
            Wait().Until(ExpectedConditions.UrlContains("ask"));
        }

        [Test]
        public void GoToYoutube()
        {
            GetDriver().Url = "https://www.youtube.com/";
            Console.WriteLine(GetDriver().Url);
            Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input#search")));
            Wait().Until(ExpectedConditions.UrlContains("youtube"));
        }
    }
}
