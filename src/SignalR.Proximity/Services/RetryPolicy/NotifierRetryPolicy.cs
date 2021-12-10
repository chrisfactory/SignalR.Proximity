using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalR.Proximity
{
    internal class NotifierRetryPolicy<TRetryPolicy> : INotifierRetryPolicy
        where TRetryPolicy : class, IRetryPolicy, new()
    {
        private readonly IRetryPolicy _policy;
        public NotifierRetryPolicy()
        {
            _policy = new TRetryPolicy();
        }
        public NotifierRetryPolicy(TRetryPolicy policy) : base()
        {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy)); ;
            _policy = policy;
        }


        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return _policy.NextRetryDelay(retryContext);
        }
    }
}
