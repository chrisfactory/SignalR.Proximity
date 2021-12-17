namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        internal static IServiceCollection Copy(this IServiceCollection source)
        {
            var array = new ServiceDescriptor[source.Count];
            source.CopyTo(array, 0);
            var services = (IServiceCollection)new ServiceCollection();
            foreach (var service in array)
                services.Add(service);
            return services;
        }
    }
}
