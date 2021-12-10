using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class ProximityBuilder : IProximityBuilder
    {
        public ProximityBuilder()
        {
            Services = new ServiceCollection();
            Services.AddOptions<ProximityConfig>();
            this.AddClientRetryPolicy();

            Services.AddSingleton<IUrlProvider, UrlProvider>();
            Services.AddSingleton<ITokenProvider, TokenProvider>();
            this.AddClientRetryPolicy();
            this.AddNotifierRetryPolicy();

            Services.AddSingleton(Services.Copy());
            Services.AddTransient<IProximityClientBuilder, FFClient>();
            Services.AddTransient<IProximityNotifierBuilder, FFNotifier>();

        }

        public IServiceCollection Services { get; }

        public void Build()
        {
            var c = Services.BuildServiceProvider().GetRequiredService<IProximityClientBuilder>();
        }
    }
    public interface IProximityClientBuilder : IProximityBuilder
    {

    }
    internal class FFClient : IProximityClientBuilder
    {
        public FFClient(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public void Build()
        {

        }
    }
    public interface IProximityNotifierBuilder : IProximityBuilder
    {

    }
    internal class FFNotifier : IProximityNotifierBuilder
    {
        public FFNotifier(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public void Build()
        {

        }
    }
}
