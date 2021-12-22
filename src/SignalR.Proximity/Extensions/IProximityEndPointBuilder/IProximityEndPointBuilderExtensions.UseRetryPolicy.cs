using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityEndPointBuilderExtensions
    {
        public static TimeSpan[] DEFAULT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(5) };


        public static IProximityEndPointBuilder UseRetryPolicy(this IProximityEndPointBuilder builder, TimeSpan[] reconnectDelays)
        {
            builder.Services.AddSingleton<IRetryPolicy, RetryPolicy>(p => new RetryPolicy(reconnectDelays));
            return builder;
        }

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
