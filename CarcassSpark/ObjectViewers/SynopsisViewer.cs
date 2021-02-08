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
using System.Text.RegularExpressions;

namespace CarcassSpark.ObjectViewers
{
    public partial class SynopsisViewer : Form
    {
        public Synopsis displayedSynopsis;

        public SynopsisViewer(Synopsis synopsis)
        {
            InitializeComponent();
            this.displayedSynopsis = synopsis;
            modNameTextBox.Text = synopsis.name;
            modAuthorTextBox.Text = synopsis.author;
            modVersionTextBox.Text = synopsis.version;
            modDescriptionTextBox.Text = synopsis.description;
            longDescriptionTextBox.Text = synopsis.description_long;
            if (synopsis.dependencies != null)
            {
                foreach (string dep in synopsis.dependencies)
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

        private void ModNameTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSynopsis.name = modNameTextBox.Text;
            if (displayedSynopsis.name == "")
            {
                displayedSynopsis.name = null;
            }
        }

        private void ModAuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSynopsis.author = modAuthorTextBox.Text;
            if (displayedSynopsis.author == "")
            {
                displayedSynopsis.author = null;
            }
        }

        private void ModVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSynopsis.version = modVersionTextBox.Text;
            if (displayedSynopsis.version == "")
            {
                displayedSynopsis.version = null;
            }
        }

        private void ModDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSynopsis.description = modDescriptionTextBox.Text;
            if (displayedSynopsis.description == "")
            {
                displayedSynopsis.description = null;
            }
        }

        private void LongDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSynopsis.description_long = longDescriptionTextBox.Text;
            if (displayedSynopsis.description_long == "")
            {
                displayedSynopsis.description_long = null;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // if (!(new Regex("^([a-zA-Z_]+)$").IsMatch(displayedSynopsis.name)))
            // {
            //     MessageBox.Show("Mod name should only consist of letters (upper and lowercase) and underscores or else the game will never say that the mod is present when used as a dependency.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // }
            if (dependeniesDataGridView.RowCount > 1)
            {
                displayedSynopsis.dependencies = new List<string>();
                foreach (DataGridViewRow row in dependeniesDataGridView.Rows)
                {
                    Synopsis.Dependency dep = new Synopsis.Dependency();
                    if (row.Cells[0].Value != null) dep.modId = row.Cells[0].Value.ToString();
                    if (row.Cells[1].Value != null) dep.VersionOperator = row.Cells[1].Value.ToString();
                    if (row.Cells[2].Value != null) dep.version = row.Cells[2].Value.ToString();
                    if (dep.modId != null)
                    {
                        displayedSynopsis.dependencies.Add(dep.ToString());
                    }
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SynopsisViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (displayedSynopsis.name == null || displayedSynopsis.name == "")
            {
                MessageBox.Show("You must specify a name for your mod.", "Name Not Specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
