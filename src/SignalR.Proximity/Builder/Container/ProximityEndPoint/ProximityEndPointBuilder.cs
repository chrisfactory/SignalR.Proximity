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
            Services.AddSingleton<IRetryPolicy, RetryPolicy>();
            Services.AddSingleton<ITokenProvider, TokenProvider>();
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
