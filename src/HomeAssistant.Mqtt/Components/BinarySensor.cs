using HomeAssistant.Mqtt.Payloads;
using System;
using System.Collections.Generic;

namespace HomeAssistant.Mqtt.Components
{
    /// <summary>
    /// MQTT Binary Sensor.
    /// See https://www.home-assistant.io/integrations/binary_sensor.mqtt/
    /// </summary>
    public class BinarySensor : ComponentBase
    {
        public const string OnValue = "ON";
        public const string OffValue = "OFF";

        /// <summary>
        /// The type of sensor. See https://www.home-assistant.io/integrations/binary_sensor/#device-class
        /// </summary>
        public BinarySensorDeviceClass DeviceClass { get; }

        public BinarySensor(string name, TimeSpan? expireAfter = null, BinarySensorDeviceClass deviceClass = default)
            : base(name, ComponentType.binary_sensor, expireAfter)
        {
            DeviceClass = deviceClass;
        }

        public void Set(bool state, IDictionary<string, string> attributes = null)
           => SetValue(state ? OnValue : OffValue, attributes);

        internal override void OverrideConfig(ConfigMessage config)
        {
            config.DeviceClass = DeviceClass.ToString();
        }
    }
}
