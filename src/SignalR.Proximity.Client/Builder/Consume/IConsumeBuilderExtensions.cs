using SignalR.Proximity.Common;
using System.Threading.Tasks;

namespace SignalR.Proximity.Client
{
    /// <summary>
    ///  Extension methods for <see cref="IConsumeBuilder{ClientProxyProvider, TContract}"/>.
    /// </summary>
    public static class IConsumeBuilderExtensions
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProxy"/> class and starts a connection to the server.
        /// </summary>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        /// </typeparam>
        /// <typeparam name="TInstance">
        ///    Represents a instance type to implemente the contract TContract
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="IConsumeBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="instance">
        ///    Represents a instance to implemente the contract TContract
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="ClientProxy"/>.
        /// </returns>
        public static ClientProxy AttachStart<TContract, TInstance>(this IConsumeBuilder<ClientProxyProvider, TContract> source, TInstance instance)
            where TInstance : class, TContract
        {
            var task = Task<ClientProxy>.Run(async () => await source.AttachStartAsync(instance));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProxy"/> class and starts a connection to the server.
        /// </summary>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        /// </typeparam>
        /// <typeparam name="TInstance">
        ///    Represents a instance type to implemente the contract TContract
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="IConsumeBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="instance">
        ///    Represents a instance to implemente the contract TContract
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="Task{ClientProxy}"/>.
        /// </returns>
        public static async Task<ClientProxy> AttachStartAsync<TContract, TInstance>(this IConsumeBuilder<ClientProxyProvider, TContract> source, TInstance instance)
             where TInstance : class, TContract
        {
            var proxy = source.Attach(instance);
            await proxy.StartAsync();
            return proxy;
        }


        /// <summary>
        ///   Initializes a new instance of the <see cref="ClientProxy"/> class.
        /// </summary>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the <see cref="ClientProxy"/> system.
        /// </typeparam>
        /// <typeparam name="TInstance">
        ///    Represents a instance type to implemente the contract TContract
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="IConsumeBuilder{ClientProxyProvider, TContract}"/> to configure
        /// </param>
        /// <param name="instance">
        ///    Represents a instance to implemente the contract TContract
        /// </param>
        /// <returns>
        ///     The instance of the <see cref="ClientProxy"/>.
        /// </returns>
        public static ClientProxy Attach<TContract, TInstance>(this IConsumeBuilder<ClientProxyProvider, TContract> source, TInstance instance)
             where TInstance : class, TContract
        {
            ClientProxy proxy = null;
            source.ConsumeCore(c =>
            {
                proxy = c.CreateProxy<TContract, TInstance>(instance);
            });
            return proxy;
        }
    }

}
