using System; 
using Microsoft.AspNetCore.Http.Connections.Client; 
using SignalR.Proximity.Client;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {
        /// <summary>
        ///     Configures the <see cref="ClientProxy"/> to use HTTP-based
        ///     transports to connect to the specified URL. 
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="url">
        ///     The URL the <see cref="HttpConnection"/> will
        ///     use.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurableContainerBuilder<ClientProxyProvider, TContract> WithUrl<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, Uri url)
        {
            source.UseConfiguration(c => c.WithUrl(url));
            return source;
        }

        /// <summary>
        ///     Configures the <see cref="ClientProxy"/> to use HTTP-based
        ///     transports to connect to the specified URL. 
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="url">
        ///     The URL the <see cref="HttpConnection"/> will
        ///     use.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurableContainerBuilder<ClientProxyProvider, TContract> WithUrl<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, string url)
        {
            source.UseConfiguration(c => c.WithUrl(url));
            return source;
        }
    }
}
 