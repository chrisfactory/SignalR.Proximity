namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the provider of proximity endpoints.
    /// </summary>
    public interface IProximityEndPointProvider
    {
        /// <summary>
        /// Gets the default endpoint.
        /// </summary>
        /// <returns>The proximity endpoint.</returns>
        IProximityEndPoint Get();
        /// <summary>
        /// Gets the endpoint by name.
        /// </summary>
        /// <param name="name">The endpoint name.</param>
        /// <returns>The proximity endpoint.</returns>
        IProximityEndPoint Get(string name);
    }
}
