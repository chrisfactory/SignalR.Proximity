using System.IO;
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
            IConfigurationRoot config = null;
            var services = CreateServices(out config);
             
            ConfigureServices(services, config);

            ConfigureSampleAppServices(services, config);
            BuildApp(services);
        }

         
        private void ConfigureServices(IServiceCollection services, IConfigurationRoot rootConfig)
        {
            services.UseProximity(proximity =>
            {
                proximity.AddEndPoint("https://localhost:5011");
                proximity.AddEndPoint("From.Code", "https://localhost:5011");
                proximity.AddEndPoint("From.ConfigFile", rootConfig.GetSection("Proximity"));
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
        private void ConfigureSampleAppServices(IServiceCollection services, IConfigurationRoot rootConfig)
        {
            services.AddSingleton<GlobalViewModel>();
            services.AddSingleton<MainWindow>();
        }
        private void BuildApp(IServiceCollection services)
        {
            this.MainWindow = services.BuildServiceProvider().GetRequiredService<MainWindow>();
            this.MainWindow.Show();
        }
        #endregion 

    }


}
