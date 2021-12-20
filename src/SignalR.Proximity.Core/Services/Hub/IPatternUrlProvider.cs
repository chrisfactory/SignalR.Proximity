using System;
namespace SignalR.Proximity
{
    public interface IPatternUrlProvider
    {
        string GetPattern();
        Uri GetHubUrl(Uri? UrlBase);
    }
}
