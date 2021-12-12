namespace SignalR.Proximity
{
    public interface IProximityNotifierBuilder<TContract> : IProximityConfigure
    {
        INotifierProxy<TContract>  Build();
    }
}
