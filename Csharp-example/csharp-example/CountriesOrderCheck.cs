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
    public class CountriesOrderCheck
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
        public void TestCountriesSorting()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.TagName("h1")),"Countries"));

            #region Countries Sorting check

            List<string>  countriesExpectedList = new List<string>();
            ICollection<IWebElement> countries = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']/td[5]"));
        
            foreach(IWebElement country in countries)
            {
                countriesExpectedList.Add(country.Text);

            }
            countriesExpectedList.OrderBy(i=>i);
            int j = 0;
            foreach (IWebElement country in countries)
            {
                Assert.AreEqual(countriesExpectedList[j], country.Text);
                j++;
            }

            #endregion

            #region Zones Sorting Check

            int countriesNumber = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']")).Count();
            for (int k = 1; k <= countriesNumber; k++)
            {
                var countryWithZones = driver.FindElement(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row'][" + k + "]"));
                if (Convert.ToInt32(countryWithZones.FindElement(By.XPath(".//td[6]")).Text) > 0)
                {
                    countryWithZones.FindElement(By.XPath(".//td[5]/a")).Click();
                    wait.Until(ExpectedConditions.ElementExists(By.Id("table-zones")));

                    List<string> zonesExpectedList = new List<string>();
                    ICollection<IWebElement> zoneRows = driver.FindElements(By.CssSelector("table#table-zones tbody tr:not(.header):not(:last-child)"));

                    foreach (IWebElement zone in zoneRows)
                    {
                        zonesExpectedList.Add(zone.FindElement(By.XPath(".//td[3]")).Text);

                    }
                    zonesExpectedList.OrderBy(i => i);
                    int m = 0;
                    foreach (IWebElement zone in zoneRows)
                    {
                        Assert.AreEqual(zonesExpectedList[m], zone.FindElement(By.XPath(".//td[3]")).Text);
                        m++;
                    }

                    driver.Navigate().Back();

                }
            }
            

            #endregion

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

    
}
