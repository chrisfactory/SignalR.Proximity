using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        public IConnection<TContract> Connect<TContract>()
        { 
            return this._provider.GetRequiredService<IConnectionBuilder<TContract>>().Build();
        }
    }
}
