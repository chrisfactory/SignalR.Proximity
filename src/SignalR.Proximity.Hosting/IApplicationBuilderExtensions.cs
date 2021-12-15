using SignalR.Proximity.Hosting;

namespace Microsoft.AspNetCore.Routing
{
    public static partial class IApplicationBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProximity<TContract>(this IEndpointRouteBuilder builder) 
        {
            return builder;
        }
        public static IEndpointRouteBuilder MapProximity<TProximityHub,TContract>(this IEndpointRouteBuilder builder)
            where TProximityHub : ProximityHub<TContract>
        {
            return builder;
        }
    }
}
