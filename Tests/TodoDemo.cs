using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Text.RegularExpressions;
using Tests.Assertions;
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
            // Verify the unchecked state
            StringAssert.DoesNotMatch(TodoPage.FirstTodo.GetCssValue("class"), new Regex("completed"));
            StringAssert.Contains(TodoPage.FirstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length} items left");
            Driver.AssertElementExists(By.ClassName("clear-completed"), false);

            // Check first todo
            TodoPage.FirstTodo.FindElement(By.CssSelector("input.toggle")).Click();

            // Verify checked state
            wait.Until(drv =>
            {
                StringAssert.Matches(TodoPage.FirstTodo.GetAttribute("class"), new Regex("completed"));
                return true;
            });
            StringAssert.Contains(TodoPage.FirstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length - 1} items left");
            Driver.AssertElementExists(By.ClassName("clear-completed"), true);

            // Uncheck first todo
            TodoPage.FirstTodo.FindElement(By.CssSelector("input.toggle")).Click();

            // Verify the unchecked state
            wait.Until(drv =>
            {
                StringAssert.DoesNotMatch(TodoPage.FirstTodo.GetAttribute("class"), new Regex("completed"));
                return true;
            });
            StringAssert.Contains(TodoPage.FirstTodo.Text, todos[0]);
            StringAssert.Contains(TodoPage.TodoCount.Text, $"{todos.Length} items left");
            Driver.AssertElementExists(By.ClassName("clear-completed"), false);
        }

        [TestMethod]
        public void VerifyClearCompleted()
        {
            var wait = new WebDriverWait(this.Driver, System.TimeSpan.FromSeconds(4));
            wait.IgnoreExceptionTypes(typeof(AssertFailedException));

            // Create the the todos
            TodoPage.CreateTodo(todos);

            foreach (var todo in todos)
            {
                IWebElement toggle = TodoPage.FirstTodo.FindElement(By.CssSelector("input.toggle"));

                // Verify the unchecked state
                StringAssert.Equals(TodoPage.FirstTodo.Text, todo);
                Assert.IsFalse(bool.Parse(toggle.GetDomProperty("checked")));
                // Clear completed button should not exist
                Driver.AssertElementExists(By.ClassName("clear-completed"), false);

                // Check first todo
                toggle.Click();

                // Verify checked state
                toggle = wait.Until(d =>
                {
                    IWebElement label = TodoPage.FirstTodo.FindElement(new ByDataTag("todo-title"));
                    // Ensure correct CSS is applied after completion
                    StringAssert.Equals(label.GetCssValue("text-decoration-line"), "line-through");
                    return TodoPage.FirstTodo.FindElement(By.CssSelector("input.toggle"));
                });
                Assert.IsTrue(bool.Parse(toggle.GetDomProperty("checked")));

                // Clear completed todo
                TodoPage.ClearCompletedBtn.Click();
            }
        }
    }
}
