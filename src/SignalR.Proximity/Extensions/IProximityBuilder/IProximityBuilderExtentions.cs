using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityBuilder"/>.
    /// </summary>
    public static partial class IProximityBuilderExtentions
    {
        /// <summary>
        /// Adds an endpoint.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="name">The endpoint name.</param>
        /// <param name="urlBase">The base URL.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, string urlBase) => source.AddEndPointCore(name, null, b => b.UseUrlBase(urlBase));
        /// <summary>
        /// Adds an endpoint.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="name">The endpoint name.</param>
        /// <param name="urlBase">The base URL.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, Uri urlBase) => source.AddEndPointCore(name, null, b => b.UseUrlBase(urlBase));
        /// <summary>
        /// Adds a default endpoint.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="urlBase">The base URL.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string urlBase) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, b => b.UseUrlBase(urlBase));
        /// <summary>
        /// Adds a default endpoint.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="urlBase">The base URL.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, Uri urlBase) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, b => b.UseUrlBase(urlBase));
        /// <summary>
        /// Adds a default endpoint with configuration.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, null, configure);
        /// <summary>
        /// Adds an endpoint with configuration.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="name">The endpoint name.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(name, null, configure);
        /// <summary>
        /// Adds a default endpoint with configuration.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, IConfiguration config, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(ContainerKeyValue.DefaultContainerName, config, configure);
        /// <summary>
        /// Adds an endpoint with configuration.
        /// </summary>
        /// <param name="source">The builder source.</param>
        /// <param name="name">The endpoint name.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="configure">The configuration action.</param>
        /// <returns>The builder.</returns>
        public static IProximityBuilder AddEndPoint(this IProximityBuilder source, string name, IConfiguration config, Action<IProximityEndPointBuilder>? configure = null) => source.AddEndPointCore(name, config, configure);

        private static IProximityBuilder AddEndPointCore(this IProximityBuilder source, string name, IConfiguration? config, Action<IProximityEndPointBuilder>? configure = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            source.Services.TryAddTransient<IProximityEndPointBuilder, ProximityEndPointBuilder>();
            source.Services.AddSingleton(p => new ContainerKeyValue(name, p.GetRequiredService<IProximityEndPointBuilder, ProximityEndPointConfig>(config, configure).Build()));
            return source;
        }
    }
}
