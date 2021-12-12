namespace SignalR.Proximity
{
    public interface IClient<TContract>
    {
        void Attach<T>(T instance) where T : class, TContract;
        void Dettach<T>(T instance) where T : class, TContract;
        void JoinGroups(params string[] groups);
        void QuitGroups(params string[] groups); 
    }
}
