using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using SignalR.Proximity.Hosting;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class IEndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern)
        {
            return builder.MapProximity<ProximityHub<TContract>, TContract>(pattern, null);
        }
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder, string pattern, Action<IProximityHubBuilder>? configureOptions)
        {
            return builder.MapProximity<ProximityHub<TContract>, TContract>(pattern, configureOptions);
        }
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string pattern)
          where TProximityHub : ProximityHub<TContract>
        {
            return builder.MapProximity<TProximityHub, TContract>(pattern, null);
        }
        public static IEndpointRouteBuilder MapProximity<TProximityHub, TContract>(this IEndpointRouteBuilder builder, string pattern, Action<IProximityHubBuilder>? configureOptions)
        where TProximityHub : ProximityHub<TContract>
        {
            var proximityBuilder = builder.ServiceProvider.GetRequiredService<IProximityHubBuilder<TProximityHub, TContract>>();
            proximityBuilder.UseEndpointRouteBuilder(builder);
            proximityBuilder.Services.Configure<ProximityConfigurationCore>(config => config.PatternBase = pattern);
            configureOptions?.Invoke(proximityBuilder);
            proximityBuilder.Build();

            return builder;
        }
    }
}
