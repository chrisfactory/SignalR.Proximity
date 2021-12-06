using Microsoft.Extensions.Configuration;
using NotifyR.Common;
using NotifyR.Notifier;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Samples.NotifyR.Notifier.WpfNetCore
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
            _serviceProvider.GetService<INotifyRNotifierFactory>();
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot rootConfig)
        {
            serviceCollection.AddNotifyR(notifyRBilder =>
            { 
                notifyRBilder.UseNotifier(notifier =>
                {
                    notifier.Configure(rootConfig.GetSection("NotifyRNotifierConfiguration"));
                    notifier.Configure("dev", rootConfig.GetSection("NotifyRNotifierConfiguration:dev"));
                    notifier.Configure("prod", rootConfig.GetSection("NotifyRNotifierConfiguration:prod"));
                });
            });
        }
    }
}
