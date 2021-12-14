﻿using System.IO; 
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration; 
using SignalR.Proximity;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            var services = new ServiceCollection();
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Config.json", optional: false, reloadOnChange: false)
                             //.AddXmlFile("Config.xml", optional: false, reloadOnChange: false)
                             .Build();

            ConfigureServices(services, config);
            services.AddSingleton<MainWindow>();

            this.MainWindow = services.BuildServiceProvider().GetRequiredService<MainWindow>();
            this.MainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot rootConfig)
        {
            serviceCollection.UseProximity(proximity =>
            {
                proximity.AddEndPoint("https://localhost:5011"); 
                proximity.AddEndPoint("From.Code", "https://localhost:5011"); 
                proximity.AddEndPoint("From.ConfigFile", rootConfig.GetSection("Proximity"));
            });


        }
    }


}
