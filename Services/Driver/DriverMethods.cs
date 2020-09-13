using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumContinousAutomation.Services.Driver
{
    public class DriverMethods
    {
        public IWebDriver CreateDriver()
        {
            var directory = Directory.GetCurrentDirectory();

            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var chromeOpt = new ChromeOptions() { UnhandledPromptBehavior = UnhandledPromptBehavior.Accept };

            return new ChromeDriver(directory + "\\drivers\\Chrome", chromeOpt);
        }
    }
}