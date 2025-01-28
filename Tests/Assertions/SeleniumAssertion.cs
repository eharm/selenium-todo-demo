using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Assertions
{
    public static class SeleniumAssertion
    {
        public static void AssertElementExists(this IWebDriver driver, By by, bool expected)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            var actual = wait.Until(d =>
            {
                try
                {
                    d.FindElement(by);
                    // If the FindElement doesn't throw an exception, AND we're expecting it to exist then short circuit
                    if (expected)
                    {
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    // If the FindElement throws an exception, AND we're expecting it to not exist then short circuit
                    if (!expected)
                    {
                        return true;
                    }
                }
                return false;
            });
            // If we get here this means the assertion is true.
            // Need to add a logging feature to log the assertion passed
        }
    }
}
