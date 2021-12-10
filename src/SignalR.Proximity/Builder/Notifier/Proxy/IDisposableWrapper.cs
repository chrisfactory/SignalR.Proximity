using System;
namespace SignalR.Proximity
{
    public interface IDisposableWrapper<T> : IDisposable
    {
        T Proxy { get; }
    }
     
}
