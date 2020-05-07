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
    public partial class LegacyViewer : Form
    {
        public Legacy displayedLegacy;
        bool editing;

        public LegacyViewer(Legacy legacy, bool? editing)
        {
            InitializeComponent();
            displayedLegacy = legacy;
            fillValues(legacy);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Legacy legacy)
        {
            idTextBox.Text = legacy.id;
            labelTextBox.Text = legacy.label;
            descriptionTextBox.Text = legacy.description;
            startdescriptionTextBox.Text = legacy.startdescription;
            imageTextBox.Text = legacy.image;
            fromEndingTextBox.Text = legacy.fromEnding;
            availableWithoutEndingMatchCheckBox.Checked = legacy.availableWithoutEndingMatch;
            startingVerbIdTextBox.Text = legacy.startingVerbId;
            if (legacy.effects != null)
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects)
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (legacy.excludesOnEnding != null)
            {
                foreach (string ending in legacy.excludesOnEnding)
                {
                    excludesOnEndingListBox.Items.Add(ending);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = editing;
            labelTextBox.ReadOnly = editing;
            descriptionTextBox.ReadOnly = editing;
            startdescriptionTextBox.ReadOnly = editing;
            imageTextBox.ReadOnly = editing;
            fromEndingTextBox.ReadOnly = editing;
            availableWithoutEndingMatchCheckBox.Enabled = editing;
            startingVerbIdTextBox.ReadOnly = editing;
            effectsDataGridView.ReadOnly = !editing;
            effectsDataGridView.AllowUserToAddRows = editing;
            effectsDataGridView.AllowUserToDeleteRows = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            addExcludesButton.Visible = editing;
            addExcludesTextBox.Visible = editing;
            excludeAddLabel.Visible = editing;
        }

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (editing) return;
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
            ev.ShowDialog();
        }

        private void excludesOnEndingListBox_DoubleClick(object sender, EventArgs e)
        {
            if (editing) return;
            string id = excludesOnEndingListBox.SelectedItem.ToString();
            LegacyViewer lv = new LegacyViewer(Utilities.getLegacy(id), false);
            lv.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (effectsDataGridView.RowCount > 1)
            {
                displayedLegacy.effects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedLegacy.effects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void addExcludesButton_Click(object sender, EventArgs e)
        {
            if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
            {
                excludesOnEndingListBox.Items.Add(addExcludesTextBox.Text);
                displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                addExcludesTextBox.Text = "";
                addExcludesTextBox.Focus();
            }
        }

        private void addExcludesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
                {
                    excludesOnEndingListBox.Items.Add(addExcludesTextBox.Text);
                    displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                    addExcludesTextBox.Text = "";
                    addExcludesTextBox.Focus();
                }
            }
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.label = labelTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.description = descriptionTextBox.Text;
        }

        private void startdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startdescription = startdescriptionTextBox.Text;
        }

        private void imageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.image = imageLabel.Text;
        }

        private void fromEndingTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.fromEnding = fromEndingTextBox.Text;
        }

        private void startingVerbIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startingVerbId = startingVerbIdTextBox.Text;
        }

        private void availableWithoutEndingMatch_CheckedChanged(object sender, EventArgs e)
        {
            displayedLegacy.availableWithoutEndingMatch = availableWithoutEndingMatchCheckBox.Checked;
        }
    }
}
