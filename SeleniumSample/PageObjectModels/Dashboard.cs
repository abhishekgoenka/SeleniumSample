using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Helpers;
using System;

namespace SeleniumSample.PageObjectModels
{
    public class Dashboard
    {
        private readonly IWebDriver driver;

        public Dashboard(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickSchedule()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //IWebElement scheduleLink = wait.Until(ExpectedConditions.ElementExists(By.LinkText("Schedule")));
            IWebElement scheduleLink = wait.Until(SeleniumHelpers.ElementExists(By.LinkText("Schedule")));
            scheduleLink.Click();
        }
    }
}
