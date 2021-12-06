using System.Collections.Generic;

namespace NotifyR.Common
{
    public class NotifyRRequest
    {

        public string Argument { get; set; }
        public IReadOnlyList<string> Arguments { get; set; }

        public ScopeDefinitionBase Scope { get; set; }
    }
}
