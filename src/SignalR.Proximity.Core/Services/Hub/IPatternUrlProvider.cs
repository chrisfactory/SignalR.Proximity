using System;
namespace SignalR.Proximity
{
    public interface IPatternUrlProvider<TContract>
    {
        string GetPattern();
        Uri GetHubUrl(Uri? UrlBase);
    }
}
