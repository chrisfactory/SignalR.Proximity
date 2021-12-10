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

            Services.AddSingleton<IUrlProvider, UrlProvider>();
            Services.AddSingleton<ITokenProvider, TokenProvider>();
            this.AddClientRetryPolicy();
            this.AddNotifierRetryPolicy();

            Services.AddSingleton(Services.Copy());
            Services.AddTransient<IProximityClientBuilder, FFClient>();
            Services.AddTransient<IProximityNotifierBuilder, FFNotifier>();
            Services.AddSingleton<IProximityContext, ProximityContext>();
            _lazyProvider = new Lazy<IServiceProvider>(() => this.Services.BuildServiceProvider());
        }

        public IServiceCollection Services { get; }
       
        public IProximityContext Build()
        {
            return _lazyProvider.Value.GetRequiredService<IProximityContext>();
        }
    }

    public interface IProximityContext
    {
        IProximityClientBuilder Client();
        IProximityNotifierBuilder Notifier();
    }
    internal class ProximityContext : IProximityContext
    {
        private readonly IServiceProvider _provider;
        public ProximityContext(IServiceProvider provider)
        {
             this._provider = provider;
        }
        public IProximityClientBuilder Client()
        {
            return _provider.GetRequiredService<IProximityClientBuilder>();
        }

        public IProximityNotifierBuilder Notifier()
        {
            return _provider.GetRequiredService<IProximityNotifierBuilder>();
        }
    }
    public interface IProximityClientBuilder : IProximityConfigure
    {

    }
    internal class FFClient : IProximityClientBuilder
    {
        public FFClient(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        
       
    }
    public interface IProximityNotifierBuilder : IProximityConfigure
    {

    }
    internal class FFNotifier : IProximityNotifierBuilder
    {
        public FFNotifier(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

     
    }
}
