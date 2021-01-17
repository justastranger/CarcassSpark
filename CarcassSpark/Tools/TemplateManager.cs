using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class TemplateManager : Form
    {
        readonly string templatesPath = Path.Combine(Path.GetFullPath(Application.StartupPath), "templates");
        bool unsavedChanged = false;

        public TemplateManager()
        {
            InitializeComponent();
            scintilla1.Styles[Style.Json.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Json.BlockComment].ForeColor = Color.Green;
            scintilla1.Styles[Style.Json.LineComment].ForeColor = Color.Green;
            scintilla1.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Json.PropertyName].ForeColor = Color.Red;
            scintilla1.Styles[Style.Json.String].ForeColor = Color.DarkRed;
            scintilla1.Styles[Style.Json.StringEol].ForeColor = Color.Pink;
            scintilla1.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Json.Keyword].ForeColor = Color.SkyBlue;
            scintilla1.Styles[Style.Json.LdKeyword].ForeColor = Color.SkyBlue;
            EnumerateTemplates();
        }

        public void EnumerateTemplates()
        {
            if (!Directory.Exists(templatesPath)) Directory.CreateDirectory(templatesPath);
            if (Directory.EnumerateFiles(templatesPath, "*.json", SearchOption.AllDirectories).Count() > 0)
            {
                foreach (string templateFile in Directory.EnumerateFiles(templatesPath, "*.json"))
                {
                    string templateJSON = File.ReadAllText(templateFile);
                    ListViewItem templateListViewItem = new ListViewItem(Path.GetFileName(templateFile)) { Tag = templateJSON, Name = Path.GetFileName(templateFile) };
                    templatesListView.Items.Add(templateListViewItem);
                }
            }
        }


        private void TemplatesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templatesListView.SelectedItems.Count != 1) return;

            scintilla1.Tag = templatesListView.SelectedItems[0].Text;
            scintilla1.Text = templatesListView.SelectedItems[0].Tag as string;
        }

        public string CreateTemplateFile(string filepath, Type entityType)
        {
            string entityJson = JsonConvert.SerializeObject(Activator.CreateInstance(entityType));
            using (FileStream fileStream = File.Open(filepath, FileMode.CreateNew))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.WriteRaw(entityJson);
                jsonTextWriter.Flush();
            }
            return entityJson;
        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            TemplateSetup templateSetup = new TemplateSetup();
            if (templateSetup.ShowDialog() == DialogResult.OK)
            {
                string filename = templateSetup.filenameTextBox.Text;
                string filepath = Path.Combine(templatesPath, filename);
                ListViewItem newListViewItem = new ListViewItem(filename) { Name = filename };
                if (File.Exists(filepath) && MessageBox.Show("File already exists, do you want to overwrite it?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                switch (templateSetup.comboBox1.Text)
                {
                    case "Aspect":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Aspect));
                        break;
                    case "Element":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Element));
                        break;
                    case "Recipe":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Recipe));
                        break;
                    case "Deck":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Deck));
                        break;
                    case "Legacy":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Legacy));
                        break;
                    case "Ending":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Ending));
                        break;
                    case "Verb":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Verb));
                        break;
                    case "Culture":
                        newListViewItem.Tag = CreateTemplateFile(filepath, typeof(Culture));
                        break;

                    default:
                        break;
                }
                templatesListView.Items.Add(newListViewItem);
            }
        }

        private void DeleteFileAndEntry(string filename)
        {
            string pathToFile = Path.Combine(Path.GetFullPath(Application.StartupPath), "templates\\", filename);
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete: " + filename + "?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                File.Delete(pathToFile);
                templatesListView.SelectedItems[0].Remove();
                scintilla1.Text = string.Empty;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (templatesListView.SelectedItems.Count != 1) return;
            string filename = templatesListView.SelectedItems[0].Text;
            DeleteFileAndEntry(filename);
        }

        private void SaveFile(string filename, string json)
        {
            string filepath = Path.Combine(templatesPath, filename);
            // File.Delete(filepath);
            using (FileStream fileStream = File.Open(filepath, FileMode.CreateNew))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;
                jsonTextWriter.WriteRaw(json);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (scintilla1.Text.Length == 0)
            {
                switch (MessageBox.Show("The editor is empty, do you want to delete the template (Yes), save an empty file (No), or cancel the attempt (Cancel)?", "Empty Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        DeleteFileAndEntry(scintilla1.Tag as string);
                        unsavedChanged = false;
                        break;
                    case DialogResult.No:
                        templatesListView.Items[scintilla1.Tag as string].Tag = string.Empty;
                        SaveFile(scintilla1.Tag as string, scintilla1.Text);
                        unsavedChanged = false;
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        MessageBox.Show("This should never, ever be seen.", "What have you done?????");
                        break;
                }
            }
            else
            {
                templatesListView.Items[scintilla1.Tag as string].Tag = scintilla1.Text;
                SaveFile(scintilla1.Tag as string, scintilla1.Text);
                unsavedChanged = false;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (unsavedChanged)
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to quit? You will lose all unsaved changes.", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void Scintilla1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (scintilla1.Text != templatesListView.Items[scintilla1.Tag as string].Tag as string)
                {
                    unsavedChanged = true;
                }
                else
                {
                    unsavedChanged = false;
                }
            }
            catch (NullReferenceException ex)
            {
                // We just deleted the file being referenced, most likely.
            }
        }
    }
}
