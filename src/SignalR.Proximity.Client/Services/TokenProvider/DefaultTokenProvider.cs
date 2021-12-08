using System;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    [Serializable]
    internal class DefaultTokenProvider : ITokenProvider
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public Task<string> AccessTokenProviderAsync(Uri baseAddress, IUserProvider userProvider)
        {
            return Task.FromResult(string.Empty);
        }
    }
}
