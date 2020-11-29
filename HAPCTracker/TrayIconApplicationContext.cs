using HAPCTracker.HomeAssistant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    /// <summary>
    /// Container for <see cref="NotifyIcon">tray icon</see> as the root
    /// application object.
    /// </summary>
    public class TrayIconApplicationContext : ApplicationContext
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
        /// The <see cref="HomeAssistantRestClient"/>. Uses <see cref="Configuration.BaseUrl"/>
        /// and <see cref="Configuration.AccessToken"/>.
        /// </summary>
        private HomeAssistantRestClient HaClient { get; set; }

        /// <summary>
        /// Name of sensor to report.
        /// </summary>
        private string SensorName { get; } = $"device_tracker.{Environment.UserName}_{Environment.MachineName}";

        /// <summary>
        /// Friendly OS name to report as attribute
        /// </summary>
        private string OsName { get; } = RuntimeInformation.OSDescription;

        private const string TrayIconTitle = "HomeAssistant Activity Monitor";

        /// <summary>
        /// Start new application context.
        /// <see cref="Configuration.LoadOrCreate">Loads existing configuration</see>
        /// or if not available/valid, presents a <see cref="ConfigForm">configuration UI</see>,
        /// then starts <see cref="MainLoop"/>.
        /// </summary>
        public TrayIconApplicationContext()
        {
            TrayIcon = new NotifyIcon()
            {
                Text = TrayIconTitle,
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenuStrip = new ContextMenuStrip(),
            };

            // ensure icon disappears when we are exiting
            Application.ApplicationExit += (_, __) => TrayIcon?.Hide();

            // load config
            var config = Configuration.LoadOrCreate();

            // setup tray menu
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Configuration", null, (_, __)
                => ConfigForm.ModifyConfig(config, () => SaveAndApply(config))));
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("E&xit", null, (_, __) => Application.Exit()));

            // apply config or prompt user
            if (config.IsValid())
            {
                ApplyConfig(config);
            }
            else
            {
                // first time through, so show UI
                if (!ConfigForm.ModifyConfig(config, () => SaveAndApply(config)))
                {
                    MessageBox.Show("Cannot continue without valid config", "No Configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    throw new Exception("Cannot continue. TODO improve this state.");
                }
            }

            // ready: show the icon
            TrayIcon.Visible = true;

            // start looping
            _ = MainLoop();
        }

        /// <summary>
        /// <see cref="Configuration.Save">Saves</see> and <see cref="ApplyConfig(Configuration)">applies</see>
        /// the <paramref name="config"/>.
        /// </summary>
        /// <param name="config"></param>
        private void SaveAndApply(Configuration config)
        {
            config.Save();
            ApplyConfig(config);
        }

        /// <summary>
        /// Applies the configuration to current form
        /// </summary>
        /// <param name="config"></param>
        private void ApplyConfig(Configuration config)
        {
            HaClient = new HomeAssistantRestClient(new Uri(config.BaseUrl), config.AccessToken);
            AwayTime = TimeSpan.FromMinutes(config.AwayMinutes);
            UpdateInterval = TimeSpan.FromSeconds(config.UpdateSeconds);
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

                    var sensorData = new SensorData
                    {
                        State = away ? "not_home" : "home",
                        Attributes = new Dictionary<string, string>
                        {
                            { "last_seen", lastInputTime.ToString() },
                            { "hostname", Environment.MachineName },
                            { "os", OsName },
                        },
                    };

                    var postResult = await HaClient.PostSensor(SensorName, sensorData).ConfigureAwait(false);
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
