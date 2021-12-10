using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public static class IProximityNotifierBuilderExtensions
    {
        public static async Task CallAsync<TContract>(this IProximityNotifierBuilder<TContract> source, Action<TContract> act)
        {
            await Task.Run(() => source.Call(act));
        }

        public static void Call<TContract>(this IProximityNotifierBuilder<TContract> source, Action<TContract> act)
        {
            using (var disposableProxy = source.Build())
            {
                act(disposableProxy.Proxy);
            }
        }
         
    }
}
