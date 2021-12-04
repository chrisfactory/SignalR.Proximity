using SignalR.Proximity.Common;
using System; 
using System.Threading.Tasks;

namespace SignalR.Proximity.Notifier
{

    /// <summary>
    ///  Extension methods for <see cref="IConsumeBuilder{NotifierProxyProvider,TContract}"/>.
    /// </summary>
    public static class IConsumeBuilderExtensions
    {
        public static async Task CallAsync<TContract>(this IConsumeBuilder<NotifierProxyProvider, TContract> source, Action<TContract> act)
        {
            await Task.Run(() => source.Call(act));
        }

        public static void Call<TContract>(this IConsumeBuilder<NotifierProxyProvider, TContract> source, Action<TContract> act)
        {
            using (var disposableProxy = source.GetProxy<TContract>())
            {
                act(disposableProxy.Proxy);
            }
        }

        public static Disposable<TContract> GetProxy<TContract>(this IConsumeBuilder<NotifierProxyProvider, TContract> source)
        {
            Disposable<TContract> proxyWrapper = null;
            source.ConsumeCore(c =>
            {
                var proxy = c.CreateProxy<TContract>();
                proxyWrapper = new Disposable<TContract>(proxy);
            });
            return proxyWrapper;
        }
    }

}
