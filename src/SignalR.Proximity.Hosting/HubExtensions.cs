using Microsoft.AspNetCore.SignalR; 
using System.Threading.Tasks;

namespace SignalR.Proximity
{

    public static class HubExtensions
    {
        internal static async Task ResolveInteractAsync(this Hub hub, ProximityHubRequest request, object[] data)
        {
            if (request == null)
                return;
            if (request.Scope == null)
                return;
            if (string.IsNullOrWhiteSpace(request.Scope.Request))
                return;


            var verbs = request.Scope.Request.Split('.');
            if (verbs.Length > 0)
            {
                switch (verbs[0])
                {
                    case "Notify":
                    {
                        if (string.IsNullOrWhiteSpace(request.Argument))
                            return;
                        IClientProxy clientProxy = ResolveInteractWithClientsProxy(hub, request.Scope);
                         await clientProxy?.SendCoreAsync(request.Argument, data);
                    }
                    break;
                    case "Client":
                    {
                        await ResolveInteractClient(hub, request.Scope);
                    }
                    break;
                    default:
                        break;
                }
            }
        }


        private static IClientProxy ResolveInteractWithClientsProxy(Hub hub, ScopeDefinitionBase scope)
        {
            IClientProxy proxy = null;
            switch (scope.Request)
            {
                case "Notify.All":
                    proxy = hub.Clients.All;
                    break;
                case "Notify.All.Except":
                    proxy = hub.Clients.AllExcept(scope.Arguments);
                    break;
                case "Notify.Group":
                    proxy = hub.Clients.Group(scope.Argument);
                    break;
                case "Notify.Groups":
                    proxy = hub.Clients.Groups(scope.Arguments);
                    break;
                case "Notify.Group.Except":
                    proxy = hub.Clients.GroupExcept(scope.Argument, scope.Arguments);
                    break;
                case "Notify.Client":
                    proxy = hub.Clients.Client(scope.Argument);
                    break;
                case "Notify.Clients":
                    proxy = hub.Clients.Clients(scope.Arguments);
                    break;
                case "Notify.User":
                    proxy = hub.Clients.User(scope.Argument);
                    break;
                case "Notify.Users":
                    proxy = hub.Clients.Users(scope.Arguments);
                    break;
                case "Notify.Others":
                    proxy = hub.Clients.Others;
                    break;
                case "Notify.Others.In.Group":
                    proxy = hub.Clients.OthersInGroup(scope.Argument);
                    break;
                default:
                    break;
            }
            return proxy;
        }


        private static async Task ResolveInteractClient(Hub hub, ScopeDefinitionBase scope)
        {
            switch (scope.Request)
            { 
                case "Client.Join.Groups":
                    if (scope.Arguments != null)
                        foreach (var item in scope.Arguments)
                        {
                            await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, item);
                        }
                    break; 
                case "Client.Quit.Groups":
                    if (scope.Arguments != null)
                        foreach (var item in scope.Arguments)
                        {
                            await hub.Groups.RemoveFromGroupAsync(hub.Context.ConnectionId, item);
                        }
                    break;

                default:
                    break;
            }
        }
    }
}
