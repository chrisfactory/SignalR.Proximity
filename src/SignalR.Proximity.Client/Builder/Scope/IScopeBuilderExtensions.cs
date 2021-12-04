using Microsoft.Extensions.DependencyInjection; 
using SignalR.Proximity.Common;

namespace SignalR.Proximity.Client
{

    /// <summary>
    ///  Extension methods for <see cref="IScopeBuilder{ClientProxyProvider, TContract}"/>.
    /// </summary>
    public static class IScopeBuilderExtensions
    {

        public static IConsumeBuilder<ClientProxyProvider, TContract> WithGroups<TContract>(this IScopeBuilder<ClientProxyProvider, TContract> source, params string[] groups)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = ClientScopeDefinition.JoinGroups(groups); });
            return source;
        }

    }


}
