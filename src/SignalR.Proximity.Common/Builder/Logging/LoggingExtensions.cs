using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignalR.Proximity.Common
{
    /// <summary>
    ///  Extension methods for <see cref="IConsumeBuilder{T, TContract}"/>.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        ///     Adds logging services to the specified <see cref="IConsumeBuilder{T, TContract}"/>.
        /// </summary>
        /// <typeparam name="T">
        ///      Represents a instance type to implemente the contract TContract
        /// </typeparam>
        /// <typeparam name="TContract">
        ///    Represents a contract used to configure the SignalRProximity system.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="IConsumeBuilder{T, TContract}"/> to configure
        /// </param>
        /// <param name="configure">
        ///     The <see cref="Microsoft.Extensions.Logging.ILoggingBuilder"/> configuration delegate.
        /// </param>
        /// <returns>
        ///     The same instance of the <see cref="IConsumeBuilder{T, TContract}"/> Instance
        ///     for chaining.
        /// </returns>
        public static IConsumeBuilder<T, TContract> AddLogging<T, TContract>(this IConsumeBuilder<T, TContract> source, Action<ILoggingBuilder> configure)
        {
            source.Services.AddLogging(configure);
            return source;
        }
    }
}
