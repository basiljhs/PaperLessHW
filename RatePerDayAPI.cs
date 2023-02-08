using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace PaperlessHW.Function
{
    public class RatePerDayAPI
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeRateProvider _exchangeRateProvider;
        public RatePerDayAPI(IHttpClientFactory httpClientFactory, ExchangeRateProvider exchangeRateProvider)
        {
            this._httpClient = httpClientFactory.CreateClient();
            this._exchangeRateProvider = exchangeRateProvider;
        }
        [FunctionName("RatePerDayAPI")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post",
            Route ="{currency}/{monthyear}")] HttpRequest req, string currency, string monthyear)
        {
            var message = await _exchangeRateProvider.GetMonthDictionaryAsync(currency, monthyear);
            return (ActionResult)new OkObjectResult(message);
        }
    }
}

