using System.Collections.Generic;
namespace SignalR.Proximity
{
    /// <summary>
    ///  Represents a type to define a SignalR notification scope.
    /// </summary>
    public class NotifierScopeDefinition : ScopeDefinitionBase
    {
        internal const string TargetAll = "Notify.All";
        internal const string TargetAllExcept = "Notify.All.Except";
        internal const string TargetGroup = "Notify.Group";
        internal const string TargetGroups = "Notify.Groups";
        internal const string TargetGroupExcept = "Notify.Group.Except";
        internal const string TargetClient = "Notify.Client";
        internal const string TargetClients = "Notify.Clients";
        internal const string TargetUser = "Notify.User";
        internal const string TargetUsers = "Notify.Users";
        internal const string TargetOthers = "Notify.Others";
        internal const string TargetOthersInGroup = "Notify.Others.In.Group";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifierScopeDefinition"/> 
        /// </summary>
        public NotifierScopeDefinition() : base() { }
        private NotifierScopeDefinition(string request) : base(request, null, null) { }
        private NotifierScopeDefinition(string request, IReadOnlyList<string> arguments) : base(request, arguments, null) { }
        private NotifierScopeDefinition(string request, string argument) : base(request, null, argument) { }
        private NotifierScopeDefinition(string request, IReadOnlyList<string> arguments, string argument) : base(request, arguments, argument) { }

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all clients connected to the hub.
        /// </summary>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition All() => new NotifierScopeDefinition(TargetAll);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all clients connected to the hub
        /// excluding the specified client connections.
        /// </summary>
        /// <param name="excludedConnectionIds"> A collection of connection IDs to exclude.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition AllExcept(string[] excludedConnectionIds) => new NotifierScopeDefinition(TargetAllExcept, excludedConnectionIds);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all connections in the specified
        /// group.
        /// </summary>
        /// <param name="group">The group name.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Group(string group) => new NotifierScopeDefinition(TargetGroup, group);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all connections in all of the
        /// specified groups.
        /// </summary>
        /// <param name="groups"> The group names.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Groups(string[] groups) => new NotifierScopeDefinition(TargetGroups, groups);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all connections in the specified
        /// group excluding the specified connections.
        /// </summary>
        /// <param name="group">The group name.</param>
        /// <param name="excludedConnectionIds">A collection of connection IDs to exclude.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition GroupExcept(string group, string[] excludedConnectionIds) => new NotifierScopeDefinition(TargetGroupExcept, excludedConnectionIds, group);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on the specified client connection.
        /// </summary>
        /// <param name="connectionId">The connection ID.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Client(string connectionId) => new NotifierScopeDefinition(TargetClient, connectionId);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on the specified client connections.
        /// </summary>
        /// <param name="connectionIds">The connection IDs.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Clients(string[] connectionIds) => new NotifierScopeDefinition(TargetClients, connectionIds);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all connections associated with
        /// the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition User(string userId) => new NotifierScopeDefinition(TargetUser, userId);

        /// <summary>
        /// Gets the scope that can be used to invoke methods on all connections associated with
        /// all of the specified users.
        /// </summary>
        /// <param name="userIds">The user IDs.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Users(string[] userIds) => new NotifierScopeDefinition(TargetUsers, userIds);

        /// <summary>
        /// Gets the scope that to all connections except the one which triggered the current invocation.
        /// </summary>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition Others() => new NotifierScopeDefinition(TargetOthers);

        /// <summary>
        /// Gets the scope that to all connections in the specified group, except the one which
        /// triggered the current invocation.
        /// </summary>
        /// <param name="group">The group name.</param>
        /// <returns>The Scope.</returns>
        public static NotifierScopeDefinition OthersInGroup(string group) => new NotifierScopeDefinition(TargetOthersInGroup, group);

    }
}
