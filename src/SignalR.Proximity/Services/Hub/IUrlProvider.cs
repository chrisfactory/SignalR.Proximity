using System;
namespace SignalR.Proximity
{
    public interface IUrlProvider
    { 
        Uri GetHubUrl<TConract>(Uri UrlBase); 
    } 
}
