using System;
using System.Windows.Forms;

namespace HAPCTracker
{
    public partial class ConfigForm : Form
    {
        private ConfigForm()
        {
            InitializeComponent();
        }

        private static Lazy<ConfigForm> _instance { get; } = new Lazy<ConfigForm>(() => new ConfigForm());

        public static void Show()
        {
            if (_instance.Value.Visible)
            {
                _instance.Value.Focus();
            }
            else
            {
                _instance.Value.ShowDialog();
            }
        }

    }
}
