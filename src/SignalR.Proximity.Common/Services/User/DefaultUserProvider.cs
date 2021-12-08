using System;

namespace SignalR.Proximity.Common
{
    [Serializable]
    public class DefaultUserProvider : IUserProvider
    {
        public string UserId { get; set; }
    }
}
