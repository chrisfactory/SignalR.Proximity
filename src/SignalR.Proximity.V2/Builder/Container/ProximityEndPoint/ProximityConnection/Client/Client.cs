using Microsoft.AspNetCore.SignalR.Client;

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
            //throw new System.NotImplementedException();
        }

        public void Dettach<T>(T instance) where T : class, TContract
        {
            //  throw new System.NotImplementedException();
        }

        public void JoinGroups(params string[] groups)
        {
            // throw new System.NotImplementedException();
        }

        public void QuitGroups(params string[] groups)
        {
            // throw new System.NotImplementedException();
        }
    }
}
