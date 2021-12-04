using System; 
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Client;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{SignalRProximityClientConfiguration}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        private static TimeSpan[] DEFAULT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(5) };


        /// <summary>
        ///     Configure the <see cref="ClientProxy"/> to automatically restore the groups if connexion is restored. 
        /// </summary>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{SignalRProximityClientConfiguration}"/> to configure
        /// </param>
        /// <param name="autoRestore">
        ///     If true, defined restore the groups if connexion is restored
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{SignalRProximityClientConfiguration}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<SignalRProximityClientConfiguration> WithGroupsAutoRestored(this IConfigurationBuilder<SignalRProximityClientConfiguration> source, bool autoRestore = true)
        {
            source.Services.Configure<ConfigurationSelector<SignalRProximityClientConfiguration>>(c =>
            {
                ((SignalRProximityClientConfiguration)c.Current).AutoRestoredGroups = autoRestore;
            });
            return source;
        }

         
        /// <summary>
        ///     Configures the <see cref="Microsoft.AspNetCore.SignalR.Client.HubConnection"/> to automatically
        ///     attempt to reconnect if the connection is lost. The client will wait the default
        ///     0, 2, 10, 30 seconds and 5 minuts respectively before trying up to four reconnect attempts.
        /// </summary>
        /// <param name="source">The <see cref="IConfigurationBuilder{SignalRProximityClientConfiguration}"/> to configure</param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{SignalRProximityClientConfiguration}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<SignalRProximityClientConfiguration> WithAutomaticReconnect(this IConfigurationBuilder<SignalRProximityClientConfiguration> source)
        {
            source.Services.Configure<ConfigurationSelector<SignalRProximityClientConfiguration>>(c =>
            {
                c.Current.RetryPolicy = new DefaultRetryPolicy(DEFAULT_RETRY_DELAYS_IN_MILLISECONDS);
            });
            return source;
        }

    }
}
