using SeleniumExtras.WaitHelpers;
using SeleniumNUnitFramework.Pages.SearchEnginePages;
using OpenQA.Selenium;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace SeleniumNUnitFramework.Tests.SearchEngineTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    internal class E2E : BaseTest
    {
        [Test, Retry(2)]
        public void GoToGoogle()
        {
            GoogleHomePage gp = new GoogleHomePage(GetDriver());
            gp.Driver.Url = "https://google.com/";
            TestContext.Out.WriteLine(gp.Driver.Url);

            gp.FinishPageLoad();
            gp.ScrollToEnd();
            gp.ScrollToTop();

            gp.Wait.Until(ExpectedConditions.UrlContains("google"));
            gp.Wait.Until(ExpectedConditions.TitleContains("Google"));

            gp.Wait.Until(ExpectedConditions.ElementIsVisible(gp.LocSearchBar));

            Assert.That(gp.SearchBar.Displayed);
        }

        [Test, Retry(2)]
        public void GoToBing()
        {
            BingHomePage bp = new BingHomePage(GetDriver());
            bp.Driver.Url = "https://bing.com/";
            TestContext.Out.WriteLine(bp.Driver.Url);

            bp.FinishPageLoad();
            bp.ScrollToEnd();
            bp.ScrollToTop();

            bp.Wait.Until(ExpectedConditions.UrlContains("bing"));
            bp.Wait.Until(ExpectedConditions.TitleContains("Bing"));

            bp.Wait.Until(ExpectedConditions.ElementIsVisible(bp.LocSearchBar));

            Assert.That(bp.SearchBar.Displayed);
        }

        [Test, Retry(2)]
        public void GoToDDG()
        {
            if(ConfigurationManager.AppSettings.Get("headless").Equals("true")) {

                TestContext.Out.WriteLine("Running in headless mode, test skipped (blocked user agent or headers).");
                Assert.That(true);
            } else {
                DdgHomePage dp = new DdgHomePage(GetDriver());
                dp.Driver.Url = "https://duckduckgo.com/";
                TestContext.Out.WriteLine(dp.Driver.Url);

                //string getNetworkLogsScript = "let performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; let network = performance.getEntries() || {}; return network;";

                dp.FinishPageLoad();
                dp.ScrollToEnd();
                dp.ScrollToTop();

                dp.Wait.Until(ExpectedConditions.UrlContains("duck"));
                dp.Wait.Until(ExpectedConditions.TitleContains("Duck"));

                dp.Wait.Until(ExpectedConditions.ElementIsVisible(dp.LocSearchBar));

                Assert.That(dp.SearchBar.Displayed);
            }
        }

        [Test, Retry(2)]
        public void GoToYahoo()
        {
            YahooHomePage yp = new YahooHomePage(GetDriver());
            yp.Driver.Url = "https://www.yahoo.com/";
            TestContext.Out.WriteLine(yp.Driver.Url);

            yp.FinishPageLoad();
            yp.ScrollToEnd();
            yp.ScrollToTop();

            yp.Wait.Until(ExpectedConditions.UrlContains("yahoo"));
            yp.Wait.Until(ExpectedConditions.TitleContains("Yahoo"));

            yp.Wait.Until(ExpectedConditions.ElementIsVisible(yp.LocSearchBar));

            Assert.That(yp.SearchBar.Displayed);
        }

        [Test, Retry(2)]
        public void GoToReddit() {
            Assert.DoesNotThrow(()=> {
                RedditHomePage rp = new RedditHomePage(GetDriver());
                rp.Driver.Url = "https://www.reddit.com/";
                TestContext.Out.WriteLine(rp.Driver.Url);

                rp.FinishPageLoad();

                rp.Wait.Until(ExpectedConditions.UrlContains("reddit"));
                rp.Wait.Until(ExpectedConditions.TitleContains("Reddit"));
                rp.WaitForVis(rp.LocSearchBar);

                Assert.That(rp.SearchBar.Displayed);

                rp.ScrollToEnd();
                rp.ScrollToEle(rp.PopularPostsHdr);

                List<IWebElement> titles = rp.WaitForPresenceOfAll(rp.LocPostTitles).ToList();

                int count = 1;
                foreach(var item in titles) {
                    TestContext.Out.WriteLine($"Item {count} innerHTML: " + item.GetAttribute("innerHTML"));
                    rp.ScrollToEle(item);
                    TestContext.Out.WriteLine($"Item {count} text: " + item.Text);
                    count++;
                }

                TestContext.Out.WriteLine("Item 4 innerHTML: " + titles[3].GetAttribute("innerHTML"));

                rp.ScrollToTop();

                string fourthPostTitle = titles[3].GetAttribute("innerHTML");
                rp.ScrollToEleWithText(fourthPostTitle);
                rp.ScrollToEle(titles[3]);
                TestContext.Out.WriteLine("Item 4 text: " + titles[3].Text);
                rp.ScrollBy(0, -150);

                rp.DoesElementHaveAttribute(titles[3].FindElement(By.XPath(".//ancestor::a[@data-click-id='body']")), "data-click-id");

                rp.GetAttributeValueDict(titles[3].FindElement(By.XPath(".//ancestor::a[@data-click-id='body']")));

                rp.ClickWhenReady(titles[3].FindElement(By.XPath(".//ancestor::a[@data-click-id='body']")));

                //titles[3].FindElement(By.XPath(".//ancestor::a[@data-click-id='body']")).Click();
                rp.DoesElementHaveAttribute(rp.WaitForVis(By.CssSelector("div[slot='title']")), "slot");

                Assert.That(rp.WaitForVis(By.CssSelector("div[slot='title']")).Displayed);
                Assert.That(rp.Driver.FindElement(By.CssSelector("div[slot='title']")).Text, Is.EqualTo(fourthPostTitle));
            });    
        }

        [Test, Retry(2)]
        public void GoToAsk()
        {
            AskHomePage ap = new AskHomePage(GetDriver());
            ap.Driver.Url = "https://www.ask.com/";
            TestContext.Out.WriteLine(ap.Driver.Url);

            ap.FinishPageLoad();
            ap.ScrollToEnd();
            ap.ScrollToTop();

            ap.Wait.Until(ExpectedConditions.UrlContains("ask"));
            ap.Wait.Until(ExpectedConditions.TitleContains("Ask"));

            ap.Wait.Until(ExpectedConditions.ElementIsVisible(ap.LocSearchBar));

            Assert.That(ap.SearchBar.Displayed);
        }

        [Test, Retry(2)]
        public void GoToYoutube()
        {
            YoutubeHomePage yp = new YoutubeHomePage(GetDriver());
            yp.Driver.Url = "https://www.youtube.com/";
            TestContext.Out.WriteLine(yp.Driver.Url);

            yp.FinishPageLoad();
            yp.ScrollToEnd();
            yp.ScrollToTop();

            yp.Wait.Until(ExpectedConditions.UrlContains("youtube"));
            yp.Wait.Until(ExpectedConditions.TitleContains("YouTube"));

            yp.Wait.Until(ExpectedConditions.ElementIsVisible(yp.LocSearchBar));

            Assert.That(yp.SearchBar.Displayed);
        }

        [Test, Retry(2)]
        [TestCase("https://www.google.com/")]
        [TestCase("https://www.bing.com/")]
        public void GoToPage(string url) {
            GetDriver().Navigate().GoToUrl(url);

            TestContext.Out.WriteLine(GetDriver().Url);

            Assert.That(GetDriver().Url.Contains(url));
        }

        [Test, Description("Add items to cart")]
        public void AddItemsToCart() {
            GetDriver().Url = "https://rahulshettyacademy.com/loginpagePractise/";

            WebDriverWait wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            GetDriver().FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            GetDriver().FindElement(By.Id("password")).SendKeys("learning" + Keys.Enter);

            string[] productsToAdd = { "iphone X", "Samsung Note 8", "Blackberry" };

            List<IWebElement> productCards = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='card h-100']"))).ToList();

            foreach(var item in productCards) {
                string itemTitle = item.FindElement(By.XPath(".//h4[@class='card-title']/a")).Text;
                //string itemName = item.FindElement(By.CssSelector("h4.card-title")).Text;

                if(productsToAdd.Contains(itemTitle)) {
                    item.FindElement(By.CssSelector("div.card-footer button")).Click();
                }
            }

            IWebElement checkoutBtn = GetDriver().FindElement(By.CssSelector("[class='nav-link btn btn-primary']"));

            string itemCountBadgeText = Regex.Replace(checkoutBtn.Text, @"[^0-9]", "");
            int itemCount = int.Parse(itemCountBadgeText);

            Assert.That(itemCount, Is.EqualTo(productsToAdd.Length));
            checkoutBtn.Click();

            IList<IWebElement> cartItems = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("h4.media-heading")));

            foreach(var item in cartItems) {
                Assert.That(productsToAdd.Contains(item.Text));
            }
        }
    }
}
