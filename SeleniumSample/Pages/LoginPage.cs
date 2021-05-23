using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace SeleniumSample.Pages
{
    class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration configuration;

        public LoginPage(IWebDriver driver, IConfiguration Configuration)
        {
            this.driver = driver;
            configuration = Configuration;
        }

        public void EnterMobileNumber()
        {
            var mobile = configuration.GetSection("mobile").Value;
            driver.FindElement(By.TagName("input")).SendKeys(mobile);
        }

        public void ClickGetOTP()
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }

        public void EnterOTP(string otp)
        {
            driver.FindElement(By.TagName("ion-item")).SendKeys(otp);
        }

        public void ClickVerifyAndProceed()
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }
    }
}
