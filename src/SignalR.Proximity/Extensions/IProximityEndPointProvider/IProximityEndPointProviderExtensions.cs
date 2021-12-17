namespace SignalR.Proximity
{
    public static partial class IProximityEndPointProviderExtensions
    {
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider)
        {
            return provider.Get().Connect<TContract>();
        }
        public static IConnection<TContract> Connect<TContract>(this IProximityEndPointProvider provider, string endPointName)
        {
            return provider.Get(endPointName).Connect<TContract>();
        }
    }
}
