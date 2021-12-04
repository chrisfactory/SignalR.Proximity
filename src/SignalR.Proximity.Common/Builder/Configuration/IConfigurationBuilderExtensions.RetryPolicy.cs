using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{TConfig}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        /// <summary> 
        ///     Configures the <see cref="Microsoft.AspNetCore.SignalR.Client.HubConnection"/> 
        ///     to automatically attempt to reconnect if the connection is lost.  
        /// </summary>
        /// <typeparam name="TConfig">The <see cref="SignalRProximityConfiguration"/> type to be configured</typeparam>
        /// <param name="source">The <see cref="IConfigurationBuilder{TConfig}"/> to configure</param>
        /// <param name="reconnectDelays">  
        ///     An array containing the delays before trying each reconnect attempt. The length
        ///     of the array represents how many failed reconnect attempts it takes before the
        ///     client will stop attempting to reconnect.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithAutomaticReconnect<TConfig>(this IConfigurationBuilder<TConfig> source, TimeSpan[] reconnectDelays)
            where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.RetryPolicy = new DefaultRetryPolicy(reconnectDelays);
            });
             
            return source;
        }

        /// <summary>
        ///     Configures the <see cref="Microsoft.AspNetCore.SignalR.Client.HubConnection"/> to automatically
        ///     attempt to reconnect if the connection is lost.
        /// </summary>
        /// <typeparam name="TConfig">The <see cref="SignalRProximityConfiguration"/> type to be configured</typeparam>
        /// <param name="source">The <see cref="IConfigurationBuilder{TConfig}"/> to configure</param>
        /// <param name="retryPolicy">  
        ///     An <see cref="IRetryPolicy"/> that controls the timing
        ///     and number of reconnect attempts.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithAutomaticReconnect<TConfig>(this IConfigurationBuilder<TConfig> source, IRetryPolicy retryPolicy)
                where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.RetryPolicy = retryPolicy ?? new DefaultRetryPolicy();
            });
            return source;
        }

    }
}
