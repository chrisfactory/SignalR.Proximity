using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
         private static TimeSpan[] DEFAULT_NOTIFIER_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromSeconds(2) };
        internal static IProximityBuilder AddNotifierRetryPolicy(this IProximityBuilder builder)
        {
            builder.AddNotifierRetryPolicy(DEFAULT_NOTIFIER_RETRY_DELAYS_IN_MILLISECONDS);
            return builder;
        }
        public static IProximityBuilder AddNotifierRetryPolicy(this IProximityBuilder builder, TimeSpan[] reconnectDelays)
        {
            builder.Services.AddSingleton<INotifierRetryPolicy>(p => new NotifierRetryPolicy<RetryPolicy>(new RetryPolicy(reconnectDelays)));
            return builder;
        }

        public static IProximityBuilder AddNotifierRetryPolicy<TRetryPolicy>(this IProximityBuilder builder)
            where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<INotifierRetryPolicy, NotifierRetryPolicy<TRetryPolicy>>();
            return builder;
        }

        public static IProximityBuilder AddNotifierRetryPolicy<TRetryPolicy>(this IProximityBuilder builder, TRetryPolicy retryPolicy)
              where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<INotifierRetryPolicy>(new NotifierRetryPolicy<TRetryPolicy>(retryPolicy));
            return builder;
        }
    }
}
