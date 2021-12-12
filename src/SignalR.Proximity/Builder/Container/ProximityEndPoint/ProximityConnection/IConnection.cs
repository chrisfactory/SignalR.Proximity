using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface IConnection<TContract>
    {
        INotifier<TContract> Notifier { get; }
        IClient<TContract> Client { get; }
        //object InfosAdmin { get; }
        Task<bool> StartAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
