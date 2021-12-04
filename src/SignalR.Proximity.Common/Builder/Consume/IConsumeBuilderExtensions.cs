using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{

    /// <summary>
    ///  Extension methods for <see cref="IConsumeBuilder{T, TContract}"/>.
    /// </summary>
    internal static class IConsumeBuilderExtensions
    {
        internal static void ConsumeCore<T, TContract>(this IConsumeBuilder<T, TContract> source, Action<T> act)
        {
            var context = source.Build();
            act(context);
        }
    }
}
