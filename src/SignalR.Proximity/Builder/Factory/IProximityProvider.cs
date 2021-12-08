namespace SignalR.Proximity
{
    public interface IProximityProvider
    {

        void Get();
        void Get(string name);
    }
}
