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
    public class GeozonesOrderCheck
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
        public void TestGeoZonesSorting()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Name("geo_zones_form")));


            int countriesCount = driver.FindElements(By.XPath("//form[@name='geo_zones_form']/table/tbody/tr[@class='row']")).Count;
            for (int j=1; j <= countriesCount; j++)
            {
                driver.FindElement(By.XPath("//form[@name='geo_zones_form']/table/tbody/tr[@class='row']["+j+ "]/td[3]/a")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.Id("table-zones")));
                List<string> zonesExpectedList = new List<string>();
                ICollection<IWebElement> zoneRows = driver.FindElements(By.CssSelector("table#table-zones tbody tr:not(.header):not(:last-child)"));

                foreach (IWebElement zone in zoneRows)
                {
                    zonesExpectedList.Add(zone.FindElement(By.XPath(".//td[3]/select/option[@selected]")).Text);

                }
                zonesExpectedList.OrderBy(i => i);
                int m = 0;
                foreach (IWebElement zone in zoneRows)
                {
                    Assert.AreEqual(zonesExpectedList[m], zone.FindElement(By.XPath(".//td[3]/select/option[@selected]")).Text);
                    m++;
                }



                driver.Navigate().Back();

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
