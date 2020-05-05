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
    public partial class ManifestViewer : Form
    {
        public Manifest displayedManifest;

        public ManifestViewer(Manifest manifest)
        {
            InitializeComponent();
            this.displayedManifest = manifest;
            modNameTextBox.Text = manifest.name;
            modAuthorTextBox.Text = manifest.author;
            modVersionTextBox.Text = manifest.version;
            modDescriptionTextBox.Text = manifest.description;
            longDescriptionTextBox.Text = manifest.description_long;
        }

        private void modNameTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.name = modNameTextBox.Text;
        }

        private void modAuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.author = modAuthorTextBox.Text;
        }

        private void modVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.version = modVersionTextBox.Text;
        }

        private void modDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description = modDescriptionTextBox.Text;
        }

        private void longDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description_long = longDescriptionTextBox.Text;
        }
    }
}
