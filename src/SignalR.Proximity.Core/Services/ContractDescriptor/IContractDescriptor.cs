using System;
using System.Collections.Generic;

namespace SignalR.Proximity
{
    /// <summary>
    /// Describes a contract.
    /// </summary>
    public interface IContractDescriptor
    {
        /// <summary>
        /// Gets the contract type.
        /// </summary>
        Type ContractType { get; }
    }
    internal interface IContractDescriptor<TContract> : IContractDescriptor
    {
        IEnumerable<MethodDescriptor> GetDescription<TProxy>(TProxy instance) where TProxy : TContract;
    }
}
