using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtentions
    {

        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, string urlBase) => source.AddEndPointCore(name, null, b => b.UseUrlBase(urlBase));
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, Uri urlBase) => source.AddEndPointCore(name, null, b => b.UseUrlBase(urlBase));
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string urlBase) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, b => b.UseUrlBase(urlBase));
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, Uri urlBase) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, b => b.UseUrlBase(urlBase));
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, configure);
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(name, null, configure);
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, IConfiguration config, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, config, configure);
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, IConfiguration config, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(name, config, configure);

        private static IProximityBuilder AddEndPointCore(this IProximityBuilder source, string? name, IConfiguration? config, Action<IProximityEndPointBuilder>? configure = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            source.Services.TryAddTransient<IProximityEndPointBuilder, ProximityEndPointBuilder>();
            source.Services.AddSingleton(p => new ContainerKeyValue(name, p.GetRequiredService<IProximityEndPointBuilder, ProximityEndPointConfig>(config, configure).Build()));
            return source;
        }
    }
}
