using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityEndPointBuilderExtensions
    {
        public static IProximityEndPointBuilder ConfigureHttpConnection(this IProximityEndPointBuilder builder, Action<HttpConnectionOptions> configureHttpConnection)
        {
            builder.Services.AddSingleton(configureHttpConnection);
            return builder;
        }

    }
}
