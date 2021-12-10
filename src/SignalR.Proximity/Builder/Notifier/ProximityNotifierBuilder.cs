using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace SignalR.Proximity
{
    internal class ProximityNotifierBuilder<TContract> : IProximityNotifierBuilder<TContract>
    {
        private readonly IHubConnectionBuilder _cnxBuilder;
        public ProximityNotifierBuilder(IServiceCollection services, IHubConnectionBuilder cnxBuilder)
        {
            _cnxBuilder = cnxBuilder;
            Services = services;
            Services.AddOptions<ScopeOptions>();
        }

        public IServiceCollection Services { get; }

        public INotifierProxy<TContract> Build()
        {
             
            Services.AddSingleton<HubConnectionBuilderConfigure<TContract>>();
            Services.AddSingleton<HubConnectionAttacher<TContract>>();
            Services.AddSingleton(p => p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionBuilderConfigure<TContract>>().Configure(_cnxBuilder));
            Services.AddSingleton(p => p.GetRequiredService<HubConnectionAttacher<TContract>>().Build());
           

            Services.AddSingleton<INotifierProxy<TContract>, NotifierProxy<TContract>>();
            return Services.BuildServiceProvider().GetRequiredService<INotifierProxy<TContract>>();
        }
    }
}
