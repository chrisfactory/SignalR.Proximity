namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the provider of Hub Url Pattern.
    /// </summary>
    public interface IPatternProvider
    {
        /// <summary>
        /// Provide the Hub Url Pattern
        /// </summary>
        /// <returns>Hub Url Pattern</returns>
        string GetPattern();
    }
}
