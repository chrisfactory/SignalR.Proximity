using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection;
namespace SignalR.Proximity
{
    internal class NotifierProxy<TContract> : INotifierProxy<TContract>
    {
        private readonly IDisposable _disposabe;

        public NotifierProxy(HubConnection hubConnection, INotifierRetryPolicy retryPolicy, ScopeDefinitionBase scope)
        {
            var proxy = DispatchProxy.Create<TContract, NotifierDispatchProxy>();

            if (proxy is NotifierDispatchProxy dispatcher)
                dispatcher.Attach(hubConnection, retryPolicy, scope);

            Proxy = proxy;
            _disposabe = proxy as IDisposable;
        }

        public TContract Proxy { get; }

        public void Dispose()
        {
            _disposabe?.Dispose();
        }
    }
}
