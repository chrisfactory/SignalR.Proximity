using System;
namespace SignalR.Proximity
{
    public interface IHubUrlPathProvider 
    {
        string Postfix { get; set; }
        bool UseMachineNamePostfix { get; set; }
        Uri GetHubUrl<TConract>(Uri UrlBase);
        string BuildNameSpace<TConract>();
    } 
}
