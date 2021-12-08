using SignalR.Proximity.Notifier;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SignalR.Proximity.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///  Extension methods for <see cref="ISignalRProximityBuilder"/>.
    /// </summary>
    public static class ServiceCollectionRootConfigurationExtensions
    {
        /// <summary>
        ///     Adds SignalRProximityNotifier services to the specified <see cref="ISignalRProximityBuilder"/> to provide the instance of <see cref="NotifierProxyProvider"/>.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="ISignalRProximityBuilder"/> to configure
        /// </param>
        /// <param name="configBuilder">
        ///     The action used to configure the options.
        /// </param>
        public static void UseNotifier(this ISignalRProximityBuilder source, Action<IRootConfigurationBuilder<SignalRProximityNotifierConfiguration>> configBuilder)
        { 
            source.Services.TryAddSingleton<ISignalRProximityNotifierFactory, SignalRProximityNotifierFactory>();
            source.Services.TryAddTransient(typeof(INotifierBuilder<>), typeof(NotifierBuilder<>));
            var builder = new RootConfigurationBuilder<SignalRProximityNotifierConfiguration>(source.Services);
            configBuilder(builder);
        }
    }
}
 