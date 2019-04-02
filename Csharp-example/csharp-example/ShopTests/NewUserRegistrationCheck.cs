using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class NewUserRegistrationCheck
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        static string GetRandomNumber()
        {
            Random rnd = new Random();
            int number = rnd.Next();
            string value=Convert.ToString(number);
            return value;

        }

        [Test]
        public void TestNewUserRegistration()
        {
            driver.Url = "http://localhost:8080/litecart";
            wait.Until(ExpectedConditions.ElementExists(By.Id("slider-wrapper")));

            driver.FindElement(By.CssSelector("[href*=create_account]")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("create-account")));

            driver.FindElement(By.Name("tax_id")).SendKeys("12345");
            driver.FindElement(By.Name("company")).SendKeys("CompanyName");
            driver.FindElement(By.Name("firstname")).SendKeys("First");
            driver.FindElement(By.Name("lastname")).SendKeys("LAst");
            driver.FindElement(By.Name("address1")).SendKeys("Address line 1");
            driver.FindElement(By.Name("address2")).SendKeys("Address line 2");
            driver.FindElement(By.Name("postcode")).SendKeys("54321");
            driver.FindElement(By.Name("city")).SendKeys("NY");
            SelectElement drpCountry = new SelectElement(driver.FindElement(By.Name("country_code")));
            drpCountry.SelectByValue("US");
            wait.Until(ExpectedConditions.ElementExists(By.Name("zone_code")));
            SelectElement stateCountry = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
            stateCountry.SelectByValue("AL");

            var email = GetRandomNumber()+"test@gmail.com";
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("12345678");
            driver.FindElement(By.Name("password")).SendKeys("Password");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("Password");

            driver.FindElement(By.Name("create_account")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.Id("box-account")));
            driver.FindElement(By.CssSelector("[href*=logout]")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.Id("box-account-login")));
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys("Password");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("box-account")));

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
