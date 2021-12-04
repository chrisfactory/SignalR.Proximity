using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    [Serializable]
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
