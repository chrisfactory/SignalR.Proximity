using System;
namespace SignalR.Proximity.Common
{
    public interface IHubNamespaceProvider
    {
        string Postfix { get; set; }
        bool UseMachineNamePostfix { get; set; }
        Uri GetHubUrl<TConract>(Uri UrlBase);
        string BuildNameSpace<TConract>();
    }
}
