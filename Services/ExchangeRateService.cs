using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace PaperlessHW.Function
{
    public class ExchangeRateProvider
    {
        private const string APIKey = "L3HlsMPwLBefbcXnrAZIYSAbW2OwVWLW";
        private const string ExternalApi = "https://api.apilayer.com/fixer/timeseries?";
        private const string QueryStringExternalApi = ExternalApi + "start_date={0}-{1}-01&end_date={0}-{1}-{2}&symbols={3}&base=ILS";

        public ExchangeRateProvider()
        {

        }

        private static Dictionary<string, decimal> GetValuesPerDatesFlat(Dictionary<string, Dictionary<string, decimal>> nestedDates)
        {
            return nestedDates.ToDictionary(a => a.Key, a => a.Value.First().Value);
        }

        public async Task<string> GetMonthDictionaryAsync(string currency, string monthyear)
        {
            var year = "20" + monthyear.Substring(0, 2);
            var month = monthyear.Substring(2, 2);
            var daysInMonth = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));

            var requestURL = string.Format(QueryStringExternalApi, year, month, daysInMonth, currency);
            var externalAPIClient = new HttpClient();
            var externalAPIRequest = new HttpRequestMessage(HttpMethod.Get, requestURL);
            externalAPIClient.DefaultRequestHeaders.Add("apikey", APIKey);

            string resultString = "";
            
            using (var response = await externalAPIClient.SendAsync(externalAPIRequest))
            {
                var result = await response.Content.ReadAsStringAsync();
                var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesModel>(result);

                var ValuesPerDatesFlatDict = GetValuesPerDatesFlat(exchangeRates.rates);

                var responeObject = new
                {
                    Max = ValuesPerDatesFlatDict.Values.Max(),
                    Min = ValuesPerDatesFlatDict.Values.Min(),
                    GRAPH = ValuesPerDatesFlatDict
                };

                resultString = JsonSerializer.Serialize(responeObject);
            }

            return resultString;
        }

    }
}