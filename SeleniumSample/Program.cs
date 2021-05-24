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
                var loginTime = DateTime.Now;
                loginPage.ClickVerifyAndProceed();

                // show dashboard path
                Dashboard dashboard = new Dashboard(driver, Configuration);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                dashboard.ClickSchedule();

                // search appointment
                Appointment appointment = new Appointment(driver, Configuration);
                appointment.ClicSearchByDistrict();
                appointment.SelectState();
                appointment.SelectDistrict();

                // look until we find that vaccine
                bool sessionExpired = false;
                do
                {
                    var sessionTime = DateTime.Now.Subtract(loginTime);
                    Console.WriteLine($"Session duration {sessionTime}");
                    if (sessionTime.TotalSeconds > 900)
                    {
                        Console.WriteLine("Session expired");
                        sessionExpired = true;
                        break;
                    }

                    appointment.ClickSearch();
                    appointment.Select18Plus();

                    // sleep for 5s
                    Console.WriteLine("Sleeping for 5s");
                    System.Threading.Thread.Sleep(5000);

                } while (!appointment.BookTheSlotIfFound());


                if (!sessionExpired)
                {
                    // wait unit vaccine is booked
                    Console.ReadKey();
                }

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
