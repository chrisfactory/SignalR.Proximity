using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection; 
using SignalR.Proximity.Notifier;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{NotifierProxyProvider,TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {
        public static IConfigurableContainerBuilder<NotifierProxyProvider, TContract> WithUser<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, string userId)
        {
            source.WithUser(() => userId);
            return source;
        }
        public static IConfigurableContainerBuilder<NotifierProxyProvider, TContract> WithUser<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, Func<string> provideUserId)
        {
            source.UseConfiguration(c =>
            {
                c.WithUserProvider(t =>
                {
                    t.UserId = provideUserId();
                });
            });
            return source;
        }
    }
}
