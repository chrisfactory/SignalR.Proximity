using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity
{
    internal class ProximityBuilder : IProximityBuilder
    {
        public ProximityBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public IProximityEndPointProvider Build()
        {
            Services.AddSingleton<IEndPointContainer, EndPointContainer>();
            Services.AddSingleton<IProximityEndPointProvider, ProximityEndPointProvider>();
            return Services.BuildServiceProvider().GetRequiredService<IProximityEndPointProvider>();
        }
    }
}
