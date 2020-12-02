using HomeAssistant.Mqtt;
using HomeAssistant.Mqtt.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mqtt;
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

        private static string MqttClientId { get; } = $"HAPCTracker{Regex.Replace(Environment.MachineName, "[^a-zA-Z0-9]+", "")}"; // alphanumeric only )

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
                => context.ShowConfigForm(config)));
            context.TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("E&xit", null, (_, __) => Application.Exit()));
            context.TrayIcon.Click += context.TrayIcon_Click;

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
                if (!context.ShowConfigForm(config))
                {
                    MessageBox.Show("Cannot continue without valid config", "No Configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    context.ExitThread();
                    return context;
                }
            }

            // ready: show the icon
            context.TrayIcon.Visible = true;

            // start looping
            _ = context.MainLoop();

            return context;
        }

        /// <summary>
        /// Helper method to show config and wire to <see cref="SaveAndApply(Configuration)"/>
        /// and <see cref="TestConfig(Configuration)"/>.
        /// </summary>
        /// <param name="config"></param>
        private bool ShowConfigForm(Configuration config)
            => ConfigForm.ModifyConfig(config, () => SaveAndApply(config), () => TestConfig(config));

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

        private static async Task TestConfig(Configuration config)
        {
            var clientId = $"{MqttClientId}test{DateTime.UtcNow:ffff}";
            var client = await HomeAssistantMqttClient.CreateAsync(config.MqttServer, clientId).ConfigureAwait(false);
            if (await client.ConnectIfNeededAsync().ConfigureAwait(false))
            {
                // success
                return;
            }

            throw client.LastConnectionError ?? new Exception("Unknown error");
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

                    switch (HaClient.LastConnectionError)
                    {
                        case null:
                            SetCurrentError(null);
                            break;
                        case MqttClientException mqttException:
                            SetCurrentError(mqttException.InnerException); // top-level exception message is redundant
                            break;
                        default:
                            SetCurrentError(HaClient.LastConnectionError);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    SetCurrentError(ex);
                }

                // max 63 chars
                try
                {
                    TrayIcon.Text = CurrentError != null
                        ? $"{TrayIconTitle}\nError: Click for details"
                        : $"{TrayIconTitle}\n{(HaClient.IsConnected ? "Connected" : "Not connected")}";
                }
                catch (Exception ex)
                {
                    TrayIcon.Text = ex.Message.Substring(0, 63);
                }

                await Task.Delay(UpdateInterval).ConfigureAwait(false);
            }
        }

        private Exception CurrentError { get; set; }

        private void SetCurrentError(Exception ex)
        {
            var message = ex?.GetMessages();
            var lastMessage = CurrentError?.GetMessages();

            CurrentError = ex;

            if (message != null && message != lastMessage)
            {
                TrayIcon.ShowBalloonTip(5000, $"{TrayIconTitle} Error", message, ToolTipIcon.Error);
            }
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEvent && mouseEvent.Button != MouseButtons.Left)
            {
                return;
            }

            if (CurrentError != null)
            {
                TrayIcon.ShowBalloonTip(5000, null, CurrentError.GetMessages(), ToolTipIcon.Error);
            }
            else if (HaClient.IsConnected)
            {
                TrayIcon.ShowBalloonTip(5000, null, $"Connected to {HaClient.ServerName} and reporting '{HaAwaySensor.Name}' ({HaAwaySensor.Type}.{HaAwaySensor.MqttName})", ToolTipIcon.Info);
            }
            else
            {
                TrayIcon.ShowBalloonTip(5000, null, "Not connected", ToolTipIcon.Warning);
            }
        }
    }
}
