using System.Threading;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumContinousAutomation.PageObjects
{
    public class TestPage
    {

        private IWebDriver _driver;

        public TestPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
            _driver.Navigate().GoToUrl("http://google.com.br/");

        }

        #region WebElements

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchInput;

        [FindsBy(How = How.Name, Using = "btnK")]
        private IWebElement searchButton;

        #endregion

        #region  Methods

        public void SearchSomething(string something)
        {
            searchInput.SendKeys(something);
            Thread.Sleep(1000);

            searchButton.Click();
            Thread.Sleep(10000);
        }


        #endregion
    }
}
