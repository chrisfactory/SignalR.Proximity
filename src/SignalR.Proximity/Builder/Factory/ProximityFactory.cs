namespace SignalR.Proximity
{
    internal class ProximityFactory : IProximityFactory
    {
        private readonly IProximityContainer _container;
        public ProximityFactory(IProximityContainer container)
        {
            _container = container;
        }
        
        public const string DefaultContainerName = "Default";
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
