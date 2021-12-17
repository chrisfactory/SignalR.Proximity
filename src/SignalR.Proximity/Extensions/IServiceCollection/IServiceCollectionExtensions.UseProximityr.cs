using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SignalR.Proximity
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection UseProximity(this IServiceCollection source, Action<IProximityBuilder> configure)
        {
            source.TryAddSingleton<IProximityBuilder, ProximityBuilder>();
            source.TryAddSingleton(p => p.GetRequiredService(configure).Build());
            source.AddSingleton(p => p.GetRequiredService<IProximityEndPointProvider>().Get());//Add default
            return source;
        }
    }
}
