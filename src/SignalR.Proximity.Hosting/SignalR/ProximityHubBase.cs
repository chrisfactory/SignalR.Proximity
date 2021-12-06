using Microsoft.AspNetCore.SignalR;
using SignalR.Proximity;
using SignalR.Proximity.Common;
using System;
using System.Threading.Tasks;

namespace SignalR.Proximity.Hubs
{  
    public abstract class ProximityHubBase : Hub
    { 

        public async void Interact(SignalRProximityRequest request, object[] metaArgs)
        {
            await this.ResolveInteractAsync(request, metaArgs);
        }
          
        public override Task OnConnectedAsync()
        {
            //var underlyingHttpContext = Context.GetHttpContext().Request.Path;
            //var ba = Context.GetHttpContext().Request.PathBase;
            //var user = Context.UserIdentifier;
            //Context.Request.Environment[typeof(HttpContextBase).FullName] as HttpContextBase;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
