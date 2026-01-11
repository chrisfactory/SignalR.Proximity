using Microsoft.AspNetCore.SignalR;

namespace SignalR.Proximity.Hosting
{
    /// <summary>
    /// Represents a Proximity hub.
    /// </summary>
    /// <typeparam name="TContract">The contract type.</typeparam>
    public class ProximityHub<TContract> : Hub
    {
        /// <summary>
        /// Interacts with the hub.
        /// </summary>
        /// <param name="request">The hub request.</param>
        /// <param name="metaArgs">The metadata arguments.</param>
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
