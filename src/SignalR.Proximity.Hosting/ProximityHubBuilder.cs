﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity.Hosting
{
    internal class ProximityHubBuilder<TProximityHub, TContract> : IProximityHubBuilder<TProximityHub, TContract>
         where TProximityHub : ProximityHub<TContract>
    {
        public ProximityHubBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<Action<HttpConnectionDispatcherOptions>>();
            Services.AddOptions<ProximityConfigurationCore>(); 
            Services.AddSingleton<IContractDescriptor<TContract>, ContractDescriptor<TContract>>();
            Services.AddSingleton<IContractDescriptor>(p => p.GetRequiredService<IContractDescriptor<TContract>>());
            Services.AddSingleton<IPatternUrlProvider, PatternUrlProvider>();
        }

        public IServiceCollection Services { get; }

        public void Build()
        {
         
            var provider = this.Services.BuildServiceProvider();
            var httpOptions = provider.GetRequiredService<Action<HttpConnectionDispatcherOptions>>();
            var endPointBuilder = provider.GetRequiredService<IEndpointRouteBuilder>();
            var patternProvider = provider.GetRequiredService<IPatternUrlProvider>();
            var pattern = patternProvider.GetPattern();
            endPointBuilder.MapHub<TProximityHub>(pattern, httpOptions);
        }
    }
}
