namespace SignalR.Proximity
{
    public interface IProximityEndPointProvider
    {
        IProximityEndPoint Get();
        IProximityEndPoint Get(string name);
    }
}
