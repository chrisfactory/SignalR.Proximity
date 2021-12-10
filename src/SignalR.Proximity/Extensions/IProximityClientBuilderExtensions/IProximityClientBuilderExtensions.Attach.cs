using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public static partial class IProximityClientBuilderExtensions
    {
        public static IClientProxy AttachStart<TContract, TInstance>(this IProximityClientBuilder<TContract> source, TInstance instance)
            where TInstance : class, TContract
        {
            var task = Task.Run(async () => await source.AttachStartAsync(instance));
            task.Wait();
            return task.Result;
        }
        public static async Task<IClientProxy> AttachStartAsync<TContract, TInstance>(this IProximityClientBuilder<TContract> source, TInstance instance)
             where TInstance : class, TContract
        {
            var proxy = source.Attach(instance);
            await proxy.StartAsync();
            return proxy;
        }

        public static IClientProxy Attach<TContract, TInstance>(this IProximityClientBuilder<TContract> source, TInstance instance)
             where TInstance : class, TContract
        {
            source.Services.AddSingleton(new InstanceValue<TContract>(instance));
            //ClientProxy proxy = null;
            //source.ConsumeCore(c =>
            //{
            //    proxy = c.CreateProxy<TContract, TInstance>(instance);
            //});
            return source.Build();
        }
    }
}
