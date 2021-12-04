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
        public static IConfigurationBuilder<TConfig> WithUserProvider<TConfig>(this IConfigurationBuilder<TConfig> source, Action<IUserProvider> configureUser)
          where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                configureUser(c.Current.UserProvider);
            });
            return source;
        }
        public static IConfigurationBuilder<TConfig> WithUserProvider<TConfig>(this IConfigurationBuilder<TConfig> source, IUserProvider userProvider)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.UserProvider = userProvider ?? new DefaultUserProvider();
            });
            return source;
        }

    }
}
