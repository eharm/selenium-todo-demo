using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace Tests.Selenium
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver drv, By by)
        {
            var wait = new WebDriverWait(drv, TimeSpan.FromSeconds(4));
            return wait.Until(d => d.FindElement(by));
        }
        
        // Figure out how to turn turn this into a custom finds by attribute from DotNetSeleniumExtras.PageObjects
        // Should have to create a new class that inherits from AbstractFindsByAttribute
        public static IWebElement FindElementByDataTag(this IWebDriver drv, string tag)
        {
            var wait = new WebDriverWait(drv, TimeSpan.FromSeconds(4));
            return wait.Until(d => d.FindElement(By.CssSelector($"[data-testid=\"{tag}\"]")));
        }

        public static ReadOnlyCollection<IWebElement> FindElementsByDataTag(this IWebDriver drv, string tag)
        {
            var wait = new WebDriverWait(drv, TimeSpan.FromSeconds(4));
            return wait.Until(d => d.FindElements(By.CssSelector($"[data-testid=\"{tag}\"]")));
        }
    }
}
