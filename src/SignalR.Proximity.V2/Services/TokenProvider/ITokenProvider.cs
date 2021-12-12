using System; 
using System.Threading.Tasks;

namespace SignalR.Proximity
{
    public interface ITokenProvider
    {  
        Task<string> GetTokenAsync(Uri baseAddress);
    }
}
