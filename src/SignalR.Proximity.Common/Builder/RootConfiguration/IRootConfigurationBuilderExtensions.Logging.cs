using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; 
using System; 

namespace SignalR.Proximity.Common
{
    public static  partial class IRootConfigurationBuilderExtensions
    { 

        public static IRootConfigurationBuilder<TConfig> AddLogging<TConfig>(this IRootConfigurationBuilder<TConfig> source, Action<ILoggingBuilder> configure)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.AddLogging(configure);
            return source;
        }
         
    }
}
