using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SignalR.Proximity
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProximity(this IServiceCollection source, Action<IServiceProvider, IProximityBuilder> configure)
        {
            source.TryAddSingleton<IProximityBuilder, ProximityBuilder>();
            source.TryAddSingleton(p => p.GetRequiredService(configure).Build());
            source.AddSingleton(p => p.GetRequiredService<IProximityEndPointProvider>().Get());//Add default
            return source;
        }
    }
}
