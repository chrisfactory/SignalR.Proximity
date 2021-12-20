using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;
using System;
namespace SignalR.Proximity
{
    public class PatternUrlProvider : IPatternUrlProvider
    {
        private readonly ProximityConfigurationCore _configs;
        private readonly IContractDescriptor _contractDescriptor;
        public PatternUrlProvider(
            IOptions<ProximityConfigurationCore> configOptions,
            IContractDescriptor contractDescriptor)
        {
            _configs = configOptions.Value;
            _contractDescriptor = contractDescriptor;
        }

        public virtual string GetPattern()
        {
            var contractType = _contractDescriptor.ContractType;

            string urlPostFixPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(_configs.PatternPostfix))
                urlPostFixPath += $".{_configs.PatternPostfix}";

            if (_configs.PatternMachineNamePostfix)
                urlPostFixPath += $".{Environment.MachineName}";

            return $"{_configs.PatternBase}/{contractType.FullName}{urlPostFixPath}".ToLower();
        }


        public virtual Uri GetHubUrl(Uri? UrlBase)
        {
            string ns = GetPattern();

            if (UrlBase != null)
                return new Uri(UrlBase, ns);
            else
                return new Uri($"{ns}", UriKind.Relative);
        }

    }
}
