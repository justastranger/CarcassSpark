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

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class MainForm : Form
    {
        private string directoryToContent = "./cultistsimulator_Data/StreamingAssets/content/";
        private string coreContent = "core/";
        private string moddedNewContent = "more/";
        public static string currentModDirectory;

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadContentButton_Click(object sender, EventArgs e)
        {
            aspectListBox.Items.Clear();
            using (FileStream aspectsFile = new FileStream(directoryToContent + coreContent + "elements/_aspects.json", FileMode.Open))
            {
                Aspect.reloadAspects(aspectsFile);
            }
            foreach ( string key in Aspect.aspectsList.Keys.ToArray())
            {
                aspectListBox.Items.Add(key);
            }
        }

        private void newModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            folderBrowserDialog1.ShowDialog();
        }

        private void selectModToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}