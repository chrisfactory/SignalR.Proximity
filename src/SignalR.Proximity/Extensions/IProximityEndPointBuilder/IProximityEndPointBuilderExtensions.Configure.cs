using Microsoft.Extensions.DependencyInjection;
using System;
namespace SignalR.Proximity
{
    public static partial class IProximityEndPointBuilderExtensions
    {
        public static IProximityEndPointBuilder UsePatternBase(this IProximityEndPointBuilder builder, string patternBase)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternBase = patternBase);

            return builder;
        }

        public static IProximityEndPointBuilder UsePatternPostfix(this IProximityEndPointBuilder builder, string patternPostfix)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternPostfix = patternPostfix);

            return builder;
        } 

        public static IProximityEndPointBuilder UsePatternMachineNamePostfix(this IProximityEndPointBuilder builder, bool patternMachineNamePostfix)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternMachineNamePostfix = patternMachineNamePostfix);

            return builder;
        }
         
    }        
}
