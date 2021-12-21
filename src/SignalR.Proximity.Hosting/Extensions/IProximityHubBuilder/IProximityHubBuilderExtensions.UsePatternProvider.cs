using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Proximity.Hosting
{
    public static partial class IProximityHubBuilderExtensions
    {
        public static IProximityHubBuilder UsePatternProvider<TPatternProvider>(this IProximityHubBuilder builder)
            where TPatternProvider : class, IPatternProvider
        {
            builder.Services.AddSingleton<IPatternProvider, TPatternProvider>();
            return builder;
        }
    }
}
