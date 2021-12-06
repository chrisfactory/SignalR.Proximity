using Microsoft.Extensions.Configuration;
using SignalR.Proximity.Common;
using System;

namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    ///  Extension methods for <see cref="IRootConfigurationBuilder{TConfig}"/>.
    /// </summary>
    public static partial class IRootConfigurationBuilderExtensions
    {

        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, IConfiguration config)
            where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<TConfig>(config);
            return source;
        }

        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, string name, IConfiguration config)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<TConfig>(name, config);
            return source;
        }

        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, IConfiguration config, Action<IConfigurationBuilder<TConfig>> conFigue)
        where TConfig : SignalRProximityConfiguration, new()
        {
            var src = source.Configure<TConfig>(config);
            source.Services.Configure<TConfig>(o =>
            {
                var builder = new SignalRProximityConfigurationBuilder<TConfig>(o);
                conFigue(builder);
                builder.Build();
            });
            return src;

        }
        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, string name, IConfiguration config, Action<IConfigurationBuilder<TConfig>> conFigue)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<TConfig>(name, config);
            source.Services.Configure<TConfig>(name, o =>
            {

                var builder = new SignalRProximityConfigurationBuilder<TConfig>(o);
                conFigue(builder);
                builder.Build();
            });
            return source;
        }



        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, Action<IConfigurationBuilder<TConfig>> conFigue)
       where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<TConfig>(o =>
            {
                var builder = new SignalRProximityConfigurationBuilder<TConfig>(o);
                conFigue(builder);
                builder.Build();
            });
            return source;

        }
        public static IRootConfigurationBuilder<TConfig> Configure<TConfig>(this IRootConfigurationBuilder<TConfig> source, string name, Action<IConfigurationBuilder<TConfig>> conFigue)
         where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<TConfig>(name, o =>
            {

                var builder = new SignalRProximityConfigurationBuilder<TConfig>(o);
                conFigue(builder);
                builder.Build();
            });
            return source;
        }

    }
}
