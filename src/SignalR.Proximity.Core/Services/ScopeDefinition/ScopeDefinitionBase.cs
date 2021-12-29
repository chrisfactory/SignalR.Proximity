using System.Collections.Generic;
namespace SignalR.Proximity
{
    /// <summary>
    /// Represents a base type to define a SignalR notification scope.
    /// </summary>
    public class ScopeDefinitionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeDefinitionBase"/> class.
        /// </summary>
        public ScopeDefinitionBase() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeDefinitionBase"/>.
        /// </summary>
        /// <param name="request" cref="Request"/>
        /// <param name="arguments" cref="Arguments"/>
        /// <param name="argument" cref="Argument"/>
        protected ScopeDefinitionBase(string request, IReadOnlyList<string>? arguments, string? argument)
        {
            Request = request;
            Arguments = arguments;
            Argument = argument;
        }

        /// <summary>
        /// Represents the target query.
        /// </summary>
        public string? Request { get; set; }

        /// <summary>
        /// Represents the query arguments.
        /// </summary>
        public IReadOnlyList<string>? Arguments { get; set; }

        /// <summary>
        /// Represents the query argument.
        /// </summary>
        public string? Argument { get; set; }
    }
}
