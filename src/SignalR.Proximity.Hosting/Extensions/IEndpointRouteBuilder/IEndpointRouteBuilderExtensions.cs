using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
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
        /// <param name="config">The configuration.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, IConfiguration config)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(config, null);
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
        /// <param name="config">The configuration.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, IConfiguration config, Action<IProximityHubBuilder> configureOptions)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(config, configureOptions);
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
        /// <param name="config">The configuration.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, IConfiguration config)
          where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(config, null);
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
        /// <param name="config">The configuration.</param>
        /// <param name="configureOptions">The configuration action.</param>
        /// <returns>The route builder.</returns>
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, IConfiguration config, Action<IProximityHubBuilder> configureOptions)
             where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(config, configureOptions);
        }
        private static IEndpointRouteBuilder InnerMapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, IConfiguration? config, Action<IProximityHubBuilder>? configureOptions)
        where TProximityHub : ProximityHub<TContract>
        {
            var proximityBuilder = builder.ServiceProvider.GetRequiredService<IProximityHubBuilder<TProximityHub, TContract>>();
            proximityBuilder.UseEndpointRouteBuilder(builder);
            if (config != null) 
                proximityBuilder.Services.Configure<ProximityConfigurationCore>(config); 
            else 
                proximityBuilder.Services.Configure<ProximityConfigurationCore>(options => { });

            configureOptions?.Invoke(proximityBuilder);
            proximityBuilder.Build();

            return builder;
        }
    }
}
