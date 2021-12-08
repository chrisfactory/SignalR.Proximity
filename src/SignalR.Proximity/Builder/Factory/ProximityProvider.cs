namespace SignalR.Proximity
{
    internal class ProximityProvider : IProximityProvider
    {
        public const string DefaultContainerName = "Default";
        private readonly IProximityContainer _container;
        public ProximityProvider(IProximityContainer container)
        {
            _container = container;
        }
        
      
        public void Get()
        {
            Get(DefaultContainerName);
        }

        public void Get(string name)
        {
           var builder= _container.GetBuilder(name);
            //return builder.Build();
        }
    }
}
