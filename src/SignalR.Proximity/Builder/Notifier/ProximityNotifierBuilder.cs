using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    internal class ProximityNotifierBuilder<TContract> : IProximityNotifierBuilder<TContract>
    {
        public ProximityNotifierBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
