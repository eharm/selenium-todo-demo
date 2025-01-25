using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using Tests.Pages;

namespace Tests
{
    public abstract class TodoTestBase : ITestBase
    {
        public TodoTestBase()
        {
            Driver = new ChromeDriver();
            this.TodoPage = new TodoPage(Driver);
            PageFactory.InitElements(Driver, this.TodoPage);
            Driver.Manage().Window.Maximize();
        }
        public TodoPage TodoPage { get; private set; }
        protected IWebDriver Driver;
        private readonly string url = @"https://demo.playwright.dev/todomvc";

        [TestInitialize]
        public void BeforeEach()
        {
            Driver.Url = url;
        }

        [TestCleanup]
        public void AfterEach()
        {
            (Driver as IJavaScriptExecutor).ExecuteScript("sessionStorage.clear()");
            (Driver as IJavaScriptExecutor).ExecuteScript("localStorage.clear()");
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
