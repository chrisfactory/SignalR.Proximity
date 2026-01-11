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
        /// Sets the base URL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="uriString">The URI string.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UseUrlBase(this IProximityEndPointBuilder builder, string uriString)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.UrlBase = new Uri(uriString));

            return builder;
        }
        /// <summary>
        /// Sets the base URL.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="uri">The URI.</param>
        /// <returns>The builder.</returns>
        public static IProximityEndPointBuilder UseUrlBase(this IProximityEndPointBuilder builder, Uri uri)
        {
            builder.Services.Configure<ProximityEndPointConfig>(c => c.UrlBase = uri);

            return builder;
        }
    }
}
