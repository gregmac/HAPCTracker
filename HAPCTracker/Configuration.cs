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
        /// The Base URL to the HomeAssistant instance.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// The HomeAssistant Long-lived Access Token.
        /// See https://developers.home-assistant.io/docs/auth_api/#long-lived-access-token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// How many minutes the PC is idle before user is considered AFK.
        /// </summary>
        public int? AwayMinutes { get; set; } = 5;

        /// <summary>
        /// Try validating configuration values.
        /// Throws an exception if invalid.
        /// </summary>
        public void ThrowIfInvalid()
        {
            if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _)) throw new ArgumentException("Invalid BaseUrl value");
            if (string.IsNullOrWhiteSpace(AccessToken)) throw new ArgumentNullException("Missing AccessToken value");
            if (!(AwayMinutes > 0)) throw new ArgumentOutOfRangeException("AwayMinutes must be greater than 0");
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
            if (subKey == null) return null;

            return new Configuration
            {
                BaseUrl = subKey.GetValue<string>(nameof(BaseUrl)),
                AccessToken = subKey.GetValue<string>(nameof(AccessToken)),
                AwayMinutes = subKey.GetValue<int?>(nameof(AwayMinutes)),
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

            subKey.SetValue(nameof(BaseUrl), BaseUrl);
            subKey.SetValue(nameof(AccessToken), AccessToken);
            subKey.SetValue(nameof(AwayMinutes), AwayMinutes);
        }
    }
}
