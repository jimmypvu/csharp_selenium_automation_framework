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
        [TestCase("yeezys")]
        [TestCase("jordans")]
        [TestCase("chuck taylor low tops")]
        public void SearchGoogle(string searchTerm) {
            GoogleHomePage ghp = new GoogleHomePage(GetDriver());
            ghp.Driver.Url = "https://google.com/";

            GoogleResultsPage grp = ghp.SearchForTerm($"{searchTerm}");

            Assert.That(grp.WaitForVis(grp.LocSearchResultStats).Displayed);

            IList<IWebElement> results = grp.WaitForVisOfAll(grp.LocSearchResultsDescriptions);

            int num = 1;
            foreach(IWebElement result in results) {
                if(!searchTerm.Contains(" ")) {
                    TestContext.Out.WriteLine($"Search result {num} text: {result.Text}");

                    Assert.That(result.Text.ToLower().Contains(searchTerm) || result.Text.ToLower().Contains(searchTerm.Substring(0, searchTerm.Length - 1)));
                } else {
                    string resultBody = result.Text.ToLower();
                    TestContext.Out.WriteLine($"Search result {num} text: {resultBody}");
                    string[] searchTerms = searchTerm.Split(" ");
                    Assert.That(resultBody.Contains(searchTerms[0]) || resultBody.Contains(searchTerms[1]) || resultBody.Contains(searchTerms[2]) || resultBody.Contains(searchTerms[3]));
                }
                num++;
                TestContext.Out.WriteLine("Assertion passed, next result");
            }
        }
    }
}
