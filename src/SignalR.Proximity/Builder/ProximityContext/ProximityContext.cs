using Microsoft.Extensions.DependencyInjection;
using System;
namespace SignalR.Proximity
{
    internal class ProximityContext : IProximityContext
    {
        private readonly IServiceProvider _provider;
        public ProximityContext(IServiceProvider provider)
        {
            this._provider = provider;
        }
        public IProximityClientBuilder<TContract> Client<TContract>() => _provider.GetRequiredService<IProximityClientBuilder<TContract>>();

        public IProximityNotifierBuilder<TContract> Notifier<TContract>() => _provider.GetRequiredService<IProximityNotifierBuilder<TContract>>();
    }
}
