using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static async Task Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(await TrayIconApplicationContext.CreateAsync().ConfigureAwait(false));
        }

    }
}
