using System.Collections.Generic; 
namespace SignalR.Proximity
{
    internal class ProximityHubRequest
    {
        public string Argument { get; set; }
        public IReadOnlyList<string> Arguments { get; set; }

        public ScopeDefinitionBase Scope { get; set; }
    }
}
