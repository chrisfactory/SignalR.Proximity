using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SignalR.Proximity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
      

        public static IServiceCollection AddProximity(this IServiceCollection source)
        {
            source.AddProximity(ProximityFactory.DefaultContainerName);
            return source;
        }
        public static IServiceCollection AddProximity(this IServiceCollection source, string containerName)
        {
            source.TryAddSingleton<IProximityFactory, ProximityFactory>();
            source.TryAddSingleton<IProximityContainer, ProximityContainer>(); 
            source.TryAddTransient<IProximityBuilder, ProximityBuilder>();
            source.AddTransient(p=> new ContainerKeyValue<IProximityBuilder>(containerName, p.GetRequiredService<IProximityBuilder>()));
            return source;
        }
    }
}
