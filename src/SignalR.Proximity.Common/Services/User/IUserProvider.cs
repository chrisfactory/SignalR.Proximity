using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    public interface IUserProvider
    { 
        string UserId { get; set; }
    }
}
