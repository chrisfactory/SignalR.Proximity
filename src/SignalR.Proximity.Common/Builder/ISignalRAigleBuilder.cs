using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    public interface ISignalRProximityBuilder
    {
        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
