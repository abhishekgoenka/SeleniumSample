using OpenQA.Selenium;
using System;

namespace SeleniumSample.Helpers
{
    sealed class SeleniumHelpers
    {
        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a
        /// page. This does not necessarily mean that the element is visible.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located.</returns>
        public static Func<IWebDriver, IWebElement> ElementExists(By locator)
        {
            return (driver) =>
            {
                try
                {
                    return driver.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            };
        }
    }
}
