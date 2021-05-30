using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using SeleniumSample.Models;
using SeleniumSample.Repository;
using SeleniumSample.Settings;
using System;
using System.Drawing;
using Tesseract;

namespace SeleniumSample.Pages
{
    class Appointment
    {
        private readonly IOptions<SeleniumSettings> options;
        private readonly VaccineDbContext context;

        public Appointment(IOptions<SeleniumSettings> options, VaccineDbContext context)
        {
            this.options = options;
            this.context = context;
        }

        public void ClicSearchByDistrict(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.ClassName("custom-checkbox")).Click();
        }

        public void SelectState(IWebDriver driver)
        {
            //create select element object 
            driver.FindElement(By.Id("mat-select-0")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id(options.Value.state)).Click();
        }

        public void SelectDistrict(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("mat-select-2")).Click();

            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id(options.Value.district)).Click();
        }

        public void ClickSearch(IWebDriver driver)
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }

        public void Select18Plus(IWebDriver driver)
        {
            var elements = driver.FindElements(By.TagName("label"));

            foreach (var e in elements)
            {
                if (e.Text == "Age 18+")
                {
                    e.Click();
                }
                else if (options.Value.covaxin && e.Text == "Covaxin")
                {
                    e.Click();
                }
                else if (options.Value.covishield && e.Text == "Covishield")
                {
                    e.Click();
                }

            }
        }

        public bool BookTheSlotIfFound(IWebDriver driver)
        {
            var elements = driver.FindElements(By.TagName("a"));

            // remove where text is empty


            foreach (var e in elements)
            {
                if (e.Text != string.Empty)
                {
                    // does text is number
                    int qty;
                    if (Int32.TryParse(e.Text, out qty))
                    {
                        Console.WriteLine($"{e.Text} vaccine found");
                        e.Click();

                        // add record in DB
                        string vaccine = "abc";
                        string center = "cen";
                        AddRecordInDB(vaccine, center, qty);
                        Console.WriteLine("Record added to DB");

                        return true;
                    }
                }
            }
            return false;
        }

        public void ClickTimeSlot(IWebDriver driver)
        {
            driver.FindElement(By.TagName("ion-button")).Click();
        }

        public void ReadCaptcha(IWebDriver driver)
        {
            IWebElement capta = driver.FindElement(By.Id("captchaImage"));
            string fileName = TakeScreenshot(capta, driver);


            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(fileName))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                        Console.WriteLine("Text (GetText): \r\n{0}", text);
                        Console.WriteLine("Text (iterator):");
                    }
                }


            }

        }


        private string TakeScreenshot(IWebElement element, IWebDriver driver)
        {
            if (!System.IO.Directory.Exists("screenshots"))
            {
                System.IO.Directory.CreateDirectory("screenshots");
            }

            string fileName = "screenshots\\" + DateTime.Now.ToString("yyyyy-MM-dd HH-mm-ss") + ".tif";
            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            Bitmap screenshot = new Bitmap(new System.IO.MemoryStream(byteArray));
            Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
            return fileName
        ;
        }

        public void AddRecordInDB(string vaccine, string center, int qty)
        {
            AvailableSlot availableSlot = new AvailableSlot();
            availableSlot.Vaccine = vaccine;
            availableSlot.Center = center;
            availableSlot.Qty = qty;
            availableSlot.DTTM = DateTime.Now;

            context.AvailableSlot.Add(availableSlot);
            context.SaveChanges();
        }
    }
}
