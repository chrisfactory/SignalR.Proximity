using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace SignalR.Proximity
{
    internal class ConnectionBuilder<TContract> : IConnectionBuilder<TContract>
    {
        public ConnectionBuilder(IHubConnectionBuilder build, IOptions<ProximityEndPointConfig> options, IServiceCollection services)
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
        private readonly ITokenProvider _tokenProvider;
        private readonly IPatternProvider _urlProvider;
        public HubConnectionBuilderConfigure(
            IOptions<ProximityEndPointConfig> configOptions,
            IRetryPolicy retryPolicy,
            ITokenProvider tokenProvider,
            IPatternProvider urlProvider)
        {
            _config = configOptions.Value;
            _retryPolicy = retryPolicy;
            _tokenProvider = tokenProvider;
            _urlProvider = urlProvider;
        }

        public IHubConnectionBuilder Configure(IHubConnectionBuilder builder)
        {
            var hubUri = GetHubUrl(_config.UrlBase, _urlProvider.GetPattern());

            _=builder.WithAutomaticReconnect(_retryPolicy)
            .WithUrl(hubUri, options =>
            {
                options.AccessTokenProvider = () => _tokenProvider.GetTokenAsync(_config.UrlBase);
                options.UseDefaultCredentials = true;

            });
            return builder;
        }

        public virtual Uri GetHubUrl(Uri? UrlBase,string pattern)
        {  
            if (UrlBase != null)
                return new Uri(UrlBase, pattern);
            else
                return new Uri($"{pattern}", UriKind.Relative);
        }
    }

}
