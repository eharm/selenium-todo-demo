using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tests
{
    public abstract class TodoTestBase : ITestBase
    {
        protected IWebDriver Driver;
        private readonly string url = @"https://demo.playwright.dev/todomvc";

        [TestInitialize]
        public void BeforeEach()
        {
            Driver = new ChromeDriver();
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
