using Microsoft.AspNetCore.SignalR.Client;

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
            throw new System.NotImplementedException();
        }
    }
}
