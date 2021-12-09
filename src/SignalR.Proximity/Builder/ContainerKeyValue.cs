namespace SignalR.Proximity
{
    internal class ContainerKeyValue
    {
        public ContainerKeyValue(string key, IProximityBuilder value) { Key = key; Value = value; }
        public string Key { get; }
        public IProximityBuilder Value { get; }
    }
}
