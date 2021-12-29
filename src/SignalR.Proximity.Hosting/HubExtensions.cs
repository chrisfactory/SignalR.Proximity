using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
namespace SignalR.Proximity
{

    internal static class HubExtensions
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
                            await clientProxy.SendCoreAsync(request.Argument, data);
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
            IClientProxy? proxy;
            switch (scope.Request)
            {
                case "Notify.All":
                    proxy = hub.Clients.All;
                    break;
                case "Notify.All.Except":
                    {
                        if (scope.Arguments == null)
                            throw new NullReferenceException(nameof(scope.Arguments));
                        proxy = hub.Clients.AllExcept(scope.Arguments);
                    }
                    break;
                case "Notify.Group":
                    {
                        if (scope.Argument == null)
                            throw new NullReferenceException(nameof(scope.Argument));
                        proxy = hub.Clients.Group(scope.Argument);
                    }
                    break;
                case "Notify.Groups":
                    {
                        if (scope.Arguments == null)
                            throw new NullReferenceException(nameof(scope.Arguments));
                        proxy = hub.Clients.Groups(scope.Arguments);
                    }
                    break;
                case "Notify.Group.Except":
                    {
                        if (scope.Arguments == null)
                            throw new NullReferenceException(nameof(scope.Arguments));
                        if (scope.Argument == null)
                            throw new NullReferenceException(nameof(scope.Argument));
                        proxy = hub.Clients.GroupExcept(scope.Argument, scope.Arguments);
                    }
                    break;
                case "Notify.Client":
                    {
                        if (scope.Argument == null)
                            throw new NullReferenceException(nameof(scope.Argument));
                        proxy = hub.Clients.Client(scope.Argument);
                    }
                    break;
                case "Notify.Clients":
                    {
                        if (scope.Arguments == null)
                            throw new NullReferenceException(nameof(scope.Arguments));
                        proxy = hub.Clients.Clients(scope.Arguments);
                    }
                    break;
                case "Notify.User":
                    {
                        if (scope.Argument == null)
                            throw new NullReferenceException(nameof(scope.Argument));
                        proxy = hub.Clients.User(scope.Argument);
                    }
                    break;
                case "Notify.Users":
                    {
                        if (scope.Arguments == null)
                            throw new NullReferenceException(nameof(scope.Arguments));
                        proxy = hub.Clients.Users(scope.Arguments);
                    }
                    break;
                case "Notify.Others":
                    proxy = hub.Clients.Others;
                    break;
                case "Notify.Others.In.Group":
                    {
                        if (scope.Argument == null)
                            throw new NullReferenceException(nameof(scope.Argument));
                        proxy = hub.Clients.OthersInGroup(scope.Argument);
                    }
                    break;
                default:
                    throw new InvalidOperationException(nameof(proxy));
            }
            if (proxy == null)
                throw new NullReferenceException(scope.Request);
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
