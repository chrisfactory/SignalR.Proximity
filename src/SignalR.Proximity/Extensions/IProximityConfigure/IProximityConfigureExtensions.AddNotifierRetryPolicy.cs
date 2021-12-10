using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
         private static TimeSpan[] DEFAULT_NOTIFIER_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromSeconds(2) };
        internal static IProximityConfigure AddNotifierRetryPolicy(this IProximityConfigure builder)
        {
            builder.AddNotifierRetryPolicy(DEFAULT_NOTIFIER_RETRY_DELAYS_IN_MILLISECONDS);
            return builder;
        }
        public static IProximityConfigure AddNotifierRetryPolicy(this IProximityConfigure builder, TimeSpan[] reconnectDelays)
        {
            builder.Services.AddSingleton<INotifierRetryPolicy>(p => new NotifierRetryPolicy<RetryPolicy>(new RetryPolicy(reconnectDelays)));
            return builder;
        }

        public static IProximityConfigure AddNotifierRetryPolicy<TRetryPolicy>(this IProximityConfigure builder)
            where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<INotifierRetryPolicy, NotifierRetryPolicy<TRetryPolicy>>();
            return builder;
        }

        public static IProximityConfigure AddNotifierRetryPolicy<TRetryPolicy>(this IProximityConfigure builder, TRetryPolicy retryPolicy)
              where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<INotifierRetryPolicy>(new NotifierRetryPolicy<TRetryPolicy>(retryPolicy));
            return builder;
        }
    }
}
