using System.Collections.Generic;

namespace SignalR.Proximity
{
    /// <summary>
    ///  Represents a type used to configure the <see cref="ClientScopeDefinition"/>.
    /// </summary>
    public class ClientScopeDefinition : ScopeDefinitionBase
    {
        internal const string ClientJoinGroups = "Client.Join.Groups";
        internal const string ClientQuitGroups = "Client.Quit.Groups";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientScopeDefinition"/> class.
        /// </summary>
        public ClientScopeDefinition() : base() { }

        //private ClientScopeDefinition(string request) : base(request, null, null) { }
        private ClientScopeDefinition(string request, IReadOnlyList<string> arguments) : base(request, arguments, null) { }
        //private ClientScopeDefinition(string request, string argument) : base(request, null, argument) { }
        //private ClientScopeDefinition(string request, IReadOnlyList<string> arguments, string argument) : base(request, arguments, argument) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientScopeDefinition"/> class.
        /// </summary>
        /// <param name="groups">
        ///    The groups names.
        /// </param>
        /// <returns>
        ///     Instance of the <see cref="ClientScopeDefinition"/> class represents a request to join groups.
        /// </returns>
        public static ClientScopeDefinition JoinGroups(string[] groups) => new ClientScopeDefinition(ClientJoinGroups, groups);

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientScopeDefinition"/> class.
        /// </summary>
        /// <param name="groups">
        ///    The groups names.
        /// </param>
        /// <returns>
        ///     Instance of the <see cref="ClientScopeDefinition"/> class represents a request to quit groups.
        /// </returns>
        public static ClientScopeDefinition QuitGroups(string[] groups) => new ClientScopeDefinition(ClientQuitGroups, groups);

    }
}
