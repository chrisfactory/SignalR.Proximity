using System;
namespace SignalR.Proximity
{
    internal class ProximityEndPointProvider : IProximityEndPointProvider
    {
        private readonly IEndPointContainer _container;
        public ProximityEndPointProvider(IEndPointContainer container)
        {
            _container = container;
        }
        public IProximityEndPoint Get()
        {
            return Get(ContainerKeyValue.DefaultContainerName);
        }
        public IProximityEndPoint Get(string name)
        {
            var endPoint = _container.Get(name);
            if (endPoint == null)
                throw new NullReferenceException(nameof(endPoint));
            return endPoint.Value;
        }
    }
}
