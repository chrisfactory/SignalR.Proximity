using Microsoft.AspNetCore.Http.Connections.Client;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityEndPointProviderExtensions
    {
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider, Action<HttpConnectionOptions>? configureHttpConnection = null)
        {
            return provider.Get().Connect<TContract>(configureHttpConnection);
        }
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider, string endPointName, Action<HttpConnectionOptions>? configureHttpConnection = null)
        {
            return provider.Get(endPointName).Connect<TContract>(configureHttpConnection);
        }
    }
}
