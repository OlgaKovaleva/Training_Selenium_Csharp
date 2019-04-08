using System;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CheckoutCheck

    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));


        }

        Boolean IsElementPresent(IWebDriver driver, By Locator)
        {
            return driver.FindElements(Locator).Count > 0;

        }


        [Test]
        public void TestAddingProductToCheckout()
        {
            

            driver.Url = "http://localhost:8080/litecart";
            wait.Until(ExpectedConditions.ElementExists(By.Id("slider-wrapper")));
            int i = 1;
            do
            {
                
                driver.FindElement(By.CssSelector("#box-most-popular [class^=product]")).Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("[name=add_cart_product]")));

                
                driver.FindElement(By.CssSelector("[name=add_cart_product]")).Click();
                var quantityInCart = driver.FindElement(By.CssSelector("#cart .quantity"));
                wait.Until(ExpectedConditions.TextToBePresentInElement(quantityInCart, Convert.ToString(i)));
                driver.Navigate().Back();
                wait.Until(ExpectedConditions.ElementExists(By.Id("slider-wrapper")));
                i++;
            } while (driver.FindElement(By.CssSelector("#cart .quantity")).Text != "3");

            driver.FindElement(By.CssSelector("#cart .link")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("box-checkout-summary")));

            do
            {
                var productList = driver.FindElement(By.CssSelector("#order_confirmation-wrapper table tbody"));
                driver.FindElement(By.CssSelector("[value=Remove]")).Click();
                wait.Until(ExpectedConditions.StalenessOf(productList));

            } while (IsElementPresent(driver, By.CssSelector("#order_confirmation-wrapper")));


            
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
