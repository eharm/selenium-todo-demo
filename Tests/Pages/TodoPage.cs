using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Tests.Pages
{
    public class TodoPage
    {
        public TodoPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        private readonly IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = ".new-todo")]
        public IWebElement NewTodo { get; set; }
    }
}
