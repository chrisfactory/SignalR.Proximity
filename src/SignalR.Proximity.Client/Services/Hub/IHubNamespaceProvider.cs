using System;
namespace SignalR.Proximity.Common
{
    public interface IHubNamespaceProvider: IHubNamespaceConfig
    { 
        Uri GetHubUrl<TConract>(Uri UrlBase);
        string BuildNameSpace<TConract>();
    }
    public interface IHubNamespaceConfig
    {
        string Postfix { get; set; }
        bool UseMachineNamePostfix { get; set; }
    }
}
