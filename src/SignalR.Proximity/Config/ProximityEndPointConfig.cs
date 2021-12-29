using SignalR.Proximity.Core;
using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the configuration structure of a connection. 
    /// </summary>
    public class ProximityEndPointConfig : ProximityConfigurationCore
    {
        /// <summary>
        /// The URL the Microsoft.AspNetCore.Http.Connections.Client.HttpConnection will use.
        /// </summary>
        public Uri? UrlBase { get; set; }
    }
}
