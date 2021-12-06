using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyR.Hubs.Middleware
{
    public static class DynamicMapHubFactoryExtensions
    {

        public static IServiceCollection AddDynamicHubFactory<THub>(this IServiceCollection services)
            where THub : Hub
        {
            services.AddSingleton<DynamicMapHubFactoryMiddleware<THub>>(); ;
            return services;
        }

        public static IApplicationBuilder UseDynamicMapHub<THub>(this IApplicationBuilder builder, string path = null, Action<IApplicationBuilder> beforeMapHub = null)
        where THub : Hub
        {
            var service = builder.ApplicationServices.GetRequiredService<DynamicMapHubFactoryMiddleware<THub>>();
            if (service == null)
                throw new InvalidOperationException("AddDynamicHubFactory please!");

            builder.UseMiddleware<DynamicMapHubFactoryMiddleware<THub>>();
            service.Use(builder, path, beforeMapHub);
            return builder;
        }
    }
}
