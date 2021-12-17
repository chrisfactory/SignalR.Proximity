using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class Notifier<TContract> : INotifier<TContract>
    {
        private readonly HubConnection _connection;
        public Notifier(HubConnection cnx)
        {
            this._connection = cnx;
        }

        public INotifierCaller<TContract> CreateCaller(NotifierScopeDefinition scope)
        {
            return new Caller(_connection, scope);
        }

        private class Caller : INotifierCaller<TContract>
        {
            private HubConnection _connection;
            private NotifierScopeDefinition _scope;

            public Caller(HubConnection connection, NotifierScopeDefinition scope)
            {
                this._connection = connection;
                this._scope = scope;
            }

            public Task NotifyAsync(Action<TContract> action)
            {
                var proxy = DispatchProxy.Create<TContract, NotifierDispatchProxy>();
                (proxy as NotifierDispatchProxy)?.Attach(_connection, _scope);
                action?.Invoke(proxy);

                return Task.CompletedTask;
            }
        }
    }
}
