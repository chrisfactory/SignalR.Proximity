using System.Collections.Generic;

namespace SignalR.Proximity.Common
{
    public class SignalRProximityRequest
    {

        public string Argument { get; set; }
        public IReadOnlyList<string> Arguments { get; set; }

        public ScopeDefinitionBase Scope { get; set; }
    }
}
