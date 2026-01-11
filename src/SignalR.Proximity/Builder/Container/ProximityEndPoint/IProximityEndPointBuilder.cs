using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the Proximity endpoint builder.
    /// </summary>
    public interface IProximityEndPointBuilder : IServicesBuilder
    {
        /// <summary>
        /// Builds the endpoint.
        /// </summary>
        /// <returns>The lazy initialized endpoint.</returns>
        Lazy<IProximityEndPoint> Build();
    }
}
