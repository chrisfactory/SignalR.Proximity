using SignalR.Proximity.Common;

namespace SignalR.Proximity.Client
{
    public interface IClientBuilder<TContract> : IConfigurableContainerBuilder<ClientProxyProvider, TContract> { }
}
