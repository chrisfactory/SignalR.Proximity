using Microsoft.AspNetCore.SignalR;

namespace Samples.Host
{
    /// <summary>
    /// Don't do this in production. Used to simulate an implementation of user authentication. 
    /// </summary>
    public class NoSecureUserIdProvider : IUserIdProvider
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
