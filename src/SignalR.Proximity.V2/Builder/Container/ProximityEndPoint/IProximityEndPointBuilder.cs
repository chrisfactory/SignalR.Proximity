using System;

namespace SignalR.Proximity
{
    public interface IProximityEndPointBuilder: IServicesBuilder
    {
        Lazy<IProximityEndPoint> Build();
    }
}
