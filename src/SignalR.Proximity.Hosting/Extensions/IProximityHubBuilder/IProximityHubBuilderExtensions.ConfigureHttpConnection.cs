using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity.Hosting
{
    public static partial class IProximityHubBuilderExtensions
    {
        public static IProximityHubBuilder ConfigureHttpConnection(this IProximityHubBuilder builder, Action<HttpConnectionDispatcherOptions> configureOptions)
        {
            builder.Services.AddSingleton(configureOptions);
            return builder;
        }
    }
}
