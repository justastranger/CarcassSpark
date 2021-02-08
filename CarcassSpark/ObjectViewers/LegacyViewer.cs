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
    public partial class LegacyViewer : Form
    {
        public Legacy displayedLegacy;
        event EventHandler<Legacy> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public LegacyViewer(Legacy legacy, EventHandler<Legacy> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            displayedLegacy = legacy;
            FillValues(legacy);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
        }

        void FillValues(Legacy legacy)
        {
            if (legacy.id != null) idTextBox.Text = legacy.id;
            if (legacy.label != null) labelTextBox.Text = legacy.label;
            if (legacy.description != null) descriptionTextBox.Text = legacy.description;
            if (legacy.startdescription != null) startdescriptionTextBox.Text = legacy.startdescription;
            if (legacy.comments != null) commentsTextBox.Text = legacy.comments;
            if (legacy.image != null)
            {
                imageTextBox.Text = legacy.image;
                if (Utilities.LegacyImageExists(legacy.image)) pictureBox1.Image = Utilities.GetLegacyImage(legacy.image);
            }
            if (legacy.deleted.HasValue) deletedCheckBox.CheckState = legacy.deleted.Value ? CheckState.Checked : CheckState.Unchecked;
            if (legacy.fromEnding != null) fromEndingTextBox.Text = legacy.fromEnding;
            if (legacy.availableWithoutEndingMatch.HasValue) availableWithoutEndingMatchCheckBox.CheckState = legacy.availableWithoutEndingMatch.Value ? CheckState.Checked : CheckState.Unchecked;
            if (legacy.startingVerbId != null) startingVerbIdTextBox.Text = legacy.startingVerbId;
            if (legacy.newstart.HasValue) newStartCheckBox.CheckState = legacy.newstart.Value ? CheckState.Checked : CheckState.Unchecked;
            if (legacy.effects != null)
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects)
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (legacy.effects_extend != null)
            {
                foreach (KeyValuePair<string, int> kvp in legacy.effects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(effectsDataGridView, kvp.Key, kvp.Value);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (legacy.effects_remove != null)
            {
                foreach (string removeId in legacy.effects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(effectsDataGridView, removeId);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (legacy.excludesOnEnding != null)
            {
                foreach (string ending in legacy.excludesOnEnding)
                {
                    excludesOnEndingListView.Items.Add(ending);
                }
            }
            if (legacy.excludesOnEnding_prepend != null)
            {
                foreach (string ending in legacy.excludesOnEnding_prepend)
                {
                    ListViewItem item = new ListViewItem(ending)
                    {
                        BackColor = Utilities.ListPrependColor
                    };
                    excludesOnEndingListView.Items.Add(item);
                }
            }
            if (legacy.excludesOnEnding_append != null)
            {
                foreach (string ending in legacy.excludesOnEnding_append)
                {
                    ListViewItem item = new ListViewItem(ending)
                    {
                        BackColor = Utilities.ListAppendColor
                    };
                    excludesOnEndingListView.Items.Add(item);
                }
            }
            if (legacy.excludesOnEnding_remove != null)
            {
                foreach (string ending in legacy.excludesOnEnding_remove)
                {
                    ListViewItem item = new ListViewItem(ending)
                    {
                        BackColor = Utilities.ListRemoveColor
                    };
                    excludesOnEndingListView.Items.Add(item);
                }
            }
            if (legacy.statusbarelements != null)
            {
                foreach (string element in legacy.statusbarelements)
                {
                    statusBarElementsListView.Items.Add(element);
                }
            }
            if (legacy.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", legacy.extends);
            }
            else if (legacy.extends?.Count == 1)
            {
                extendsTextBox.Text = legacy.extends[0];
            }
        }

        void SetEditingMode(bool editing)
        {
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly =!editing;
            descriptionTextBox.ReadOnly = !editing;
            startdescriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            imageTextBox.ReadOnly = !editing;
            fromEndingTextBox.ReadOnly = !editing;
            availableWithoutEndingMatchCheckBox.Enabled = editing;
            startingVerbIdTextBox.ReadOnly = !editing;
            effectsDataGridView.ReadOnly = !editing;
            effectsDataGridView.AllowUserToAddRows = editing;
            effectsDataGridView.AllowUserToDeleteRows = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            addExcludesButton.Visible = editing;
            addExcludesTextBox.Visible = editing;
            removeButton.Visible = editing;
            excludeAddLabel.Visible = editing;
            deletedCheckBox.Enabled = editing;
        }

        private void EffectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(effectsDataGridView.Rows[e.RowIndex].Cells[0].Value is string id)) return;
            ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
            ev.Show();
        }

        private void ExcludesOnEndingListView_DoubleClick(object sender, EventArgs e)
        {
            string id = excludesOnEndingListView.SelectedItems[0].ToString();
            LegacyViewer lv = new LegacyViewer(Utilities.GetLegacy(id), null, null);
            lv.Show();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Legacies must have an ID");
                return;
            }
            if (effectsDataGridView.RowCount > 1)
            {
                displayedLegacy.effects = null;
                displayedLegacy.effects_extend = null;
                displayedLegacy.effects_remove = null;
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    string key = row.Cells[0].Value?.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (key != null)
                    {
                        if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                        {
                            if (displayedLegacy.effects_extend == null) displayedLegacy.effects_extend = new Dictionary<string, int>();
                            // this will cause carcass spark to simply discard the row if there's no value, since that's needed.
                            if (value.HasValue) displayedLegacy.effects_extend[key] = value.Value;
                        }
                        else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                        {
                            if (displayedLegacy.effects_remove == null) displayedLegacy.effects_remove = new List<string>();
                            if (!displayedLegacy.effects_remove.Contains(key)) displayedLegacy.effects_remove.Add(key);
                        }
                        else
                        {
                            if (displayedLegacy.effects == null) displayedLegacy.effects = new Dictionary<string, int>();
                            // this will cause carcass spark to simply discard the row if there's no value, since that's needed.
                            if (value.HasValue) displayedLegacy.effects[key] = value.Value;
                        }
                    }
                    if (displayedLegacy.effects?.Count == 0) displayedLegacy.effects = null;
                    if (displayedLegacy.effects_extend?.Count == 0) displayedLegacy.effects_extend = null;
                    if (displayedLegacy.effects_remove?.Count == 0) displayedLegacy.effects_remove = null;
                    //if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedLegacy.effects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            Close();
            SuccessCallback?.Invoke(this, displayedLegacy);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddExcludesButton_Click(object sender, EventArgs e)
        {
            if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
            {
                excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                addExcludesTextBox.Text = "";
                addExcludesTextBox.Focus();
            }
        }

        private void AddExcludesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (addExcludesTextBox.Text != "" && addExcludesTextBox.Text != null)
                {
                    excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                    displayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                    addExcludesTextBox.Text = "";
                    addExcludesTextBox.Focus();
                }
            }
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.id = idTextBox.Text;
            if (displayedLegacy.id == "")
            {
                displayedLegacy.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.label = labelTextBox.Text;
            if (displayedLegacy.label == "")
            {
                displayedLegacy.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.description = descriptionTextBox.Text;
            if (displayedLegacy.description == "")
            {
                displayedLegacy.description = null;
            }
        }

        private void StartdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startdescription = startdescriptionTextBox.Text;
            if (displayedLegacy.startdescription == "")
            {
                displayedLegacy.startdescription = null;
            }
        }

        private void ImageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.image = imageTextBox.Text;
            if (Utilities.GetLegacyImage(imageTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.GetLegacyImage(imageTextBox.Text);
            }
            if (displayedLegacy.image == "")
            {
                displayedLegacy.image = null;
            }
        }

        private void FromEndingTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.fromEnding = fromEndingTextBox.Text;
            if (displayedLegacy.fromEnding == "")
            {
                displayedLegacy.fromEnding = null;
            }
        }

        private void StartingVerbIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.startingVerbId = startingVerbIdTextBox.Text;
            if (displayedLegacy.startingVerbId == "")
            {
                displayedLegacy.startingVerbId = null;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (displayedLegacy.excludesOnEnding.Contains(excludesOnEndingListView.SelectedItems[0].Text))
            {
                displayedLegacy.excludesOnEnding.Remove(excludesOnEndingListView.SelectedItems[0].Text);
                excludesOnEndingListView.Items.Remove(excludesOnEndingListView.SelectedItems[0]);
                if (displayedLegacy.excludesOnEnding.Count == 0) displayedLegacy.excludesOnEnding = null;
            }
        }

        private void AddStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (displayedLegacy.statusbarelements != null && displayedLegacy.statusbarelements.Count == 4)
            {
                MessageBox.Show("There is currently a limit of 4 statusbarelements");
                return;
            }
            if (statusBarElementTextBox.Text != "" && statusBarElementTextBox.Text != null)
            {
                if (displayedLegacy.statusbarelements == null) displayedLegacy.statusbarelements = new List<string>();
                statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                displayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
                statusBarElementTextBox.Text = "";
                statusBarElementTextBox.Focus();
            }
        }

        private void RemoveStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (displayedLegacy.statusbarelements.Contains(statusBarElementsListView.SelectedItems[0].Text))
            {
                displayedLegacy.statusbarelements.Remove(statusBarElementsListView.SelectedItems[0].Text);
                statusBarElementsListView.Items.Remove(statusBarElementsListView.SelectedItems[0]);
                if (displayedLegacy.statusbarelements.Count == 0) displayedLegacy.statusbarelements = null;
            }
        }

        private void StatusBarElementTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (displayedLegacy.statusbarelements != null && displayedLegacy.statusbarelements.Count == 4)
                {
                    MessageBox.Show("There is currently a limit of 4 statusbarelements");
                    return;
                }
                if (statusBarElementTextBox.Text != "" && statusBarElementTextBox.Text != null)
                {
                    if (displayedLegacy.statusbarelements == null) displayedLegacy.statusbarelements = new List<string>();
                    statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                    displayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
                    statusBarElementTextBox.Text = "";
                    statusBarElementTextBox.Focus();
                }
            }
        }

        private void SetAsExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
        }

        private void SetAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
        }

        private void EffectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[0].Value?.ToString();
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {
                if (displayedLegacy.effects_extend.ContainsKey(key)) displayedLegacy.effects_extend.Remove(key);
                if (displayedLegacy.effects_extend.Count == 0) displayedLegacy.effects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {
                if (displayedLegacy.effects_remove.Contains(key)) displayedLegacy.effects_remove.Remove(key);
                if (displayedLegacy.effects_remove.Count == 0) displayedLegacy.effects_remove = null;
            }
            else
            {
                if (displayedLegacy.effects.ContainsKey(key)) displayedLegacy.effects.Remove(key);
                if (displayedLegacy.effects.Count == 0) displayedLegacy.effects = null;
            }
        }

        private void AvailableWithoutEndingMatchCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Checked) displayedLegacy.availableWithoutEndingMatch = true;
            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Unchecked) displayedLegacy.availableWithoutEndingMatch = false;
            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Indeterminate) displayedLegacy.availableWithoutEndingMatch = null;
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedLegacy.comments = commentsTextBox.Text;
            if (displayedLegacy.comments == "")
            {
                displayedLegacy.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedLegacy.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedLegacy.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedLegacy.deleted = null;
        }

        private void NewStartCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (newStartCheckBox.CheckState == CheckState.Checked) displayedLegacy.newstart = true;
            if (newStartCheckBox.CheckState == CheckState.Unchecked) displayedLegacy.newstart = false;
            if (newStartCheckBox.CheckState == CheckState.Indeterminate) displayedLegacy.newstart = null;
        }

        private void TableCoverImageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (tableCoverImageTextBox.Text != "")
            {
                displayedLegacy.tablecoverimage = tableCoverImageTextBox.Text;
            } else
            {
                displayedLegacy.tablecoverimage = null;
            }
        }

        private void TableSurfaceImageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (tableSurfaceImageTextBox.Text != "")
            {
                displayedLegacy.tablesurfaceimage = tableSurfaceImageTextBox.Text;
            }
            else
            {
                displayedLegacy.tablesurfaceimage = null;
            }
        }

        private void TableEdgeImageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (tableEdgeImageTextBox.Text != "")
            {
                displayedLegacy.tableedgeimage = tableEdgeImageTextBox.Text;
            }
            else
            {
                displayedLegacy.tableedgeimage = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedLegacy.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                if (extendsTextBox.Text != "") displayedLegacy.extends = new List<string> { extendsTextBox.Text };
                else displayedLegacy.extends = null;
            }
        }
    }
}
