using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderServiceExtensions
    {

        public static T GetRequiredService<T>(this IServiceProvider provider, Action<T> configure)
        {
            var result = provider.GetRequiredService<T>();
            configure?.Invoke(result);
            return result;
        }
    }
}
