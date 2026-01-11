using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using SignalR.Proximity.Hosting;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extensions for <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    public static partial class IEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps a Proximity hub.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(null, null);
        }
        /// <summary>
        /// Maps a Proximity hub with a pattern.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="pattern">The route pattern.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(pattern, null);
        }
        /// <summary>
        /// Maps a Proximity hub with configuration.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, Action<IProximityHubBuilder>? configureOptions)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(null, configureOptions);
        }
        /// <summary>
        /// Maps a Proximity hub with pattern and configuration.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="pattern">The route pattern.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern, Action<IProximityHubBuilder> configureOptions)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(pattern, configureOptions);
        }


        /// <summary>
        /// Maps a custom Proximity hub.
        /// </summary>
        /// <typeparam name="TProximityHub">The hub type.</typeparam>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder)
         where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(null, null);
        }
        /// <summary>
        /// Maps a custom Proximity hub with a pattern.
        /// </summary>
        /// <typeparam name="TProximityHub">The hub type.</typeparam>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="pattern">The route pattern.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string? pattern = null)
          where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(pattern, null);
        }
        /// <summary>
        /// Maps a custom Proximity hub with configuration.
        /// </summary>
        /// <typeparam name="TProximityHub">The hub type.</typeparam>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, Action<IProximityHubBuilder> configureOptions)
            where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(null, configureOptions);
        }
        /// <summary>
        /// Maps a custom Proximity hub with pattern and configuration.
        /// </summary>
        /// <typeparam name="TProximityHub">The hub type.</typeparam>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="builder">The route builder.</param>
        /// <param name="pattern">The route pattern.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string pattern, Action<IProximityHubBuilder> configureOptions)
             where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(pattern, configureOptions);
        }
        private static IEndpointRouteBuilder InnerMapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string? pattern, Action<IProximityHubBuilder>? configureOptions)
        where TProximityHub : ProximityHub<TContract>
        {
            var proximityBuilder = builder.ServiceProvider.GetRequiredService<IProximityHubBuilder<TProximityHub, TContract>>();
            proximityBuilder.UseEndpointRouteBuilder(builder);
            proximityBuilder.Services.Configure<ProximityConfigurationCore>(config =>
            {
                if (pattern != null && !string.IsNullOrEmpty(pattern))
                    config.PatternBase = pattern;

            });
            configureOptions?.Invoke(proximityBuilder);
            proximityBuilder.Build();

            return builder;
        }
    }
}
