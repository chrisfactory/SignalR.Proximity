using Microsoft.AspNetCore.SignalR.Client;
using System;
namespace SignalR.Proximity
{
    /// <summary>
    /// Represents a default implementation of <see cref="Microsoft.AspNetCore.SignalR.Client.IRetryPolicy"/> 
    /// </summary>
    public class RetryPolicy : IRetryPolicy
    {

        private readonly TimeSpan?[] _retryDelays = new TimeSpan?[] { null };
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> 
        /// </summary>
        public RetryPolicy() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> 
        /// </summary>
        /// <param name="retryDelays">Represents the sequence for retrying. </param>
        public RetryPolicy(TimeSpan[] retryDelays)
        {
            _retryDelays = new TimeSpan?[retryDelays.Length + 1];

            for (int i = 0; i < retryDelays.Length; i++)
            {
                _retryDelays[i] = retryDelays[i];
            }
        }

        /// <inheritdoc />
        public virtual TimeSpan? NextRetryDelay(RetryContext retryContext)
        { 
            return _retryDelays[retryContext.PreviousRetryCount];
        }
    }

}
