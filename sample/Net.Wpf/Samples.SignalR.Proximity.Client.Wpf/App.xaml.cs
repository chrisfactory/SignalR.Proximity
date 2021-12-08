using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity;
using System.IO;
using System.Windows;
namespace Samples.SignalR.Proximity.Client.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
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
            //   _serviceProvider.GetService<ISignalRProximityClientFactory>();//Force to resolve
            _serviceProvider.GetService<IProximityFactory>();//Force to resolve
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot rootConfig)
        {
            serviceCollection.AddProximity();
            serviceCollection.AddProximity("toaster.container");
            serviceCollection.AddProximity("chat.container");

            //serviceCollection.AddProximity(builder =>
            //{
                //builder.UseClient(notifier =>
                //{
                //    notifier.Configure(rootConfig.GetSection("SignalRProximityClientConfiguration"), c =>
                //       {
                //           c.WithUserProvider(u => u.UserId = "chris");
                //           c.WithAutomaticReconnect();
                //           c.WithGroupsAutoRestored();
                //       });
                //    notifier.Configure("dev", rootConfig.GetSection("SignalRProximityClientConfiguration:dev"));
                //    notifier.Configure("prod", rootConfig.GetSection("SignalRProximityClientConfiguration:prod"));
                //});
            //});
        }
    }


}
