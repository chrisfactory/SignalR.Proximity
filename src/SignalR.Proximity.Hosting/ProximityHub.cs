using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalR.Proximity.Hosting
{
    public class ProximityHub<TContract> : Hub
    {
        public virtual async void Interact(ProximityHubRequest request, object[] metaArgs)
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

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
