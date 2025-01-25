using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
                StringAssert.Contains(TodoPage.TodoCount.Text.Trim(), $"{i + 1} item{(i == 0 ? "" : "s")} left");
            }
        }
    }
}
