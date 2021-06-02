using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Helpers;
using System;

namespace SeleniumSample.Pages
{
    public class Dashboard
    {
        public Dashboard()
        {

        }

        public void ClickSchedule(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //IWebElement scheduleLink = wait.Until(ExpectedConditions.ElementExists(By.LinkText("Schedule")));
            IWebElement scheduleLink = wait.Until(SeleniumHelpers.ElementExists(By.LinkText("Schedule")));
            scheduleLink.Click();
        }
    }
}
