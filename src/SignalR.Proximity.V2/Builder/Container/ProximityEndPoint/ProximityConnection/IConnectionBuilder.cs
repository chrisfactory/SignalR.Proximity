namespace SignalR.Proximity
{
    internal interface IConnectionBuilder<TContract> : IServicesBuilder
    {
        IConnection<TContract> Build();
    }
}
