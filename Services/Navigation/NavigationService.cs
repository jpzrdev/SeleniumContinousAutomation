using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using SeleniumContinousAutomation.PageObjects;
using SeleniumContinousAutomation.Services.Driver;
using SeleniumContinousAutomation.Services.RestApi;

namespace SeleniumContinousAutomation.Services.Navigation
{
    public class NavigationService
    {
        private readonly ILogger _logger;
        private readonly DriverMethods _driverMethods;
        private readonly RestApiService _api;

        public NavigationService(DriverMethods driverMethods, ILogger<NavigationService> logger, RestApiService api)
        {
            _logger = logger;
            _driverMethods = driverMethods;
            _api = api;
        }



        public void Navigation1()
        {
            //We can get data from an api to our TestModel.cs object, but because we dont have one this code is commented

            // var data = _api.GetTestModelData();


            var driver = _driverMethods.CreateDriver();

            try
            {
                _logger.LogInformation("Navigation 1 Start.");

                TestPage testPage = new TestPage(driver);
                testPage.SearchSomething("something");

                _logger.LogInformation("Navigation End.");
            }
            catch (Exception ex)
            {
                _logger.LogError("This log will save the Exception in ErrorLog. Error:{0}. InnerException:{1}." + ex.Message, ex.InnerException.Message);
            }

            driver.Quit();
        }

        public void Navigation2()
        {
            var driver = _driverMethods.CreateDriver();
            driver.Manage().Window.Maximize();

            driver.Quit();
        }





    }
}