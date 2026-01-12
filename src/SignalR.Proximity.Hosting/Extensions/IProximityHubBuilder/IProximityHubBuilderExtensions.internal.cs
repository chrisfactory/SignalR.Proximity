using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Hosting
{
    public static partial class IProximityHubBuilderExtensions
    {
        internal static IProximityHubBuilder UseEndpointRouteBuilder(this IProximityHubBuilder builder, IEndpointRouteBuilder endPointBuilder)
        {
            builder.Services.AddSingleton(endPointBuilder);
            return builder;
        }
    }
}
