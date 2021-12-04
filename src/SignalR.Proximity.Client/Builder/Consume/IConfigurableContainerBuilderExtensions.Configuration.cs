using System;
using SignalR.Proximity.Client;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {

        /// <summary>
        ///     Choose global configuration by name to configure the <see cref="ClientProxy"/> system..
        /// </summary>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="name">
        ///     The name of the options instance.
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="IScopeBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IScopeBuilder<ClientProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, string name)
        {
            return source.UseConfigNameCore<SignalRProximityClientConfiguration, ClientProxyProvider, TContract>(name);
        }

        /// <summary>
        ///  Choose global configuration by name and override thes options to configure the <see cref="ClientProxy"/> system.
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="name">
        ///     The name of the options instance.
        /// </param>
        /// <param name="reConFigue">
        ///     The action used to configure the options.
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="IScopeBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IScopeBuilder<ClientProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, string name, Action<IConfigurationBuilder<SignalRProximityClientConfiguration>> reConFigue)
        {
            source.UseConfigNameCore<SignalRProximityClientConfiguration, ClientProxyProvider, TContract>(name);
            source.UseConfiguration(reConFigue);
            return source;
        }

        /// <summary>
        ///     Define the options to configure the <see cref="ClientProxy"/> system.
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="builderConfig">
        ///     The action used to configure the options.
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="IScopeBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IScopeBuilder<ClientProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, Action<IConfigurationBuilder<SignalRProximityClientConfiguration>> builderConfig)
        {
            var builder = new SignalRProximityConfigurationBuilder<SignalRProximityClientConfiguration>(source);
            builderConfig(builder);
            return source;
        }

    }
}
 