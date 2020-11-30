using System.Text.Json.Serialization;

namespace HomeAssistant.Mqtt.Payloads
{
    /// <summary>
    /// Configuration message used for HomeAssistant discovery.
    /// See https://www.home-assistant.io/docs/mqtt/discovery/
    /// </summary>
    public class ConfigMessage
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dev_cla")]
        public string DeviceClass { get; set; }

        [JsonPropertyName("stat_t")]
        public string StateTopic { get; set; }

        [JsonPropertyName("val_tpl")]
        public string ValueTemplate { get; set; }

        [JsonPropertyName("exp_aft")]
        public int? ExpireAfter { get; set; }

        [JsonPropertyName("json_attr_t")]
        public string JsonAttributesTopic { get; set; }

        [JsonPropertyName("json_attr_tpl")]
        public string JsonAttributesTemplate { get; set; }
    }
}
