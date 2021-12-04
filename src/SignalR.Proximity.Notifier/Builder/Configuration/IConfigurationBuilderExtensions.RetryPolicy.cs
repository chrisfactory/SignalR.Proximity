using System; 
using Microsoft.Extensions.DependencyInjection; 
using SignalR.Proximity.Common;
using SignalR.Proximity.Notifier;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{SignalRProximityNotifierConfiguration}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        private static TimeSpan[] DEFAULT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromSeconds(2) };

        /// <summary>
        ///     Configures the <see cref="Microsoft.AspNetCore.SignalR.Client.HubConnection"/> to automatically
        ///     attempt to reconnect if the connection is lost. The client will wait the default
        ///     0, 2 seconds respectively before trying up to four reconnect attempts.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{SignalRProximityNotifierConfiguration}"/> to configure.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{SignalRProximityNotifierConfiguration}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<SignalRProximityNotifierConfiguration> WithAutomaticReconnect(this IConfigurationBuilder<SignalRProximityNotifierConfiguration> source)
        {
            source.Services.Configure<ConfigurationSelector<SignalRProximityNotifierConfiguration>>(c =>
            {
                c.Current.RetryPolicy = new DefaultRetryPolicy(DEFAULT_RETRY_DELAYS_IN_MILLISECONDS);
            });
            return source;
        } 
    }
}
