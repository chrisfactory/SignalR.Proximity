using Microsoft.Extensions.DependencyInjection;
using System; 

namespace SignalR.Proximity
{
    public static partial class IProximityBuilderExtensions
    {
        public static IProximityBuilder UseUrlBase(this IProximityBuilder builder, Uri uri)
        {
            builder.Services.Configure<ProximityConfig>(c => c.UrlBase = uri);

            return builder;
        }
    }
}
