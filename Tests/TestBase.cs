using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    internal class TestBase
    {
        [ClassInitialize]
        public void BeforeAll()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestInitialize]
        public void BeforeEach()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
    }
}
