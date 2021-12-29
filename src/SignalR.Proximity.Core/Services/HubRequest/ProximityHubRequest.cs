using System.Collections.Generic;
namespace SignalR.Proximity
{
    /// <summary>
    /// Represents a notification message.
    /// </summary>
    public class ProximityHubRequest
    {
        /// <summary>
        /// The notification argument. 
        /// </summary>
        public string? Argument { get; set; }

        /// <summary>
        /// The notification arguments. 
        /// </summary>
        public IReadOnlyList<string>? Arguments { get; set; }

        /// <summary>
        /// The scope of notification.
        /// </summary>
        public ScopeDefinitionBase? Scope { get; set; }
    }
}
