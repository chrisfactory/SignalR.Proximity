using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the Proximity builder.
    /// </summary>
    public interface IProximityBuilder
    {
        /// <summary>
        /// Gets the services collection.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Builds the provider.
        /// </summary>
        /// <returns>The proximity provider.</returns>
        IProximityEndPointProvider Build();
    }
}
