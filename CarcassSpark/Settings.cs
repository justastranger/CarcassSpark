using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace CarcassSpark
{
    public partial class Settings : Form
    {
        public static JObject settings = new JObject();
        // settings["openWithVanilla"]
        // settings["loadPreviousMods"]
        // settings["previousMods"]
        // settings["saveCleanedVanillaContent"]
        // settings["loadAllFlowchartNodes"]
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public Settings()
        {
            InitializeComponent();
            PopulateSettings();
        }

        public static void SaveSettings()
        {
            using (FileStream settingsFile = File.Open(currentDirectory + "csmt.settings.json", FileMode.Create))
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

        void PopulateSettings()
        {
            if (settings["openWithVanilla"] != null) openWithVanillaCheckBox.Checked = settings["openWithVanilla"].ToObject<bool>();
            if (settings["loadPreviousMods"] != null) loadPreviousModsCheckBox.Checked = settings["loadPreviousMods"].ToObject<bool>();
            if (settings["previousMods"] != null) previousModsTextBox.Text = String.Join("\r\n", settings["previousMods"].ToObject<List<string>>());
            if (settings["saveCleanedVanillaContent"] != null) saveCleanedVanillaContentCheckBox.Checked = settings["saveCleanedVanillaContent"].ToObject<bool>();
            if (settings["loadAllFlowchartNodes"] != null) loadAllFlowchartNodesCheckBox.Checked = settings["loadAllFlowchartNodes"].ToObject<bool>();
        }

        private void OpenWithVanillaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["openWithVanilla"] = openWithVanillaCheckBox.Checked;
            SaveSettings();
        }

        private void LoadPreviousModsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["loadPreviousMods"] = loadPreviousModsCheckBox.Checked;
            SaveSettings();
        }

        public static List<string> GetPreviousMods()
        {
            return settings["previousMods"].ToObject<List<string>>();
        }

        private void SaveCleanedVanillaContentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["saveCleanedVanillaContent"] = saveCleanedVanillaContentCheckBox.Checked;
            SaveSettings();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

            List<string> modPaths = previousModsTextBox.Text.Split("\r\n".ToCharArray()).ToList();
            List<string> validatedPaths = new List<string>();
            foreach (string path in modPaths)
            {
                if (Directory.Exists(path) && File.Exists(path + "\\manifest.json")) validatedPaths.Add(path);
            }
            settings["previousMods"] = JArray.FromObject(validatedPaths);
            
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
    }
}
