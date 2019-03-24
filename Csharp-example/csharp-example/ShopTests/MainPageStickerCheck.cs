using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MainPageStickerCheck
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
        public void TestStickerPresence()
        {
            driver.Url = "http://localhost:8080/litecart";
            wait.Until(ExpectedConditions.ElementExists(By.Id("slider-wrapper")));

            var productItems = driver.FindElements(By.CssSelector("[class^=product]"));
            foreach(var productItem in productItems)
            {
                var stickerAmount=productItem.FindElements(By.CssSelector("[class^=sticker]")).Count;
                Assert.AreEqual(1, stickerAmount);
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
