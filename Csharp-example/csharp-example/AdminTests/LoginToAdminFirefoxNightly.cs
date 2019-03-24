using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LoginToAdminFirefoxNightly
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestLoginToAdmin()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("box-apps-menu-wrapper")));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
