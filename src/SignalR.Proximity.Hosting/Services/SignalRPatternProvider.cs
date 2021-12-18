using Microsoft.Extensions.Options;
using SignalR.Proximity.Core;

namespace SignalR.Proximity.Hosting
{
    internal class SignalRPatternProvider<TContract> : ISignalRPatternProvider
    {
        private readonly IPatternUrlProvider<TContract> urlProvider;
        private readonly ProximityConfigurationCore _config;
        public SignalRPatternProvider(IPatternUrlProvider<TContract> urlProvider, IOptions<ProximityConfigurationCore> configurationOptions)
        {
            this.urlProvider = urlProvider;
            this._config = configurationOptions.Value;
        }
        public string GetPattern()
        {
            if (string.IsNullOrWhiteSpace(_config.PatternBase))
                return $"{urlProvider.BuildNameSpace()}";
            else
                return $"{_config.PatternBase}/{urlProvider.BuildNameSpace()}";
        }
    }
}
