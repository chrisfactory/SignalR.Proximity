using System;

namespace SignalR.Proximity
{
    internal interface IEndPointContainer
    {
        Lazy<IProximityEndPoint> Get(string name);
    }
}
