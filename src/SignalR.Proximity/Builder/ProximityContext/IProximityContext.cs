namespace SignalR.Proximity
{
    public interface IProximityContext
    {
        IProximityClientBuilder<TContract> Client<TContract>();
        IProximityNotifierBuilder<TContract> Notifier<TContract>();
    }
}
