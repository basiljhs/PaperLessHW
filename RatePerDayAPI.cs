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
//using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

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
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "currency", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Currency** parameter")]
        [OpenApiParameter(name: "monthyear", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **MonthYear** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route ="{currency}/{monthyear}")] HttpRequest req,string currency,string monthyear)
        {         
            
            string message=await _exchangeRateProvider.GetMonthDictionaryAsync(currency,monthyear);
           
            
            return (ActionResult)new OkObjectResult(message);

        }
    }
}

