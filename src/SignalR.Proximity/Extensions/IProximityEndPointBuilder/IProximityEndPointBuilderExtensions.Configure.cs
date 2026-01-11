using Microsoft.Extensions.DependencyInjection;
using System;
namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityEndPointBuilder"/> configuration.
    /// </summary>
    public static partial class IProximityEndPointBuilderExtensions
    {
        /// <summary>
        /// Configures the pattern base.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="patternBase">The pattern base string.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UsePatternBase(this IProximityEndPointBuilder builder, string patternBase)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternBase = patternBase);

            return builder;
        }

        /// <summary>
        /// Configures the pattern postfix.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="patternPostfix">The pattern postfix string.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UsePatternPostfix(this IProximityEndPointBuilder builder, string patternPostfix)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternPostfix = patternPostfix);

            return builder;
        }

        /// <summary>
        /// Configures whether to use machine name in pattern postfix.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="patternMachineNamePostfix">True to use machine name postfix.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UsePatternMachineNamePostfix(this IProximityEndPointBuilder builder, bool patternMachineNamePostfix)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.PatternMachineNamePostfix = patternMachineNamePostfix);

            return builder;
        }

    }
}
