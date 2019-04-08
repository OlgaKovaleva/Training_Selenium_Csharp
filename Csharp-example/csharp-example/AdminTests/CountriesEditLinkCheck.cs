using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace csharp_example
{
    [TestFixture]
    public class CountriesEditLinkCheck
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
        public void TestCountriesEditLinkOpening()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.TagName("h1")), "Countries"));
            driver.FindElement(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']/td[5]/a")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("table-zones")));
            var externalLinks = driver.FindElements(By.XPath("//i[@class='fa fa-external-link']/.."));
            foreach(var externalLink in externalLinks)
            {
                var originWindow = driver.CurrentWindowHandle;
                var originalWindows = driver.WindowHandles;
                externalLink.Click();
                
                string newWindowOpening = wait.Until<string>((d) =>
                {
                    string foundWindow = null;
                    List<string> newWindows = driver.WindowHandles.Except(originalWindows).ToList();
                    if (newWindows.Count > 0)
                    {
                        foundWindow= newWindows[0];
                    }

                    return foundWindow;
                });

                driver.SwitchTo().Window(newWindowOpening);
                driver.Close();
                driver.SwitchTo().Window(originWindow);

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
