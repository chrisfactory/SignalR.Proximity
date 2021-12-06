using Microsoft.Extensions.Configuration;
using NotifyR.Common;
using NotifyR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Samples.NotifyR.Client.WpfNetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Config.json", optional: false, reloadOnChange: false)
                             //.AddXmlFile("Config.xml", optional: false, reloadOnChange: false)
                             .Build();


            //NotifyRNotifierFactory.ConfigureRoot(notifyRBilder =>
            //{
            //    notifyRBilder.Configure(config.GetSection("NotifyRClientConfiguration"));
            //    notifyRBilder.Configure("dev", config.GetSection("NotifyRClientConfiguration:dev"));
            //    notifyRBilder.Configure("prod", config.GetSection("NotifyRClientConfiguration:prod"));
            //});
        }
    }
}
