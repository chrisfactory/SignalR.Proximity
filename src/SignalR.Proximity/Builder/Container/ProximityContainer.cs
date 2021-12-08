using System.Collections.Generic;

namespace SignalR.Proximity
{
    internal class ContainerKeyValue<TValue>
    {
        public ContainerKeyValue(string key, TValue value) { Key = key; Value = value; }
        public string Key { get; }
        public TValue Value { get; }
    }
    internal class ProximityContainer : IProximityContainer
    {
        private readonly Dictionary<string, IProximityBuilder> container = new Dictionary<string, IProximityBuilder>();
        public ProximityContainer(IEnumerable<ContainerKeyValue<IProximityBuilder>> values)
        {
            foreach (var builderKV in values)
            {
                container.Add(builderKV.Key, builderKV.Value);
            }
        }
      
        //public IProximityBuilder CreateBuilder(string name)
        //{
        //    return null;
        //}

        public IProximityBuilder GetBuilder(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
