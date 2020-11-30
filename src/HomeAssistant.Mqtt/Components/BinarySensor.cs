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

        public BinarySensor(string name, TimeSpan? expireAfter = null)
            : base(name, ComponentType.binary_sensor, expireAfter)
        { }

        public void Set(bool state, IDictionary<string, string> attributes = null)
           => SetValue(state ? OnValue : OffValue, attributes);
    }
}
