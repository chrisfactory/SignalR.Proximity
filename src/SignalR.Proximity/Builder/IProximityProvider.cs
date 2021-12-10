namespace SignalR.Proximity
{
    public interface IProximityProvider
    {

        IProximityContext Get();
        IProximityContext Get(string name);
    }
}
