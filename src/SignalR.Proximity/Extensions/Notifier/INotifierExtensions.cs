namespace SignalR.Proximity
{
    public static class INotifierExtensions
    {
        public static INotifierCaller<TContract> ToAllExcept<TContract>(this INotifier<TContract> source, params string[] excludedConnectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.AllExcept(excludedConnectionIds));
        }

        public static INotifierCaller<TContract> ToGroupExcept<TContract>(this INotifier<TContract> source, string groupName, params string[] excludedConnectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.GroupExcept(groupName, excludedConnectionIds));
        }

        public static INotifierCaller<TContract> ToClients<TContract>(this INotifier<TContract> source, string connectionId)
        {
            return source.CreateCaller(NotifierScopeDefinition.Client(connectionId));
        }

        public static INotifierCaller<TContract> ToClients<TContract>(this INotifier<TContract> source, params string[] connectionIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.Clients(connectionIds));
        }

        public static INotifierCaller<TContract> ToUsers<TContract>(this INotifier<TContract> source, string userId)
        {
            return source.CreateCaller(NotifierScopeDefinition.User(userId));
        }

        public static INotifierCaller<TContract> ToUsers<TContract>(this INotifier<TContract> source, params string[] userIds)
        {
            return source.CreateCaller(NotifierScopeDefinition.Users(userIds));
        }

        public static INotifierCaller<TContract> ToOthers<TContract>(this INotifier<TContract> source)
        {
            return source.CreateCaller(NotifierScopeDefinition.Others());
        }

        public static INotifierCaller<TContract> ToOthersInGroup<TContract>(this INotifier<TContract> source, string group)
        {
            return source.CreateCaller(NotifierScopeDefinition.OthersInGroup(group));
        }

        public static INotifierCaller<TContract> ToAll<TContract>(this INotifier<TContract> source)
        {
            return source.CreateCaller(NotifierScopeDefinition.All());
        }

        public static INotifierCaller<TContract> ToGroups<TContract>(this INotifier<TContract> source, string groupName)
        {
            return source.CreateCaller(NotifierScopeDefinition.Group(groupName));
        }

        public static INotifierCaller<TContract> ToGroups<TContract>(this INotifier<TContract> source, params string[] groups)
        {
            return source.CreateCaller(NotifierScopeDefinition.Groups(groups));
        }

    }
}
