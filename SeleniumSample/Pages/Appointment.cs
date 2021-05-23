using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace SeleniumSample.Pages
{
    class Appointment
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration configuration;

        public Appointment(IWebDriver driver, IConfiguration Configuration)
        {
            this.driver = driver;
            configuration = Configuration;
        }

        public void ClicSearchByDistrict()
        {
            driver.FindElement(By.ClassName("custom-checkbox")).Click();
        }
    }
}
