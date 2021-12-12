using System;

namespace SignalR.Proximity
{
    internal interface IClientBuilder<TContract> : IServicesBuilder
    {
        Lazy<IClient<TContract>> Build();
    }
}
