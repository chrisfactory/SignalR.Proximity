using System.Collections.Generic;

namespace SignalR.Proximity
{
    public interface IContractDescriptor<TContract>
    {
        IEnumerable<MethodDescriptor> GetDescription<TProxy>(TProxy instance) where TProxy : TContract;
    }
}
