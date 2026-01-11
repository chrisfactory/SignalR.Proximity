using Microsoft.AspNetCore.Http.Connections.Client;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the Proximity endpoint.
    /// </summary>
    public interface IProximityEndPoint
    {
        /// <summary>
        /// Connects to the endpoint with a contract.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="configureHttpConnection">Action to configure the HTTP connection.</param>
        /// <returns>The connection.</returns>
        IConnection<TContract> Connect<TContract>(Action<HttpConnectionOptions>? configureHttpConnection = null);
    }
}
