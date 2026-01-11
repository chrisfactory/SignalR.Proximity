using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    /// <summary>
    /// Defines a notifier for a specific contract.
    /// </summary>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public interface INotifier<TContract>
    {
        /// <summary>
        /// Creates a caller for the specified scope.
        /// </summary>
        /// <param name="scope">The scope definition.</param>
        /// <returns>A notifier caller.</returns>
        INotifierCaller<TContract> CreateCaller(NotifierScopeDefinition scope);
    }

    /// <summary>
    /// Defines a caller that can notify clients using a contract.
    /// </summary>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public interface INotifierCaller<TContract>
    {
        /// <summary>
        /// Notifies clients by executing the specified action on the contract.
        /// </summary>
        /// <param name="action">The action to invoke on the contract.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task NotifyAsync(Action<TContract> action);
    }
}
