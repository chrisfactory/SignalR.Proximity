using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="IProximityEndPointBuilder"/> HTTP connection configuration.
    /// </summary>
    public static partial class IProximityEndPointBuilderExtensions
    {
        /// <summary>
        /// Configures the HTTP connection options.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configureHttpConnection">Action to configure options.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder ConfigureHttpConnection(this IProximityEndPointBuilder builder, Action<HttpConnectionOptions> configureHttpConnection)
        {
            builder.Services.AddSingleton(configureHttpConnection);
            return builder;
        }

    }
}
