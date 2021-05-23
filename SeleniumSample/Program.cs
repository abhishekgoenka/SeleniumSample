using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Pages;
using System;

namespace SeleniumSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = BuildConfiguration();
            var url = Configuration.GetSection("url").Value;
            var mobile = Configuration.GetSection("mobile").Value;

            ChromeOptions option = new ChromeOptions();

            option.LeaveBrowserRunning = true;
            option.AddArgument("--start-maximized");

            using (IWebDriver driver = new ChromeDriver(option))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);

                // LoginPage
                LoginPage loginPage = new LoginPage(driver, Configuration);
                loginPage.EnterMobileNumber();
                loginPage.ClickGetOTP();

                // OTP user input
                Console.WriteLine("Enter OTP");
                var otp = Console.ReadLine();

                loginPage.EnterOTP(otp);
                loginPage.ClickVerifyAndProceed();

                // show dashboard path
                Dashboard dashboard = new Dashboard(driver, Configuration);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                dashboard.ClicSchedule();

                Appointment appointment = new Appointment(driver, Configuration);
                appointment.ClicSearchByDistrict();

                //driver.FindElement(By.Name("q")).SendKeys("cheese" + Keys.Enter);
                //wait.Until(webDriver => webDriver.FindElement(By.CssSelector("h3")).Displayed);
                //IWebElement firstResult = driver.FindElement(By.CssSelector("h3"));
                //Console.WriteLine(firstResult.GetAttribute("textContent"));

                Console.ReadKey();
            }



        }

        static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();
        }
    }
}
