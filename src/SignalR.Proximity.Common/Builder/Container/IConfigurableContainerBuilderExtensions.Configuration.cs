using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Common
{

    /// <summary>
    ///  Extension methods for <see cref="IConfigurableContainerBuilder{T, TContract}"/>.
    /// </summary>
    public static partial class IConfigurableContainerBuilderExtensions
    {
        //public static IScopeBuilder<T, TContract> UseConfiguration<T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, Action<IConfigurationBuilder<SignalRProximityConfiguration>> builderConfig)
        //{
        //    return source.UseConfiguration<SignalRProximityConfiguration, T, TContract>(builderConfig);
        //}
        //public static IScopeBuilder<T, TContract> UseConfiguration<TConfig, T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, Action<IConfigurationBuilder<TConfig>> builderConfig)
        //    where TConfig : SignalRProximityConfiguration, new()
        //{
        //    var builder = new SignalRProximityConfigurationBuilder<TConfig>(source);
        //    builderConfig(builder);
        //    return source;
        //}
        //public static IScopeBuilder<T, TContract> UseConfiguration<T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, string name)
        //{
        //    return source.UseConfigNameCore(name);
        //}
        //public static IScopeBuilder<T, TContract> UseConfiguration<T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, string name, Action<IConfigurationBuilder<SignalRProximityConfiguration>> reConFigue)
        //{
        //    return source.UseConfiguration<SignalRProximityConfiguration, T,TContract>(name, reConFigue);
        //}
        //public static IScopeBuilder<T, TContract> UseConfiguration<TConfig, T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, string name, Action<IConfigurationBuilder<TConfig>> reConFigue)
        //     where TConfig : SignalRProximityConfiguration, new()
        //{
        //    source.UseConfigNameCore(name);
        //    source.UseConfiguration(reConFigue);
        //    return source;
        //}


        internal static IScopeBuilder<T, TContract> UseConfigNameCore<TConfig, T, TContract>(this IConfigurableContainerBuilder<T, TContract> source, string name)
                 where TConfig : SignalRProximityConfiguration, new()
        {
            source.Services.Configure<ConfigurationSelector<TConfig>>(c => c.SelectConfiguration(name));
            return source;
        }
    }
}
