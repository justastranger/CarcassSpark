using System;
using System.IO;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class TemplateSetup : Form
    {
        public TemplateSetup()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (filenameTextBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Invalid characters in file name.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
