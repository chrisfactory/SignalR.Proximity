using SignalR.Proximity.Common;
using System; 

namespace SignalR.Proximity.Client
{
    [Serializable]
    /// <summary>
    ///    Represents a type used to configure the <see cref="ClientProxy"/>.
    /// </summary> 
    public class SignalRProximityClientConfiguration : SignalRProximityConfiguration
    {
        /// <summary>
        /// If true, defined restore the groups if connexion is restored
        /// </summary>
        public bool AutoRestoredGroups { get; set; }
    }
}
