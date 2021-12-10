namespace SignalR.Proximity
{
    public interface IProximityClientBuilder<TContract> : IProximityConfigure
    {
        IClientProxy<TContract> Build();
    }
}
