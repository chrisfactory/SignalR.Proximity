using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity
{
    internal class ProximityClientBuilder<TContract> : IProximityClientBuilder<TContract>
    {
        public ProximityClientBuilder(IServiceCollection services)
        {
            Services = services.Copy();
            Services.AddOptions<ScopeOptions>();
            Services.AddSingleton(p => p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);

            //Services.AddSingleton<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.AddSingleton<HubConnectionBuilderConfigure<TContract>>();
            Services.AddSingleton<HubConnectionAttacher<TContract>>();

            Services.AddSingleton(p => p.GetRequiredService<HubConnectionBuilderConfigure<TContract>>().Configure(new HubConnectionBuilder()));
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionAttacher<TContract>>().Attach(p.GetRequiredService<IHubConnectionBuilder>().Build()));

            Services.AddSingleton<IClientProxy<TContract>, ClientProxy<TContract>>();
        }

        public IServiceCollection Services { get; }

        public IClientProxy<TContract> Build()
        {
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
        public HubConnectionAttacher(InstanceValue<TContract> instanceValue)
        {
            _instanceValue = instanceValue;
        }

        public HubConnection Attach(HubConnection cnx)
        {
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

            var connection = builder
                .WithAutomaticReconnect(_retryPolicy)
                .WithUrl(hubUri, options =>
                {
                    options.AccessTokenProvider = () => _tokenProvider.GetTokenAsync(_config.UrlBase);
                    options.UseDefaultCredentials = true;

                });
            return builder;
        }
    }


}
