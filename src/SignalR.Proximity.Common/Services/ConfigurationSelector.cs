using Microsoft.Extensions.Options;
using System; 
using System.Text.Json;

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
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(TConfig);
            }
            var targetType = source.GetType();
            return (TConfig)JsonSerializer.Deserialize(JsonSerializer.Serialize(source, targetType), targetType); 
        }
    }
}
