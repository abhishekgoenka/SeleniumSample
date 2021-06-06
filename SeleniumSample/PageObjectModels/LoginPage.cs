using OpenQA.Selenium;
using SeleniumSample.Settings;

namespace SeleniumSample.PageObjectModels
{
    class LoginPage : Page
    {
        private readonly SeleniumSettings options;

        public LoginPage(SeleniumSettings options, IWebDriver driver) : base(driver)
        {
            this.options = options;
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
