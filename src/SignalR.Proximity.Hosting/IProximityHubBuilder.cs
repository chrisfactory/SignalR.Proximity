namespace SignalR.Proximity.Hosting
{
    public interface IProximityHubBuilder : IServicesBuilder
    {

    }
    public interface IProximityHubBuilder<TProximityHub, TContract> : IProximityHubBuilder
        where TProximityHub : ProximityHub<TContract>
    {
        void Build();
    }
}
