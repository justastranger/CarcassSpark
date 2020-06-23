using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class ClipboardImporter : Form
    {
        public string objectType;
        public string objectText;

        public ClipboardImporter()
        {
            InitializeComponent();
        }
        

        private void contentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectType = contentTypeComboBox.Text;
        }

        private void ClipboardImporter_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                contentTextBox.Text = Clipboard.GetText();
            }
        }

        private void contentTextBox_TextChanged(object sender, EventArgs e)
        {
            objectText = contentTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (objectType == null)
            {
                MessageBox.Show("Please select an object type from the combo box next to the OK button.");
                return;
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
