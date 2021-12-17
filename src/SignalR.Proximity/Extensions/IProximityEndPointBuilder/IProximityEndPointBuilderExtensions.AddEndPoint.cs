using Microsoft.Extensions.DependencyInjection;
using System;
namespace SignalR.Proximity
{
    public static class IProximityEndPointBuilderExtensions
    {
        public static IProximityEndPointBuilder UseUrlBase(this IProximityEndPointBuilder builder, string uriString)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.UrlBase = new Uri(uriString));

            return builder;
        }
        public static IProximityEndPointBuilder UseUrlBase(this IProximityEndPointBuilder builder, Uri uri)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.UrlBase = uri);

            return builder;
        }
    }
}
