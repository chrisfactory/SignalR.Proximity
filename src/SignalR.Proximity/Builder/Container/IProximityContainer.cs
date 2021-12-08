namespace SignalR.Proximity
{
    internal interface IProximityContainer
    {
        //IProximityBuilder CreateBuilder(string name);
        IProximityBuilder GetBuilder(string name);
    }
}
