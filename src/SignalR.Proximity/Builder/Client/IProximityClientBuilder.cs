namespace SignalR.Proximity
{
    public interface IProximityClientBuilder<TContract> : IProximityConfigure
    {
        IClientProxy Build();
    }
}
