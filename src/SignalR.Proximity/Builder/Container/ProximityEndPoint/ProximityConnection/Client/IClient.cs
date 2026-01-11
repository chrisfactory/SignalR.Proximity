using System.Threading.Tasks;

namespace SignalR.Proximity
{
    /// <summary>
    /// Defines a client for handling callbacks of a contract.
    /// </summary>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public interface IClient<TContract>
    {
        /// <summary>
        /// Attaches an implementation instance to handle contract callbacks.
        /// </summary>
        /// <typeparam name="T">The type of the instance, must implement the contract.</typeparam>
        /// <param name="instance">The instance to attach.</param>
        void Attach<T>(T instance) where T : class, TContract;

        /// <summary>
        /// Detaches an implementation instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance, must implement the contract.</typeparam>
        /// <param name="instance">The instance to detach.</param>
        void Dettach<T>(T instance) where T : class, TContract;

        /// <summary>
        /// Joins the specified groups.
        /// </summary>
        /// <param name="groups">The groups to join.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task JoinGroupsAsync(params string[] groups);

        /// <summary>
        /// Quits the specified groups.
        /// </summary>
        /// <param name="groups">The groups to quit.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task QuitGroupsAsync(params string[] groups);
    }
}
