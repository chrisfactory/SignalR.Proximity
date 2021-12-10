using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
        public static IProximityConfigure UseUrlBase(this IProximityConfigure builder, string uriString)
        {
            builder.Services.Configure<ProximityConfig>(c => c.UrlBase = new Uri(uriString));

            return builder;
        }
        public static IProximityConfigure UseUrlBase(this IProximityConfigure builder, Uri uri)
        {
            builder.Services.Configure<ProximityConfig>(c => c.UrlBase = uri);

            return builder;
        }
    }
}
