using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;
using System;
namespace SignalR.Proximity
{
    internal class ActionPatternProvider : IPatternProvider
    {
        private readonly ProximityConfigurationCore _configs;
        private readonly IContractDescriptor _contractDescriptor;
        private readonly Func<ProximityConfigurationCore, IContractDescriptor, string> _actionPattern;
        public ActionPatternProvider(
            IOptions<ProximityConfigurationCore> configOptions,
            IContractDescriptor contractDescriptor,
            Func<ProximityConfigurationCore, IContractDescriptor, string> actionPattern)
        {
            _configs = configOptions.Value;
            _contractDescriptor = contractDescriptor;
            _actionPattern = actionPattern;
        }

        public virtual string GetPattern()
        {
            return _actionPattern(_configs, _contractDescriptor);
        }
    }
}
