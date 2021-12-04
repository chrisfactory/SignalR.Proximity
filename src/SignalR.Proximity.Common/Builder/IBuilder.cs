using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common.Builder
{ 
    /// <summary>
    /// Specifies the Builder contract for a collection of service descriptors.
    /// </summary>
    public interface IServiceBuilder
    {
        /// <summary>
        ///     A collection of service descriptors.
        /// </summary>
        IServiceCollection Services { get; }
    } 
}
