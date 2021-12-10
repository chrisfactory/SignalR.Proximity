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
        public IProximityClientBuilder Client => _provider.GetRequiredService<IProximityClientBuilder>();
        public IProximityNotifierBuilder Notifier => _provider.GetRequiredService<IProximityNotifierBuilder>();
    }
}
