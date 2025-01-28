using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;
using System.Linq;
using Tests.Selenium;

namespace Tests.Pages
{
    public class TodoPage(IWebDriver driver)
    {
        #region properties
        [FindsBy(How = How.ClassName, Using = "new-todo")]
        [CacheLookup]
        public IWebElement NewTodo { get; set; }

        [FindsBy(How = How.ClassName, Using = "clear-completed")]
        public IWebElement ClearCompletedBtn { get; set; }

        public ReadOnlyCollection<IWebElement> AllTodos { get => driver.FindElementsByDataTag("todo-item"); }

        public IWebElement FirstTodo { get => AllTodos.First(); }

        public IWebElement TodoCount { get => driver.FindElementByDataTag("todo-count"); }
        #endregion properties

        #region methods
        public void CreateTodo(string newTodo)
        {
            this.NewTodo.SendKeys(newTodo + Keys.Enter);
        }

        public void CreateTodo(string[] newTodos)
        {
            foreach (var todo in newTodos)
            {
                this.CreateTodo(todo);
            }
        }
        #endregion methods
    }
}
