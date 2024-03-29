﻿using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    internal class ProximityEndPointBuilder : IProximityEndPointBuilder
    {
        public ProximityEndPointBuilder()
        {
            Services = new ServiceCollection();
            Services.AddOptions<ProximityEndPointConfig>();
            Services.AddSingleton<IOptions<ProximityConfigurationCore>>(p => p.GetRequiredService<IOptions<ProximityEndPointConfig>>());
            this.UseDefaultRetryPolicy();
            Services.AddSingleton<Action<HttpConnectionOptions>>((e) => { });
            Services.AddSingleton<IPatternProvider, ContractPatternProvider>();
        }

        public IServiceCollection Services { get; }

        public Lazy<IProximityEndPoint> Build()
        {
            Services.AddTransient<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.AddTransient(typeof(IConnectionBuilder<>), typeof(ConnectionBuilder<>));
            Services.AddSingleton<IProximityEndPoint, ProximityEndPoint>();
            var services = Services.Copy();
            services.AddSingleton(services);
            return new Lazy<IProximityEndPoint>(() => services.BuildServiceProvider().GetRequiredService<IProximityEndPoint>());
        }
    }
}
