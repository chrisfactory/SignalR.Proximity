using System;

namespace SignalR.Proximity
{
    /// <summary>
    /// Decorates an interface to automatically generate a TypeScript contract file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ProximityTypeScriptCodeSyncAttribute : Attribute
    {
        /// <summary>
        /// Gets the target file path for generation.
        /// </summary>
        public string TargetPath { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProximityTypeScriptCodeSyncAttribute"/> class.
        /// </summary>
        /// <param name="targetPath">The relative path to generate the TypeScript file.</param>
        public ProximityTypeScriptCodeSyncAttribute(string targetPath)
        {
            TargetPath = targetPath;
        }
    }
}
