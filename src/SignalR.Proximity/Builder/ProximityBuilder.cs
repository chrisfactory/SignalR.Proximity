using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class ProximityBuilder : IProximityBuilder
    {
        public ProximityBuilder()
        {
            Services = new ServiceCollection();
            Services.AddOptions<ProximityConfig>();
            Services.AddTransient<IHubUrlPathProvider, DefaultHubUrlPathProvider>();
            Services.AddTransient<Microsoft.AspNetCore.SignalR.Client.IRetryPolicy, DefaultRetryPolicy>();
            Services.AddTransient<ITokenProvider, DefaultTokenProvider>();
           
        }

        public IServiceCollection Services { get; }

        public void Build()
        {
            var opt = Services.BuildServiceProvider().GetRequiredService<IOptions<ProximityConfig>>().Value;
        }
    }

}
