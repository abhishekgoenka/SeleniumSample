using OpenQA.Selenium;

namespace SeleniumSample.PageObjectModels
{
    public abstract class Page
    {
        protected readonly IWebDriver driver;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
