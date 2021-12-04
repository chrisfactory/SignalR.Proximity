using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SignalR.Proximity.Common
{ 
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{TConfig}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder<TConfig> WithUrl<TConfig>(this IConfigurationBuilder<TConfig> source, string url)
            where TConfig : SignalRProximityConfiguration, new()
        {
            return source.WithUrl(new Uri(url));
        }

        public static IConfigurationBuilder<TConfig> WithUrl<TConfig>(this IConfigurationBuilder<TConfig> source, Uri url)
            where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.UrlBase = url;
            });
            return source;
        }

    }
}
