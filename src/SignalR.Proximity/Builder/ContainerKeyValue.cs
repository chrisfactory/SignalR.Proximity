namespace SignalR.Proximity
{
    internal class ContainerKeyValue
    {
        public ContainerKeyValue(string key, IProximityFactory value) { Key = key; Value = value; }
        public string Key { get; }
        public IProximityFactory Value { get; }
    }
}
