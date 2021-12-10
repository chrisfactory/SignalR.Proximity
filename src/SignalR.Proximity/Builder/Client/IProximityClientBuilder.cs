namespace SignalR.Proximity
{
    public interface IProximityClientBuilder : IProximityConfigure
    {
        IClientProxy Build();
    }
}
