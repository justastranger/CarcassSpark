using ScintillaNET;
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
            scintillaEditor.Styles[Style.Json.Default].ForeColor = Color.Silver;
            scintillaEditor.Styles[Style.Json.BlockComment].ForeColor = Color.Green;
            scintillaEditor.Styles[Style.Json.LineComment].ForeColor = Color.Green;
            scintillaEditor.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintillaEditor.Styles[Style.Json.PropertyName].ForeColor = Color.Red;
            scintillaEditor.Styles[Style.Json.String].ForeColor = Color.DarkRed;
            scintillaEditor.Styles[Style.Json.StringEol].ForeColor = Color.Pink;
            scintillaEditor.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            scintillaEditor.Styles[Style.Json.Keyword].ForeColor = Color.SkyBlue;
            scintillaEditor.Styles[Style.Json.LdKeyword].ForeColor = Color.SkyBlue;
        }


        private void contentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objectType = contentTypeComboBox.Text;
        }

        private void ClipboardImporter_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                scintillaEditor.Text = Clipboard.GetText();
            }
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

        private void scintillaEditor_TextChanged(object sender, EventArgs e)
        {
            objectText = scintillaEditor.Text;
        }
    }
}
