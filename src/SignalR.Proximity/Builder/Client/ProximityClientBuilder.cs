using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity
{
    internal class ProximityClientBuilder<TContract> : IProximityClientBuilder<TContract>
    {
        private readonly IHubConnectionBuilder _cnxBuilder;
        public ProximityClientBuilder(IServiceCollection services, IHubConnectionBuilder cnxBuilder)
        {
            _cnxBuilder = cnxBuilder;
            Services = services.Copy();
            Services.AddOptions<ScopeOptions>();
        }

        public IServiceCollection Services { get; }

        public IClientProxy<TContract> Build()
        {
            Services.AddSingleton<HubConnectionBuilderConfigure<TContract>>();
            Services.AddSingleton<HubConnectionAttacher<TContract>>();
            Services.AddSingleton(p => p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionBuilderConfigure<TContract>>().Configure(_cnxBuilder));
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionAttacher<TContract>>().Build());
            Services.AddSingleton<IClientProxy<TContract>, ClientProxy<TContract>>();
            return Services.BuildServiceProvider().GetRequiredService<IClientProxy<TContract>>();
        }
    }
    internal class InstanceValue<TContract>
    {
        public InstanceValue(TContract instance)
        {
            Value = instance;
        }
        public TContract Value { get; }
    }
    internal class HubConnectionAttacher<TContract>
    {
        private readonly InstanceValue<TContract> _instanceValue;
        private readonly IHubConnectionBuilder _connectionBuilder;
        public HubConnectionAttacher(IHubConnectionBuilder builder, InstanceValue<TContract> instanceValue)
        {
            _connectionBuilder = builder;
            _instanceValue = instanceValue;
        }

        public HubConnection Build()
        {
            HubConnection cnx = _connectionBuilder.Build();
            foreach (var item in MethodContractDescriptor.Create(_instanceValue.Value))
                cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);
            return cnx;
        }
    }

    internal class HubConnectionBuilderConfigure<TContract>
    {
        private readonly ProximityConfig _config;
        private readonly IRetryPolicy _retryPolicy;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUrlProvider<TContract> _urlProvider;
        public HubConnectionBuilderConfigure(
            IOptions<ProximityConfig> configOptions,
            IClientRetryPolicy retryPolicy,
            ITokenProvider tokenProvider,
            IUrlProvider<TContract> urlProvider)
        {
            _config = configOptions.Value;
            _retryPolicy = retryPolicy;
            _tokenProvider = tokenProvider;
            _urlProvider = urlProvider;
        }

        public IHubConnectionBuilder Configure(IHubConnectionBuilder builder)
        {

            var hubUri = _urlProvider.GetHubUrl(_config.UrlBase);


            builder.WithAutomaticReconnect(_retryPolicy)
            .WithUrl(hubUri, options =>
            {
                options.AccessTokenProvider = () => _tokenProvider.GetTokenAsync(_config.UrlBase);
                options.UseDefaultCredentials = true;

            });
            return builder;
        }
    }


}
