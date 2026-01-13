using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityEndPointBuilder"/> URL configuration.
    /// </summary>
    public static partial class IProximityEndPointBuilderExtensions
    {

        /// <summary>
        /// Configures the <see cref="IHubConnectionBuilder"/> used to build the connection.
        /// </summary>
        /// <param name="builder">The <see cref="IProximityEndPointBuilder"/> instance.</param>
        /// <param name="hubBuilder">The action to configure the <see cref="IHubConnectionBuilder"/>.</param>
        /// <returns>The <see cref="IProximityEndPointBuilder"/> instance.</returns>
        public static IProximityEndPointBuilder ConfigureHubConnectionBuilder(this IProximityEndPointBuilder builder, Action<IHubConnectionBuilder> hubBuilder)
        {
            builder.Services.AddSingleton(hubBuilder);
            return builder;
        }

    }
}
