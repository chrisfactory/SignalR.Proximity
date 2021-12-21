using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity.Hosting
{
    public static partial class IProximityHubBuilderExtensions
    {
        public static IProximityHubBuilder UsePatternProvider<TPatternProvider>(this IProximityHubBuilder builder)
            where TPatternProvider : class, IPatternProvider
        {
            builder.Services.AddSingleton<IPatternProvider, TPatternProvider>();
            return builder;
        }
        public static IProximityHubBuilder UsePatternProvider(this IProximityHubBuilder builder, Func<ProximityConfigurationCore, IContractDescriptor, string> actionPattern)
        {
            builder.Services.AddSingleton(actionPattern);
            builder.Services.AddSingleton<IPatternProvider, ActionPatternProvider>();
            return builder;
        }

    }
}
