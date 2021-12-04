using SignalR.Proximity.Common;
using System;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///  Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static ISignalRProximityBuilder AddSignalRProximity(this IServiceCollection services, Action<ISignalRProximityBuilder> configure)
        {
            var builder = new SignalRProximityBuilder(services);
            configure(builder);
            return builder;
        }
    }
}
