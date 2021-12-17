using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    internal class TokenProvider : ITokenProvider
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public Task<string?> GetTokenAsync(Uri? baseAddress)
        {
            return Task.FromResult<string?>(string.Empty);
        }
    }
}
