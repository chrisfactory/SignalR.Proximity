using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Hosting
{
    internal class SignalRPatternProvider<TContract> : ISignalRPatternProvider
    {
        private readonly IUrlProvider<TContract> urlProvider;
        public SignalRPatternProvider(IUrlProvider<TContract> urlProvider)
        {
            this.urlProvider = urlProvider;
        }
        public string GetPattern()
        {
            return urlProvider.BuildNameSpace();
        }
    }
}
