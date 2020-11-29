﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace HAPCTracker.HomeAssistant
{
    public class HomeAssistantRestClient
    {
        public HomeAssistantRestClient(Uri baseUrl, string accessToken)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            BaseUrl = baseUrl;
        }

        public Uri BaseUrl { get; }
        private HttpClient Client { get; }

        private JsonSerializerOptions SerializerOptions { get; }
            = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task PostSensor(string sensorName, SensorData data)
        {
            var json = new StringContent(JsonSerializer.Serialize(data, SerializerOptions));
            var postUrl = new Uri(BaseUrl, $"api/sensor/{sensorName}");

            await Client.PostAsync(postUrl, json).ConfigureAwait(false);
            // todo: error handling
        }
    }
}