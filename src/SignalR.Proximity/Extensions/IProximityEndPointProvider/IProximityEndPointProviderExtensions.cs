using Microsoft.AspNetCore.Http.Connections.Client;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityEndPointProvider"/>.
    /// </summary>
    public static partial class IProximityEndPointProviderExtensions
    {
        /// <summary>
        /// Connects to the default endpoint with the specified contract.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="configureHttpConnection">Action to configure HTTP connection options.</param>
        /// <returns>A connection instance.</returns>
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider, Action<HttpConnectionOptions>? configureHttpConnection = null)
        {
            return provider.Get().Connect<TContract>(configureHttpConnection);
        }
        /// <summary>
        /// Connects to a named endpoint with the specified contract.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="endPointName">The endpoint name.</param>
        /// <param name="configureHttpConnection">Action to configure HTTP connection options.</param>
        /// <returns>A connection instance.</returns>
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider, string endPointName, Action<HttpConnectionOptions>? configureHttpConnection = null)
        {
            return provider.Get(endPointName).Connect<TContract>(configureHttpConnection);
        }
    }
}
