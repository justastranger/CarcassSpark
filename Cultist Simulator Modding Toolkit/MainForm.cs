using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class MainForm : Form
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private string directoryToVanillaContent = "./cultistsimulator_Data/StreamingAssets/content/core/";

        public MainForm()
        {
            InitializeComponent();
            //ModViewer mv = new ModViewer(directoryToVanillaContent, true);
            //Utilities.currentMods.Add(mv);
            //mv.Show();
        }

        private void loadVanillaButton_Click(object sender, EventArgs e)
        {
            
            //folderBrowserDialog1.SelectedPath = currentDirectory;
            //DialogResult dr = folderBrowserDialog1.ShowDialog();
            //if (dr == DialogResult.OK)
            //{
            //    string location = folderBrowserDialog1.SelectedPath;
            //    ModViewer mv = new ModViewer(location, true);
            //    Utilities.currentMods.Add(mv);
            //    mv.Show();
            //}
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
                mv.Show();
            }
        }

        private void openSettingsButton_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
