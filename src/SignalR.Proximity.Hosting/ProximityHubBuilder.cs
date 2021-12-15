using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Hosting
{
    internal class ProximityHubBuilder<TProximityHub, TContract> : IProximityHubBuilder<TProximityHub, TContract>
         where TProximityHub : ProximityHub<TContract>
    {
        public ProximityHubBuilder()
        {
            Services = new ServiceCollection();
            Services.AddSingleton<IUrlProvider<TContract>, UrlProvider<TContract>>();
            Services.AddSingleton<ISignalRPatternProvider, SignalRPatternProvider<TContract>>();
        }

        public IServiceCollection Services { get; }

        public void Build()
        {
            var provider = this.Services.BuildServiceProvider();
            var endPointBuilder = provider.GetRequiredService<IEndpointRouteBuilder>();
            var patternProvider = provider.GetRequiredService<ISignalRPatternProvider>();
            var pattern = patternProvider.GetPattern();
            //var ... = Action<HttpConnectionDispatcherOptions> ? configureOptions
            endPointBuilder.MapHub<TProximityHub>(pattern);
        }
    }
}
