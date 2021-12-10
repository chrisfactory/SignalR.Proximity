using Microsoft.Extensions.DependencyInjection;
using System; 
namespace SignalR.Proximity
{
    public static partial class IProximityClientBuilderExtensions
    {
        public static IProximityClientBuilder<TContract> WithGroups<TContract>(this IProximityClientBuilder<TContract> source, params string[] groups)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = ClientScopeDefinition.JoinGroups(groups); });
            return source;
        }
    }
}
