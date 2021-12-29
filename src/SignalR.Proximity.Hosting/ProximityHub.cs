using Microsoft.AspNetCore.SignalR;

namespace SignalR.Proximity.Hosting
{
    public class ProximityHub<TContract> : Hub
    {
        public virtual async void Interact(ProximityHubRequest request, object[] metaArgs)
        {
            await this.ResolveInteractAsync(request, metaArgs);
        }

        //public override Task OnConnectedAsync()
        //{
        //    var httpContext = Context.GetHttpContext();
        //    var request = httpContext.Request;
        //    var cnxId = Context.ConnectionId;
        //    var user = Context.User;  
        //    var userId = Context.UserIdentifier; 
        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
