using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity
{
    internal class ProximityClientBuilder: IProximityClientBuilder
    {
        public ProximityClientBuilder(IServiceCollection services)
        {
            Services = services;
            Services.AddOptions<ScopeOptions>();
            services.AddSingleton(p=>p.GetRequiredService<IOptions<ScopeOptions>>().Value.Scope);
            Services.AddSingleton<IHubConnectionBuilder, HubConnectionBuilder>();
            Services.AddTransient(p=>p.GetRequiredService<IHubConnectionBuilder>().Build());

            //foreach (var item in MethodContractDescriptor.Create<TContract>(instance))
            //    cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);

            var hubUri = config.HubNamespaceProvider.GetHubUrl<TContract>(urlBase, scheme);

            var connection = hubBuilder
                .WithAutomaticReconnect(config.RetryPolicy)
                .WithUrl(hubUri, options =>
                {
                    if (tokenProvider != null)
                    {
                        options.AccessTokenProvider = () => tokenProvider.AccessTokenProviderAsync(urlBase, userProvider);
                        options.UseDefaultCredentials = true;
                    }
                });


            Services.AddTransient<IClientProxy, ClientProxy>();
        }

        public IServiceCollection Services { get; }

        public IClientProxy Build()
        {
            return Services.BuildServiceProvider().GetRequiredService<IClientProxy>();
        }
    }
}
