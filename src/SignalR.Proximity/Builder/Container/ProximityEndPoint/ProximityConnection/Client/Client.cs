using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class Client<TContract> : IClient<TContract>
    {
        private readonly HubConnection _connection;
        private readonly IContractDescriptor<TContract> _descriptor;
        public Client(HubConnection cnx, IContractDescriptor<TContract> descriptor)
        {
            this._connection = cnx;
            this._descriptor = descriptor;
        }
        public void Attach<T>(T instance) where T : class, TContract
        {
            HubConnection cnx = _connection;
            foreach (var item in this._descriptor.GetDescription<T>(instance))
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
            if (groups != null && _connection.State == HubConnectionState.Connected)
                await _connection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.JoinGroups(groups) }, new object[0]); 
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
