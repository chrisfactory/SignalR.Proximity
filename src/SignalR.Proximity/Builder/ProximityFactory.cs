using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class ProximityFactory : IProximityFactory
    {
        private Lazy<IServiceProvider> _lazyProvider;
        public ProximityFactory()
        {
            Services = new ServiceCollection();
            Services.AddOptions<ProximityConfig>();
            this.AddClientRetryPolicy();

            Services.AddSingleton(typeof(IUrlProvider<>), typeof(UrlProvider<>));
            Services.AddSingleton<ITokenProvider, TokenProvider>();
            this.AddClientRetryPolicy();
            this.AddNotifierRetryPolicy();

          
            _lazyProvider = new Lazy<IServiceProvider>(() => this.Services.BuildServiceProvider());
        }

        public IServiceCollection Services { get; }

        public IProximityContext Build()
        {
            Services.AddSingleton(Services.Copy());
            Services.AddTransient<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.AddTransient(typeof(IProximityClientBuilder<>), typeof(ProximityClientBuilder<>));
            Services.AddTransient(typeof(IProximityNotifierBuilder<>), typeof(ProximityNotifierBuilder<>));
            Services.AddSingleton<IProximityContext, ProximityContext>();
            return _lazyProvider.Value.GetRequiredService<IProximityContext>();
        }
    }
}
