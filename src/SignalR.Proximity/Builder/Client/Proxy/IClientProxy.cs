using System;
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface IClientProxy: IDisposable
    { 
        void JoinGroups(params string[] groups);
        void QuitGroups(params string[] groups);
        Task<bool> StartAsync();
        Task StopAsync();
    }
}
