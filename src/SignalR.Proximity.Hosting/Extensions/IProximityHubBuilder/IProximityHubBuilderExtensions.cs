using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection; 

namespace SignalR.Proximity.Hosting
{
    internal static class IProximityHubBuilderExtensions
    {
        public static IProximityHubBuilder<TProximityHub, TContract> UseEndpointRouteBuilder<TProximityHub, TContract>(this IProximityHubBuilder<TProximityHub, TContract> builder, IEndpointRouteBuilder endPointBuilder)
             where TProximityHub : ProximityHub<TContract>
        {
            builder.Services.AddSingleton(endPointBuilder);
            return builder;
        }
    }
}
