using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    public partial class StatusForm : Form
    {
        public StatusForm()
        {
            InitializeComponent();
        }

        private void StatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public Task SetStatus(string text)
            => UiStatusLabel.Invoke(() => UiStatusLabel.Text = text);
    }
}
