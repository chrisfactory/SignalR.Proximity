using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    /// <summary>
    /// Defines a builder that exposes a service collection.
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// Gets the service collection.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
