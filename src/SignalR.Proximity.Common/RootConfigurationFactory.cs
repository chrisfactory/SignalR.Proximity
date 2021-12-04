using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Common.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR; 
namespace SignalR.Proximity.Common
{
    internal class RootConfigurationFactory
    {
        private object _sync = new object();
        private bool _init = false;
        private IServiceCollection Services;

        private RootConfigurationFactory()
        {

        }
         

        public static RootConfigurationFactory Current { get; private set; } = new RootConfigurationFactory();



        public IServiceCollection ConfigureRootCore<TConfig>(IServiceCollection servicesSource, Action<IRootConfigurationBuilder<TConfig>> configBuilder)
            where TConfig : SignalRProximityConfiguration, new()
        {
            lock (_sync)
            { 

                CheckAccess(servicesSource);
               
                var builder = new RootConfigurationBuilder<TConfig>(Services);
                configBuilder(builder);
             
                return Services;
            }
        }

        public IServiceCollection GetServices()
        {
            lock (_sync)
            {
                CheckAccess(null);
                return Services;
            }
        }
        private void CheckAccess(IServiceCollection servicesSource)
        {
            if (_init)
            {
                if (servicesSource != null && Services != servicesSource)
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                if (servicesSource != null)
                    Services = Services ?? servicesSource;
                else
                    Services = Services ?? new ServiceCollection();
                _init = true;
            }

        }
    }
}
