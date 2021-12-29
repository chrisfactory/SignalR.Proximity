using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface IConnection<TContract>
    {
        INotifier<TContract> Notifier { get; }
        IClient<TContract> Client { get; }


        string? ConnectionId { get; }

        Task<bool> StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
