using HomeAssistant.Mqtt;
using HomeAssistant.Mqtt.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    /// <summary>
    /// Container for <see cref="NotifyIcon">tray icon</see> as the root
    /// application object.
    /// </summary>
    public sealed class TrayIconApplicationContext : ApplicationContext
    {
        /// <summary>
        /// Tray icon itself
        /// </summary>
        private NotifyIcon TrayIcon { get; }

        /// <summary>
        /// How long to consider away. Comes from <see cref="Configuration.AwayMinutes"/>.
        /// </summary>
        private TimeSpan AwayTime { get; set; }

        /// <summary>
        /// How frequently to update HomeAssistant. Comes from <see cref="Configuration.UpdateSeconds"/>.
        /// </summary>
        private TimeSpan UpdateInterval { get; set; }

        /// <summary>
        /// The connection to <see cref="Configuration.MqttServer"/>.
        /// </summary>
        private HomeAssistantMqttClient HaClient { get; set; }

        /// <summary>
        /// The sensor used to indicate HA status
        /// </summary>
        private BinarySensor HaAwaySensor { get; set; }

        /// <summary>
        /// Name of sensor to report.
        /// </summary>
        private string SensorName { get; } = $"{Environment.UserName} on {Environment.MachineName}";

        private string MqttClientId { get; } = $"HAPCTracker{Regex.Replace(Environment.MachineName, "[^a-zA-Z0-9]+", "")}"; // alphanumeric only )

        /// <summary>
        /// Friendly OS name to report as attribute
        /// </summary>
        private string OsName { get; } = RuntimeInformation.OSDescription;

        private const string TrayIconTitle = "HomeAssistant Activity Monitor";

        private TrayIconApplicationContext()
        {
            TrayIcon = new NotifyIcon()
            {
                Text = TrayIconTitle,
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenuStrip = new ContextMenuStrip(),
            };
        }

        /// <summary>
        /// Start new application context.
        /// <see cref="Configuration.LoadOrCreate">Loads existing configuration</see>
        /// or if not available/valid, presents a <see cref="ConfigForm">configuration UI</see>,
        /// then starts <see cref="MainLoop"/>.
        /// </summary>
        public static async Task<TrayIconApplicationContext> CreateAsync()
        {
            var context = new TrayIconApplicationContext();

            // ensure icon disappears when we are exiting
            Application.ApplicationExit += (_, __) => context.TrayIcon?.Hide();

            // load config
            var config = Configuration.LoadOrCreate();

            // setup tray menu
            context.TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Configuration", null, (_, __)
                => ConfigForm.ModifyConfig(config, () => context.SaveAndApply(config))));
            context.TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("E&xit", null, (_, __) => Application.Exit()));

            // TODO unclear why this is needed here - but ConnectedAsync never finishes without it
            await Task.Delay(1).ConfigureAwait(false);

            // apply config or prompt user
            if (config.IsValid())
            {
                await context.ApplyConfig(config).ConfigureAwait(true);
            }
            else
            {
                // first time through, so show UI
                if (!ConfigForm.ModifyConfig(config, () => context.SaveAndApply(config)))
                {
                    MessageBox.Show("Cannot continue without valid config", "No Configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    throw new Exception("Cannot continue. TODO improve this state.");
                }
            }

            // ready: show the icon
            context.TrayIcon.Visible = true;

            // start looping
            _ = context.MainLoop();

            return context;
        }

        /// <summary>
        /// <see cref="Configuration.Save">Saves</see> and <see cref="ApplyConfig(Configuration)">applies</see>
        /// the <paramref name="config"/>.
        /// </summary>
        /// <param name="config"></param>
        private async Task SaveAndApply(Configuration config)
        {
            config.Save();
            await ApplyConfig(config).ConfigureAwait(false);
        }

        /// <summary>
        /// Applies the configuration to current form
        /// </summary>
        /// <param name="config"></param>
        private async Task ApplyConfig(Configuration config)
        {
            AwayTime = TimeSpan.FromMinutes(config.AwayMinutes);
            UpdateInterval = TimeSpan.FromSeconds(config.UpdateSeconds);

            HaClient = await HomeAssistantMqttClient.CreateAsync(config.MqttServer, MqttClientId).ConfigureAwait(false);

            HaAwaySensor = new BinarySensor(SensorName, AwayTime.Add(TimeSpan.FromSeconds(5)), BinarySensorDeviceClass.occupancy);
            await HaClient.AddAsync(HaAwaySensor).ConfigureAwait(false);
        }

        /// <summary>
        /// Main processing loop. Updates HomeAssistant
        /// every <see cref="UpdateInterval"/>.
        /// </summary>
        private async Task MainLoop()
        {
            while (true)
            {
                try
                {
                    var lastInputTimeAgo = LastInputInfo.LastInputTime();
                    var lastInputTime = DateTimeOffset.Now.Subtract(lastInputTimeAgo);

                    var away = lastInputTimeAgo > AwayTime;

                    HaAwaySensor.Set(
                        !away,
                        new Dictionary<string, string>
                        {
                            { "last_seen", lastInputTime.ToString() },
                            { "hostname", Environment.MachineName },
                            { "os", OsName },
                        });
                }
                catch(Exception ex)
                {
                    TrayIcon.Text = ex.Message;
                }
                await Task.Delay(UpdateInterval).ConfigureAwait(false);
            }
        }
    }
}
