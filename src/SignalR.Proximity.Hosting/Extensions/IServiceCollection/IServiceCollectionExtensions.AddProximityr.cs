using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SignalR.Proximity.Hosting
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProximity(this IServiceCollection source)
        {
            source.TryAddTransient(typeof(IProximityHubBuilder<,>), typeof(ProximityHubBuilder<,>));
            return source;
        }
    }
}
