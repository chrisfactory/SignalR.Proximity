using System.Collections.Generic;

namespace SignalR.Proximity
{
   
    internal class ProximityContainer : IProximityContainer
    {
        private readonly Dictionary<string, IProximityBuilder> container = new Dictionary<string, IProximityBuilder>();
        public ProximityContainer(IEnumerable<ContainerKeyValue> values)
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
