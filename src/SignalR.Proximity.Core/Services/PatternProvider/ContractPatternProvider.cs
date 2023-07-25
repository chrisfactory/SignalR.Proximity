using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;
using System;
namespace SignalR.Proximity
{
    /// <summary>
    /// Represents the provider of Hub Url Pattern.
    /// </summary>
    public class ContractPatternProvider : IPatternProvider
    {
        private readonly ProximityConfigurationCore _configs;
        private readonly IContractDescriptor _contractDescriptor;
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractPatternProvider"/> class.
        /// </summary>
        /// <param name="configOptions">Options configuration</param>
        /// <param name="contractDescriptor">Contract Descriptor</param>
        public ContractPatternProvider(
            IOptions<ProximityConfigurationCore> configOptions,
            IContractDescriptor contractDescriptor)
        {
            _configs = configOptions.Value;
            _contractDescriptor = contractDescriptor;
        }
        /// <inheritdoc />
        public virtual string GetPattern()
        {
            var contractType = _contractDescriptor.ContractType;

            string urlPostFixPath = string.Empty;
            if (!string.IsNullOrWhiteSpace(_configs.PatternPostfix))
                urlPostFixPath += $".{_configs.PatternPostfix}";

            if (_configs.PatternMachineNamePostfix)
                urlPostFixPath += $".{Environment.MachineName}";


            var basePattern = string.Empty;

            if (_configs.PatternBase != null && !string.IsNullOrEmpty(_configs.PatternBase)) 
                basePattern = _configs.PatternBase.StartsWith("/") ? _configs.PatternBase : $"/{_configs.PatternBase}";
             
            return $"{basePattern}/{contractType.FullName}{urlPostFixPath}".ToLower();
        }
    }
}
