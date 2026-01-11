using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SignalR.Proximity.Hosting
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Proximity hosting services to the collection.
        /// </summary>
        /// <param name="source">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddProximity(this IServiceCollection source)
        {
            source.TryAddTransient(typeof(IProximityHubBuilder<,>), typeof(ProximityHubBuilder<,>));
            return source;
        }
    }
}
