﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Proximity.Common
{ 
    [Serializable]
    public class DefaultHubNamespaceProvider : IHubNamespaceProvider
    {

        public string Postfix { get; set; }
        public bool UseMachineNamePostfix { get; set; }

        public Uri GetHubUrl<TConract>(Uri UrlBase)
        {
            string ns = $"hubs/{BuildNameSpace<TConract>()}";

            if (UrlBase != null)
                return new Uri(UrlBase, ns);
            else
                return new Uri($"/{ns}", UriKind.Relative);
        }

        public string BuildNameSpace<TConract>()
        {
            var contractType = typeof(TConract);

            string urlPostFixPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(Postfix))
            {
                urlPostFixPath += $".{Postfix}";
            }

            if (UseMachineNamePostfix)
            {
                urlPostFixPath += $".{Environment.MachineName}";
            }

            return $"{contractType.FullName}{urlPostFixPath}".ToLower();
        }
    }
}
