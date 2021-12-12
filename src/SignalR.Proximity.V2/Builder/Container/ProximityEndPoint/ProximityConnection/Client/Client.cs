using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class Client<TContract> : IClient<TContract>
    {
        private readonly HubConnection _connection;
        public Client(HubConnection cnx)
        {
            this._connection = cnx;
        }
        public void Attach<T>(T instance) where T : class, TContract
        {
            HubConnection cnx = _connection;
            foreach (var item in MethodContractDescriptor.Create<TContract>(instance))
                cnx.On(item.Key, item.GetArgsTypes(), item.ReceiveAsync); 
        }

        public void Dettach<T>(T instance) where T : class, TContract
        {
            //HubConnection cnx = _connection;
            //foreach (var item in MethodContractDescriptor.Create(instance))
            //    cnx.().On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);
        }


        void DettachAll()
        {
            //HubConnection cnx = _connection;
            //foreach (var item in MethodContractDescriptor.Create(instance))
            //    cnx.Remove().On(item.Key, item.GetArgsTypes(), item.ReceiveAsync);
            //cnx.Remove()
        }


        /// <summary>
        ///     Adds a connection to the specified groups.
        /// </summary>
        /// <param name="groups">
        ///     The groups names.
        /// </param>
        public async Task JoinGroupsAsync(params string[] groups)
        {
            if (groups != null)
            {
                if (_connection.State == HubConnectionState.Connected)
                    await _connection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.JoinGroups(groups) }, new object[0]);
            }
        }


        /// <summary>
        ///     Removes a connection from the specified groups.
        /// </summary>
        /// <param name="groups">
        ///     The groups names.
        /// </param>
        public async Task QuitGroupsAsync(params string[] groups)
        {
            if (_connection.State == HubConnectionState.Connected)
                await _connection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.QuitGroups(groups) }, new object[0]);

        } 

    }
}
