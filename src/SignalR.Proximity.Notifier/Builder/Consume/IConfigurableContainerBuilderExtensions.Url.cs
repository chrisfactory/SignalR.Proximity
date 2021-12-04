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
        public static IConfigurableContainerBuilder<NotifierProxyProvider, TContract> WithUrl<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, Uri url)
        {
            source.UseConfiguration(c => c.WithUrl(url));
            return source;
        }
        public static IConfigurableContainerBuilder<NotifierProxyProvider, TContract> WithUrl<TContract>(this IConfigurableContainerBuilder<NotifierProxyProvider, TContract> source, string url)
        {
            source.UseConfiguration(c => c.WithUrl(url));
            return source;
        }
    }
}