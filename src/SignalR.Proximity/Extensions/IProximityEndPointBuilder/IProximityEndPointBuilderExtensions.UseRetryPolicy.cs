using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityEndPointBuilder"/> retry policy.
    /// </summary>
    public static partial class IProximityEndPointBuilderExtensions
    {
        /// <summary>
        /// Default retry delays in milliseconds.
        /// </summary>
        public static readonly TimeSpan[] DEFAULT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(5) };


        /// <summary>
        /// Configures the retry policy with custom delays.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="reconnectDelays">The reconnect delays.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UseRetryPolicy(this IProximityEndPointBuilder builder, TimeSpan[] reconnectDelays)
        {
            builder.Services.AddSingleton<IRetryPolicy, RetryPolicy>(p => new RetryPolicy(reconnectDelays));
            return builder;
        }

        /// <summary>
        /// Configures a custom retry policy type.
        /// </summary>
        /// <typeparam name="TRetryPolicy">The retry policy type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UseRetryPolicy<TRetryPolicy>(this IProximityEndPointBuilder builder)
            where TRetryPolicy : class, IRetryPolicy
        {
            builder.Services.AddSingleton<IRetryPolicy, TRetryPolicy>();
            return builder;
        }


        internal static IProximityEndPointBuilder UseDefaultRetryPolicy(this IProximityEndPointBuilder builder)
        {
            builder.Services.AddSingleton<IRetryPolicy, RetryPolicy>(p => new RetryPolicy(DEFAULT_RETRY_DELAYS_IN_MILLISECONDS));
            return builder;
        }
    }
}
