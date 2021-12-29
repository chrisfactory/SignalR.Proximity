namespace SignalR.Proximity.Core
{
    /// <summary>
    /// Represents the basic configuration. 
    /// </summary>
    public class ProximityConfigurationCore
    {
        /// <summary>
        /// The route pattern.
        /// </summary>
        public string? PatternBase { get; set; }
        /// <summary>
        /// Pattern postfix for the Hub url.
        /// </summary>
        public string? PatternPostfix { get; set; }
        /// <summary>
        /// Pattern postfix Machine Name for the Hub url. 
        /// </summary>
        public bool PatternMachineNamePostfix { get; set; }
    }
}
