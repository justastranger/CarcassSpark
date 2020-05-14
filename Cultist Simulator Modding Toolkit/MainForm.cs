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
using CultistSimulatorModdingToolkit.ObjectTypes;
using CultistSimulatorModdingToolkit.ObjectViewers;

namespace CultistSimulatorModdingToolkit
{
    public partial class MainForm : Form
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private string directoryToVanillaContent = "./cultistsimulator_Data/StreamingAssets/content/core/";

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(currentDirectory + "csmt.settings.json"))
            {
                Settings.loadSettings(currentDirectory + "csmt.settings.json");
            }
            if (Settings.settings["openWithVanilla"] != null && Settings.settings["openWithVanilla"].ToObject<bool>())
            {
                ModViewer mv = new ModViewer(directoryToVanillaContent, true);
                Utilities.currentMods.Add(mv);
                mv.Show();
            }
            if (Settings.settings["rememberPreviousMod"] != null && Settings.settings["rememberPreviousMod"].ToObject<bool>())
            {
                ModViewer mv = new ModViewer(Settings.settings["previousMod"].ToString(), false);
                Utilities.currentMods.Add(mv);
                mv.Show();
            }
        }

        private void loadVanillaButton_Click(object sender, EventArgs e)
        {
            ModViewer mv = new ModViewer(directoryToVanillaContent, true);
            Utilities.currentMods.Add(mv);
            mv.Show();
        }

        private void openModButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = currentDirectory;
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string location = folderBrowserDialog1.SelectedPath;
                ModViewer mv = new ModViewer(location, false);
                Utilities.currentMods.Add(mv);
                Settings.settings["previousMod"] = mv.currentDirectory;
                mv.Show();
            }
        }

        private void openSettingsButton_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
