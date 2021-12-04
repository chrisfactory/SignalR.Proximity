using SignalR.Proximity.Common.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{
    public interface IConsumeBuilder<T, TContract> : IServiceBuilder
    {
        T Build();
    }
}
