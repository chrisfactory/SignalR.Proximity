using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface INotifier<TContract>
    {
        INotifierCaller<TContract> CreateCaller(NotifierScopeDefinition scope);
    }

    public interface INotifierCaller<TContract>
    {
        Task NotifyAsync(Action<TContract> action);
    }
}
