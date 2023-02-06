using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PaperlessHW.Function{

    using System.Linq;
    public class ExchangeRates
    {
        public bool success { get; set; }
        public bool timeseries { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string @base { get; set; }
        //private Dictionary<string, object> _rates;
        
        public Dictionary<string, decimal> ValuePerDates;
        public Dictionary<string, Dictionary<string,decimal>> _rates;
        public Dictionary<string, Dictionary<string,decimal>> rates{
            get
            {
                return _rates;
            }
         
            set
            {
                _rates=value;
                ValuePerDates=new Dictionary<string, decimal>();
                minValue=int.MaxValue;
                maxValue=0;

                value.ToList().ForEach(a=>AddValue(a.Key,a.Value.First().Value));
            }
           
        }

        private void AddValue(string date,decimal value){
            ValuePerDates.Add(date,value);
            if(value>maxValue)
                maxValue=value;
            if(value<minValue)
                minValue=value;
        }

        public decimal minValue{
            get;
            set;
        }
        
        public decimal maxValue{
            get;
            set;
        }


    }
}

