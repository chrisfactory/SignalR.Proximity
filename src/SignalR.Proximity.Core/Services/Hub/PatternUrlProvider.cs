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

        public string GetPattern()
        {
            var contractType = typeof(TContract);

            string urlPostFixPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(_configs.PatternPostfix)) 
                urlPostFixPath += $".{_configs.PatternPostfix}"; 

            if (_configs.PatternMachineNamePostfix) 
                urlPostFixPath += $".{Environment.MachineName}"; 

            return $"{_configs.PatternBase}/{contractType.FullName}{urlPostFixPath}".ToLower();
        }


        public Uri GetHubUrl(Uri? UrlBase)
        {
            string ns = GetPattern();

            if (UrlBase != null)
                return new Uri(UrlBase, ns);
            else
                return new Uri($"{ns}", UriKind.Relative);
        } 

    }
}
