using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;


namespace SumCalculationHomeWork
{
    public class SumHW
    {
        private static IWebDriver _driver;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
            _driver.Url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html";

            // Test passes even without closing popup
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement closePopUpButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("at-cv-lightbox-close")));
            closePopUpButton.Click();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            _driver.Quit();
        }

        [TestCase("2", "2", "4", TestName = "2 + 2 = 4")]
        [TestCase("-5", "3", "-2", TestName = "-5 + 3 = -2")]
        [TestCase("a", "b", "NaN", TestName = "a + b = NaN")]
        public static void SumCalculationTest(string valueOfA, string valueOfB, string expectedResult)
        {
            // Find elements
            IWebElement sum1InputField = _driver.FindElement(By.Id("sum1"));
            sum1InputField.SendKeys(valueOfA);

            IWebElement sum2InputField = _driver.FindElement(By.Id("sum2"));
            sum2InputField.SendKeys(valueOfB);

            // CSSSelector button.btn:nth-child(3)
            IWebElement getTotalButton = _driver.FindElement(By.CssSelector("button.btn:nth-child(3)"));
            getTotalButton.Click();

            // Clear the input fields
            sum1InputField.Clear();
            sum2InputField.Clear();

            // #displayvalue
            IWebElement resultText = _driver.FindElement(By.Id("displayvalue"));

            Assert.AreEqual(expectedResult, resultText.Text, "Wrong answer");
        }
    }
}
