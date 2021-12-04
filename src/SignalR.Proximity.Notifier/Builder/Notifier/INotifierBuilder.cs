using SignalR.Proximity.Common;

namespace SignalR.Proximity.Notifier
{

    public interface INotifierBuilder<TContract> : IConfigurableContainerBuilder<NotifierProxyProvider, TContract>
    {
    }
}
