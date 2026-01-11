using System;

namespace SignalR.Proximity
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ProximityTypeScriptCodeSyncAttribute : Attribute
    {
        public string TargetPath { get; }

        public ProximityTypeScriptCodeSyncAttribute(string targetPath)
        {
            TargetPath = targetPath;
        }
    }
}
