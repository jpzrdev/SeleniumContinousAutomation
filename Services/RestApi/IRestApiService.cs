using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace SeleniumContinousAutomation.Services.RestApi
{
    public interface IRestApiService
    {

        [AllowAnyStatusCode]

        [Get("/testendpoint/")]
        Task<Response<Model.TestModel>> GetTestModelData();

    }
}