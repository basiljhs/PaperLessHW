using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof (PaperlessHW.Function.Startup))]
namespace PaperlessHW.Function{
    
    public class Startup:FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder){
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IExchangeRateProvider>((s)=>{return new ExchangeRateProvider();});
            //builder.Services.AddSingleton<ILoggerProvider>(); 
        }
    }
}

