namespace SignalR.Proximity
{
    /// <summary>
    /// Extensions for <see cref="INotifier{TContract}"/> to create callers for specific scopes.
    /// </summary>
    public static class INotifierExtensions
    {
        /// <summary>
        /// Creates a caller for all connections except the specified ones.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="excludedConnectionIds">The excluded connection IDs.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToAllExcept<TContract>(this INotifier<TContract> source, params string[] excludedConnectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.AllExcept(excludedConnectionIds));
        }

        /// <summary>
        /// Creates a caller for a group except the specified connections.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="groupName">The group name.</param>
        /// <param name="excludedConnectionIds">The excluded connection IDs.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToGroupExcept<TContract>(this INotifier<TContract> source, string groupName, params string[] excludedConnectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.GroupExcept(groupName, excludedConnectionIds));
        }

        /// <summary>
        /// Creates a caller for a specific client.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="connectionId">The connection ID.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToClients<TContract>(this INotifier<TContract> source, string connectionId)
        {
            return source.CreateCaller(NotifierScopeDefinition.Client(connectionId));
        }

        /// <summary>
        /// Creates a caller for specific clients.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="connectionIds">The connection IDs.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToClients<TContract>(this INotifier<TContract> source, params string[] connectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.Clients(connectionIds));
        }

        /// <summary>
        /// Creates a caller for a specific user.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToUsers<TContract>(this INotifier<TContract> source, string userId)
        {
            return source.CreateCaller(NotifierScopeDefinition.User(userId));
        }

        /// <summary>
        /// Creates a caller for specific users.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="userIds">The user IDs.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToUsers<TContract>(this INotifier<TContract> source, params string[] userIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.Users(userIds));
        }

        /// <summary>
        /// Creates a caller for all other connections.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToOthers<TContract>(this INotifier<TContract> source)
        {
            return source.CreateCaller(NotifierScopeDefinition.Others());
        }

        /// <summary>
        /// Creates a caller for others in a specific group.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="group">The group name.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToOthersInGroup<TContract>(this INotifier<TContract> source, string group)
        {
            return source.CreateCaller(NotifierScopeDefinition.OthersInGroup(group));
        }

        /// <summary>
        /// Creates a caller for all connections.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToAll<TContract>(this INotifier<TContract> source)
        {
            return source.CreateCaller(NotifierScopeDefinition.All());
        }

        /// <summary>
        /// Creates a caller for a specific group.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="groupName">The group name.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToGroups<TContract>(this INotifier<TContract> source, string groupName)
        {
            return source.CreateCaller(NotifierScopeDefinition.Group(groupName));
        }

        /// <summary>
        /// Creates a caller for specific groups.
        /// </summary>
        /// <typeparam name="TContract">The contract type.</typeparam>
        /// <param name="source">The notifier source.</param>
        /// <param name="groups">The group names.</param>
        /// <returns>A notifier caller.</returns>
        public static INotifierCaller<TContract> ToGroups<TContract>(this INotifier<TContract> source, params string[] groups)
        {
            return source.CreateCaller(NotifierScopeDefinition.Groups(groups));
        }

    }
}
