﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samples.Framework.WPF;
using SignalR.Proximity;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _provider;
        public App()
        {
            var services = CreateServices(out var config);

            ConfigureServices(services, config);


            ConfigureSampleAppServices(services);
            _provider= services.BuildServiceProvider();
        }


        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot rootConfig)
        {
            services.UseProximity(proximity =>
            {
                proximity.AddEndPoint(rootConfig.GetSection("Proximity"), b =>
                {
                    b.ConfigureHttpConnection(cnxOptions => cnxOptions.Headers.Add("username", "anonymous"));
                });
            });
        }


        #region Not relevant for the examples 
        private static IServiceCollection CreateServices(out IConfigurationRoot config)
        {
            var services = new ServiceCollection();
            config =new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Config.json", optional: false, reloadOnChange: false)
                        //.AddXmlFile("Config.xml", optional: false, reloadOnChange: false)
                        .Build();

            return services;
        }
        private static void ConfigureSampleAppServices(IServiceCollection services)
        {

            services.AddSingleton<MainWindow>();
            services.AddSingleton<GlobalViewModel>();
            services.AddSingleton(UsersProvider.Users.Single(u => u.IsProfessor));
            services.AddSingleton<ProfessorViewModel>();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.MainWindow =this._provider.GetRequiredService<MainWindow>();
            this.MainWindow.Show();
        }
        #endregion 

    }
     
}
