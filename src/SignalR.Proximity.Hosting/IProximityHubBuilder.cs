using Microsoft.AspNetCore.Http.Connections;
using System;

namespace SignalR.Proximity.Hosting
{
    public interface IProximityHubBuilder<TProximityHub, TContract> : IServicesBuilder
        where TProximityHub : ProximityHub<TContract>
    {
        void Build(Action<HttpConnectionDispatcherOptions>? configureOptions);
    }
}
