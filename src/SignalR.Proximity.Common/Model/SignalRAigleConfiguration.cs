using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalR.Proximity.Common
{
    [Serializable]
    public class SignalRProximityConfiguration
    {
        public bool IsActivated { get; set; } = true;
        public string DisplayName { get; set; }
        public Uri UrlBase { get; set; }
        public IHubNamespaceProvider HubNamespaceProvider { get; set; } = new DefaultHubNamespaceProvider();
        public ITokenProvider TokenProvider { get; set; }//;// = new DefaultTokenProvider();
        public IUserProvider UserProvider { get; set; } = new DefaultUserProvider();
        public IRetryPolicy RetryPolicy { get; set; } = new DefaultRetryPolicy(); 
    }
     
}
