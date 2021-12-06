using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NotifyR.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyR.Hubs
{ 
    [Authorize(AuthenticationSchemes = "Bearer")]
    public abstract class BearerGenericHubBase : Hub
    { 

        public async void Interact(NotifyRRequest request, object[] metaArgs)
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
