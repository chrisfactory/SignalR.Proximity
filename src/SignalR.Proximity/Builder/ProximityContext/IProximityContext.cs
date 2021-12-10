namespace SignalR.Proximity
{
    public interface IProximityContext
    {
        IProximityClientBuilder Client { get; }
        IProximityNotifierBuilder Notifier { get; }
    }
}
