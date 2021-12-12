using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    internal class NotifierBuilder<TContract> : INotifierBuilder<TContract>
    {
        public NotifierBuilder()
        {
            Services = new ServiceCollection();
        }
        public IServiceCollection Services { get; }

        public Lazy<INotifier<TContract>> Build()
        {
            var services = this.Services.Copy();
            return new Lazy<INotifier<TContract>>(() => services.BuildServiceProvider().GetRequiredService<INotifier<TContract>>());
        }
    }
}
