using System;
using System.Threading.Tasks;

namespace PaperlessHW.Function{
    public interface IExchangeRateProvider{
        Task<string> GetMonthDictionaryAsync(string currency,string monthyear);

    }
}