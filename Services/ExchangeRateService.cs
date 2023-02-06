using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;


namespace PaperlessHW.Function{
    class ExchangeRateProvider : IExchangeRateProvider
    {
        
        public ExchangeRateProvider(){

        }
        
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }


        public async Task<string> GetMonthDictionaryAsync(string currency,string monthyear){
            var s=new Dictionary<int, string>();
            string message="";
            using (HttpClient externalAPICurrencyClient= new HttpClient())
            {
                //Big Assumtion as to the validy of the incoming data
                var year="20"+monthyear.Substring(0,2);
                var month=monthyear.Substring(2,2);
                
                HttpRequestMessage externalAPICurrencyRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.apilayer.com/fixer/timeseries?start_date={0}-{1}-01&end_date={0}-{1}-{4}&symbols={2}&base={3}", year,month,currency,"ILS",DateTime.DaysInMonth(int.Parse(year), int.Parse(month))));
                
                externalAPICurrencyClient.DefaultRequestHeaders.Add("apikey", "L3HlsMPwLBefbcXnrAZIYSAbW2OwVWLW");
                HttpResponseMessage response = await externalAPICurrencyClient.SendAsync(externalAPICurrencyRequest);
                var result = response.Content.ReadAsStringAsync().Result;
                
                
                var exchangeRates=JsonSerializer.Deserialize<ExchangeRates>(result);
                                
                var responeObject= new {Max=exchangeRates.maxValue,Min=exchangeRates.minValue,GRAPH = exchangeRates.ValuePerDates};
                
                message+=JsonSerializer.Serialize(responeObject);
              
    
            }

            return message;
        }

    }
}