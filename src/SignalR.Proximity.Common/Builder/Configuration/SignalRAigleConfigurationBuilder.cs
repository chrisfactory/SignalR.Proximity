using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SignalR.Proximity.Common.Builder;
namespace SignalR.Proximity.Common
{
    public class SignalRProximityConfigurationBuilder<TConfig> : IConfigurationBuilder<TConfig>
        where TConfig : SignalRProximityConfiguration, new()
    {
        public SignalRProximityConfigurationBuilder(TConfig config)
        {
            Services = new ServiceCollection();
            Services.Configure<ConfigurationSelector<TConfig>>(c => c.UseConfigs(config));

        }
        public SignalRProximityConfigurationBuilder(IServiceBuilder context)
        {
            Services = context.Services;
        }

        /// <summary>
        ///     Specifies the contract for a collection of service descriptors.
        /// </summary>
        public IServiceCollection Services { get; private set; }


        public void Build()
        {
            var provider = Services.BuildServiceProvider();
            var opt = provider.GetService<IOptionsMonitor<ConfigurationSelector<TConfig>>>().CurrentValue;
        }
    }

}
