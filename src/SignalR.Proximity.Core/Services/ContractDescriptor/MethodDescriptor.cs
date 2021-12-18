using System;
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
                miDefinition = mi.GetGenericMethodDefinition();

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
}
