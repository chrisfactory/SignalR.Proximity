using System.Collections.Generic;
namespace SignalR.Proximity
{
    public class ScopeDefinitionBase
    {
        public ScopeDefinitionBase()
        {

        }
        protected ScopeDefinitionBase(string request, IReadOnlyList<string>? arguments, string? argument)
        {
            Request = request;
            Arguments = arguments;
            Argument = argument;
        }
        public string? Request { get; set; }
        public IReadOnlyList<string>? Arguments { get; set; }
        public string? Argument { get; set; }
    }
}
