using System.Collections.Generic;

namespace SignalR.Proximity
{
    internal class ProximityProvider : IProximityProvider
    {
        private readonly Dictionary<string, IProximityFactory> _container = new Dictionary<string, IProximityFactory>();
        public const string DefaultContainerName = "Default";
        public ProximityProvider(IEnumerable<ContainerKeyValue> values)
        {
            foreach (var builderKV in values) 
                _container.Add(builderKV.Key, builderKV.Value);
        } 

        public IProximityContext Get()
        {
            return Get(DefaultContainerName);
        }

        public IProximityContext Get(string name)
        {
             _container.TryGetValue(name, out var value);
            return value?.Build();
        }
         
    }
}
