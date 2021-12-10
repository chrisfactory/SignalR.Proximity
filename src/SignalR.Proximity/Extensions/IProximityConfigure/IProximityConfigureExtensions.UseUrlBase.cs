using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
        public static TProximity UseUrlBase<TProximity>(this TProximity builder, string uriString)
            where TProximity : IProximityConfigure
        {
            builder.Services.Configure<ProximityConfig>(c => c.UrlBase = new Uri(uriString));

            return builder;
        }
        public static TProximity UseUrlBase<TProximity>(this TProximity builder, Uri uri)
                 where TProximity : IProximityConfigure
        {
            builder.Services.Configure<ProximityConfig>(c => c.UrlBase = uri);

            return builder;
        }
    }
}
