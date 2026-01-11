namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Copies the service collection.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <returns>A new service collection with copied descriptors.</returns>
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
