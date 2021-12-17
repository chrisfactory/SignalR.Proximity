using System;
namespace SignalR.Proximity
{
    public interface IPatternUrlProvider<TContract>
    {
        Uri GetHubUrl(Uri? UrlBase, string? pattern);
        string BuildNameSpace();
    }
}
