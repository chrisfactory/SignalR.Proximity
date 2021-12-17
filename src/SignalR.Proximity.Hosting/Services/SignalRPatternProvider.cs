using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Hosting
{
    internal class SignalRPatternProvider<TContract> : ISignalRPatternProvider
    {
        private readonly IPatternUrlProvider<TContract> urlProvider;
        private readonly ProximityHubBuilderConfiguration _config;
        public SignalRPatternProvider(IPatternUrlProvider<TContract> urlProvider, IOptions<ProximityHubBuilderConfiguration> configurationOptions)
        {
            this.urlProvider = urlProvider;
            this._config = configurationOptions.Value;
        }
        public string GetPattern()
        {
            if (string.IsNullOrWhiteSpace(_config.SignalRPattern))
                return $"{urlProvider.BuildNameSpace()}";
            else
                return $"{_config.SignalRPattern}/{urlProvider.BuildNameSpace()}";
        }
    }
}
