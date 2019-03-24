using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class AllPagesCheck
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        [Test]
        public void TestAdminMenu()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("box-apps-menu-wrapper")));

            var menuItemsNumber = driver.FindElements(By.CssSelector("#box-apps-menu-wrapper ul li#app-")).Count;
            for (int i=1; i<= menuItemsNumber; i++)
            {
                driver.FindElement(By.CssSelector("li#app-:nth-child("+i+") a")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.TagName("h1")));

                var submenuItemsNumber = driver.FindElement(By.CssSelector("li#app-:nth-child(" + i + ")"));
                   var submenuNOnSelectedItems=submenuItemsNumber.FindElements(By.CssSelector("li")).Count;
                for (int j=1; j<= submenuNOnSelectedItems; j++)
                {
                    var submenuItem = driver.FindElement(By.CssSelector("li#app-:nth-child(" + i + ")")).FindElement(By.CssSelector("li:nth-child(" + j + ") a"));
                    submenuItem.Click();
                    wait.Until(ExpectedConditions.ElementExists(By.TagName("h1")));

                }
            }
           

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
