using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    /// <summary>
    /// Represents a connection bound to a contract.
    /// </summary>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public interface IConnection<TContract>
    {
        /// <summary>
        /// Gets the notifier for sending notifications.
        /// </summary>
        INotifier<TContract> Notifier { get; }

        /// <summary>
        /// Gets the client for handling incoming requests.
        /// </summary>
        IClient<TContract> Client { get; }


        /// <summary>
        /// Gets the connection ID.
        /// </summary>
        string? ConnectionId { get; }

        /// <summary>
        /// Starts the connection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if connected successfully, false otherwise.</returns>
        Task<bool> StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops the connection.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
