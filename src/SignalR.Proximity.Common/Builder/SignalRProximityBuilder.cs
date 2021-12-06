using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    public class SignalRProximityBuilder : ISignalRProximityBuilder
    {
        public SignalRProximityBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        public IServiceCollection Services { get; private set; }

    } 
}
