using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityEndPointBuilderExtensions
    {
        public static IProximityEndPointBuilder UsePatternProvider<TPatternProvider>(this IProximityEndPointBuilder builder)
            where TPatternProvider : class, IPatternProvider
        {
            builder.Services.AddSingleton<IPatternProvider, TPatternProvider>();
            return builder;
        }
        public static IProximityEndPointBuilder UsePatternProvider(this IProximityEndPointBuilder builder, Func<ProximityConfigurationCore, IContractDescriptor, string> actionPattern)
        {
            builder.Services.AddSingleton(actionPattern);
            builder.Services.AddSingleton<IPatternProvider, ActionPatternProvider>();
            return builder;
        }
    }
}
