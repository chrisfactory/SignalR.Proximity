using System;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{TConfig}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        /// <summary>
        ///    Sets the Namespace provider instance that will be called for each HTTP request.
        /// </summary>
        /// <typeparam name="TConfig">
        ///    The type instance that inherits the abstraction from <see cref="SignalRProximityConfiguration"/>.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{TConfig}"/> to configure
        /// </param>
        /// <param name="configureNamespace"></param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithNamespaceProvider<TConfig>(this IConfigurationBuilder<TConfig> source, Action<IHubNamespaceProvider> configureNamespace)
          where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                configureNamespace(c.Current.HubNamespaceProvider);
            });
            return source;
        }

        /// <summary>
        ///     Sets the Namespace provider instance that will be called for each HTTP request.
        /// </summary>
        /// <typeparam name="TConfig">
        ///    The type instance that inherits the abstraction from <see cref="SignalRProximityConfiguration"/>.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{TConfig}"/> to configure
        /// </param>
        /// <param name="namespaceProvider"></param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithNamespaceProvider<TConfig>(this IConfigurationBuilder<TConfig> source, IHubNamespaceProvider namespaceProvider)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.HubNamespaceProvider = namespaceProvider ?? new DefaultHubNamespaceProvider();
            });
            return source;
        }

    }
}