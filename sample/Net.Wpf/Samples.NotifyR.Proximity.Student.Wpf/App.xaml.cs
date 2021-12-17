using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace Samples.SignalR.Proximity.Student.Wpf
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

            //   var _serviceProvider = serviceCollection.BuildServiceProvider(); 
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot rootConfig)
        {

        }
    }
}
