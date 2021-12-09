using System;

namespace SignalR.Proximity
{
    public class ProximityConfig
    {
        public Uri UrlBase { get; set; }

        public void Copy(ProximityConfig from)
        {
            this.UrlBase= from.UrlBase;
        }
    }
}
