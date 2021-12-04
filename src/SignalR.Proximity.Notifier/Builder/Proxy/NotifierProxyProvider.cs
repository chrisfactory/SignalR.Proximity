using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalR.Proximity.Common; 
using System.Reflection;

namespace SignalR.Proximity.Notifier
{
    public class NotifierProxyProvider : IProxyProvider
    {
        private SignalRProximityNotifierConfiguration _configurtion;
        private ScopeDefinitionBase _scope;
        private IHubConnectionBuilder _connectionBuilder;
        public NotifierProxyProvider(
          ILoggerFactory loggerFactory,
            IHubConnectionConfigurationFactory hubConnectionConfigurationFactory,
            IOptions<ConfigurationSelector<SignalRProximityNotifierConfiguration>> configOptions,
            IOptions<ScopeOptions> scopeOptions,
            IHubConnectionBuilder connectionBuilder
           )
        {
            _configurtion = configOptions.Value.Current;
            _scope = scopeOptions.Value.Scope;
            _connectionBuilder = connectionBuilder;
        }

        public TContract CreateProxy<TContract>()
        {
            var proxy = DispatchProxy.Create<TContract, NotifierDispatchProxy>();

            if (proxy is NotifierDispatchProxy dispatcher)
            {
                dispatcher.Attach(_connectionBuilder.Build(), _configurtion, _scope);
            }
            return proxy;

        }

    }
}
