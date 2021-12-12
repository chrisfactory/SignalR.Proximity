namespace SignalR.Proximity
{
    public interface IProximityEndPoint
    {
        IConnection<TContract> Connect<TContract>();
    }
}
