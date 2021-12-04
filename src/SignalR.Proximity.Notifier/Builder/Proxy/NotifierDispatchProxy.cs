using Microsoft.AspNetCore.SignalR.Client;
using SignalR.Proximity.Common;
using System; 
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity.Notifier
{
    public class NotifierDispatchProxy : DispatchProxy, IDisposable
    {
        private bool disposedValue = false;
        private HubConnection _hubConnection;
        private SignalRProximityNotifierConfiguration _configuration;
        private ScopeDefinitionBase _scope;
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Task.Run(async () =>
            {
                await InnerInvoke(targetMethod, args);
            }).Wait();
            return null;
        }
        private async Task InnerInvoke(MethodInfo targetMethod, object[] metaArgs)
        {
            var methodKey = targetMethod.ToString();
            if (targetMethod.IsGenericMethod)
                throw new InvalidOperationException($"{methodKey} ne peut être generique");
            if (targetMethod.ReturnType != typeof(void))
                throw new InvalidOperationException($"{methodKey} ReturnType ne peut être différent de void");

            if (await _hubConnection.StartWithRetryAsync(_configuration.RetryPolicy, CancellationToken.None))
                await _hubConnection.InvokeAsync("Interact", new SignalRProximityRequest() { Argument = methodKey, Scope = _scope }, metaArgs);
        }




        internal void Attach(HubConnection hubConnection, SignalRProximityNotifierConfiguration configuration, ScopeDefinitionBase scope)
        {
            _hubConnection = hubConnection;
            _configuration = configuration;
            _scope = scope;
        }

        #region IDisposable Support
        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    var cnx = _hubConnection;
                    if (cnx != null)
                    {
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

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
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
