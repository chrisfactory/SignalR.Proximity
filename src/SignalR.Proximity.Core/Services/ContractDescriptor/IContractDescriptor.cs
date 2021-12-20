using System;
using System.Collections.Generic;

namespace SignalR.Proximity
{
    public interface IContractDescriptor
    {
        Type ContractType { get; }
    }
    internal interface IContractDescriptor<TContract>: IContractDescriptor
    {
        IEnumerable<MethodDescriptor> GetDescription<TProxy>(TProxy instance) where TProxy : TContract;
    }
}
