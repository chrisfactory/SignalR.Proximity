using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Proximity services to the collection.
        /// </summary>
        /// <param name="source">The service collection.</param>
        /// <param name="configure">Configuration action.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddProximity(this IServiceCollection source, Action<IServiceProvider, IProximityBuilder> configure)
        {
            source.TryAddSingleton<IProximityBuilder, ProximityBuilder>();
            source.TryAddSingleton(p => p.GetRequiredService(configure).Build());
            source.AddSingleton(p => p.GetRequiredService<IProximityEndPointProvider>().Get());//Add default
            return source;
        }
    }
}
