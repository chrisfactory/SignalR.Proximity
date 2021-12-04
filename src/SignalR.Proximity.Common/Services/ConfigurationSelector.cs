using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SignalR.Proximity.Common
{
    public class ConfigurationSelector<TConfig>
           where TConfig : SignalRProximityConfiguration, new()
    {
        private IOptionsMonitor<TConfig> _configs;
        private bool _AutoClone;
        public TConfig Current { get; private set; }

        internal void UseConfigs(IOptionsMonitor<TConfig> configs, bool autoClone)
        {
            _AutoClone = autoClone;
            _configs = configs;
            SelectConfiguration(null);
        }
        internal void UseConfigs(TConfig configs)
        {
            Current = configs;
        }
        internal void SelectConfiguration(string name)
        {
            if (_configs != null)
            {
                if (_AutoClone)
                    Current = Clone(_configs.Get(name));
                else
                    Current = _configs.Get(name);
            }
        }

        private TConfig Clone(TConfig source)
        {

            if (!typeof(TConfig).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(TConfig);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (TConfig)formatter.Deserialize(stream);
            }
        }




    }
}
