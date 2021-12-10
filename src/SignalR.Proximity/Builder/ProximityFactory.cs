﻿using Microsoft.Extensions.DependencyInjection;
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
            Services.AddTransient<IProximityClientBuilder, ProximityClientBuilder>();
            Services.AddTransient<IProximityNotifierBuilder, ProximityNotifierBuilder>();
            Services.AddSingleton<IProximityContext, ProximityContext>();
            _lazyProvider = new Lazy<IServiceProvider>(() => this.Services.BuildServiceProvider());
        }

        public IServiceCollection Services { get; }
       
        public IProximityContext Build()
        {
            return _lazyProvider.Value.GetRequiredService<IProximityContext>();
        }
    }
}