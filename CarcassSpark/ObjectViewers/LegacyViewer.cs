using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class LegacyViewer : Form, IGameObjectViewer
    {
        public Legacy DisplayedLegacy;

        private event EventHandler<Legacy> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public LegacyViewer(Legacy legacy, EventHandler<Legacy> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedLegacy = legacy;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
        }

        private void FillValues(Legacy legacy)
        {
            if (legacy.ID != null)
            {
                idTextBox.Text = legacy.ID;
            }

            if (legacy.label != null)
            {
                labelTextBox.Text = legacy.label;
            }

            if (legacy.description != null)
            {
                descriptionTextBox.Text = legacy.description;
            }

            if (legacy.startdescription != null)
            {
                startdescriptionTextBox.Text = legacy.startdescription;
            }

            if (legacy.comments != null)
            {
                commentsTextBox.Text = legacy.comments;
            }

            if (legacy.image != null)
            {
                imageTextBox.Text = legacy.image;
                if (Utilities.LegacyImageExists(legacy.image))
                {
                    pictureBox1.Image = Utilities.GetLegacyImage(legacy.image);
                }
            }
            if (legacy.deleted.HasValue)
            {
                deletedCheckBox.CheckState = legacy.deleted.Value ? CheckState.Checked : CheckState.Unchecked;
            }

            if (legacy.fromEnding != null)
            {
                fromEndingTextBox.Text = legacy.fromEnding;
            }

            if (legacy.availableWithoutEndingMatch.HasValue)
            {
                availableWithoutEndingMatchCheckBox.CheckState = legacy.availableWithoutEndingMatch.Value ? CheckState.Checked : CheckState.Unchecked;
            }

            if (legacy.startingVerbId != null)
            {
                startingVerbIdTextBox.Text = legacy.startingVerbId;
            }

            if (legacy.newstart.HasValue)
            {
                newStartCheckBox.CheckState = legacy.newstart.Value ? CheckState.Checked : CheckState.Unchecked;
            }

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
            if (legacy.excludesOnEnding != null)
            {
                foreach (string ending in legacy.excludesOnEnding)
                {
                    excludesOnEndingListView.Items.Add(ending);
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

        private void SetEditingMode(bool editing)
        {
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
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
            if (!(effectsDataGridView.Rows[e.RowIndex].Cells[0].Value is string id))
            {
                return;
            }

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
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Legacies must have an ID");
                return;
            }
            if (effectsDataGridView.RowCount > 1)
            {
                DisplayedLegacy.effects = null;
                DisplayedLegacy.effects_extend = null;
                DisplayedLegacy.effects_remove = null;
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    string key = row.Cells[0].Value?.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (key != null)
                    {
                        if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                        {
                            if (DisplayedLegacy.effects_extend == null)
                            {
                                DisplayedLegacy.effects_extend = new Dictionary<string, int>();
                            }
                            // this will cause carcass spark to simply discard the row if there's no value, since that's needed.
                            if (value.HasValue)
                            {
                                DisplayedLegacy.effects_extend[key] = value.Value;
                            }
                        }
                        else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                        {
                            if (DisplayedLegacy.effects_remove == null)
                            {
                                DisplayedLegacy.effects_remove = new List<string>();
                            }

                            if (!DisplayedLegacy.effects_remove.Contains(key))
                            {
                                DisplayedLegacy.effects_remove.Add(key);
                            }
                        }
                        else
                        {
                            if (DisplayedLegacy.effects == null)
                            {
                                DisplayedLegacy.effects = new Dictionary<string, int>();
                            }
                            // this will cause carcass spark to simply discard the row if there's no value, since that's needed.
                            if (value.HasValue)
                            {
                                DisplayedLegacy.effects[key] = value.Value;
                            }
                        }
                    }
                    if (DisplayedLegacy.effects?.Count == 0)
                    {
                        DisplayedLegacy.effects = null;
                    }

                    if (DisplayedLegacy.effects_extend?.Count == 0)
                    {
                        DisplayedLegacy.effects_extend = null;
                    }

                    if (DisplayedLegacy.effects_remove?.Count == 0)
                    {
                        DisplayedLegacy.effects_remove = null;
                    }
                    //if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedLegacy.effects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedLegacy);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddExcludesButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(addExcludesTextBox.Text))
            {
                excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                DisplayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                addExcludesTextBox.Text = "";
                addExcludesTextBox.Focus();
            }
        }

        private void AddExcludesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(addExcludesTextBox.Text))
                {
                    excludesOnEndingListView.Items.Add(addExcludesTextBox.Text);
                    DisplayedLegacy.excludesOnEnding.Add(addExcludesTextBox.Text);
                    addExcludesTextBox.Text = "";
                    addExcludesTextBox.Focus();
                }
            }
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.ID = idTextBox.Text;
            if (DisplayedLegacy.ID == "")
            {
                DisplayedLegacy.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.label = labelTextBox.Text;
            if (DisplayedLegacy.label == "")
            {
                DisplayedLegacy.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.description = descriptionTextBox.Text;
            if (DisplayedLegacy.description == "")
            {
                DisplayedLegacy.description = null;
            }
        }

        private void StartdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.startdescription = startdescriptionTextBox.Text;
            if (DisplayedLegacy.startdescription == "")
            {
                DisplayedLegacy.startdescription = null;
            }
        }

        private void ImageTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.image = imageTextBox.Text;
            if (Utilities.GetLegacyImage(imageTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.GetLegacyImage(imageTextBox.Text);
            }
            if (DisplayedLegacy.image == "")
            {
                DisplayedLegacy.image = null;
            }
        }

        private void FromEndingTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.fromEnding = fromEndingTextBox.Text;
            if (DisplayedLegacy.fromEnding == "")
            {
                DisplayedLegacy.fromEnding = null;
            }
        }

        private void StartingVerbIdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.startingVerbId = startingVerbIdTextBox.Text;
            if (DisplayedLegacy.startingVerbId == "")
            {
                DisplayedLegacy.startingVerbId = null;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (DisplayedLegacy.excludesOnEnding.Contains(excludesOnEndingListView.SelectedItems[0].Text))
            {
                DisplayedLegacy.excludesOnEnding.Remove(excludesOnEndingListView.SelectedItems[0].Text);
                excludesOnEndingListView.Items.Remove(excludesOnEndingListView.SelectedItems[0]);
                if (DisplayedLegacy.excludesOnEnding.Count == 0)
                {
                    DisplayedLegacy.excludesOnEnding = null;
                }
            }
        }

        private void AddStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (DisplayedLegacy.statusbarelements != null && DisplayedLegacy.statusbarelements.Count == 4)
            {
                MessageBox.Show("There is currently a limit of 4 statusbarelements");
                return;
            }
            if (!string.IsNullOrEmpty(statusBarElementTextBox.Text))
            {
                if (DisplayedLegacy.statusbarelements == null)
                {
                    DisplayedLegacy.statusbarelements = new List<string>();
                }

                statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                DisplayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
                statusBarElementTextBox.Text = "";
                statusBarElementTextBox.Focus();
            }
        }

        private void RemoveStatusBarElementButton_Click(object sender, EventArgs e)
        {
            if (DisplayedLegacy.statusbarelements.Contains(statusBarElementsListView.SelectedItems[0].Text))
            {
                DisplayedLegacy.statusbarelements.Remove(statusBarElementsListView.SelectedItems[0].Text);
                statusBarElementsListView.Items.Remove(statusBarElementsListView.SelectedItems[0]);
                if (DisplayedLegacy.statusbarelements.Count == 0)
                {
                    DisplayedLegacy.statusbarelements = null;
                }
            }
        }

        private void StatusBarElementTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (DisplayedLegacy.statusbarelements != null && DisplayedLegacy.statusbarelements.Count == 4)
                {
                    MessageBox.Show("There is currently a limit of 4 statusbarelements");
                    return;
                }
                if (!string.IsNullOrEmpty(statusBarElementTextBox.Text))
                {
                    if (DisplayedLegacy.statusbarelements == null)
                    {
                        DisplayedLegacy.statusbarelements = new List<string>();
                    }

                    statusBarElementsListView.Items.Add(statusBarElementTextBox.Text);
                    DisplayedLegacy.statusbarelements.Add(statusBarElementTextBox.Text);
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
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {
                if (DisplayedLegacy.effects_extend.ContainsKey(key))
                {
                    DisplayedLegacy.effects_extend.Remove(key);
                }

                if (DisplayedLegacy.effects_extend.Count == 0)
                {
                    DisplayedLegacy.effects_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {
                if (DisplayedLegacy.effects_remove.Contains(key))
                {
                    DisplayedLegacy.effects_remove.Remove(key);
                }

                if (DisplayedLegacy.effects_remove.Count == 0)
                {
                    DisplayedLegacy.effects_remove = null;
                }
            }
            else
            {
                if (DisplayedLegacy.effects.ContainsKey(key))
                {
                    DisplayedLegacy.effects.Remove(key);
                }

                if (DisplayedLegacy.effects.Count == 0)
                {
                    DisplayedLegacy.effects = null;
                }
            }
        }

        private void AvailableWithoutEndingMatchCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedLegacy.availableWithoutEndingMatch = true;
            }

            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedLegacy.availableWithoutEndingMatch = false;
            }

            if (availableWithoutEndingMatchCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedLegacy.availableWithoutEndingMatch = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.comments = commentsTextBox.Text;
            if (DisplayedLegacy.comments == "")
            {
                DisplayedLegacy.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedLegacy.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedLegacy.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedLegacy.deleted = null;
            }
        }

        private void NewStartCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (newStartCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedLegacy.newstart = true;
            }

            if (newStartCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedLegacy.newstart = false;
            }

            if (newStartCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedLegacy.newstart = null;
            }
        }

        private void TableCoverImageTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.tablecoverimage = tableCoverImageTextBox.Text != "" ? tableCoverImageTextBox.Text : null;
        }

        private void TableSurfaceImageTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.tablesurfaceimage = tableSurfaceImageTextBox.Text != "" ? tableSurfaceImageTextBox.Text : null;
        }

        private void TableEdgeImageTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedLegacy.tableedgeimage = tableEdgeImageTextBox.Text != "" ? tableEdgeImageTextBox.Text : null;
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                DisplayedLegacy.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedLegacy.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void LegacyViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedLegacy);
        }
    }
}
