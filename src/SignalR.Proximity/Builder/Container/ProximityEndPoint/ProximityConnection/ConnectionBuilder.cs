using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity
{
    internal class ConnectionBuilder<TContract> : IConnectionBuilder<TContract>
    {
        public ConnectionBuilder(IHubConnectionBuilder build, IOptions<ProximityEndPointConfig> options, IServiceCollection services)
        {
            Services = services.Copy();

            Services.AddSingleton<HubConnectionBuilderConfigure<TContract>>();
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionBuilderConfigure<TContract>>().Configure(build));
            Services.AddSingleton(p => p.GetRequiredService<IHubConnectionBuilder>().Build());

            Services.AddSingleton<IClientBuilder<TContract>, ClientBuilder<TContract>>();
            Services.AddSingleton(p => p.GetRequiredService<IClientBuilder<TContract>>().Build());
            Services.AddSingleton<INotifierBuilder<TContract>, NotifierBuilder<TContract>>();
            Services.AddSingleton(p => p.GetRequiredService<INotifierBuilder<TContract>>().Build());

            Services.AddSingleton<IConnection<TContract>, Connection<TContract>>();
        }
        public IServiceCollection Services { get; }

        public IConnection<TContract> Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<IConnection<TContract>>();
        }
    }



    //internal class InstanceValue<TContract>
    //{
    //    public InstanceValue(TContract instance)
    //    {
    //        Value = instance;
    //    }
    //    public TContract Value { get; }
    //}
    //internal class HubConnectionAttacher<TContract>
    //{
    //    private readonly InstanceValue<TContract> _instanceValue;
    //    private readonly IHubConnectionBuilder _connectionBuilder;
    //    public HubConnectionAttacher(IHubConnectionBuilder builder, InstanceValue<TContract> instanceValue)
    //    {
    //        _connectionBuilder = builder;
    //        _instanceValue = instanceValue;
    //    }

    //    public HubConnection Build()
    //    {
    //        HubConnection cnx = _connectionBuilder.Build();
    //        foreach (var item in MethodContractDescriptor.Create(_instanceValue.Value))
    //            cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);
    //        return cnx;
    //    }
    //}

    internal class HubConnectionBuilderConfigure<TContract>
    {
        private readonly ProximityEndPointConfig _config;
        private readonly IRetryPolicy _retryPolicy;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUrlProvider<TContract> _urlProvider;
        public HubConnectionBuilderConfigure(
            IOptions<ProximityEndPointConfig> configOptions,
            IRetryPolicy retryPolicy,
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

            var hubUri = _urlProvider.GetHubUrl(_config.UrlBase,_config.Pattern);


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
