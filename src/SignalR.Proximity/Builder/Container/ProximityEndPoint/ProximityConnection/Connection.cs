using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class Connection<TContract> : IConnection<TContract>
    {
        private readonly HubConnection _connection;
        private readonly IRetryPolicy _policy;
        private readonly Lazy<INotifier<TContract>> _notifier;
        private readonly Lazy<IClient<TContract>> _client;
       
        public Connection(
            HubConnection connection,
            IRetryPolicy policy,
            Lazy<INotifier<TContract>> notifier,
            Lazy<IClient<TContract>> client)
        {
            _connection = connection;
            _policy = policy;
            _notifier = notifier;
            _client = client;
        }


        public INotifier<TContract> Notifier { get { return _notifier.Value; } }
        public IClient<TContract> Client { get { return _client.Value; } }

        public string? ConnectionId { get { return _connection.ConnectionId; } }

        public Task<bool> StartAsync(CancellationToken cancellationToken = default(CancellationToken)) => _connection.StartWithRetryAsync(_policy, cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken = default(CancellationToken)) => _connection.StopAsync(cancellationToken);

    }
}
