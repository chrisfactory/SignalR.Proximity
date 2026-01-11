using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity.Hosting
{
    public static partial class IProximityHubBuilderExtensions
    {
        /// <summary>
        /// Uses a custom pattern provider type.
        /// </summary>
        /// <typeparam name="TPatternProvider">The pattern provider type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IProximityHubBuilder UsePatternProvider<TPatternProvider>(this IProximityHubBuilder builder)
            where TPatternProvider : class, IPatternProvider
        {
            builder.Services.AddSingleton<IPatternProvider, TPatternProvider>();
            return builder;
        }
        /// <summary>
        /// Uses a custom pattern provider action.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionPattern">The action to generate the pattern.</param>
        /// <returns>The builder.</returns>
        public static IProximityHubBuilder UsePatternProvider(this IProximityHubBuilder builder, Func<ProximityConfigurationCore, IContractDescriptor, string> actionPattern)
        {
            builder.Services.AddSingleton(actionPattern);
            builder.Services.AddSingleton<IPatternProvider, ActionPatternProvider>();
            return builder;
        }

    }
}
