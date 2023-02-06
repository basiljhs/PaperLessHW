using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
//using Microsoft.Extensions.Logging;

namespace PaperlessHW.Function
{
    public class RatePerDayAPI
    {
        //private readonly ILogger<RatePerDayAPI> _logger;
        private readonly HttpClient _httpClient;
        private readonly IExchangeRateProvider _exchangeRateProvider;

        

        public RatePerDayAPI(IHttpClientFactory httpClientFactory, IExchangeRateProvider exchangeRateProvider)//, ILogger<RatePerDayAPI> log)
        {
            this._httpClient=httpClientFactory.CreateClient();
            //this._logger = log;
            this._exchangeRateProvider=exchangeRateProvider;
            
        }

        [FunctionName("RatePerDayAPI")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route ="{currency}/{monthyear}")] HttpRequest req,string currency,string monthyear)
        {         
            
            string message=await _exchangeRateProvider.GetMonthDictionaryAsync(currency,monthyear);
           
            
            return (ActionResult)new OkObjectResult(message);

        }
    }
}

