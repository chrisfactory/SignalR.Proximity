using Microsoft.Extensions.DependencyInjection;
using System; 
namespace SignalR.Proximity
{
    public static class IProximityClientBuilderExtensions
    {
        public static IProximityClientBuilder WithGroups(this IProximityClientBuilder source, params string[] groups)
        {
            source.Services.Configure<ScopeOptions>(c => { c.Scope = ClientScopeDefinition.JoinGroups(groups); });
            return source;
        }
    }
}
