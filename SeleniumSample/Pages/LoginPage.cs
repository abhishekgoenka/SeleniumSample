using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace SeleniumSample.Pages
{
    class LoginPage
    {
        private readonly IConfiguration configuration;

        public LoginPage(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public void EnterMobileNumber(IWebDriver driver)
        {
            var mobile = configuration.GetSection("SeleniumConfig:mobile").Value;
            driver.FindElement(By.TagName("input")).SendKeys(mobile);
        }

        public void ClickGetOTP(IWebDriver driver)
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }

        public void EnterOTP(IWebDriver driver, string otp)
        {
            driver.FindElement(By.TagName("ion-item")).SendKeys(otp);
        }

        public void ClickVerifyAndProceed(IWebDriver driver)
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }
    }
}
