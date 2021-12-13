using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public class NotifierDispatchProxy : DispatchProxy
    {
        private HubConnection _hubConnection;
        private ScopeDefinitionBase _scope;

        internal void Attach(HubConnection hubConnection, ScopeDefinitionBase scope)
        {
            _hubConnection = hubConnection;
            _scope = scope;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var methodKey = targetMethod.ToString();
            if (targetMethod.IsGenericMethod)
                throw new InvalidOperationException($"{methodKey} ne peut être generique");
            if (targetMethod.ReturnType != typeof(void))
                throw new InvalidOperationException($"{methodKey} ReturnType ne peut être différent de void");

            var task = _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Argument = methodKey, Scope = _scope }, args);
            var aw = task.GetAwaiter();
            aw.GetResult();
            return null;
        } 

    }
}
