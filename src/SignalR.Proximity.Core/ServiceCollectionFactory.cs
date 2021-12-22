using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Core
{
    internal static class ServiceCollectionFactory
    {
        /// <summary>
        /// resolve conflict  
        ///     Microsoft.Extensions.DependencyInjection.Abstractions, Version=6.0.0.0
        ///     Microsoft.Extensions.DependencyInjection, Version=3.1.0.0
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection Create()
        {
            return new ServiceCollection();
        }
    }
}
