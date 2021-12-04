using Microsoft.AspNetCore.SignalR.Client; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; 
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SignalR.Proximity.Common;

namespace SignalR.Proximity.Client
{
   
    public class ClientBuilder<TContract> : IClientBuilder<TContract>
    {
        private readonly ILoggerFactory _rootLoggerFactory;
        public ClientBuilder(IOptionsMonitor<SignalRProximityClientConfiguration> configs) : this(configs, null)
        {
        }
        public ClientBuilder(IOptionsMonitor<SignalRProximityClientConfiguration> configs, ILoggerFactory loggerFactory)
        {
            _rootLoggerFactory = loggerFactory;
            Services = new ServiceCollection();
            Services.AddSingleton<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.Configure<ConfigurationSelector<SignalRProximityClientConfiguration>>(c => { c.UseConfigs(configs, true); });
            Services.AddTransient<ClientProxyProvider>();
        }

        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        public IServiceCollection Services { get; private set; }
        public ClientProxyProvider Build()
        {
            Services.AddSingleton<IHubConnectionConfigurationFactory, HubConnectionConfigurationFactory<SignalRProximityClientConfiguration, TContract>>();

            if (_rootLoggerFactory != null)
                Services.TryAddSingleton<ILoggerFactory>(_rootLoggerFactory);
            else
                Services.AddLogging();
            var serviceProvider = Services.BuildServiceProvider();

            return serviceProvider.GetService<ClientProxyProvider>();
        }

    }
}
