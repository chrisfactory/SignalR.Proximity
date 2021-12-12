using System;

namespace SignalR.Proximity
{
    internal interface INotifierBuilder<TContract>: IServicesBuilder
    {
        Lazy<INotifier<TContract>> Build();
    }
}
