using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection; 
using SignalR.Proximity.Core;
using SignalR.Proximity.Hosting;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class IEndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(null, null);
        }
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(pattern, null);
        }
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, Action<IProximityHubBuilder>? configureOptions)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(null, configureOptions);
        }
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern, Action<IProximityHubBuilder> configureOptions)
        {
            return builder.InnerMapProximity<ProximityHub<TContract>, TContract>(pattern, configureOptions);
        }


        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder)
         where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(null, null);
        }
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string pattern = null)
          where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(pattern, null);
        }
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, Action<IProximityHubBuilder> configureOptions)
            where TProximityHub : ProximityHub<TContract>
        {
            return builder.InnerMapProximity<TProximityHub, TContract>(null, configureOptions);
        }
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
