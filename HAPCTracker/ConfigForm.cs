using System;
using System.Windows.Forms;

namespace HAPCTracker
{
    public partial class ConfigForm : Form
    {
        private ConfigForm(Configuration config, Action saveCallback)
        {
            InitializeComponent();
            SaveCallback = saveCallback;

            UiUrl.Bind(config, nameof(Configuration.BaseUrl));
            UiToken.Bind(config, nameof(Configuration.AccessToken));
            UiAfkTime.Bind(config, nameof(Configuration.AwayMinutes));
            UiUpdateSeconds.Bind(config, nameof(Configuration.UpdateSeconds));
        }

        private static ConfigForm Instance { get; set; }
        public Action SaveCallback { get; }

        /// <summary>
        /// Show the form as a dialog, and return true if the user saves changes.
        /// If an instance of the form is already visible, it is brought to the
        /// foreground and this method returns false.
        /// </summary>
        /// <param name="config">The configuration. The values in this
        /// object will be modified</param>
        /// <param name="saveCallback">Callback when save is called</param>
        public static bool ModifyConfig(Configuration config, Action saveCallback)
        {
            if (Instance != null)
            {
                Instance.Show();
                Instance.BringToFront();
                return false;
            }

            Instance = new ConfigForm(config, saveCallback);

            return Instance.ShowDialog() == DialogResult.OK;
        }

        private void UiSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCallback.Invoke();

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
