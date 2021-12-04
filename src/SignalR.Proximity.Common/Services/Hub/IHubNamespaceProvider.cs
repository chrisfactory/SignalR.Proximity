using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{
    public interface IHubNamespaceProvider
    {
        string Postfix { get; set; }
        bool UseMachineNamePostfix { get; set; }
        Uri GetHubUrl<TConract>(Uri UrlBase,string authenticationScheme);
        string BuildNameSpace<TConract>();
    }
}
