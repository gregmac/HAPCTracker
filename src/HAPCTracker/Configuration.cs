using Microsoft.Win32;
using System;

namespace HAPCTracker
{
    /// <summary>
    /// Configuration values
    /// </summary>
    public class Configuration
    {
        public const string RegistryKey = @"SOFTWARE\HAPCTracker";

        /// <summary>
        /// The URL to the HomeAssistant MQTT server
        /// </summary>
        public string MqttServer { get; set; }

        /* 
         * MQTT DiscoveryPrefix
         * MQTT Auth
         */

        /// <summary>
        /// How many minutes the PC is idle before user is considered AFK.
        /// </summary>
        public int AwayMinutes { get; set; } = DefaultAwayMinutes;

        public static int DefaultAwayMinutes = 5;

        /// <summary>
        /// How many frequently (in seconds) to post status to HomeAssistant.
        /// </summary>
        public int UpdateSeconds { get; set; } = DefaultUpdateSeconds;

        public static int DefaultUpdateSeconds = 30;

        /// <summary>
        /// Try validating configuration values.
        /// Throws an exception if invalid.
        /// </summary>
        public void ThrowIfInvalid()
        {
            new UriBuilder(MqttServer); // will throw if invalid
            if (AwayMinutes <= 0) throw new ArgumentOutOfRangeException("AwayMinutes must be greater than 0");
            if (UpdateSeconds <= 0) throw new ArgumentOutOfRangeException("UpdateSeconds must be greater than 0");
        }

        /// <summary>
        /// Checks if the config is <see cref="ThrowIfInvalid">valid</see>.
        /// Does not throw.
        /// </summary>
        public bool IsValid()
        {
            try
            {
                ThrowIfInvalid();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Load configuration from the Windows registry,
        /// or returns a new <see cref="Configuration"/> using defaults.
        /// </summary>
        public static Configuration LoadOrCreate()
        {
            using var subKey = Registry.CurrentUser.OpenSubKey(RegistryKey, true);

            // new config
            if (subKey == null) return new Configuration();

            // load config
            return new Configuration
            {
                MqttServer = subKey.GetValue<string>(nameof(MqttServer)),
                AwayMinutes = subKey.GetValue<int>(nameof(AwayMinutes), DefaultAwayMinutes),
                UpdateSeconds = subKey.GetValue<int>(nameof(UpdateSeconds), DefaultUpdateSeconds),
            };
        }

        /// <summary>
        /// Save configuration to Windows registry.
        /// <see cref="ThrowIfInvalid">Throws an error if invalid.</see>
        /// </summary>
        public void Save()
        {
            ThrowIfInvalid();

            using var subKey =
                Registry.CurrentUser.OpenSubKey(RegistryKey, true)
                ?? Registry.CurrentUser.CreateSubKey(RegistryKey);

            subKey.SetValue(nameof(MqttServer), MqttServer);
            subKey.SetValue(nameof(AwayMinutes), AwayMinutes);
            subKey.SetValue(nameof(UpdateSeconds), UpdateSeconds);
        }
    }
}
