using Microsoft.Extensions.Configuration;
using SignalR.Proximity;
using System;
namespace Microsoft.Extensions.DependencyInjection
{
    internal static partial class ServiceProviderExtensions
    {
        internal static T GetRequiredService<T>(this IServiceProvider provider, Action<IServiceProvider, T>? configure)
            where T : notnull
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            var result = provider.GetRequiredService<T>();
            configure?.Invoke(provider, result);
            return result;
        }

        internal static T GetRequiredService<T, TOptions>(this IServiceProvider provider, IConfiguration? config, Action<T>? configure = null)
            where TOptions : class
            where T : IServicesBuilder
        {
            var result = provider.GetRequiredService<T>();
            if (config != null)
                result.Services.Configure<TOptions>(config);
            configure?.Invoke(result);
            return result;
        }
    }
}