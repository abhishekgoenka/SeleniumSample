using OpenQA.Selenium;
using SeleniumSample.Settings;

namespace SeleniumSample.PageObjectModels
{
    class LoginPage
    {
        private readonly SeleniumSettings options;
        private readonly IWebDriver driver;

        public LoginPage(SeleniumSettings options, IWebDriver driver)
        {
            this.options = options;
            this.driver = driver;
        }


        public void EnterMobileNumber() => driver.FindElement(By.TagName("input")).SendKeys(options.mobile);

        public void ClickGetOTP() => driver.FindElement(By.TagName("ion-button")).Click();

        public void EnterOTP(string otp) => driver.FindElement(By.TagName("ion-item")).SendKeys(otp);

        public Dashboard ClickVerifyAndProceed()
        {
            driver.FindElement(By.TagName("ion-button")).Click();
            return new Dashboard(driver);
        }
    }
}
