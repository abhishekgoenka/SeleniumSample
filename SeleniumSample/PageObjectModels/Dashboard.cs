using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Helpers;
using System;

namespace SeleniumSample.PageObjectModels
{
    public class Dashboard : Page
    {
        public Dashboard(IWebDriver driver) : base(driver)
        {
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
