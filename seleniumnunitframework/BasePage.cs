using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;

namespace SeleniumNUnitFramework
{
    internal class BasePage
    {
        public IWebDriver Driver;
        public WebDriverWait Wait;
        public Actions Acts;
        public IJavaScriptExecutor Jse;

        public BasePage(IWebDriver givenDriver) {
            Driver = givenDriver;

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Acts = new Actions(Driver);

            Jse = (IJavaScriptExecutor)Driver;

            PageFactory.InitElements(Driver, this);
        }

        /************************
         * REUSABLE PAGE METHODS
         ************************/

        public bool IsPageLoadComplete() {
            return Jse.ExecuteScript("return document.readyState").Equals("complete");
        }

        public void WaitForPageLoad() {
            bool pageLoadComplete = false;

            do {
                TestContext.Progress.WriteLine("Waiting for page load.");
                pageLoadComplete = IsPageLoadComplete();
            } while (!pageLoadComplete);

            TestContext.Progress.WriteLine("Page load complete.");
        }

        public void ScrollToEle(IWebElement element) {
            try {
                TestContext.Progress.WriteLine($"Scrolling element into view: {element.GetAttribute("outerHTML")}");
                Jse.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Progress.WriteLine("Scrolled to element.");
            }catch (Exception e) {

                TestContext.Progress.WriteLine($"Could not find element: {element.GetAttribute("outerHTML")}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public void ScrollToEle(By locator) {
            try {
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(locator));
                TestContext.Progress.WriteLine($"Scrolling to element located by: {locator}");
                Jse.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Progress.WriteLine($"Scrolled to element: {element.GetAttribute("outerHTML")}");
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find element located by: {locator}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public void ScrollToEleWithText(String text) {
            try {
                TestContext.Progress.WriteLine($"Scrolling to element containing text: '{text}'");
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[contains(text(),\"{text}\")]")));
                Jse.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Progress.WriteLine($"Scrolled to element with text: {element.GetAttribute("outerHTML")}");
            } catch (Exception e) {

                TestContext.Progress.WriteLine($"Could not find element containing text: '{text}'" +
                    $"\n{e.Message}" +
                    $"\n{e.StackTrace}");
            }
        }

        public void ScrollBy(int xPixels, int yPixels) {
            Jse.ExecuteScript($"window.scrollBy({xPixels}, {yPixels})");
            //TestContext.Progress.WriteLine($"Scrolled down by {yPixels} pixels, right by {xPixels} pixels");
        }

        public void ScrollToEnd() {
            Jse.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
            TestContext.Progress.WriteLine("Scrolled to end of page.");
        }

        public void ScrollToTop() {
            Jse.ExecuteScript("window.scrollTo(0,0)");
            TestContext.Progress.WriteLine("Scrolled to top of page.");
        }

        public void WaitForVis(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for visibility of element located by: {locator}");
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                TestContext.Progress.WriteLine($"Element is visible.");
            }catch (Exception e) {

                TestContext.Progress.WriteLine($"Could not find elemented located by: {locator}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public IWebElement WaitForVisAndGet(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for visibility to get element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                TestContext.Progress.WriteLine($"Found element: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find element located by: {locator}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
            return null;
        }

        public void WaitForVisOfAll(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for visibility of all elements located by: {locator}");
                IList<IWebElement> eles = Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
                TestContext.Progress.WriteLine($"Elements are visible ({eles.Count} elements).");
            } catch (NoSuchElementException e) {

                TestContext.Progress.WriteLine($"Timed out waiting for visibility.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public IList<IWebElement> WaitForVisAndGetAll(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for visibility to get all elements located by: {locator}");
                IList<IWebElement> elements = Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
                TestContext.Progress.WriteLine($"Got all visible elements ({elements.Count} elements)");
                return elements;
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not get all elements, timed out waiting for visibility.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }

            return null;
        }

        public void WaitForPresence(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for presence of element located by: {locator}");
                Wait.Until(ExpectedConditions.ElementExists(locator));
                TestContext.Progress.WriteLine("Element is present on DOM.");
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find elemented located by: {locator}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public IWebElement WaitForPresenceAndGet(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for presence to get element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(locator));
                TestContext.Progress.WriteLine($"Got element present on DOM: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find element located by: {locator}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
            return null;
        }

        public void WaitForPresenceOfAll(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for presence of all elements located by: {locator}");
                Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                TestContext.Progress.WriteLine("Elements are present on DOM.");
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find elements.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public IList<IWebElement> WaitForPresenceAndGetAll(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for presence of and getting all elements located by: {locator}");
                IList<IWebElement> elements = Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                TestContext.Progress.WriteLine($"Got elements present on DOM ({elements.Count} elements)");
                return elements;
            } catch(Exception e) {

                TestContext.Progress.WriteLine($"Could not find elements.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }

            return null;
        }

        public void WaitForClick(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for element at {locator} to become clickable.");
                Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                TestContext.Progress.WriteLine("Element became clickable.");
            } catch (Exception e) {

                TestContext.Progress.WriteLine("Element did not become clickable.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public IWebElement WaitForClickAndGet(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting for element at {locator} to become clickable and getting element.");
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                TestContext.Progress.WriteLine($"Got element once clickable: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch(Exception e) {

                TestContext.Progress.WriteLine("Element did not become clickable.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }

            return null;
        }

        public void ClickWhenReady(By locator) {
            try {
                TestContext.Progress.WriteLine($"Waiting to click element located by: {locator}");
                Wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
                TestContext.Progress.WriteLine($"Clicked on element located by: {locator}");
            } catch(Exception e) {

                TestContext.Progress.WriteLine("Element did not become clickable.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public void ClickWhenReady(IWebElement element) {
            try {
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">")+1);
                TestContext.Progress.WriteLine($"Waiting to click element: {elementHTML}");
                Wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                try {
                    Acts.MoveToElement(element).Click().Perform();
                }catch(IgnoreException) { }
                TestContext.Progress.WriteLine("Clicked on element.");
            } catch(Exception e) {

                TestContext.Progress.WriteLine("Element did not become clickable.\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}");
            }
        }

        public bool DoesElementHaveAttribute(IWebElement element, string attribute) {
            string[] attributes = GetElementAttributes(element);

            return attributes.Contains(attribute);
        }

        public string[] GetElementAttributes(IWebElement element) {
            string html = element.GetAttribute("outerHTML").Substring(1, element.GetAttribute("outerHTML").IndexOf(">") - 1);

            html = Regex.Replace(html, "=\"[^\"]*\"", string.Empty);

            string[] attributes = html.Split(' ').Skip(1).ToArray();

            TestContext.Progress.WriteLine($"Element attributes for {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)} : \"{String.Join(", ", attributes)}\"");

            //foreach(var attribute in attributes) {
            //    TestContext.Progress.WriteLine(attribute);
            //}

            return attributes;
        }

        public Dictionary<string, string> GetAttributeValueDict(IWebElement element) {
            string tagContents = element.GetAttribute("outerHTML").Substring(1, element.GetAttribute("outerHTML").IndexOf(">") - 1);

            int firstIndexOfSpace = tagContents.IndexOf(" ");
            string discardedTag = tagContents.Substring(0, firstIndexOfSpace + 1);

            string attributesAndValues = tagContents.Substring(firstIndexOfSpace + 1, tagContents.Length - discardedTag.Length);

            Dictionary<string, string> attributeValuePairs = new Dictionary<string, string>();

            foreach(Match match in Regex.Matches(attributesAndValues, @"(\S+)=(""[^""]*"")|(\S+)")) {
                if(match.Groups[1].Success) {
                    attributeValuePairs.Add(match.Groups[1].Value, match.Groups[2].Value.Trim('"'));
                } else {
                    attributeValuePairs.Add(match.Groups[3].Value, null);
                }
            }

            TestContext.Progress.WriteLine($"Attribute-Value pairs for {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">")+1)} :");

            foreach(KeyValuePair<string, string> attribute in attributeValuePairs) {
                TestContext.Progress.WriteLine($"{attribute.Key} : \"{attribute.Value}\"");
            }

            return attributeValuePairs;
        }
    }
}
