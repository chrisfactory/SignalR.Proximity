using System;
using SignalR.Proximity.Client;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {
        /// <summary>
        ///     Configures the <see cref="ClientProxy"/> to use the user identifier
        ///     to catch notifications to the user. 
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="userId">
        ///    The user identifier to catch notifications to the user.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurableContainerBuilder<ClientProxyProvider, TContract> WithUser<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, string userId)
        {
            source.WithUser(() => userId);
            return source;
        }


        /// <summary>
        ///     Configures the <see cref="ClientProxy"/> to use the user identifier provider
        ///     to catch notifications to the user. 
        /// </summary>
        ///<typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        ///</typeparam>
        /// <param name="source">
        ///     The <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="provideUserId">
        ///    The user identifier provider to catch notifications to the user.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConfigurableContainerBuilder{ClientProxyProvider, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConfigurableContainerBuilder<ClientProxyProvider, TContract> WithUser<TContract>(this IConfigurableContainerBuilder<ClientProxyProvider, TContract> source, Func<string> provideUserId)
        {
            source.UseConfiguration(c =>
            {
                c.WithUserProvider(t =>
                {
                    t.UserId = provideUserId();
                });
            });
            return source;
        }
    }
}
 