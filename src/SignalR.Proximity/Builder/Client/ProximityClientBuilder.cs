using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity
{
    internal class ProximityClientBuilder<TContract> : IProximityClientBuilder<TContract>
    {
        public ProximityClientBuilder(IServiceCollection services)
        {
            Services = services;
            Services.AddOptions<ScopeOptions>();
            services.AddSingleton(p => p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);
            Services.AddSingleton<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.AddSingleton(p => p.GetRequiredService<IHubConnectionBuilder>().Build());
            Services.AddSingleton<HubConnectionAttacher<TContract>>();


            //var hubUri = config.HubNamespaceProvider.GetHubUrl<TContract>(urlBase, scheme);

            //var connection = hubBuilder
            //    .WithAutomaticReconnect(config.RetryPolicy)
            //    .WithUrl(hubUri, options =>
            //    {
            //        if (tokenProvider != null)
            //        {
            //            options.AccessTokenProvider = () => tokenProvider.AccessTokenProviderAsync(urlBase, userProvider);
            //            options.UseDefaultCredentials = true;
            //        }
            //    });


            Services.AddSingleton<IClientProxy, ClientProxy>();
        }

        public IServiceCollection Services { get; }

        public IClientProxy Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<IClientProxy>();
        }
    }
    internal class InstanceValue<TContract>
    {
        public InstanceValue(TContract instance)
        {
            Value = instance;
        }
        public TContract Value { get; }
    }
    internal class HubConnectionAttacher<TContract>
    {
        public HubConnectionAttacher(HubConnection cnx, InstanceValue<TContract> instanceValue)
        {
            foreach (var item in MethodContractDescriptor.Create(instanceValue.Value))
                cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);
        }
    }

}
