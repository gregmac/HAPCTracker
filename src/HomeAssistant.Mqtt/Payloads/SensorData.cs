using System.Collections.Generic;

namespace HomeAssistant.Mqtt.Payloads
{
    /// <summary>
    /// Data for a HoemAssistant sensor.
    /// Based on https://developers.home-assistant.io/docs/api/rest/#actions:~:text=%F0%9F%94%92-,POST,%2Fapi%2Fstates%2F%3Centity_id%3E,-%F0%9F%94%92
    /// </summary>
    public class SensorData
    {
        /// <summary>
        /// State (value) object
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// Optional additional data for the sensor
        /// </summary>
        public IDictionary<string, string> Attributes { get; set; }
    }
}
