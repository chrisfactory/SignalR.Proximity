using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    internal class ClientBuilder<TContract> : IClientBuilder<TContract>
    {
        public ClientBuilder(HubConnection cnx)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(cnx);
        }
        public IServiceCollection Services { get; }

        public Lazy<IClient<TContract>> Build()
        {
            var services = this.Services.Copy();
            services.AddSingleton<IContractDescriptor<TContract>, ContractDescriptor<TContract>>();
            services.AddTransient<IClient<TContract>, Client<TContract>>();
            return new Lazy<IClient<TContract>>(() => services.BuildServiceProvider().GetRequiredService<IClient<TContract>>());
        }
    }
}
