using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;
using System;
namespace SignalR.Proximity
{
    public class PatternUrlProvider<TContract> : IPatternUrlProvider<TContract>
    {
        private readonly ProximityConfigurationCore _configs;
        public PatternUrlProvider(IOptions<ProximityConfigurationCore> configOptions)
        {
            _configs = configOptions.Value;
        }
        public string? Postfix { get; set; }
        public bool UseMachineNamePostfix { get; set; }

        public Uri GetHubUrl(Uri? UrlBase, string? pattern)
        {
            string ns = $"{pattern}/{BuildNameSpace()}";

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
