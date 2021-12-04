using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SignalR.Proximity.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{
    internal class RootConfigurationBuilder<TConfig> : IRootConfigurationBuilder<TConfig>
         where TConfig : SignalRProximityConfiguration, new()
    {
        public RootConfigurationBuilder(IServiceCollection services)
        {
            Services = services;

        }

        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        public IServiceCollection Services { get; private set; }

    }
}
