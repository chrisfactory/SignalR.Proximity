using Microsoft.Extensions.DependencyInjection;
namespace SignalR.Proximity
{
    public static partial class IProximityEndPointBuilderExtensions
    {
        public static IProximityEndPointBuilder UsePatternProvider<TPatternProvider>(this IProximityEndPointBuilder builder)
            where TPatternProvider : class, IPatternProvider
        {
            builder.Services.AddSingleton<IPatternProvider, TPatternProvider>();
            return builder;
        }
    }
}
