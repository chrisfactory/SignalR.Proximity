using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; 
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SignalR.Proximity.Common;

namespace SignalR.Proximity.Notifier
{
    internal class NotifierBuilder<TContract> : INotifierBuilder<TContract>
    {
        private readonly ILoggerFactory _rootLoggerFactory;
        public NotifierBuilder(IOptionsMonitor<SignalRProximityNotifierConfiguration> configs, ISignalRProximityNotifierFactory f) : this(configs, f, null)
        {
        }
        public NotifierBuilder(IOptionsMonitor<SignalRProximityNotifierConfiguration> configs, ISignalRProximityNotifierFactory f, ILoggerFactory loggerFactory)
        {
            _rootLoggerFactory = loggerFactory;
            Services = new ServiceCollection();
            Services.AddSingleton<IHubConnectionBuilder, HubConnectionBuilder>();


            Services.Configure<ConfigurationSelector<SignalRProximityNotifierConfiguration>>(c => { c.UseConfigs(configs, true); });

            this.UseScopeAll();
        }
        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        public IServiceCollection Services { get; private set; }
        public NotifierProxyProvider Build()
        {
            Services.AddTransient<NotifierProxyProvider>();
            Services.AddSingleton<IHubConnectionConfigurationFactory, HubConnectionConfigurationFactory<SignalRProximityNotifierConfiguration, TContract>>();

            if (_rootLoggerFactory != null)
                Services.TryAddSingleton<ILoggerFactory>(_rootLoggerFactory);
            else
                Services.AddLogging();

            var serviceProvider = Services.BuildServiceProvider();
            return serviceProvider.GetService<NotifierProxyProvider>();
        }
    }
}

