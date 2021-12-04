using System; 
using Microsoft.Extensions.DependencyInjection; 

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurationBuilder{TConfig}"/>.
    /// </summary>
    public static partial class IConfigurationBuilderExtensions
    {
        /// <summary>
        ///     Configure the token provider instance that will be called to return a token for each HTTP request.
        /// </summary>
        /// <typeparam name="TConfig">
        ///    The type instance that inherits the abstraction from <see cref="SignalRProximityConfiguration"/>.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{TConfig}"/> to configure
        /// </param>
        /// <param name="configureToken"></param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithTokenProvider<TConfig>(this IConfigurationBuilder<TConfig> source, Action<ITokenProvider> configureToken)
          where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                configureToken(c.Current.TokenProvider);
            });
            return source;
        }

        /// <summary>
        ///     Sets the token provider instance that will be called to return a token for each HTTP request.
        /// </summary>
        /// <typeparam name="TConfig">
        ///    The type instance that inherits the abstraction from <see cref="SignalRProximityConfiguration"/>.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurationBuilder{TConfig}"/> to configure
        /// </param>
        /// <param name="tokenProvider"></param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurationBuilder{TConfig}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurationBuilder<TConfig> WithTokenProvider<TConfig>(this IConfigurationBuilder<TConfig> source, ITokenProvider tokenProvider)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c =>
            {
                c.Current.TokenProvider = tokenProvider;//?? new DefaultTokenProvider();
            });
            return source;
        }

    }
} 