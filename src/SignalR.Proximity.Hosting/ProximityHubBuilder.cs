using Microsoft.AspNetCore.Builder;
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
            Services.AddOptions<ProximityConfigurationCore>();
            Services.AddSingleton<IPatternUrlProvider<TContract>, PatternUrlProvider<TContract>>();
            Services.AddSingleton<ISignalRPatternProvider, SignalRPatternProvider<TContract>>();
        }

        public IServiceCollection Services { get; }

        public void Build(Action<HttpConnectionDispatcherOptions>? configureOptions)
        {
            var provider = this.Services.BuildServiceProvider();
            var endPointBuilder = provider.GetRequiredService<IEndpointRouteBuilder>();
            var patternProvider = provider.GetRequiredService<ISignalRPatternProvider>();
            var pattern = patternProvider.GetPattern();
            endPointBuilder.MapHub<TProximityHub>(pattern, configureOptions);
        }
    }
}
