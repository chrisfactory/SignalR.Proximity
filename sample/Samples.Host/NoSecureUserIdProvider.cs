using Microsoft.AspNetCore.SignalR;

namespace Samples.Host
{
    public class NoSecuredUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();
            if (httpContext != null)
            {
                var request = httpContext.Request;
                if (request.Headers.ContainsKey("username"))
                {
                    var fakeUserName = request.Headers["username"].ToString();
                    return fakeUserName;
                }
            }
            return null;
        }
    }
}
