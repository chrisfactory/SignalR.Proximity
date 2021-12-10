using System;
namespace SignalR.Proximity
{
    public interface IUrlProvider<TContract>
    { 
        Uri GetHubUrl(Uri UrlBase); 
    } 
}
