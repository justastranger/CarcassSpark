using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.ObjectViewers
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
            if (manifest.dependencies != null)
            {
                foreach (string dep in manifest.dependencies)
                {
                    string[] depPieces = dep.Split(' ');
                    if (depPieces.Count() > 0)
                    {
                        if (depPieces.Count() == 1) dependeniesDataGridView.Rows.Add(dep);
                        if (depPieces.Count() == 3) dependeniesDataGridView.Rows.Add(depPieces[0], depPieces[1], depPieces[2]);
                    }
                    // dependeniesDataGridView.Rows.Add(dep.modId, dep.VersionOperator, dep.version);
                }
            }
        }

        private void modNameTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.name = modNameTextBox.Text;
            if (displayedManifest.name == "")
            {
                displayedManifest.name = null;
            }
        }

        private void modAuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.author = modAuthorTextBox.Text;
            if (displayedManifest.author == "")
            {
                displayedManifest.author = null;
            }
        }

        private void modVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.version = modVersionTextBox.Text;
            if (displayedManifest.version == "")
            {
                displayedManifest.version = null;
            }
        }

        private void modDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description = modDescriptionTextBox.Text;
            if (displayedManifest.description == "")
            {
                displayedManifest.description = null;
            }
        }

        private void longDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedManifest.description_long = longDescriptionTextBox.Text;
            if (displayedManifest.description_long == "")
            {
                displayedManifest.description_long = null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if(dependeniesDataGridView.RowCount > 1)
            {
                displayedManifest.dependencies = new List<string>();
                foreach (DataGridViewRow row in dependeniesDataGridView.Rows)
                {
                    Manifest.Dependency dep = new Manifest.Dependency();
                    if (row.Cells[0].Value != null) dep.modId = row.Cells[0].Value.ToString();
                    if (row.Cells[1].Value != null) dep.VersionOperator = row.Cells[1].Value.ToString();
                    if (row.Cells[2].Value != null) dep.version = row.Cells[2].Value.ToString();
                    if (dep.modId != null)
                    {
                        displayedManifest.dependencies.Add(dep.ToString());
                    }
                }
            }
            displayedManifest.name.Replace(' ', '_');
            displayedManifest.name.Replace('.', '_');
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
