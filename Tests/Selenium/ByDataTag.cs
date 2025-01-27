using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Selenium
{
    public class ByDataTag : By
    {
        private readonly By cssSelector;

        private readonly string tagName = "data-testid";

        public ByDataTag(string tagValue = "")
        {
            // If there's an empty string find all data tags within the search context
            if (string.IsNullOrEmpty(tagValue))
            {
                cssSelector = By.CssSelector($"[{tagName}]");
            }
            else
            {
                cssSelector = By.CssSelector($"[{tagName}=\"{tagValue}\"]");
            }
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return cssSelector.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return cssSelector.FindElements(context);
        }
    }
}
