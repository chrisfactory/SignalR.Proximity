using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity.Hosting
{
    /// <summary>
    /// Extensions for <see cref="IProximityHubBuilder"/> HTTP connection configuration.
    /// </summary>
    public static partial class IProximityHubBuilderExtensions
    {
        /// <summary>
        /// Configures the HTTP connection options.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configureOptions">Action to configure options.</param>
        /// <returns>The builder.</returns>
        public static IProximityHubBuilder ConfigureHttpConnection(this IProximityHubBuilder builder, Action<HttpConnectionDispatcherOptions> configureOptions)
        {
            builder.Services.AddSingleton(configureOptions);
            return builder;
        }
    }
}
