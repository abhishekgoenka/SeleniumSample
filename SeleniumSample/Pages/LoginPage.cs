using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using SeleniumSample.Settings;

namespace SeleniumSample.Pages
{
    class LoginPage
    {
        private readonly IOptions<SeleniumSettings> options;

        public LoginPage(IOptions<SeleniumSettings> options)
        {
            this.options = options;
        }

        public void EnterMobileNumber(IWebDriver driver)
        {
            driver.FindElement(By.TagName("input")).SendKeys(options.Value.mobile);
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
