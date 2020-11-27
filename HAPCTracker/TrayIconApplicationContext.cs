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

            // setup menu
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Configuration", null, (_, __) => ConfigForm.Show()));
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("E&xit", null, (_, __) => Application.Exit()));

            // ready: show the icon
            TrayIcon.Visible = true;
        }
    }
}
