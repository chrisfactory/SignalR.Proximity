using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace SignalR.Proximity
{
    internal class ConnectionBuilder<TContract> : IConnectionBuilder<TContract>
    {
        public ConnectionBuilder(IHubConnectionBuilder build, IServiceCollection services)
        {
            Services = services.Copy();
            Services.AddSingleton<IContractDescriptor<TContract>, ContractDescriptor<TContract>>();
            Services.AddSingleton<IContractDescriptor>(p => p.GetRequiredService<IContractDescriptor<TContract>>());

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


    internal class HubConnectionBuilderConfigure<TContract>
    {
        private readonly ProximityEndPointConfig _config;
        private readonly IRetryPolicy _retryPolicy;
        private readonly IPatternProvider _urlProvider;
        private readonly Action<HttpConnectionOptions> _configureHttpConnection;
        private readonly Action<IHubConnectionBuilder>? _customConfigHub;
        public HubConnectionBuilderConfigure(
            IServiceProvider serviceProvider,
            IOptions<ProximityEndPointConfig> configOptions,
            IRetryPolicy retryPolicy,
            IPatternProvider urlProvider,
            Action<HttpConnectionOptions> configureHttpConnection)
        {
            _customConfigHub = serviceProvider.GetService<Action<IHubConnectionBuilder>>();
            _config = configOptions.Value;
            _retryPolicy = retryPolicy;
            _urlProvider = urlProvider;
            _configureHttpConnection = configureHttpConnection;
        }

        public IHubConnectionBuilder Configure(IHubConnectionBuilder builder)
        {
            var hubUri = GetHubUrl(_config.UrlBase, _urlProvider.GetPattern());
            _ = builder
                .WithAutomaticReconnect(_retryPolicy)
                .WithUrl(hubUri, _configureHttpConnection);
            if (_customConfigHub != null)
                _customConfigHub(builder);
            return builder;
        }

        public virtual Uri GetHubUrl(Uri? UrlBase, string pattern)
        {
            if (UrlBase != null)
                return new Uri(UrlBase, pattern);
            else
                return new Uri($"{pattern}", UriKind.Relative);
        }
    }

}
