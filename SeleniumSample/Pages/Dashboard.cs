using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumSample.Pages
{
    public class Dashboard
    {
        private readonly IConfiguration configuration;

        public Dashboard(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public void ClickSchedule(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("Schedule")));
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.LinkText("Schedule")).Click();
        }
    }
}
