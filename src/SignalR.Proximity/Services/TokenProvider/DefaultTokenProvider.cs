using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{ 
    internal class DefaultTokenProvider : ITokenProvider
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public Task<string> AccessTokenProviderAsync(Uri baseAddress)
        {
            return Task.FromResult(string.Empty);
        }
    }
}
