using HomeAssistant.Mqtt.Payloads;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HomeAssistant.Mqtt.Components
{
    /// <summary>
    /// Represents an MQTT HomeAssistant component.
    /// See https://www.home-assistant.io/docs/mqtt/discovery/
    /// </summary>
    public abstract class ComponentBase
    {
        /// <summary>
        /// Name of the sensor
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// MQTT channel name. Must be all alphanumeric and underscores
        /// </summary>
        public string MqttName { get; }

        /// <summary>
        /// Amount of time without an update before sensor value becomes unknown.
        /// </summary>
        public TimeSpan? ExpireAfter { get; }

        /// <summary>
        /// The <see cref="ComponentType"/>.
        /// </summary>
        public ComponentType Type { get; }

        /// <summary>
        /// Current value
        /// </summary>
        public SensorData CurrentState { get; private set; }

        /// <summary>
        /// Event raised when data changes by calling <see cref="SetValue(object, IDictionary{string, string})"/>
        /// </summary>
        internal event EventHandler<SensorData> SensorDataChanged;

        protected ComponentBase(string name, ComponentType type, TimeSpan? expireAfter = null)
        {
            Name = name;
            MqttName = Regex.Replace(name.ToLowerInvariant(), "[^a-z0-9]+", "_");
            Type = type;
            ExpireAfter = expireAfter;
        }

        /// <summary>
        /// Modifies the <see cref="CurrentState"/>
        /// and invokes the <see cref="SensorDataChanged"/> event.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="attributes"></param>
        protected void SetValue(object state, IDictionary<string, string> attributes = null)
        {
            CurrentState = new SensorData()
            {
                State = state,
                Attributes = attributes,
            };
            SensorDataChanged?.Invoke(this, CurrentState);
        }

        /// <summary>
        /// Hook to override the <see cref="ConfigMessage"/> with additional data
        /// </summary>
        /// <param name="config"></param>
        internal virtual void OverrideConfig(ConfigMessage config)
        {
            // nothing
        }
    }
}
