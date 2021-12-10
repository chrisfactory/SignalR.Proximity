using Microsoft.Extensions.DependencyInjection; 

namespace SignalR.Proximity
{
     
    public static partial class IProximityNotifierBuilderExtensions
    {
        public static IProximityNotifierBuilder<TContract> UseScopeAllExcept<TContract>(this IProximityNotifierBuilder<TContract> source, params string[] excludedConnectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.AllExcept(excludedConnectionIds); });
            return source;
        }

        public static IProximityNotifierBuilder<TContract> UseScopeGroupExcept<TContract>(this IProximityNotifierBuilder<TContract> source, string groupName, params string[] excludedConnectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.GroupExcept(groupName, excludedConnectionIds); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeClients<TContract>(this IProximityNotifierBuilder<TContract> source, string connectionId)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Client(connectionId); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeClients<TContract>(this IProximityNotifierBuilder<TContract> source, params string[] connectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Clients(connectionIds); });
            return source;
        }

        public static IProximityNotifierBuilder<TContract> UseScopeUsers<TContract>(this IProximityNotifierBuilder<TContract> source, string userId)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.User(userId); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeUsers<TContract>(this IProximityNotifierBuilder<TContract> source, params string[] userIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Users(userIds); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeOthers<TContract>(this IProximityNotifierBuilder<TContract> source)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Others(); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeOthersInGroup<TContract>(this IProximityNotifierBuilder<TContract> source, string group)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.OthersInGroup(group); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeAll<TContract>(this IProximityNotifierBuilder<TContract> source)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.All(); });
            return source;
        }
        public static IProximityNotifierBuilder<TContract> UseScopeGroups<TContract>(this IProximityNotifierBuilder<TContract> source, string groupName)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Group(groupName); });
            return source;
        }

        public static IProximityNotifierBuilder<TContract> UseScopeGroups<TContract>(this IProximityNotifierBuilder<TContract> source, params string[] groups)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Groups(groups); });
            return source;
        }

    }


}
