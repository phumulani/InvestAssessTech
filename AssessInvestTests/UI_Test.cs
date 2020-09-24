using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssessInvestTests
{
    [TestFixture]
    public class UI_Test
    {
        public static IWebDriver driver;
        private static readonly string chromeLocation = @"C:\DevWork\AssessInvestTests\AssessInvestTests";

        [TestCase("https://www.investec.com/", "test@investec.co.za")]
        public static void Sign_up_to_receive_focus_insights_straight_to_your_inbox(string url, string email)
        {
            try
            {
                NavigateToWebsite(url);

                NavigateToUnderstandingCashInvestmentInterestRates();
                
                SignUpToReceiveFocusInsights(email);

                //Thread.Sleep(5000);
                //Validate success screen.
                var isAddUserButtonDisplayed = driver.FindElement(By.CssSelector("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(2) > div")).Displayed;

                driver.Close();
                driver.Quit();

                Assert.IsTrue(isAddUserButtonDisplayed, "Sign up successfull.");
            }
            catch (Exception)
            {
                driver.Close();
                driver.Quit();
            }

            Console.WriteLine("Test Completed Successfuly.");
        }

        private static void SignUpToReceiveFocusInsights(string email)
        {
            ScrollToElement("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(1) > section > fieldset:nth-child(3) > button");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            driver.FindElement(By.CssSelector("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(1) > section > fieldset:nth-child(2) > div:nth-child(1) > text-input > div > div.text-input__input-holder.text-input__input-holder--animated > input")).SendKeys("Client Name");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            driver.FindElement(By.CssSelector("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(1) > section > fieldset:nth-child(2) > div:nth-child(2) > text-input > div > div.text-input__input-holder.text-input__input-holder--animated > input")).SendKeys("Client Surname");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            driver.FindElement(By.CssSelector("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(1) > section > fieldset:nth-child(2) > div:nth-child(3) > text-input > div > div.text-input__input-holder.text-input__input-holder--animated > input")).SendKeys(email);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            driver.FindElement(By.CssSelector("#content > div:nth-child(7) > div.ch-collapsible-container.parbase > div > div > div.content-hub-form-container__content.component-bordered.component-bordered--small > div > div > div > div > div.reference.parbase > div > div > div > form > div:nth-child(1) > section > fieldset:nth-child(2) > div.checkbox.parbase > checkbox-input > div > div > div:nth-child(1) > button")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[7]/div[2]/div/div/div[2]/div/div/div/div/div[1]/div/div/div/form/div[1]/section/fieldset[3]/button")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
        }

        private static void NavigateToUnderstandingCashInvestmentInterestRates()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("search-toggle")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("searchBarInput")).SendKeys("investments rates");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("searchBarButton")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.CssSelector("#content > div.search-results.component.ng-scope > div > div.row > div > div:nth-child(3) > secondary-cta > a")).Click();

            //Scroll to web element
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            ScrollToElement(".button-primary");

            driver.FindElement(By.CssSelector(".button-primary")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private static void NavigateToWebsite(string url)
        {
            driver = new ChromeDriver(chromeLocation);
            driver.Url = url;
            driver.Manage().Window.Maximize();

            string popupWindowHandle = CurrentWindowHandle(driver);
        }

        public static void ScrollToElement(string elementStr)
        {
            var element = driver.FindElement(By.CssSelector(elementStr));

            Actions actions = new Actions(driver);

            actions.MoveToElement(element);

            actions.Perform();
        }
        
        private static string CurrentWindowHandle(IWebDriver driver)
        {
            string popupWindowHandle = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(popupWindowHandle);
            return popupWindowHandle;
        }
    }
}
