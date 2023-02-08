using System.Collections.Generic;

namespace PaperlessHW.Function
{
    public class ExchangeRatesModel
    {
        public bool success{get;set;}
        public bool timeseries{get;set;}
        public string start_date{get;set;}
        public string end_date{get;set;}
        public string @base{get;set;}
        public Dictionary<string, Dictionary<string, decimal>> rates{get;set;}

    }
}

