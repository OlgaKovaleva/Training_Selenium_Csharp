using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace csharp_example
{
    [TestFixture]
    public class NewProductAddCheck
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
            string value = Convert.ToString(number);
            return value;

        }

        [Test]
        public void TestNewProductAdding()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.TagName("h1")), "Catalog"));
            driver.FindElement(By.CssSelector("[href*=edit_product]")).Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.TagName("h1")), "Add New Product"));
            driver.FindElement(By.XPath("//strong[contains(.,'Status')]/../label[1]/input")).Click();

            var name = "Product Name"+ GetRandomNumber();
            driver.FindElement(By.Name("name[en]")).SendKeys(name);
            driver.FindElement(By.Name("code")).SendKeys("NewProductCode");

            driver.FindElement(By.CssSelector("[data-name=Subcategory]")).Click();
            SelectElement drpCategory= new SelectElement(driver.FindElement(By.Name("default_category_id")));
            drpCategory.SelectByText("Subcategory");

            driver.FindElement(By.XPath("//strong[contains(.,'Product Groups')]/../div/table/tbody/tr[4]/td[1]")).Click();
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys(Keys.Home + "7");
        
            string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "beautiful-composition-with-palm-leaves-yellow-background_24972-68.jpg");
            
            driver.FindElement(By.XPath("//strong[contains(.,'Upload Images')]/../table/tbody/tr[1]/td/input")).SendKeys(fullPath);
            driver.FindElement(By.XPath("//strong[contains(.,'Date Valid From')]/../input")).SendKeys("01012019");
            driver.FindElement(By.XPath("//strong[contains(.,'Date Valid From')]/../input")).SendKeys("01122019");

            driver.FindElement(By.CssSelector(".tabs ul li:nth-child(2)")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Name("manufacturer_id")));
            SelectElement drdManufacturer = new SelectElement(driver.FindElement(By.Name("manufacturer_id")));
            drdManufacturer.SelectByValue("1");
            //SelectElement drdSupplier = new SelectElement(driver.FindElement(By.Name("supplier_id")));
            //drdSupplier.SelectByValue("1");
            driver.FindElement(By.Name("keywords")).SendKeys("keys words new");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("short desc");
            driver.FindElement(By.Name("description[en]")).SendKeys("full desc");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("Title");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("meta desc");

            driver.FindElement(By.CssSelector(".tabs ul li:nth-child(4)")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Name("purchase_price_currency_code")));
            driver.FindElement(By.XPath("//strong[contains(.,'Purchase Price')]/../input")).Clear();
            driver.FindElement(By.XPath("//strong[contains(.,'Purchase Price')]/../input")).SendKeys(Keys.Home + "35");
            SelectElement drdCurrency = new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")));
            drdCurrency.SelectByValue("USD");

            driver.FindElement(By.Name("gross_prices[USD]")).Clear();
            driver.FindElement(By.Name("gross_prices[USD]")).SendKeys("44");
            driver.FindElement(By.Name("gross_prices[EUR]")).Clear();
            driver.FindElement(By.Name("gross_prices[EUR]")).SendKeys("40");

            driver.FindElement(By.Name("save")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".notice.success")));

            var rows = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']/td[3]/a"));
            bool k=false;
            foreach (var row in rows)
            {
                if (row.Text == name)
                    k = true;
            }
            Assert.IsTrue(k);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }


}
