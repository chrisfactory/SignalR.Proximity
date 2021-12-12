using System;

namespace SignalR.Proximity
{
    internal class ContainerKeyValue
    {
        public const string DefaultContainerName = "Default";

        public ContainerKeyValue(string key, Lazy<IProximityEndPoint> value) { Key = key; Value = value; }
        public string Key { get; }
        public Lazy<IProximityEndPoint> Value { get; }
    }
}
