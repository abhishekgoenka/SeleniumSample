using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;

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
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.ClassName("custom-checkbox")).Click();
        }

        public void SelectState()
        {
            var state = configuration.GetSection("state").Value;

            //create select element object 
            driver.FindElement(By.Id("mat-select-0")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id(state)).Click();
        }

        public void SelectDistrict()
        {
            var district = configuration.GetSection("district").Value;

            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("mat-select-2")).Click();
            
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id(district)).Click();
        }

        public void ClickSearch()
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }

        public void Select18Plus()
        {
            var covaxin = configuration.GetSection("covaxin").Value;
            var covishield = configuration.GetSection("covishield").Value;

            var elements = driver.FindElements(By.TagName("label"));

            foreach (var e in elements)
            {
                if (e.Text == "Age 18+")
                {
                    e.Click();
                }
                else if (covaxin == "1" && e.Text == "Covaxin")
                {
                    e.Click();
                }
                else if (covishield == "1" && e.Text == "Covishield")
                {
                    e.Click();
                }

            }
        }

        public bool BookTheSlotIfFound()
        {
            var elements = driver.FindElements(By.LinkText("Available"));
            if (elements.Count > 0)
            {
                // vaccine found. 
                Console.WriteLine("We found vaccine");
                elements[0].Click();
                return true;
            }
            return false;
        }
    }
}
