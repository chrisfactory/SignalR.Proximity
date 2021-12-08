using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity
{
    public interface IProximityBuilder
    {
        IServiceCollection Services { get; }

        void Build();
    }
}
