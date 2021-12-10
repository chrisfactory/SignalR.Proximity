using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    internal class ProximityClientBuilder: IProximityClientBuilder
    {
        public ProximityClientBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
