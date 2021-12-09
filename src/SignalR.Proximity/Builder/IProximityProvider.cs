namespace SignalR.Proximity
{
    public interface IProximityProvider
    {

        IProximityBuilder Get();
        IProximityBuilder Get(string name);
    }
}
