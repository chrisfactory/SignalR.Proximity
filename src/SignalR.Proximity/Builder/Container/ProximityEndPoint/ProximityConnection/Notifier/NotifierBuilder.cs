using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    internal class NotifierBuilder<TContract> : INotifierBuilder<TContract>
    {
        public NotifierBuilder(HubConnection cnx)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(cnx);
        }
        public IServiceCollection Services { get; }

        public Lazy<INotifier<TContract>> Build()
        {
            var services = this.Services.Copy();
            services.AddTransient<INotifier<TContract>, Notifier<TContract>>();
            return new Lazy<INotifier<TContract>>(() => services.BuildServiceProvider().GetRequiredService<INotifier<TContract>>());
        }
    }
}
