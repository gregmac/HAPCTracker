using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HAPCTracker
{
    /// <summary>
    /// Container for <see cref="NotifyIcon">tray icon</see> as the root
    /// application object.
    /// </summary>
    public class TrayIconApplicationContext : ApplicationContext
    {
        private NotifyIcon TrayIcon { get; }

        public TrayIconApplicationContext()
        {
            TrayIcon = new NotifyIcon()
            {
                Text = "HomeAssistant Activity Monitor",
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenuStrip = new ContextMenuStrip(),
            };

            // ensure icon disappears when we are exiting
            Application.ApplicationExit += (_, __) => TrayIcon?.Hide();

            // load config
            var config = Configuration.LoadOrCreate();

            // setup config UI
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Configuration", null, (_, __)
                => ConfigForm.ModifyConfig(config, () => config.Save())));

            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("E&xit", null, (_, __) => Application.Exit()));

            if (config?.IsValid() != true)
            {
                if (config == null) config = new Configuration();

                // show UI 
                if (!ConfigForm.ModifyConfig(config, () => config.Save()))
                {
                    MessageBox.Show("Cannot continue without valid config", "No Configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    throw new System.Exception("Cannot continue. TODO improve this state.");
                }
            }

            // ready: show the icon
            TrayIcon.Visible = true;
        }
    }
}
