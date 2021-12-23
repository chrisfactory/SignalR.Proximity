using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    internal class ProximityEndPoint : IProximityEndPoint
    {
        private readonly IServiceProvider _provider;
        public ProximityEndPoint(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public IConnection<TContract> Connect<TContract>(Action<HttpConnectionOptions>? configureHttpConnection = null)
        {
            var builder = this._provider.GetRequiredService<IConnectionBuilder<TContract>>();
            if (configureHttpConnection != null)
                builder.Services.AddSingleton(configureHttpConnection);
            return builder.Build();
        }
    }
}
