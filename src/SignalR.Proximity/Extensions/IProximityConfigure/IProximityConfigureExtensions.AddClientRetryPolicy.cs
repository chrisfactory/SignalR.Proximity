using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
        private static TimeSpan[] DEFAULT_CLIENT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(5) };

        internal static IProximityConfigure AddClientRetryPolicy(this IProximityConfigure builder)
        {
            builder.AddClientRetryPolicy(DEFAULT_CLIENT_RETRY_DELAYS_IN_MILLISECONDS);
            return builder;
        }

        public static IProximityConfigure AddClientRetryPolicy(this IProximityConfigure builder, TimeSpan[] reconnectDelays)
        {
            builder.Services.AddSingleton<IClientRetryPolicy>(p => new ClientRetryPolicy<RetryPolicy>(new RetryPolicy(reconnectDelays)));
            return builder;
        }

        public static IProximityConfigure AddClientRetryPolicy<TRetryPolicy>(this IProximityConfigure builder)
            where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<IClientRetryPolicy, ClientRetryPolicy<TRetryPolicy>>();
            return builder;
        }

        public static IProximityConfigure AddClientRetryPolicy<TRetryPolicy>(this IProximityConfigure builder, TRetryPolicy retryPolicy)
              where TRetryPolicy : class, IRetryPolicy, new()
        {
            builder.Services.AddSingleton<IClientRetryPolicy>(new ClientRetryPolicy<TRetryPolicy>(retryPolicy));
            return builder;
        }
    }
}
