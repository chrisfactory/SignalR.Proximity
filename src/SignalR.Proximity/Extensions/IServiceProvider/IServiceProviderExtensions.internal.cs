using System;
namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceProviderExtensions
    {
        internal static T GetRequiredService<T>(this IServiceProvider provider, Action<T> configure)
        {
            var result = provider.GetRequiredService<T>();
            configure?.Invoke(result);
            return result;
        }
    }
}
