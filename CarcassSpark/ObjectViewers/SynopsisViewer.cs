using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class SynopsisViewer : Form
    {
        public Synopsis DisplayedSynopsis;

        public SynopsisViewer(Synopsis synopsis)
        {
            InitializeComponent();
            DisplayedSynopsis = synopsis;
        }

        private void FillValues()
        {
            modNameTextBox.Text = DisplayedSynopsis.name;
            modAuthorTextBox.Text = DisplayedSynopsis.author;
            modVersionTextBox.Text = DisplayedSynopsis.version;
            modDescriptionTextBox.Text = DisplayedSynopsis.description;
            longDescriptionTextBox.Text = DisplayedSynopsis.description_long;
            if (DisplayedSynopsis.dependencies != null)
            {
                foreach (string dep in DisplayedSynopsis.dependencies)
                {
                    string[] depPieces = dep.Split(' ');
                    if (depPieces.Any())
                    {
                        if (depPieces.Length == 1)
                        {
                            dependeniesDataGridView.Rows.Add(dep);
                        }

                        if (depPieces.Length == 3)
                        {
                            dependeniesDataGridView.Rows.Add(depPieces[0], depPieces[1], depPieces[2]);
                        }
                    }
                    // dependeniesDataGridView.Rows.Add(dep.modId, dep.VersionOperator, dep.version);
                }
            }
        }

        #region "Text Changed" events

        private void ModNameTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSynopsis.name = GetTextFromBox(modNameTextBox);
        }

        private void ModAuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSynopsis.author = GetTextFromBox(modAuthorTextBox);
        }

        private void ModVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSynopsis.version = GetTextFromBox(modVersionTextBox);
        }

        private void ModDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSynopsis.description = GetTextFromBox(modDescriptionTextBox);
        }

        private void LongDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSynopsis.description_long = GetTextFromBox(longDescriptionTextBox);
        }

        private string GetTextFromBox(TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text) ? null : textBox.Text;
        }

        #endregion

        private void OkButton_Click(object sender, EventArgs e)
        {
            // if (!(new Regex("^([a-zA-Z_]+)$").IsMatch(displayedSynopsis.name)))
            // {
            //     MessageBox.Show("Mod name should only consist of letters (upper and lowercase) and underscores or else the game will never say that the mod is present when used as a dependency.", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // }
            if (dependeniesDataGridView.RowCount > 1)
            {
                DisplayedSynopsis.dependencies = new List<string>();
                foreach (DataGridViewRow row in dependeniesDataGridView.Rows)
                {
                    Synopsis.Dependency dep = new Synopsis.Dependency();
                    if (row.Cells[0].Value != null)
                    {
                        dep.modId = row.Cells[0].Value.ToString();
                    }

                    if (row.Cells[1].Value != null)
                    {
                        dep.VersionOperator = row.Cells[1].Value.ToString();
                    }

                    if (row.Cells[2].Value != null)
                    {
                        dep.version = row.Cells[2].Value.ToString();
                    }

                    if (dep.modId != null)
                    {
                        DisplayedSynopsis.dependencies.Add(dep.ToString());
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
            if (string.IsNullOrEmpty(DisplayedSynopsis.name))
            {
                MessageBox.Show("You must specify a name for your mod.", "Name Not Specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void SynopsisViewer_Shown(object sender, EventArgs e)
        {
            FillValues();
        }
    }
}
