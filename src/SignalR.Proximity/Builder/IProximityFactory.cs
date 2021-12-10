using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity
{
    public interface IProximityConfigure
    {
        IServiceCollection Services { get; } 
    }
    public interface IProximityFactory: IProximityConfigure
    {  
        IProximityContext Build();
    }
}
