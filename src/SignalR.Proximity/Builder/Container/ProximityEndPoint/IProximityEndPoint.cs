using Microsoft.AspNetCore.Http.Connections.Client;
using System;

namespace SignalR.Proximity
{
    public interface IProximityEndPoint
    {
        IConnection<TContract> Connect<TContract>(Action<HttpConnectionOptions>? configureHttpConnection = null);
    }
}
