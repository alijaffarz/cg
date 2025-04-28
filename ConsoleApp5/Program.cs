using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginWithUnreliablePopupHandlingTest
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void LoginValid()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();


            Assert.That(driver.Url.Contains("inventory.html"), "Login failed. User is not on the inventory page.");
            IWebElement titleElement = driver.FindElement(By.XPath("//*[@id=\"header_container\"]/div[1]/div[2]/div"));
            Console.WriteLine(titleElement.Text);
            Assert.That(titleElement.Text, Is.EqualTo("Swag Labs"), "The text 'Swag Labs' is not present in the header.");

        }
        [Test]
        public void WrongPassword()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("sadasda");
            driver.FindElement(By.Id("login-button")).Click();

            IWebElement titleElement = driver.FindElement(By.XPath("//*[@id=\"login_button_container\"]/div/form/div[3]/h3"));
            Console.WriteLine(titleElement.Text);
            Assert.That(titleElement.Text, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));


            //*[@id="login_button_container"]/div/form/div[3]/h3

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}