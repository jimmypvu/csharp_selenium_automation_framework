using SeleniumExtras.WaitHelpers;
using SeleniumNUnitFramework.Pages;

namespace SeleniumNUnitFramework.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class E2E : BaseTest
    {
        [Test]
        public void GoToGoogle()
        {
            GoogleHomePage gp = new GoogleHomePage(GetDriver());
            gp.Driver.Url = "https://google.com/";
            Console.WriteLine(gp.Driver.Url);

            gp.WaitForPageLoad();
            gp.ScrollToEnd();
            gp.ScrollToTop();

            gp.Wait.Until(ExpectedConditions.UrlContains("google"));
            gp.Wait.Until(ExpectedConditions.TitleContains("Google"));

            gp.Wait.Until(ExpectedConditions.ElementIsVisible(gp.LocSearchBar));

            Assert.That(gp.SearchBar.Displayed);
        }

        [Test]
        public void GoToBing()
        {
            BingHomePage bp = new BingHomePage(GetDriver());
            bp.Driver.Url = "https://bing.com/";
            Console.WriteLine(bp.Driver.Url);

            bp.WaitForPageLoad();
            bp.ScrollToEnd();
            bp.ScrollToTop();

            bp.Wait.Until(ExpectedConditions.UrlContains("bing"));
            bp.Wait.Until(ExpectedConditions.TitleContains("Bing"));

            bp.Wait.Until(ExpectedConditions.ElementIsVisible(bp.LocSearchBar));

            Assert.That(bp.SearchBar.Displayed);
        }

        [Test]
        public void GoToDDG()
        {
            DdgHomePage dp = new DdgHomePage(GetDriver());
            dp.Driver.Url = "https://duckduckgo.com/";
            Console.WriteLine(dp.Driver.Url);

            //string getNetworkLogsScript = "let performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; let network = performance.getEntries() || {}; return network;";

            dp.WaitForPageLoad();
            dp.ScrollToEnd();
            dp.ScrollToTop();

            dp.Wait.Until(ExpectedConditions.UrlContains("duck"));
            dp.Wait.Until(ExpectedConditions.TitleContains("Duck"));

            dp.Wait.Until(ExpectedConditions.ElementIsVisible(dp.LocSearchBar));

            Assert.That(dp.SearchBar.Displayed);
        }

        [Test]
        public void GoToYahoo()
        {
            YahooHomePage yp = new YahooHomePage(GetDriver());
            yp.Driver.Url = "https://www.yahoo.com/";
            Console.WriteLine(yp.Driver.Url);

            yp.WaitForPageLoad();
            yp.ScrollToEnd();
            yp.ScrollToTop();

            yp.Wait.Until(ExpectedConditions.UrlContains("yahoo"));
            yp.Wait.Until(ExpectedConditions.TitleContains("Yahoo"));

            yp.Wait.Until(ExpectedConditions.ElementIsVisible(yp.LocSearchBar));

            Assert.That(yp.SearchBar.Displayed);
        }

        [Test]
        public void GoToReddit()
        {
            RedditHomePage rp = new RedditHomePage(GetDriver());
            rp.Driver.Url = "https://www.reddit.com/";
            Console.WriteLine(rp.Driver.Url);

            rp.WaitForPageLoad();
            rp.ScrollToEnd();
            rp.ScrollToTop();

            rp.Wait.Until(ExpectedConditions.UrlContains("reddit"));
            rp.Wait.Until(ExpectedConditions.TitleContains("Reddit"));

            rp.Wait.Until(ExpectedConditions.ElementIsVisible(rp.LocSearchBar));

            Assert.That(rp.SearchBar.Displayed);
        }

        [Test]
        public void GoToAsk()
        {
            AskHomePage ap = new AskHomePage(GetDriver());
            ap.Driver.Url = "https://www.ask.com/";
            Console.WriteLine(ap.Driver.Url);

            ap.WaitForPageLoad();
            ap.ScrollToEnd();
            ap.ScrollToTop();

            ap.Wait.Until(ExpectedConditions.UrlContains("ask"));
            ap.Wait.Until(ExpectedConditions.TitleContains("Ask"));

            ap.Wait.Until(ExpectedConditions.ElementIsVisible(ap.LocSearchBar));

            Assert.That(ap.SearchBar.Displayed);
        }

        [Test]
        public void GoToYoutube()
        {
            YoutubeHomePage yp = new YoutubeHomePage(GetDriver());
            yp.Driver.Url = "https://www.youtube.com/";
            Console.WriteLine(yp.Driver.Url);

            yp.WaitForPageLoad();
            yp.ScrollToEnd();
            yp.ScrollToTop();

            yp.Wait.Until(ExpectedConditions.UrlContains("youtube"));
            yp.Wait.Until(ExpectedConditions.TitleContains("YouTube"));

            yp.Wait.Until(ExpectedConditions.ElementIsVisible(yp.LocSearchBar));

            Assert.That(yp.SearchBar.Displayed);
        }
    }
}
