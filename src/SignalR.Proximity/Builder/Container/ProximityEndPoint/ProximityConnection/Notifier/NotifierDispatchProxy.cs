using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public class NotifierDispatchProxy : DispatchProxy
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        private HubConnection _hubConnection;
        private ScopeDefinitionBase _scope;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        internal void Attach(HubConnection hubConnection, ScopeDefinitionBase scope)
        {
            _hubConnection = hubConnection;
            _scope = scope;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null)
                return null;

            var methodKey = targetMethod.ToString()??string.Empty;
            if (targetMethod.IsGenericMethod)
                throw new InvalidOperationException($"{methodKey} ne peut être generique");
            if (targetMethod.ReturnType != typeof(void))
                throw new InvalidOperationException($"{methodKey} ReturnType ne peut être différent de void");
            _= Task.Factory.StartNew(async () => await _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Argument = methodKey, Scope = _scope }, args));

            return null;
        }

    }
}
