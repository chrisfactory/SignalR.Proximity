using Microsoft.AspNetCore.SignalR.Client;
using System; 
namespace SignalR.Proximity.Common
{ 
    internal class DefaultRetryPolicy : IRetryPolicy
    {
        private static TimeSpan?[] DEFAULT_RETRY_DELAYS_IN_MILLISECONDS = new TimeSpan?[] { null };

        private TimeSpan?[] _retryDelays;

        public DefaultRetryPolicy()
        {
            _retryDelays = DEFAULT_RETRY_DELAYS_IN_MILLISECONDS;
        }

        public DefaultRetryPolicy(TimeSpan[] retryDelays)
        {
            _retryDelays = new TimeSpan?[retryDelays.Length + 1];

            for (int i = 0; i < retryDelays.Length; i++)
            {
                _retryDelays[i] = retryDelays[i];
            }
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return _retryDelays[retryContext.PreviousRetryCount];
        }
    }

}
