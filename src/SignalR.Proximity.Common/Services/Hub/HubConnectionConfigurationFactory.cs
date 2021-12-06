using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    public interface IHubConnectionConfigurationFactory
    {

    }
    internal class HubConnectionConfigurationFactory<TConfig, TContract> : IHubConnectionConfigurationFactory
           where TConfig : SignalRProximityConfiguration, new()
    {
        public HubConnectionConfigurationFactory(IOptions<ConfigurationSelector<TConfig>> configOptions, IHubConnectionBuilder hubBuilder)
        {

            var config = configOptions.Value.Current;

            var urlBase = config.UrlBase; 
            var tokenProvider = config.TokenProvider;
            var userProvider = config.UserProvider;

            var hubUri = config.HubNamespaceProvider.GetHubUrl<TContract>(urlBase);

            var connection = hubBuilder
                .WithAutomaticReconnect(config.RetryPolicy)
                .WithUrl(hubUri, options =>
            {
                if (tokenProvider != null)
                {
                    options.AccessTokenProvider = () => tokenProvider.AccessTokenProviderAsync(urlBase, userProvider);
                    options.UseDefaultCredentials = true;
                }
            });
        }

    }
}
