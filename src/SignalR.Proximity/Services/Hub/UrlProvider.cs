using System;
namespace SignalR.Proximity
{  
    public class UrlProvider<TContract> : IUrlProvider<TContract>
    { 
        public string Postfix { get; set; }
        public bool UseMachineNamePostfix { get; set; }

        public Uri GetHubUrl(Uri UrlBase)
        {
            string ns = $"hubs/{BuildNameSpace()}";

            if (UrlBase != null)
                return new Uri(UrlBase, ns);
            else
                return new Uri($"/{ns}", UriKind.Relative);
        }

        public string BuildNameSpace()
        {
            var contractType = typeof(TContract);

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
