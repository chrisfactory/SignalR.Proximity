using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Notifier;
using System.IO;
using System.Windows;

namespace Samples.SignalR.Proximity.Notifier.Wpf
{
    public partial class App : Application
    {

        public App()
        {

            var serviceCollection = new ServiceCollection();

            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Config.json", optional: false, reloadOnChange: false)
                             //.AddXmlFile("Config.xml", optional: false, reloadOnChange: false)
                             .Build();

            ConfigureServices(serviceCollection, config);

            var _serviceProvider = serviceCollection.BuildServiceProvider();
            _serviceProvider.GetService<ISignalRProximityNotifierFactory>();//Force to resolve
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot rootConfig)
        {
            serviceCollection.AddSignalRProximity(builder =>
            {
                builder.UseNotifier(notifier =>
                { 
                    notifier.Configure(rootConfig.GetSection("SignalRProximityNotifierConfiguration"));
                    notifier.Configure("dev", rootConfig.GetSection("SignalRProximityNotifierConfiguration:dev"));
                    notifier.Configure("prod", rootConfig.GetSection("SignalRProximityNotifierConfiguration:prod"));
                });
            });
        }
    }
}
