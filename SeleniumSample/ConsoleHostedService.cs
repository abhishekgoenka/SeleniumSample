using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumSample.Pages;
using SeleniumSample.Repository;
using SeleniumSample.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumSample
{
    internal sealed class ConsoleHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly VaccineDbContext _dbContext;
        private readonly LoginPage _loginPage;
        private readonly Dashboard _dashboard;
        private readonly Appointment _appointment;
        private readonly IOptions<SeleniumSettings> _options;

        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime, VaccineDbContext dbContext, LoginPage loginPage, Dashboard dashboard, Appointment appointment, IOptions<SeleniumSettings> options)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _dbContext = dbContext;
            _loginPage = loginPage;
            _dashboard = dashboard;
            _appointment = appointment;
            _options = options;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            _dbContext.Database.EnsureCreated();

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        _logger.LogInformation("Chrome web driver started...");
                        ChromeOptions option = new ChromeOptions();
                        option.AddArgument("--start-maximized");

                        using (IWebDriver driver = new ChromeDriver(option))
                        {
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            driver.Navigate().GoToUrl(_options.Value.URL);

                            // LoginPage
                            _loginPage.EnterMobileNumber(driver);
                            _loginPage.ClickGetOTP(driver);

                            // OTP user input
                            Console.Write("Enter OTP : ");
                            var otp = Console.ReadLine();

                            _loginPage.EnterOTP(driver, otp);
                            var loginTime = DateTime.Now;
                            _loginPage.ClickVerifyAndProceed(driver);

                            // show dashboard path
                            _dashboard.ClickSchedule(driver);

                            // search appointment
                            _appointment.ClicSearchByDistrict(driver);
                            _appointment.SelectState(driver);
                            _appointment.SelectDistrict(driver);

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

                                _appointment.ClickSearch(driver);
                                _appointment.Select18Plus(driver);

                                // sleep for 5s
                                Console.WriteLine("Sleeping for 5s");
                                //Thread.Sleep(5000);
                                await Task.Delay(5000);

                            } while (!_appointment.BookTheSlotIfFound(driver));


                            if (!sessionExpired)
                            {
                                _appointment.ClickTimeSlot(driver);

                                // todo: This doesn't work as expected
                                _appointment.ReadCaptcha(driver);

                                // wait unit vaccine is booked
                                Console.ReadKey();
                            }
                        }


                        // Simulate real work is being done
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unhandled exception!");
                    }
                    finally
                    {
                        // Stop the application once the work is done
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
