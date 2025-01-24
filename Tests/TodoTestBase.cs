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
        }
        public TodoPage TodoPage { get; private set; }
        protected IWebDriver Driver;
        private readonly string url = @"https://demo.playwright.dev/todomvc";

        [TestInitialize]
        public void BeforeEach()
        {
            Driver.Manage().Window.Maximize();
            Driver.Url = url;
        }

        [TestCleanup]
        public void AfterEach()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
