using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumNUnitFramework.Pages.SearchEnginePages;
using SeleniumNUnitFramework.Utils;

namespace SeleniumNUnitFramework.Tests.SearchEngineTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    internal class SearchTestsDDT : BaseTest
    {
        [Test, TestCaseSource("GetTestData_SearchTerms")]
        public void SearchGoogle(string searchTerm) {
            GoogleHomePage ghp = new GoogleHomePage(GetDriver());
            ghp.Driver.Url = "https://google.com/";

            GoogleResultsPage grp = ghp.SearchFor($"{searchTerm}");

            Assert.That(grp.WaitForVis(grp.LocSearchResultsStats).Displayed);

            IList<IWebElement> results = grp.WaitForVisOfAll(grp.LocSearchResultsDescriptions);

            int num = 1;
            foreach(IWebElement result in results) {
                if(!searchTerm.Contains(" ")) {
                    TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                    Assert.That(result.Text.ToLower().Contains(searchTerm) || result.Text.ToLower().Contains(searchTerm.Substring(0, searchTerm.Length - 1)));
                } else {
                    TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                    string[] searchTerms = searchTerm.Split(" ");

                    bool resultContainsAnySearchTerms = searchTerms.Any(term => result.Text.ToLower().Contains(term));

                    List<string> matchingTerms = searchTerms.Where(term => result.Text.ToLower().Contains(term) || result.Text.ToLower().Contains(term.Substring(0, term.Length - 1))).ToList();

                    TestContext.Out.WriteLine($"Result contains any search terms? {resultContainsAnySearchTerms}\n" +
                        $"Matching terms: {String.Join(", ", matchingTerms)}");

                    Assert.That(resultContainsAnySearchTerms, Is.True);
                }
                num++;
            }
        }

        [Test]
        public void SingleGoogleSearch() {
            string searchTerm = "california";
            GoogleHomePage ghp = new GoogleHomePage(GetDriver());
            ghp.Driver.Url = "https://google.com/";

            GoogleResultsPage grp = ghp.SearchFor($"{searchTerm}");

            Assert.That(grp.WaitForVis(grp.LocSearchResultsStats).Displayed);

            IList<IWebElement> results = grp.WaitForVisOfAll(grp.LocSearchResultsDescriptions);

            int num = 1;
            foreach(IWebElement result in results) {
                if(!searchTerm.Contains(" ")) {
                    TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                    Assert.That(result.Text.ToLower().Contains(searchTerm) || result.Text.ToLower().Contains(searchTerm.Substring(0, searchTerm.Length - 1)));
                } else {
                    TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                    string[] searchTerms = searchTerm.Split(" ");

                    bool resultContainsAnySearchTerms = searchTerms.Any(term => result.Text.ToLower().Contains(term));

                    List<string> matchingTerms = searchTerms.Where(term => result.Text.ToLower().Contains(term) || result.Text.ToLower().Contains(term.Substring(0, term.Length - 1))).ToList();

                    TestContext.Out.WriteLine($"Result contains any search terms? {resultContainsAnySearchTerms}\n" +
                        $"Matching terms: {String.Join(", ", matchingTerms)}");

                    Assert.That(resultContainsAnySearchTerms, Is.True);
                }
                num++;
            }
        }

        [Test, TestCaseSource("GetTestData_SearchTerms"), Retry(3)]
        public void SearchBing(string searchTerm) {
            Assert.DoesNotThrow(() => {
                BingHomePage bhp = new BingHomePage(GetDriver());
                bhp.Driver.Url = "https://bing.com/";

                BingResultsPage brp = bhp.SearchFor($"{searchTerm}");

                brp.WaitForPresence(brp.LocResultsPageLogoLink);

                IList<IWebElement> results = brp.WaitForPresenceOfAll(brp.LocSearchResultsDescriptions);

                int num = 1;
                foreach(IWebElement result in results) {
                    if(!searchTerm.Contains(" ")) {
                        TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                        Assert.That(result.Text.ToLower().Contains(searchTerm) || result.Text.ToLower().Contains(searchTerm.Substring(0, searchTerm.Length - 1)));
                    } else {
                        TestContext.Out.WriteLine($"Text for search result {num}: {result.Text}");

                        string[] searchTerms = searchTerm.Split(" ");

                        bool resultContainsAnySearchTerms = searchTerms.Any(term => result.Text.ToLower().Contains(term) || result.Text.ToLower().Contains(term.Substring(0, term.Length - 1)) || result.Text.ToLower().Contains("shoe") || result.Text.ToLower().Contains("sneakers") || result.Text.ToLower().Contains("shop"));

                        List<string> matchingTerms = searchTerms.Where(term => result.Text.ToLower().Contains(term) || result.Text.ToLower().Contains(term.Substring(0, term.Length - 1))).ToList();

                        TestContext.Out.WriteLine($"Result text contains any search terms? {resultContainsAnySearchTerms}\n" +
                            $"Matching terms: {String.Join(", ", matchingTerms)}");

                        Assert.That(resultContainsAnySearchTerms, Is.True);
                    }
                    num++;
                }
            });
        }

        public static IEnumerable<TestCaseData> GetTestData_SearchTerms() {
            yield return new TestCaseData(GetJson().GetData("searchTerm1"));
            yield return new TestCaseData(GetJson().GetData("searchTerm2"));
            yield return new TestCaseData(GetJson("SearchTermsTestData.json").GetData("searchTerm3"));
            yield return new TestCaseData("chuck taylor low tops");
            yield return new TestCaseData("black chuck taylor high tops mens new size 8.5");
        }

        //public static IEnumerable<TestCaseData> GetTestData_SearchTerms() {
        //    yield return new TestCaseData("gucci");
        //    yield return new TestCaseData("yeezys");
        //    yield return new TestCaseData("vans");
        //    yield return new TestCaseData("chuck taylor low tops");
        //    yield return new TestCaseData("black chuck taylor high tops mens new size 8.5");
        //}
    }
}
