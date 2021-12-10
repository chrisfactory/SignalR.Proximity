using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    internal class ProximityNotifierBuilder: IProximityNotifierBuilder
    {
        public ProximityNotifierBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
