using System;
using System.Collections.Generic;

namespace SignalR.Proximity
{
    internal class EndPointContainer : IEndPointContainer
    {
        private Dictionary<string, Lazy<IProximityEndPoint>> _endPoints = new Dictionary<string, Lazy<IProximityEndPoint>>();
        public EndPointContainer(IEnumerable<ContainerKeyValue> containers)
        {
            foreach (var builderKV in containers)
                _endPoints.Add(builderKV.Key, builderKV.Value);
        }
        public Lazy<IProximityEndPoint>? Get(string name)
        {
            _endPoints.TryGetValue(name, out  Lazy<IProximityEndPoint>? endPoint);
            return endPoint;
        }
    }
}
