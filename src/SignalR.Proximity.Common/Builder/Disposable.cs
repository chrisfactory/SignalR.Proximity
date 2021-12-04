using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{
    public sealed class Disposable<T> : IDisposable
    {
        public T Proxy { get { return _baseObject; } }

        private readonly T _baseObject;
        private readonly IDisposable _disposabe;

        public Disposable(T obj)
        {
            _baseObject = obj;
            _disposabe = obj as IDisposable;
        }

        public void Dispose()
        {
            _disposabe?.Dispose();
        }
    }
}
