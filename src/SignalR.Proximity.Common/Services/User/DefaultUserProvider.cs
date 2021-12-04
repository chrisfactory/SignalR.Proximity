using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Proximity.Common
{
    [Serializable]
    public class DefaultUserProvider : IUserProvider
    {
        public string UserId { get; set; }
    }
}
