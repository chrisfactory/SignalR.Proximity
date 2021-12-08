using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SignalR.Proximity;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {

        public static IServiceCollection AddProximity(this IServiceCollection source, Action<IProximityBuilder> configure = null)
        {
            source.AddProximity(ProximityProvider.DefaultContainerName, null, configure);
            return source;
        }
        public static IServiceCollection AddProximity(this IServiceCollection source, string name, Action<IProximityBuilder> configure = null)
        {
            source.AddProximity(name, null, configure);
            return source;
        }

        public static IServiceCollection AddProximity(this IServiceCollection source, IConfiguration config, Action<IProximityBuilder> configure = null)
        {
            source.AddProximity(ProximityProvider.DefaultContainerName, config, configure);
            return source;
        }
        public static IServiceCollection AddProximity(this IServiceCollection source, string name, IConfiguration config, Action<IProximityBuilder> configure = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            source.TryAddSingleton<IProximityProvider, ProximityProvider>();
            source.TryAddSingleton<IProximityContainer, ProximityContainer>();
            source.TryAddTransient<IProximityBuilder, ProximityBuilder>();
            source.AddSingleton(p =>
            {
                var builder = p.GetRequiredService<IProximityBuilder>((c) =>
                {
                    if (config!=null)
                        c.Services.Configure<ProximityConfig>(config);
                    configure?.Invoke(c);
                });
                builder.Build();
                return new ContainerKeyValue(name, builder);
            });
            return source;
        }
    }
}
