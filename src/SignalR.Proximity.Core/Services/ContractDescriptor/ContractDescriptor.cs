using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignalR.Proximity
{

    public class MethodDescriptor
    {
        public MethodDescriptor(object instance, MethodInfo meth)
        {
            Instance=instance;
            Method=meth;
            Key = GetMethodKey(meth);
        }

        public object Instance { get; }
        public MethodInfo Method { get; }
        public string Key { get; }

        public static string GetMethodKey(MethodInfo mi)
        {
            MethodInfo miDefinition = mi;
            if (mi.IsGenericMethod)
                miDefinition = mi.GetGenericMethodDefinition();//.GetBaseDefinition();

            return miDefinition.ToString()??String.Empty;
        }
        internal Task ReceiveAsync(object?[] arg1)
        {
            Method.Invoke(Instance, arg1);
            return Task.CompletedTask;
        }

        internal Type[] GetArgsTypes()
        {
            var argsTypes = Method.GetParameters();
            return argsTypes.Select(m => m.ParameterType).ToArray();
        }
    }

    public interface IContractDescriptor<TContract>
    {
        IEnumerable<MethodDescriptor> GetDescription<TProxy>(TProxy instance) where TProxy : TContract;
    }
    internal class ContractDescriptor<TContract> : IContractDescriptor<TContract>
    {

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
