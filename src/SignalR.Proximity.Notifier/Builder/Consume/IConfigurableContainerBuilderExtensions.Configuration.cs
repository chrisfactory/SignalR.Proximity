using System; 
using SignalR.Proximity.Notifier; 

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{NotifierProxyProvider,TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {

        public static IScopeBuilder<NotifierProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, string name)
        {
            return source.UseConfigNameCore<SignalRProximityNotifierConfiguration, NotifierProxyProvider, TContract>(name);
        }

        public static IScopeBuilder<NotifierProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, string name, Action<IConfigurationBuilder<SignalRProximityNotifierConfiguration>> reConFigue)
        {
            source.UseConfigNameCore<SignalRProximityNotifierConfiguration, NotifierProxyProvider, TContract>(name);
            source.UseConfiguration(reConFigue);
            return source;
        }

        public static IScopeBuilder<NotifierProxyProvider, TContract> UseConfiguration<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, Action<IConfigurationBuilder<SignalRProximityNotifierConfiguration>> builderConfig)
        {
            var builder = new SignalRProximityConfigurationBuilder<SignalRProximityNotifierConfiguration>(source);
            builderConfig(builder);
            return source;
        }

    }
}
