using ScintillaNET;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class JsonEditor : Form
    {
        public string ObjectType;
        public string ObjectText;

        public JsonEditor(string json)
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
            scintillaEditor.Text = json;
            if (Settings.settings["jsonEditorLastContentType"] != null && Settings.settings["jsonEditorLastContentType"].ToString() != "")
            {
                contentTypeComboBox.Text = Settings.settings["jsonEditorLastContentType"].ToString();
            }
        }

        public JsonEditor(string json, string type)
        {
            InitializeComponent();
            try
            {
                contentTypeComboBox.Text = type;
            }
            catch (Exception)
            {
                MessageBox.Show("That was an invalid type.");
                return;
            }
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
            scintillaEditor.Text = json;
        }

        public JsonEditor(string json, bool hideComboBox, bool readOnly)
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
            scintillaEditor.Text = json;
            if (hideComboBox)
            {
                contentTypeComboBox.Visible = false;
            }
            else
            {
                if (Settings.settings["jsonEditorLastContentType"] != null && Settings.settings["jsonEditorLastContentType"].ToString() != "")
                {
                    contentTypeComboBox.Text = Settings.settings["jsonEditorLastContentType"].ToString();
                }
            }
            scintillaEditor.ReadOnly = readOnly;
            Text = readOnly ? "JSON Viewer" : "JSON Editor";
        }

        public JsonEditor(string json, string type, bool readOnly)
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
            scintillaEditor.Text = json;
            contentTypeComboBox.Enabled = false;
            contentTypeComboBox.Text = type;
            scintillaEditor.ReadOnly = readOnly;
            Text = readOnly ? "JSON Viewer" : "JSON Editor";
        }

        private void ContentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjectType = contentTypeComboBox.Text;
            Settings.settings["jsonEditorLastContentType"] = ObjectType;
            Settings.SaveSettings();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScintillaEditor_TextChanged(object sender, EventArgs e)
        {
            ObjectText = scintillaEditor.Text;
        }

        private void JsonEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel && contentTypeComboBox.Visible && ObjectType == null)
            {
                MessageBox.Show("Please select an object type from the combo box next to the OK button.");
                e.Cancel = true;
            }
        }
    }
}
