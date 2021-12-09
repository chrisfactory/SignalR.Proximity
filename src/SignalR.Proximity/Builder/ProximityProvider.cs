using System.Collections.Generic;

namespace SignalR.Proximity
{
    internal class ProximityProvider : IProximityProvider
    {
        private readonly Dictionary<string, IProximityBuilder> _container = new Dictionary<string, IProximityBuilder>();
        public const string DefaultContainerName = "Default";
        public ProximityProvider(IEnumerable<ContainerKeyValue> values)
        {
            foreach (var builderKV in values) 
                _container.Add(builderKV.Key, builderKV.Value);
        } 

        public IProximityBuilder Get()
        {
            return Get(DefaultContainerName);
        }

        public IProximityBuilder Get(string name)
        {
             _container.TryGetValue(name, out var value);
            return value;
        }
    }
}
