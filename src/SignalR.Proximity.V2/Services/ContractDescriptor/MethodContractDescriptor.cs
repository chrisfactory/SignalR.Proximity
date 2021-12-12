using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; 
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class MethodContractDescriptor
    {
        private MethodInfo method;
        private object _instance;
        private MethodContractDescriptor(object instance, MethodInfo me)
        {
            _instance = instance;
            method = me;
            Key = GetMethodKey(me);
        }
        public string Key { get; private set; }

        public static string GetMethodKey(MethodInfo mi)
        {
            MethodInfo miDefinition = mi;
            if (mi.IsGenericMethod)
                miDefinition = mi.GetGenericMethodDefinition();//.GetBaseDefinition();

            return miDefinition.ToString();
        }


        internal Task ReceiveAsync(object[] arg1)
        {
            method.Invoke(_instance, arg1);
            return Task.CompletedTask;
        }

        internal Type[] GetArgsTypes()
        {
            var argsTypes = method.GetParameters();
            return argsTypes.Select(m => m.ParameterType).ToArray();
        }
        public static IEnumerable<MethodContractDescriptor> Create<TProxy>(TProxy instance)
        {
            var proxyType = typeof(TProxy);
            if (!proxyType.IsInterface)
                throw new ArgumentException($"{proxyType.FullName} n'est pas une interface");

            foreach (var meth in proxyType.GetMethods())
            {
                var result = new MethodContractDescriptor(instance, meth);
                yield return result;
            }
        }
    }
}
