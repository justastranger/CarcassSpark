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
        // settings["rememberPreviousMod"]
        // settings["previousMod"]
        // settings["saveCleanedVanillaContent"]
        // settings["loadAllFlowchartNodes"]
        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public Settings()
        {
            InitializeComponent();
            populateSettings();
        }

        public static void saveSettings()
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

        public static void loadSettings(string settingsFilePath)
        {
            settings = JsonConvert.DeserializeObject<JObject>(new StreamReader(settingsFilePath).ReadToEnd());
        }

        void populateSettings()
        {
            if (settings["openWithVanilla"] != null) openWithVanillaCheckBox.Checked = settings["openWithVanilla"].ToObject<bool>();
            if (settings["rememberPreviousMod"] != null) rememberPreviousModCheckBox.Checked = settings["rememberPreviousMod"].ToObject<bool>();
            if (settings["previousMod"] != null) previousModTextBox.Text = settings["previousMod"].ToString();
            if (settings["saveCleanedVanillaContent"] != null) saveCleanedVanillaContentCheckBox.Checked = settings["saveCleanedVanillaContent"].ToObject<bool>();
        }

        private void openWithVanillaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["openWithVanilla"] = openWithVanillaCheckBox.Checked;
            saveSettings();
        }

        private void rememberPreviousModCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["rememberPreviousMod"] = rememberPreviousModCheckBox.Checked;
            saveSettings();
        }

        private void saveCleanedVanillaContentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["saveCleanedVanillaContent"] = saveCleanedVanillaContentCheckBox.Checked;
            saveSettings();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(previousModTextBox.Text))
            {
                settings["previousMod"] = previousModTextBox.Text;
            }
            else
            {
                MessageBox.Show("Directory does not exist for \"Previously Loaded Mod\" Text Box value, resetting to previous value.");
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadAllFlowchartNodesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            settings["loadAllFlowchartNodes"] = loadAllFlowchartNodesCheckBox.Checked;
        }
    }
}
