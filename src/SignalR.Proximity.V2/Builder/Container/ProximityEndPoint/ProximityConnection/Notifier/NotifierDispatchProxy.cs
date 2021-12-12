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
             
                await _hubConnection.InvokeAsync("Interact", new ProximityHubRequest() { Argument = methodKey, Scope = _scope }, metaArgs);
        }


        internal void Attach(HubConnection hubConnection, ScopeDefinitionBase scope)
        {
            _hubConnection = hubConnection;
            _scope = scope; 
        }
         

    }
}
