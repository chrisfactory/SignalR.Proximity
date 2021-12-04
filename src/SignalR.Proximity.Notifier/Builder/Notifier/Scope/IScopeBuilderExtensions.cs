using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Notifier;

namespace SignalR.Proximity.Common
{

    /// <summary>
    ///  Extension methods for <see cref="IScopeBuilder{NotifierProxyProvider,TContract}"/>.
    /// </summary>
    public static class IScopeBuilderExtensions
    {
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeAllExcept<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, params string[] excludedConnectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.AllExcept(excludedConnectionIds); });
            return source;
        }

        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeGroupExcept<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, string groupName, params string[] excludedConnectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.GroupExcept(groupName, excludedConnectionIds); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeClients<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, string connectionId)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Client(connectionId); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeClients<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, params string[] connectionIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Clients(connectionIds); });
            return source;
        }

        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeUsers<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, string userId)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.User(userId); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeUsers<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, params string[] userIds)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Users(userIds); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeOthers<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Others(); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeOthersInGroup<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, string group)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.OthersInGroup(group); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeAll<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.All(); });
            return source;
        }
        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeGroups<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, string groupName)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Group(groupName); });
            return source;
        }

        public static IConsumeBuilder<NotifierProxyProvider, TContract> UseScopeGroups<TContract>(this IScopeBuilder<NotifierProxyProvider, TContract> source, params string[] groups)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = NotifierScopeDefinition.Groups(groups); });
            return source;
        }

    }


}
