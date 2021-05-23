using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

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

        public void ClicSchedule()
        {
            //WebDriverWait

            while (driver.FindElement(By.LinkText("Schedule")) == null)
            {


            }
            driver.FindElement(By.LinkText("Schedule")).Click();
        }
    }
}
