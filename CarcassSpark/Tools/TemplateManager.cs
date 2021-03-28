using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using ScintillaNET;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public enum TemplateManagerMode
    {
        Normal,
        Selecting
    }
    
    public partial class TemplateManager : Form
    {
        private readonly string templatesPath = Path.Combine(Path.GetFullPath(Application.StartupPath), "templates");
        private bool unsavedChanged = false;
        public ListViewItem SelectedItem;

        public TemplateManager()
        {
            InitializeComponent();
            SetScintillaStyle();
            EnumerateTemplates();
        }

        public TemplateManager(TemplateManagerMode mode, Type entityType)
        {
            InitializeComponent();
            SetScintillaStyle();
            EnumerateTemplates();
            if (mode == TemplateManagerMode.Selecting)
            {
                SetSelectionMode();
            }

            if (entityType != null)
            {
                foreach (ListViewItem item in templatesListView.Items)
                {
                    if (!item.Text.Contains(entityType.Name))
                    {
                        item.Remove();
                    }
                }
            }
        }

        private void SetSelectionMode()
        {
            selectButton.Visible = true;
        }

        private void SetScintillaStyle()
        {
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
        }

        public void EnumerateTemplates()
        {
            if (!Directory.Exists(templatesPath))
            {
                Directory.CreateDirectory(templatesPath);
            }

            if (Directory.EnumerateFiles(templatesPath, "*.json", SearchOption.AllDirectories).Any())
            {
                foreach (string templateFile in Directory.EnumerateFiles(templatesPath, "*.json", SearchOption.AllDirectories))
                {
                    string folderName = Path.GetDirectoryName(templateFile)?.Split(Path.DirectorySeparatorChar).Last();
                    ListViewGroup group = templatesListView.Groups[folderName] ?? new ListViewGroup(folderName, folderName);
                    if (!templatesListView.Groups.Contains(group))
                    {
                        templatesListView.Groups.Add(group);
                    }
                    string templateJson = File.ReadAllText(templateFile);
                    ListViewItem templateListViewItem = new ListViewItem(Path.GetFileName(templateFile)) { Tag = templateJson, Name = Path.GetFileName(templateFile), Group = group };
                    templatesListView.Items.Add(templateListViewItem);
                }
            }
        }


        private void TemplatesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templatesListView.SelectedItems.Count == 1)
            {
                SelectedItem = templatesListView.SelectedItems[0];
                scintilla1.Tag = templatesListView.SelectedItems[0].Text;
                scintilla1.Text = templatesListView.SelectedItems[0].Tag as string;
            }

        }

        public ListViewItem CreateTemplateFile(ListViewItem item, string filename, Type entityType)
        {
            string newFileName = entityType.Name + "_" + filename + ".json";
            string entityJson = JsonConvert.SerializeObject(Activator.CreateInstance(entityType));
            string filepath = Path.Combine(templatesPath, entityType.Name, newFileName);
            if (!Directory.Exists(Path.Combine(templatesPath, entityType.Name)))
            {
                Directory.CreateDirectory(Path.Combine(templatesPath, entityType.Name));
            }

            if (File.Exists(filepath) && MessageBox.Show("File already exists, do you want to overwrite it?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return item;
            }
            using (FileStream fileStream = File.Open(filepath, FileMode.CreateNew))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.WriteRaw(entityJson);
                jsonTextWriter.Flush();
            }
            item.Tag = entityJson;
            item.Text = newFileName;
            item.Name = newFileName;
            return item;
        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            TemplateSetup templateSetup = new TemplateSetup();
            if (templateSetup.ShowDialog() == DialogResult.OK)
            {
                string filename = templateSetup.filenameTextBox.Text;
                ListViewItem newListViewItem = new ListViewItem();
                switch (templateSetup.comboBox1.Text)
                {
                    case "Aspect":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Aspect));
                        break;
                    case "Element":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Element));
                        break;
                    case "Recipe":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Recipe));
                        break;
                    case "Deck":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Deck));
                        break;
                    case "Legacy":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Legacy));
                        break;
                    case "Ending":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Ending));
                        break;
                    case "Verb":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Verb));
                        break;
                    case "Culture":
                        newListViewItem = CreateTemplateFile(newListViewItem, filename, typeof(Culture));
                        break;
                }
                if (newListViewItem.Tag != null)
                {
                    templatesListView.Items.Add(newListViewItem);
                }
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
            if (templatesListView.SelectedItems.Count != 1)
            {
                return;
            }

            string filename = templatesListView.SelectedItems[0].Text;
            DeleteFileAndEntry(filename);
        }

        private void SaveFile(string filename, string json)
        {
            string type = filename.Split('_')[0];
            string filepath = Path.Combine(templatesPath, type, filename);
            using (FileStream fileStream = File.Open(filepath, FileMode.Create))
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
            if (!unsavedChanged || VerifyQuit())
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void Scintilla1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                unsavedChanged = scintilla1.Text != (templatesListView.Items[scintilla1.Tag as string].Tag as string);
            }
            catch (NullReferenceException)
            {
                // We just deleted the file being referenced, most likely.
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null && (!unsavedChanged || VerifyQuit()))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool VerifyQuit()
        {
            return DialogResult.Yes == MessageBox.Show("Are you sure you want to quit? You will lose all unsaved changes.", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}
