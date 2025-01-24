using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Tests
{
    [TestClass]
    public class TodoDemo : TodoTestBase
    {
        [TestMethod]
        public void CreateTodos()
        {
            TodoPage.NewTodo.SendKeys("Testing" + Keys.Enter);
        }
    }
}
