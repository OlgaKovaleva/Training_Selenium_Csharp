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
    public class ProductDetailsCheckChrome
  
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
        public void TestProductDetailsPage()
        {
            driver.Url = "http://localhost:8080/litecart";
            wait.Until(ExpectedConditions.ElementExists(By.Id("slider-wrapper")));

            var product = driver.FindElement(By.CssSelector("#box-campaigns [class^=product]"));
            var productTitleOnMain = product.FindElement(By.CssSelector(".name"));
            var productTitleOnMainText = product.FindElement(By.CssSelector(".name")).Text;
            var priceRegularOnMain = product.FindElement(By.CssSelector(".regular-price"));
            var priceCampaignOnMain = product.FindElement(By.CssSelector(".campaign-price"));
            var priceRegularOnMainText = product.FindElement(By.CssSelector(".regular-price")).Text;
            var priceCampaignOnMainText = product.FindElement(By.CssSelector(".campaign-price")).Text;
            //grey color checking
            var colorOfRegularPriceOnMain = priceRegularOnMain.GetCssValue("color");
            if (colorOfRegularPriceOnMain.Contains("rgba"))
            {
                string[] numbers = colorOfRegularPriceOnMain.Replace("rgba(", "").Replace(")", "").Split(',');
                int r = int.Parse(numbers[0].Trim());
                int g = int.Parse(numbers[1].Trim());
                int b = int.Parse(numbers[2].Trim());
                Assert.AreEqual(r, g);
                Assert.AreEqual(g, b);
            }
            else
            {
                string[] numbers = colorOfRegularPriceOnMain.Replace("rgb(", "").Replace(")", "").Split(',');
                int r = int.Parse(numbers[0].Trim());
                int g = int.Parse(numbers[1].Trim());
                int b = int.Parse(numbers[2].Trim());
                Assert.AreEqual(r, g);
                Assert.AreEqual(g, b);
            }
            
           
            
            //strike text checking
            Assert.AreEqual("s", priceRegularOnMain.TagName);
                     
            //red color checking
            var colorOfCampaignPriceOnMain = priceCampaignOnMain.GetCssValue("color");
            if (colorOfCampaignPriceOnMain.Contains("rgba"))
            {
                string[] numbers2 = colorOfCampaignPriceOnMain.Replace("rgba(", "").Replace(")", "").Split(',');
                int g2 = int.Parse(numbers2[1].Trim());
                int b2 = int.Parse(numbers2[2].Trim());
                Assert.AreEqual(0, g2);
                Assert.AreEqual(0, b2);
            }
            
            else
            {
                string[] numbers2 = colorOfCampaignPriceOnMain.Replace("rgb(", "").Replace(")", "").Split(',');
                int g2 = int.Parse(numbers2[1].Trim());
                int b2 = int.Parse(numbers2[2].Trim());
                Assert.AreEqual(0, g2);
                Assert.AreEqual(0, b2);
            }
            //strong text checking
            Assert.AreEqual("strong", priceCampaignOnMain.TagName);
            //size checking
            Size sizeRegular= priceRegularOnMain.Size;
            Size sizeCampaign = priceCampaignOnMain.Size;
            Assert.Greater(sizeCampaign.Height, sizeRegular.Height);
            Assert.Greater(sizeCampaign.Width, sizeRegular.Width);
            //go to product details page
            product.FindElement(By.CssSelector("a.link")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("box-product")));
            var productTitleOnDetails = driver.FindElement(By.CssSelector("#box-product .title"));
            var priceRegularOnDetails = driver.FindElement(By.CssSelector("#box-product .regular-price"));
            var priceCampaignOnDetails = driver.FindElement(By.CssSelector("#box-product .campaign-price"));
            //title and prices verification
            Assert.AreEqual(productTitleOnMainText, productTitleOnDetails.Text);
            Assert.AreEqual(priceRegularOnMainText, priceRegularOnDetails.Text);
            Assert.AreEqual(priceCampaignOnMainText, priceCampaignOnDetails.Text);

            //grey color checking
            var colorOfRegularPriceOnDetails = priceRegularOnDetails.GetCssValue("color");
            if (colorOfRegularPriceOnDetails.Contains("rgba"))
            {
                string[] numbers3 = colorOfRegularPriceOnMain.Replace("rgba(", "").Replace(")", "").Split(',');
                int r3 = int.Parse(numbers3[0].Trim());
                int g3 = int.Parse(numbers3[1].Trim());
                int b3 = int.Parse(numbers3[2].Trim());
                Assert.AreEqual(r3, g3);
                Assert.AreEqual(g3, b3);
            }
            else
            {
                string[] numbers3 = colorOfRegularPriceOnMain.Replace("rgb(", "").Replace(")", "").Split(',');
                int r3 = int.Parse(numbers3[0].Trim());
                int g3 = int.Parse(numbers3[1].Trim());
                int b3 = int.Parse(numbers3[2].Trim());
                Assert.AreEqual(r3, g3);
                Assert.AreEqual(g3, b3);
            }
            
            //strike text checking
            Assert.AreEqual("s", priceRegularOnDetails.TagName);

            //red color checking
            var colorOfCampaignPriceOnDetails = priceCampaignOnDetails.GetCssValue("color");
            if (colorOfCampaignPriceOnDetails.Contains("rgba"))
            {
                string[] numbers4 = colorOfCampaignPriceOnMain.Replace("rgba(", "").Replace(")", "").Split(',');
                int g4 = int.Parse(numbers4[1].Trim());
                int b4 = int.Parse(numbers4[2].Trim());
                Assert.AreEqual(0, g4);
                Assert.AreEqual(0, b4);
            }
            else
            {
                string[] numbers4 = colorOfCampaignPriceOnMain.Replace("rgb(", "").Replace(")", "").Split(',');
                int g4 = int.Parse(numbers4[1].Trim());
                int b4 = int.Parse(numbers4[2].Trim());
                Assert.AreEqual(0, g4);
                Assert.AreEqual(0, b4);
            }
            
            //strong text checking
            Assert.AreEqual("strong", priceCampaignOnDetails.TagName);
            //size checking
            Size sizeRegularOnDetails = priceRegularOnDetails.Size;
            Size sizeCampaignOnDetails = priceCampaignOnDetails.Size;
            Assert.Greater(sizeCampaignOnDetails.Height, sizeRegularOnDetails.Height);
            Assert.Greater(sizeCampaignOnDetails.Width, sizeRegularOnDetails.Width);



        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
