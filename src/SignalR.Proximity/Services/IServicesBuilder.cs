using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    public interface IServicesBuilder
    {
        IServiceCollection Services { get; }
    }
}
