using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    /// <summary>
    /// Describes a method within a contract.
    /// </summary>
    public class MethodDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDescriptor"/> class.
        /// </summary>
        /// <param name="instance">The contract instance.</param>
        /// <param name="meth">The method info.</param>
        public MethodDescriptor(object instance, MethodInfo meth)
        {
            Instance = instance;
            Method = meth;
            Key = GetMethodKey(meth);
        }

        /// <summary>
        /// Gets the contract instance.
        /// </summary>
        public object Instance { get; }
        /// <summary>
        /// Gets the method info.
        /// </summary>
        public MethodInfo Method { get; }
        /// <summary>
        /// Gets the unique key of the method.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Generates a unique key for a method.
        /// </summary>
        /// <param name="mi">The method info.</param>
        /// <returns>The unique key string.</returns>
        public static string GetMethodKey(MethodInfo mi)
        {
            MethodInfo miDefinition = mi;
            if (mi.IsGenericMethod)
                miDefinition = mi.GetGenericMethodDefinition();

            return miDefinition.ToString() ?? String.Empty;
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
