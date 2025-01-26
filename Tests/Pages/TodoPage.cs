using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;
using Tests.Selenium;

namespace Tests.Pages
{
    public class TodoPage
    {
        public TodoPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        private readonly IWebDriver _driver;

        #region properties
        [FindsBy(How = How.ClassName, Using = "new-todo")]
        [CacheLookup]
        public IWebElement NewTodo { get; set; }

        public ReadOnlyCollection<IWebElement> ClearCompletedBtn { get => _driver.FindElements(By.ClassName("clear-completed")); }

        public ReadOnlyCollection<IWebElement> AllTodos { get => _driver.FindElementsByDataTag("todo-item"); }

        public IWebElement TodoCount { get => _driver.FindElementByDataTag("todo-count"); }
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
