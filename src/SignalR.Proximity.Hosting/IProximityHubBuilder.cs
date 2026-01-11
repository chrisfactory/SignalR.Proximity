namespace SignalR.Proximity.Hosting
{
    /// <summary>
    /// Represents a Proximity hub builder.
    /// </summary>
    public interface IProximityHubBuilder : IServicesBuilder
    {

    }
    /// <summary>
    /// Represents a Proximity hub builder.
    /// </summary>
    /// <typeparam name="TProximityHub">The hub type.</typeparam>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public interface IProximityHubBuilder<TProximityHub, TContract> : IProximityHubBuilder
        where TProximityHub : ProximityHub<TContract>
    {
        /// <summary>
        /// Builds the hub.
        /// </summary>
        void Build();
    }
}
