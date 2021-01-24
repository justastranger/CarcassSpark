using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.Tools
{
    public partial class JsonCleaner : Form
    {

        private readonly Dictionary<string, string> jsonFiles = new Dictionary<string, string>();
        private string output;

        public JsonCleaner()
        {
            InitializeComponent();
            inputBrowserDialog.SelectedPath = Utilities.BaseDirectory;
            outputBrowserDialog.SelectedPath = Utilities.BaseDirectory;
        }

        private void SelectInputFolderButton_Click(object sender, EventArgs e)
        {
            if (inputBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                jsonFiles.Clear();
                inputTextBox.Text = inputBrowserDialog.SelectedPath;
                foreach (string file in Directory.EnumerateFiles(inputBrowserDialog.SelectedPath, "*.json", SearchOption.AllDirectories))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        JObject deserialized = JObject.Parse(new StreamReader(fs).ReadToEnd());
                        string newText = Utilities.SerializeObject(deserialized);
                        // isolates the file's path from the selected folder
                        string path = file.Remove(file.IndexOf(inputBrowserDialog.SelectedPath), inputBrowserDialog.SelectedPath.Length);
                        jsonFiles.Add(path, newText);
                    }
                }
                MessageBox.Show("Loaded and cleaned " + jsonFiles.Count.ToString() + " files. Ready to save.");
                selectOutputFolderButton.Enabled = true;
            }
        }

        private void SelectOutputFolderButton_Click(object sender, EventArgs e)
        {
            if (outputBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                saveButton.Enabled = true;
                output = outputBrowserDialog.SelectedPath;
                outputTextBox.Text = output;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach (string file in jsonFiles.Keys)
            {
                string newPath = output + file;
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(newPath, FileMode.Create))))
                {
                    jtw.WriteRaw(jsonFiles[file]);
                }
            }
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "Explorer.exe",
                Arguments = output
            };
            Process.Start(startInfo);
        }
    }
}
