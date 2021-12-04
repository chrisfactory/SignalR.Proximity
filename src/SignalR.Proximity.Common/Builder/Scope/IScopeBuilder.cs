using Microsoft.Extensions.DependencyInjection;
using SignalR.Proximity.Common.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{

    public interface IScopeBuilder<T, TContract> : IConsumeBuilder<T, TContract>
    {

    }
}
