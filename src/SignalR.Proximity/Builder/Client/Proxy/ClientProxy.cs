using Microsoft.AspNetCore.SignalR.Client; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
 

    /// <summary>
    ///    Represents an instances of type <see cref="ClientProxy"/>.  
    /// </summary>
    internal class ClientProxy : IClientProxy
    {
        private object _sync = new object();
        private bool disposedValue = false;
        private HubConnection _hubConnection;
        private List<string> _groups = new List<string>();
        private IRetryPolicy _IRetryPolicy;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProxy"/> class.
        /// </summary>
        /// <param name="cnx">
        ///     A connection used to invoke hub methods on a SignalR Server.
        /// </param>
        /// <param name="scope">
        ///     A <see cref="ScopeDefinitionBase"/> that represents the abstract scope of SignalR perimeters notifications. 
        /// </param>
        /// <param name="retryPolicy">
        ///     An abstraction that controls when the client attempts to reconnect and how many times it does so.
        /// </param> 
        public ClientProxy(HubConnection cnx, ScopeDefinitionBase scope, IClientRetryPolicy retryPolicy)
        {
            _hubConnection = cnx;
            _hubConnection.Reconnected += ReconnectedHandler;
            _IRetryPolicy = retryPolicy;
            if (scope != null)
            {
                switch (scope.Request)
                {
                    case ClientScopeDefinition.ClientJoinGroups:
                        if (scope.Arguments != null)
                            foreach (var item in scope.Arguments)
                                _groups.Add(item);
                        break;
                    default:
                        break;
                }
                _groups = _groups.Distinct().ToList();
            }

        }




        /// <summary>
        ///     Starts a connection to the server.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task"/> that represents the asynchronous start.
        /// </returns>
        public async Task<bool> StartAsync()
        {
            bool result = await _hubConnection.StartWithRetryAsync(_IRetryPolicy, CancellationToken.None);
            if (result)
                await ResotreScope();
            return result;
        }


        /// <summary>
        ///     Stops a connection to the server.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task"/> that represents the asynchronous start.
        /// </returns>
        public Task StopAsync()
        {
            return _hubConnection.StopAsync();
        }


        /// <summary>
        ///     Adds a connection to the specified groups.
        /// </summary>
        /// <param name="groups">
        ///     The groups names.
        /// </param>
        public void JoinGroups(params string[] groups)
        {
            lock (_sync)
            {
                int oldCount = _groups.Count;
                int newCount = oldCount;
                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        _groups.Add(group);
                    }
                    _groups = _groups.Distinct().ToList();
                    newCount = _groups.Count;
                    if (oldCount != newCount && _hubConnection.State == HubConnectionState.Connected)
                        _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.JoinGroups(groups) }, new object[0]);
                }
            }
        }


        /// <summary>
        ///     Removes a connection from the specified groups.
        /// </summary>
        /// <param name="groups">
        ///     The groups names.
        /// </param>
        public void QuitGroups(params string[] groups)
        {
            lock (_sync)
            {
                int oldCount = _groups.Count;
                int newCount = oldCount;
                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        _groups.Remove(group);
                    }
                    newCount = _groups.Count;
                    if (oldCount != newCount && _hubConnection.State == HubConnectionState.Connected)
                        _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.QuitGroups(groups) }, new object[0]);
                }
            }
        }





        private Task ReconnectedHandler(string arg)
        {
            return ResotreScope();
        }
        private Task ResotreScope()
        {
            //if (_config.AutoRestoredGroups)  <-----------------------------------------------######
            lock (_sync)
            {
                if (_groups.Count > 0 && _hubConnection.State == HubConnectionState.Connected)
                    return _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Scope = ClientScopeDefinition.JoinGroups(_groups.ToArray()) }, new object[0]);
            }
            return Task.CompletedTask;
        }




        #region IDisposable Support
        /// <summary>
        ///     Disposes the <see cref="ClientProxy"/>.
        /// </summary>
        /// <param name="disposing">
        /// Flg is disposing
        /// </param>
        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    var cnx = _hubConnection;
                    if (cnx != null)
                    {
                        cnx.Reconnected -= ReconnectedHandler;
                        cnx.DisposeAsync();
                    }
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~DynamicSignalRClientProxy() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        /// <summary>
        ///     Disposes the <see cref="ClientProxy"/>.
        /// </summary>
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }


        #endregion

    }
}
