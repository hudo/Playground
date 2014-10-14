using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var chromeDriver = new ChromeDriver("drivers");
            
            chromeDriver.Navigate().GoToUrl("http://www.gsmarena.com");

            var driverWait = new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 10));

            driverWait.Until(ExpectedConditions.ElementExists(By.Id("brandmenu")));

            var brands = chromeDriver.FindElementsByCssSelector("div#brandmenu a");

            Console.WriteLine("Brands: " + brands.Count);

            foreach (var brand in brands)
            {
                Console.WriteLine(brand.Text);
            }

            Console.ReadKey();

            chromeDriver.Close();
            chromeDriver.Dispose();
        }
    }
}
