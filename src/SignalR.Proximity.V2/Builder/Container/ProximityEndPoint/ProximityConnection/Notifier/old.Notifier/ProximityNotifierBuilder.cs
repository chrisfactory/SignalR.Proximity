using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace SignalR.Proximity
{
    internal class ProximityNotifierBuilder<TContract> : IProximityNotifierBuilder<TContract>
    {
        public ProximityNotifierBuilder(IServiceCollection services)
        {
            Services = services.Copy();
            Services.AddOptions<ScopeOptions>();
        }

        public IServiceCollection Services { get; }

        public INotifierProxy<TContract> Build()
        { 
            Services.AddTransient<HubConnectionBuilderConfigure<TContract>>(); 
            Services.AddTransient(p => p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);
            Services.AddTransient(p => p.GetRequiredService<HubConnectionBuilderConfigure<TContract>>().Configure(new HubConnectionBuilder()));
            Services.AddTransient(p => p.GetRequiredService<IHubConnectionBuilder>().Build()); 

            Services.AddTransient<INotifierProxy<TContract>, NotifierProxy<TContract>>();
            return Services.BuildServiceProvider().GetRequiredService<INotifierProxy<TContract>>();
        }
    }
}
