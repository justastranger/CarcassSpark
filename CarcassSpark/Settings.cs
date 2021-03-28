using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark
{
    public partial class Settings : Form
    {
        public static JObject settings = new JObject();

        // settings["openWithVanilla"] bool
        // settings["loadPreviousMods"] bool
        // settings["previousMods"] string[]
        // settings["saveCleanedVanillaContent"] bool
        // settings["loadAllFlowchartNodes"] bool
        // settings["portable"] bool
        // settings["GamePath"] string
        private static readonly string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public Settings()
        {
            InitializeComponent();
            PopulateSettings();
        }

        public static void SaveSettings()
        {
            // Takes the contents of the previousMods setting, validates it to remove duplicates and paths without a synopsis file
            if (HasPreviousMods())
            {
                settings["previousMods"] = JArray.FromObject(ValidatePreviousMods(GetPreviousMods()));
            }
            else if (settings.ContainsKey("previousMods"))
            {
                settings.Remove("previousMods");
            }

            using (FileStream settingsFile = File.Open(CurrentDirectory + "csmt.settings.json", FileMode.Create))
            {
                string settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(settingsFile)))
                {
                    jtw.WriteRaw(settingsJson);
                }
            }
        }

        public static void LoadSettings(string settingsFilePath)
        {
            try
            {
                settings = JsonConvert.DeserializeObject<JObject>(new StreamReader(settingsFilePath).ReadToEnd());
            }
            catch (IOException)
            {
                MessageBox.Show("Settings file in use by another process!", "IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void PopulateSettings()
        {
            if (settings["loadPreviousMods"] != null)
            {
                loadPreviousModsCheckBox.Checked = settings["loadPreviousMods"].ToObject<bool>();
            }

            if (settings["previousMods"]?.ToObject<List<string>>()?.Count > 0)
            {
                previousModsTextBox.Text = string.Join("\r\n", settings["previousMods"].ToObject<List<string>>());
            }

            if (settings["loadAllFlowchartNodes"] != null)
            {
                loadAllFlowchartNodesCheckBox.Checked = settings["loadAllFlowchartNodes"].ToObject<bool>();
            }

            if (settings["portable"] != null)
            {
                portableCheckBox.Checked = settings["portable"].ToObject<bool>();
            }

            if (settings["GamePath"] != null)
            {
                GamePathTextBox.Text = settings["GamePath"].ToString();
            }
        }

        private void LoadPreviousModsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["loadPreviousMods"] = loadPreviousModsCheckBox.Checked;
            SaveSettings();
        }

        public static bool HasPreviousMods()
        {
            return settings["previousMods"] is JArray && settings["previousMods"].Any();
        }

        public static List<string> GetPreviousMods()
        {
            return settings["previousMods"]?.ToObject<List<string>>();
        }

        public static bool RemovePreviousMod(string path)
        {
            List<string> tmp = settings["previousMods"]?.ToObject<List<string>>();
            if (tmp != null && tmp.Contains(path))
            {
                tmp.RemoveAll(str => str == path);
                settings["previousMods"] = JArray.FromObject(tmp);
                SaveSettings();
                return true;
            }
            return false;
        }

        public static void AddPreviousMod(string path)
        {
            (settings["previousMods"] as JArray)?.Add(path);
            SaveSettings();
        }

        public static void InitPreviousMods(string path)
        {
            settings["previousMods"] = new JArray(path);
            SaveSettings();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // take the contents of the previous mods text box and store it
            List<string> modPaths = previousModsTextBox.Text.Split("\r\n".ToCharArray()).ToList();
            if (modPaths.Count > 0)
            {
                settings["previousMods"] = JArray.FromObject(modPaths);
            }
            else if (settings.ContainsKey("previousMods"))
            {
                settings.Remove("previousMods");
            }
            // then we validate them in this function
            SaveSettings();
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadAllFlowchartNodesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["loadAllFlowchartNodes"] = loadAllFlowchartNodesCheckBox.Checked;
            SaveSettings();
        }

        private void PreviousModsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void GamePathTextBox_DoubleClick(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = CurrentDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(folderBrowserDialog.SelectedPath + "\\cultistsimulator.exe"))
                {
                    GamePathTextBox.Text = folderBrowserDialog.SelectedPath;
                }
                else
                {
                    MessageBox.Show("Please select your game's installation folder.");
                }
            }
        }

        private static List<string> ValidatePreviousMods(List<string> previousMods)
        {
            if (previousMods == null)
            {
                return null;
            }
            List<string> validatedPaths = new List<string>();
            foreach (string path in previousMods.Distinct())
            {
                if (Directory.Exists(path) && File.Exists(path + "\\synopsis.json"))
                {
                    validatedPaths.Add(path);
                }
            }
            // settings["previousMods"] = JArray.FromObject(validatedPaths.Distinct());
            return validatedPaths;
        }

        private void PortableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.settings["portable"] = portableCheckBox.Checked;
            SaveSettings();
        }
    }
}
