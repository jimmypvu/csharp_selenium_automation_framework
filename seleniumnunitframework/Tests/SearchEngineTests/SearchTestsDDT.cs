using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumNUnitFramework.Pages.SearchEnginePages;

namespace SeleniumNUnitFramework.Tests.SearchEngineTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class SearchTestsDDT : BaseTest
    {
        [Test]
        [TestCase("jordans")]
        [TestCase("yeezys")]
        [TestCase("vans")]
        [TestCase("chuck taylor low tops")]
        [TestCase("black chuck taylor high tops mens new size 8.5")]
        public void SearchGoogle(string searchTerm) {
            GoogleHomePage ghp = new GoogleHomePage(GetDriver());
            ghp.Driver.Url = "https://google.com/";

            GoogleResultsPage grp = ghp.SearchForTerm($"{searchTerm}");

            Assert.That(grp.WaitForVis(grp.LocSearchResultStats).Displayed);

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
    }
}
