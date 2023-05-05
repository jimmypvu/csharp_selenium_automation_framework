using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumNUnitFramework.Utils
{
    internal static class BrowserManager
    {
        public static IWebDriver GetChromeDriver() {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = GetChromeOptions();
            return new ChromeDriver(options);
        }

        public static IWebDriver GetFirefoxDriver() {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            FirefoxOptions options = GetFirefoxOptions();
            return new FirefoxDriver(options);
        }

        public static IWebDriver GetEdgeDriver() {
            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            EdgeOptions options = GetEdgeOptions();
            return new EdgeDriver(options);
        }

        private static ChromeOptions GetChromeOptions() {
            ChromeOptions options = new ChromeOptions();
            if (ConfigurationManager.AppSettings.Get("headless").Equals("true")) {
                options.AddArgument("--headless");
                options.AddArgument("--window-size=1920,1080");
            }
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.DismissAndNotify;
            //string projDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string combinedPath = Path.Combine(projDirectory, "Resources\\2.0.0_0.crx");
            //options.AddExtension(combinedPath);
            //options.SetLoggingPreference(LogType.Performance, LogLevel.All);
            //options.AddArgument("--enable-logging --v=1");
            
            return options;
        }

        private static FirefoxOptions GetFirefoxOptions() {
            FirefoxOptions options = new FirefoxOptions();
            if (ConfigurationManager.AppSettings.Get("headless").Equals("true")) {
                options.AddArgument("-width=1920");
                options.AddArgument("-height=1080");
                options.AddArgument("--headless");
            }
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.DismissAndNotify;

            return options;
        }

        private static EdgeOptions GetEdgeOptions() {
            EdgeOptions options = new EdgeOptions();
            if (ConfigurationManager.AppSettings.Get("headless").Equals("true")) {
                options.AddArgument("--window-size=1920,1080");
                options.AddArgument("--headless");
            }
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.DismissAndNotify;

            return options;
        }      
    }
}
