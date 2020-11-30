using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    public sealed partial class ConfigForm : Form
    {
        private ConfigForm(Configuration config, Func<Task> saveCallback)
        {
            InitializeComponent();
            SaveCallback = saveCallback;

            UiMqttServer.Bind(config, nameof(Configuration.MqttServer));
            UiAfkTime.Bind(config, nameof(Configuration.AwayMinutes));
            UiUpdateSeconds.Bind(config, nameof(Configuration.UpdateSeconds));
        }

        private static ConfigForm Instance { get; set; }
        public Func<Task> SaveCallback { get; }

        /// <summary>
        /// Show the form as a dialog, and return true if the user saves changes.
        /// If an instance of the form is already visible, it is brought to the
        /// foreground and this method returns false.
        /// </summary>
        /// <param name="config">The configuration. The values in this
        /// object will be modified</param>
        /// <param name="saveCallback">Callback when save is called</param>
        public static bool ModifyConfig(Configuration config, Func<Task> saveCallbackAsync)
        {
            if (Instance != null)
            {
                Instance.Show();
                Instance.BringToFront();
                return false;
            }

            Instance = new ConfigForm(config, saveCallbackAsync);

            return Instance.ShowDialog() == DialogResult.OK;
        }

        private async void UiSave_Click(object sender, EventArgs e)
        {
            try
            {
                await SaveCallback.Invoke().ConfigureAwait(true);

                // close form
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UiCancel_Click(object sender, EventArgs e)
            => Close(); // let ConfigForm_FormClosing handle this

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK) return; // bypass if uiSave closed it

            if (MessageBox.Show(this, "Continue without saving?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // close form
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                // prevent closing
                e.Cancel = true;
            }
        }
        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }
    }
}
