using HomeAssistant.Mqtt.Components;
using HomeAssistant.Mqtt.Payloads;
using System;
using System.Collections.Concurrent;
using System.Net.Mqtt;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistant.Mqtt
{
    public class HomeAssistantMqttClient
    {
        protected IMqttClient Client { get; }
        public MqttClientCredentials Credentials { get; }

        protected JsonSerializerOptions SerializerOptions { get; }

        protected ConcurrentBag<ComponentBase> Sensors { get; }

        protected HomeAssistantMqttClient(IMqttClient client, string serverName, MqttClientCredentials credentials)
        {
            Client = client;
            Credentials = credentials;

            SerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
            SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            Sensors = new ConcurrentBag<ComponentBase>();
            ServerName = serverName;
        }

        /// <summary>
        /// Indicates if the client is currently connected
        /// </summary>
        public bool IsConnected => Client.IsConnected;

        public Exception LastConnectionError { get; private set; }

        /// <summary>
        /// The server name we're connected to
        /// </summary>
        public string ServerName { get; }

        public static async Task<HomeAssistantMqttClient> CreateAsync(string server, string clientId, string username = null, string password = null)
        {
            var credentials = new MqttClientCredentials(
                clientId: clientId,
                userName: username,
                password: password);
            var client = await MqttClient.CreateAsync(server).ConfigureAwait(true);

            return new HomeAssistantMqttClient(client, server, credentials);
        }

        public async Task<bool> ConnectIfNeededAsync()
        {
            if (Client.IsConnected) return true;

            try
            {
                await Client.ConnectAsync(Credentials).ConfigureAwait(false);
                LastConnectionError = null;
                return true;
            }
            catch(Exception ex)
            {
                LastConnectionError = ex;
                return false;
            }
        }

        protected byte[] Serialize<T>(T value)
            => JsonSerializer.SerializeToUtf8Bytes(value, SerializerOptions);

        public async Task PublishAsync<T>(string topic, T payload, MqttQualityOfService qos = MqttQualityOfService.AtMostOnce)
        {
            if (await ConnectIfNeededAsync().ConfigureAwait(false))
            {
                await Client.PublishAsync(
                    new MqttApplicationMessage(
                        topic,
                        Serialize(payload)),
                    qos,
                    retain: true)
                    .ConfigureAwait(false);
            }
        }

        public async Task AddAsync(ComponentBase item)
        {
            Sensors.Add(item);
            item.SensorDataChanged += Item_SensorDataChanged;

            await PublishConfig(item).ConfigureAwait(false);
            if (item.CurrentState != null)
            {
                await PublishValue(item, item.CurrentState).ConfigureAwait(false);
            }
        }

        private void Item_SensorDataChanged(object sender, SensorData e)
        {
            if (sender is ComponentBase haSensor)
            {
                _ = PublishValue(haSensor, e);
            }
        }

        protected async Task PublishConfig(ComponentBase component)
        {
            var configChannel = $"homeassistant/{component.Type}/{component.MqttName}/config";
            var valueChannel = $"homeassistant/{component.Type}/{component.MqttName}/value";
            var payload = new ConfigMessage
            {
                Name = component.Name,
                StateTopic = valueChannel,
                ValueTemplate = $"{{{{ value_json.{nameof(SensorData.State).ToLowerInvariant()} }}}}",
                JsonAttributesTopic = valueChannel,
                JsonAttributesTemplate = $"{{{{ value_json.{nameof(SensorData.Attributes).ToLowerInvariant()} | tojson }}}}",
                ExpireAfter = component.ExpireAfter.HasValue ? (int?)Math.Ceiling(component.ExpireAfter.Value.TotalSeconds) : null,
            };
            component.OverrideConfig(payload);
            await PublishAsync(configChannel, payload, MqttQualityOfService.AtLeastOnce).ConfigureAwait(false);
        }

        protected async Task PublishValue(ComponentBase component, SensorData value)
        {
            var valueChannel = $"homeassistant/{component.Type}/{component.MqttName}/value";
            await PublishAsync(valueChannel, value, MqttQualityOfService.AtMostOnce).ConfigureAwait(false);
        }
    }
}
