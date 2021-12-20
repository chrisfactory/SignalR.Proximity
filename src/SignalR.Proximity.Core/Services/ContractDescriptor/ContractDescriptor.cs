using System;
using System.Collections.Generic;

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

            foreach (var meth in proxyType.GetMethods())
            {
                var result = new MethodDescriptor(instance, meth);
                yield return result;
            }
        }
    }
}
