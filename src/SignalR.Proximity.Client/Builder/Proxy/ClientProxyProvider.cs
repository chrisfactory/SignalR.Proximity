using Microsoft.AspNetCore.SignalR.Client; 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalR.Proximity.Common;

namespace SignalR.Proximity.Client
{
    /// <summary>
    ///    Represents a type that can create instances of <see cref="ClientProxy"/>. 
    /// </summary>
    public class ClientProxyProvider: IProxyProvider 
    {
        private SignalRProximityClientConfiguration _configurtion;
        private ScopeDefinitionBase _scope;
        private IHubConnectionBuilder _connectionBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProxyProvider"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        ///    Represents a type used to configure the HubConnectionConfiguration system and create instances of
        ///    <see cref="IHubConnectionConfigurationFactory"/>.
        /// </param>
        /// <param name="hubConnectionConfigurationFactory">
        ///    Represents a type used to configure the logging system and create instances of
        ///    <see cref="Microsoft.Extensions.Logging.ILogger"/> from the registered <see cref="Microsoft.Extensions.Logging.ILoggerProvider"/>s.
        /// </param>
        /// <param name="configOptions">
        ///     Used to retrieve configured <see cref="ConfigurationSelector{SignalRProximityClientConfiguration}"/>. 
        /// </param>
        /// <param name="scopeOptions">
        ///     Used to retrieve configured <see cref="ScopeOptions"/> 
        /// </param>
        /// <param name="connectionBuilder">
        ///     Represents a type used to build and create instances of <see cref="HubConnection"/>.
        /// </param>
        public ClientProxyProvider(
            ILoggerFactory loggerFactory,
            IHubConnectionConfigurationFactory hubConnectionConfigurationFactory,
            IOptions<ConfigurationSelector<SignalRProximityClientConfiguration>> configOptions,
            IOptions<ScopeOptions> scopeOptions,
            IHubConnectionBuilder connectionBuilder
           )
        { 
             _configurtion = configOptions.Value.Current;
            _scope = scopeOptions.Value.Scope;
            _connectionBuilder = connectionBuilder;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProxy"/> class.
        /// </summary>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        /// </typeparam>
        /// <typeparam name="TInstance">
        ///    Represents a instance type to implemente the contract TContract
        /// </typeparam>
        /// <param name="instance">
        ///    Represents a instance to implemente the contract TContract
        /// </param>
        /// <returns>
        ///    Represents a instance of <see cref="ClientProxy"/> type to implemente dynamicly the contract TContract
        /// </returns>
        public ClientProxy CreateProxy<TContract, TInstance>(TInstance instance)
            where TInstance : class, TContract
        {

            var cnx = _connectionBuilder.Build();
            foreach (var item in MethodContractDescriptor.Create<TContract>(instance))
                cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);

            return new ClientProxy(cnx, _scope, _configurtion.RetryPolicy, _configurtion);
        }

    }
}
