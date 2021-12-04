using Microsoft.Extensions.DependencyInjection;
using System;

namespace SignalR.Proximity.Notifier
{
    public interface ISignalRProximityNotifierFactory
    {
        INotifierBuilder<TContract> New<TContract>();
    }
    public class SignalRProximityNotifierFactory : ISignalRProximityNotifierFactory
    {
        private static IServiceProvider provider;
       
        public SignalRProximityNotifierFactory(IServiceProvider p)
        {
            provider = p; 
        }
          

        public INotifierBuilder<TContract> New<TContract>()
        {
            return provider.GetService<INotifierBuilder<TContract>>();
        }
        internal static ISignalRProximityNotifierFactory Get()
        {
           return  provider.GetService<ISignalRProximityNotifierFactory>();
        }
        public static INotifierBuilder<TContract> Create<TContract>()
        {
            return Get().New<TContract>();
        }
    }

}
