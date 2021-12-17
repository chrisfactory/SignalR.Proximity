using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface IClient<TContract>
    {
        void Attach<T>(T instance) where T : class, TContract;
        void Dettach<T>(T instance) where T : class, TContract;
        Task JoinGroupsAsync(params string[] groups);
        Task QuitGroupsAsync(params string[] groups);
    }
}
