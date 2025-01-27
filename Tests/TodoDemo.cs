using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tests.Selenium;

namespace Tests
{
    [TestClass]
    public class TodoDemo : TodoTestBase
    {
        readonly string[] todos = { "First Todo", "Second Todo", "Third Todo" };

        [TestMethod]
        public void CreateTodos()
        {
            // Create a new wait for the number of elements and ignore assertion failures
            var wait = new WebDriverWait(this.Driver, System.TimeSpan.FromSeconds(4));
            wait.IgnoreExceptionTypes(typeof(AssertFailedException));
            Assert.AreEqual("todos", Driver.FindElement(By.CssSelector("header h1")).Text, true);
            for (int i = 0; i < todos.Length; i++)
            {
                TodoPage.NewTodo.SendKeys(todos[i] + Keys.Enter);

                // Confirm number of todos
                wait.Until(drv =>
                {
                    var els = drv.FindElementsByDataTag("todo-item");
                    Assert.AreEqual(i + 1, els.Count);
                    return els;
                });
                // Confirm todos are active and text matches
                IWebElement lastEl = TodoPage.AllTodos.Last();
                StringAssert.Contains(lastEl.Text, todos[i]);
                StringAssert.DoesNotMatch(lastEl.GetCssValue("class"), new Regex("completed"));

                // Confirm todo count
                StringAssert.Contains(TodoPage.TodoCount.Text, $"{i + 1} item{(i == 0 ? "" : "s")} left");
            }
        }

        [TestMethod]
        public void VerifyCheckUncheckFunctionality()
        {
            var wait = new WebDriverWait(this.Driver, System.TimeSpan.FromSeconds(4));
            wait.IgnoreExceptionTypes(typeof(AssertFailedException));

            // Create the the todos
            TodoPage.CreateTodo(todos);

            // Will use the first item in the list to check/uncheck
            IWebElement firstTodo = TodoPage.AllTodos.First();

            // Verify the unchecked state
            StringAssert.DoesNotMatch(firstTodo.GetCssValue("class"), new Regex("completed"));
            StringAssert.Contains(firstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length} items left");
            Assert.AreEqual(0, TodoPage.ClearCompletedBtn.Count);

            // Check first todo
            firstTodo.FindElement(By.CssSelector("input.toggle")).Click();

            // Verify checked state
            firstTodo = wait.Until(drv =>
            {
                IWebElement first = TodoPage.AllTodos.First();
                StringAssert.Matches(first.GetAttribute("class"), new Regex("completed"));
                return first;
            });
            StringAssert.Contains(firstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length - 1} items left");
            Assert.AreEqual(1, TodoPage.ClearCompletedBtn.Count);

            // Uncheck first todo
            firstTodo.FindElement(By.CssSelector("input.toggle")).Click();

            // Verify the unchecked state
            firstTodo = wait.Until(drv =>
            {
                IWebElement first = TodoPage.AllTodos.First();
                StringAssert.DoesNotMatch(first.GetAttribute("class"), new Regex("completed"));
                return first;
            });
            StringAssert.Contains(firstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length} items left");
            Assert.AreEqual(0, TodoPage.ClearCompletedBtn.Count);
        }
    }
}
