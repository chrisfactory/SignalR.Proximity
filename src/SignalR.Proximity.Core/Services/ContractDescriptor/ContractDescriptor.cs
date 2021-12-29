using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.Proximity
{
    internal class ContractDescriptor<TContract> : IContractDescriptor<TContract>
    {
        public Type ContractType => typeof(TContract);

        public IEnumerable<MethodDescriptor> GetDescription<TProxy>(TProxy instance)
            where TProxy : TContract
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var proxyType = typeof(TContract);
            if (!proxyType.IsInterface)
                throw new ArgumentException($"{proxyType.FullName} n'est pas une interface");
            return proxyType.GetMethods().Select(meth => new MethodDescriptor(instance, meth));
        }
    }
}
