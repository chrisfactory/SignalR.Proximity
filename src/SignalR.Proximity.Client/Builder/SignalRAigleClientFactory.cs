using Microsoft.Extensions.DependencyInjection; 
using System; 

namespace SignalR.Proximity.Client
{

    public interface ISignalRProximityClientFactory
    {
        IClientBuilder<TContract> New<TContract>();
    }
    public class SignalRProximityClientFactory : ISignalRProximityClientFactory
    {
        private static IServiceProvider provider;

        public SignalRProximityClientFactory(IServiceProvider p)
        { 
            provider = p;
        }
        public IClientBuilder<TContract> New<TContract>()
        {
            return provider.GetService<IClientBuilder<TContract>>();
        }
        internal static ISignalRProximityClientFactory Get()
        {
            return provider.GetService<ISignalRProximityClientFactory>();
        }
        public static IClientBuilder<TContract> Create<TContract>()
        {
            return Get().New<TContract>();
        }
    }
}
