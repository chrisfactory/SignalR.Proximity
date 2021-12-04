using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SignalR.Proximity.Common;
using SignalR.Proximity.Client;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///  Extension methods for <see cref="ISignalRProximityBuilder"/>.
    /// </summary>
    public static class IRootConfigurationBuilderExtensions
    {

        /// <summary>
        ///     Adds SignalRProximityClient services to the specified <see cref="ISignalRProximityBuilder"/> to provide the instance of <see cref="ClientProxy"/>.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="ISignalRProximityBuilder"/> to configure
        /// </param>
        /// <param name="configBuilder">
        ///     The action used to configure the options.
        /// </param>
        public static void UseClient(this ISignalRProximityBuilder source, Action<IRootConfigurationBuilder<SignalRProximityClientConfiguration>> configBuilder)
        { 
            source.Services.TryAddSingleton<ISignalRProximityClientFactory, SignalRProximityClientFactory>();
            source.Services.TryAddTransient(typeof(IClientBuilder<>), typeof(ClientBuilder<>));
            var builder = new RootConfigurationBuilder<SignalRProximityClientConfiguration>(source.Services);
            configBuilder(builder);
        }
    }
}
