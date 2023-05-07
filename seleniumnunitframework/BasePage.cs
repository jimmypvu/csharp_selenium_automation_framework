using AngleSharp.Dom;
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
        public Actions Act;
        public IJavaScriptExecutor Js;

        public BasePage(IWebDriver givenDriver) {
            Driver = givenDriver;

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Act = new Actions(Driver);

            Js = (IJavaScriptExecutor)Driver;

            PageFactory.InitElements(Driver, this);
        }

        /************************
         * REUSABLE PAGE METHODS
         ************************/

        public bool IsPageLoadComplete() {
            return Js.ExecuteScript("return document.readyState").Equals("complete");
        }

        public void FinishPageLoad() {
            bool pageLoadComplete = false;
            try {
                do {
                    TestContext.Out.WriteLine("Waiting for page load.");
                    pageLoadComplete = IsPageLoadComplete();
                } while(!pageLoadComplete);

                TestContext.Out.WriteLine("Page load complete.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Page load failed.\n{e}");
            }
        }

        public void ScrollToEle(IWebElement element) {
            try {
                TestContext.Out.WriteLine($"Scrolling element into view: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                Js.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Out.WriteLine("Scrolled to element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not find element.\n{e}");
            }
        }

        public void ScrollToEle(By locator) {
            try {
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(locator));
                TestContext.Out.WriteLine($"Scrolling to element located by: {locator}");
                Js.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Out.WriteLine($"Scrolled to element: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not find element.\n{e}");
            }
        }

        public void ScrollToEleWithText(String text) {
            try {
                TestContext.Out.WriteLine($"Scrolling to element containing text: '{text}'");
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[contains(text(),\"{text}\")]")));
                Js.ExecuteScript("arguments[0].scrollIntoView(false, {behavior: \"smooth\"})", element);
                TestContext.Out.WriteLine($"Scrolled to element with text: {element.GetAttribute("outerHTML")}");
            } catch (Exception e) {
                TestContext.Out.WriteLine($"Could not find element containing text: '{text}'\n{e}");
            }
        }

        public void ScrollBy(int xPixels, int yPixels) {
            Js.ExecuteScript($"window.scrollBy({xPixels}, {yPixels})");
            TestContext.Out.WriteLine($"Scrolled down by {yPixels} pixels, right by {xPixels} pixels");
        }

        public void ScrollToEnd() {
            Js.ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
            TestContext.Out.WriteLine("Scrolled to end of page.");
        }

        public void ScrollToTop() {
            Js.ExecuteScript("window.scrollTo(0,0)");
            TestContext.Out.WriteLine("Scrolled to top of page.");
        }

        public IWebElement FindEle(By locator) {
            try {
                TestContext.Out.WriteLine($"Finding element located by: {locator}");
                IWebElement element = Driver.FindElement(locator);
                TestContext.Out.WriteLine($"Found element: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch (Exception e) {
                TestContext.Out.WriteLine($"Could not find element.\n{e}");
            }
            return null;
        }

        public IWebElement WaitForVis(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting for visibility of element located by: {locator}");
                IWebElement element =  Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                TestContext.Out.WriteLine($"Element is visible: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            }catch (Exception e) {
                TestContext.Out.WriteLine($"Could not find element.\n{e}");
            }
            return null;
        }

        public IList<IWebElement> WaitForVisOfAll(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting for visibility of all elements located by: {locator}");
                IList<IWebElement> elements = Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
                TestContext.Out.WriteLine($"Elements are visible ({elements.Count} elements).");
                return elements;
            } catch (Exception e) {
                TestContext.Out.WriteLine($"Timed out waiting for visibility.\n{e}");
            }
            return null;
        }

        public IWebElement WaitForPresence(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting for presence of element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(locator));
                TestContext.Out.WriteLine($"Element is present on DOM: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not find element.\n{e}");
            }
            return null;
        }

        public IList<IWebElement> WaitForPresenceOfAll(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting for presence of all elements located by: {locator}");
                IList<IWebElement> elements = Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                TestContext.Out.WriteLine($"Elements are present on DOM ({elements.Count} elements)");
                return elements;
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not find elements.\n{e}");
            }
            return null;
        }

        public IWebElement WaitForClickable(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting for clickability of element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                TestContext.Out.WriteLine($"Element became clickable: {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                return element;
            } catch (Exception e) {
                TestContext.Out.WriteLine($"Element did not become clickable.\n{e}");
            }
            return null;
        }

        public void ClickWhenReady(By locator) {
            try {
                WaitForClickable(locator).Click();
                TestContext.Out.WriteLine("Clicked on element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void ClickWhenReady(IWebElement element) {
            try {
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">")+1);
                TestContext.Out.WriteLine($"Waiting to click element: {elementHTML}");
                Wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                TestContext.Out.WriteLine("Clicked on element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void RightClick(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting to right click element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                Act.ContextClick(element).Perform();
                TestContext.Out.WriteLine($"Right clicked on element: {elementHTML}");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void RightClick(IWebElement element) {
            try {
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                TestContext.Out.WriteLine($"Waiting to right click element: {elementHTML}");
                Wait.Until(ExpectedConditions.ElementToBeClickable(element));
                Act.ContextClick(element).Perform();
                TestContext.Out.WriteLine("Right clicked on element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void DoubleClick(By locator) {
            try {
                TestContext.Out.WriteLine($"Waiting to doubleclick element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                Act.DoubleClick(element).Perform();
                TestContext.Out.WriteLine($"Doubleclicked on element: {elementHTML}");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void DoubleClick(IWebElement element) {
            try {
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                TestContext.Out.WriteLine($"Waiting to doubleclick element: {elementHTML}");
                Wait.Until(ExpectedConditions.ElementToBeClickable(element));
                Act.DoubleClick(element).Perform();
                TestContext.Out.WriteLine("Doubleclicked on element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Element not clicked (unclickable or click intercepted).\n{e}");
            }
        }

        public void HoverOn(By locator) {
            try {
                TestContext.Out.WriteLine($"Mousing over element located by: {locator}");
                IWebElement element = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                Act.MoveToElement(element).Perform();
                TestContext.Out.WriteLine($"Hovered mouse over element: {elementHTML}");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not mouseover element.\n{e}");
            }
        }

        public void HoverOn(IWebElement element) {
            try {
                string elementHTML = element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1);
                TestContext.Out.WriteLine($"Mousing over element: {elementHTML}");
                Act.MoveToElement(element).Perform();
                TestContext.Out.WriteLine("Hovered mouse over element.");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Could not mouseover element.\n{e}");
            }
        }

        public void EnterText(IWebElement element, string text) {
            try {
                TestContext.Out.WriteLine($"Entering text '{text}' to element {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}");
                element.SendKeys(text);
                TestContext.Out.WriteLine($"Input element value: {element.GetAttribute("value")}");
            } catch(Exception e) {
                TestContext.Out.WriteLine($"Text not entered.\n{e}");
            }
        }

        public bool DoesElementHaveAttribute(IWebElement element, string attribute) {
            string[] attributes = GetElementAttributes(element);
            bool hasAttribute = attributes.Contains(attribute);

            TestContext.Out.WriteLine($"Does element {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)} have attribute \"{attribute}\": {hasAttribute}");

            return hasAttribute;
        }

        public string[] GetElementAttributes(IWebElement element) {
            try {
                string html = element.GetAttribute("outerHTML").Substring(1, element.GetAttribute("outerHTML").IndexOf(">") - 1);

                html = Regex.Replace(html, "=\"[^\"]*\"", string.Empty);

                string[] attributes = html.Split(' ').Skip(1).ToArray();

                TestContext.Out.WriteLine($"Element attributes for {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}:\n{String.Join(", ", attributes)}");

                return attributes;
            } catch(Exception e) {
                TestContext.Out.WriteLine($"{e}");
            }
            return null;
        }

        public Dictionary<string, string> GetAttributeValueDict(IWebElement element) {
            try {
                string tagContents = element.GetAttribute("outerHTML").Substring(1, element.GetAttribute("outerHTML").IndexOf(">") - 1);

                string discardedTag = tagContents.Substring(0, tagContents.IndexOf(" ") + 1);

                string attributesAndValues = tagContents.Substring(tagContents.IndexOf(" ") + 1, tagContents.Length - discardedTag.Length);

                Dictionary<string, string> attributeValuePairs = new Dictionary<string, string>();

                foreach(Match match in Regex.Matches(attributesAndValues, @"(\S+)=(""[^""]*"")|(\S+)")) {
                    if(match.Groups[1].Success) {
                        attributeValuePairs.Add(match.Groups[1].Value, match.Groups[2].Value.Trim('"'));
                    } else {
                        attributeValuePairs.Add(match.Groups[3].Value, null);
                    }
                }

                TestContext.Out.WriteLine($"Attribute-Value pairs for {element.GetAttribute("outerHTML").Substring(0, element.GetAttribute("outerHTML").IndexOf(">") + 1)}:");

                foreach(KeyValuePair<string, string> attribute in attributeValuePairs) {
                    TestContext.Out.WriteLine($"{attribute.Key} : \"{attribute.Value}\"");
                }

                return attributeValuePairs;
            } catch(Exception e) {
                TestContext.Out.WriteLine($"{e}");
            }
            return null;
        }

        public void Pause() {
            Thread.Sleep(2500);
        }

        public void Pause(int millis) {
            Thread.Sleep(millis);
        }


    }
}
