using System;
using System.Collections.Generic;
using System.Net;
using RestEase;

namespace SeleniumContinousAutomation.Services.RestApi
{
    public class RestApiService
    {

        private readonly IRestApiService _api;

        public RestApiService()
        {
            _api = RestClient.For<IRestApiService>("http://test.test/");
        }

        public Model.TestModel GetTestModelData()
        {
            var message = string.Empty;

            var response = _api.GetTestModelData();

            response.Wait();

            if (response.Result.ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Result.GetContent();
                return result;
            }

            throw new Exception("Error");
        }



    }
}