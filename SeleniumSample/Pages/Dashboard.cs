using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumSample.Pages
{
    public class Dashboard
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration configuration;

        public Dashboard(IWebDriver driver, IConfiguration Configuration)
        {
            this.driver = driver;
            configuration = Configuration;
        }

        public void ClickSchedule()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("Schedule")));
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.LinkText("Schedule")).Click();
        }
    }
}
