namespace SignalR.Proximity.Hosting
{
    public interface IProximityHubBuilder<TProximityHub, TContract> : IServicesBuilder
        where TProximityHub : ProximityHub<TContract>
    {
        void Build();
    }
}
