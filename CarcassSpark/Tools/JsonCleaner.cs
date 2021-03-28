using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                int failCount = 0;
                int successCount = 0;
                jsonFiles.Clear();
                inputTextBox.Text = inputBrowserDialog.SelectedPath;
                foreach (string file in Directory.EnumerateFiles(inputBrowserDialog.SelectedPath, "*.json", SearchOption.AllDirectories))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(file, FileMode.Open))
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string newText = "{}";
                            if (!sr.EndOfStream)
                            {
                                JObject deserialized = JObject.Parse(sr.ReadToEnd());
                                newText = Utilities.SerializeObject(deserialized);
                            }
                            // isolates the file's path from the selected folder
                            string path = file.Remove(file.IndexOf(inputBrowserDialog.SelectedPath), inputBrowserDialog.SelectedPath.Length);
                            jsonFiles.Add(path, newText);
                        }
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        if (failCount <= 3)
                        {
                            MessageBox.Show("Failed to load and clean the following file:\r\n" + file + "\r\n" + ex.Message);
                        }
                        failCount++;
                    }
                }
                MessageBox.Show("Loaded and cleaned " + successCount + " files. "
                    + (failCount > 0 ? "Failed to clean " + failCount + " files. " : "")
                    + " Ready to save.");

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
