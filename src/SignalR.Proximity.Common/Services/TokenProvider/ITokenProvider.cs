using SignalR.Proximity.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    public interface ITokenProvider
    { 
        DateTimeOffset? AbsoluteExpiration { get; set; }
        Task<string> AccessTokenProviderAsync(Uri baseAddress, IUserProvider userProvider);
    }
}
