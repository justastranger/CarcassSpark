using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class AspectViewer : Form, IGameObjectViewer
    {
        public Aspect DisplayedAspect;
        private Dictionary<string, Induces> inducesDictionary;

        private event EventHandler<Aspect> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public AspectViewer(Aspect aspect, EventHandler<Aspect> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedAspect = aspect;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                this.SuccessCallback += successCallback;
                SetEditingMode(true);
            }
            else
            {
                SetEditingMode(false);
            }
        }

        private void SetEditingMode(bool editing)
        {
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentTextBox.ReadOnly = !editing;
            isHiddenCheckBox.Enabled = editing;
            noartworkneededCheckBox.Enabled = editing;
            inducesDataGridView.AllowUserToAddRows = editing;
            inducesDataGridView.AllowUserToDeleteRows = editing;
            inducesDataGridView.ReadOnly = !editing;
            inheritsTextBox.ReadOnly = !editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            okButton.Visible = editing;
            deletedCheckBox.Enabled = editing;
            verbIconTextBox.ReadOnly = !editing;
        }

        public void FillValues(Aspect aspect)
        {
            if (aspect.ID != null)
            {
                idTextBox.Text = aspect.ID;
            }

            if (aspect.label != null)
            {
                labelTextBox.Text = aspect.label;
            }

            if ((!aspect.noartneeded.HasValue) || aspect.noartneeded.Value == false)
            {
                if (aspect.icon != null)
                {
                    iconTextBox.Text = aspect.icon;
                    if (Utilities.AspectImageExists(aspect.icon))
                    {
                        pictureBox1.Image = Utilities.GetAspectImage(aspect.icon);
                    }
                }
                else if (Utilities.AspectImageExists(aspect.ID))
                {
                    pictureBox1.Image = Utilities.GetAspectImage(aspect.ID);
                }
            }
            if (aspect.description != null)
            {
                descriptionTextBox.Text = aspect.description;
            }

            if (aspect.isHidden.HasValue)
            {
                isHiddenCheckBox.Checked = aspect.isHidden.Value;
            }

            if (aspect.noartneeded.HasValue)
            {
                noartworkneededCheckBox.Checked = aspect.noartneeded.Value;
            }

            if (aspect.inherits != null)
            {
                inheritsTextBox.Text = aspect.inherits;
            }

            if (aspect.comments != null)
            {
                commentTextBox.Text = aspect.comments;
            }

            if (aspect.deleted.HasValue)
            {
                deletedCheckBox.Checked = aspect.deleted.Value;
            }

            if (aspect.verbicon != null)
            {
                verbIconTextBox.Text = aspect.verbicon;
            }

            if (aspect.induces_prepend != null)
            {
                foreach (Induces induces in aspect.induces_prepend)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional ?? false);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListPrependColor;
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.induces != null)
            {
                inducesDictionary = new Dictionary<string, Induces>();
                foreach (Induces induces in aspect.induces)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional ?? false);
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.induces_append != null)
            {
                foreach (Induces induces in aspect.induces_append)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional ?? false);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListAppendColor;
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.induces_remove != null)
            {
                foreach (string removeId in aspect.induces_remove)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, removeId);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListRemoveColor;
                    inducesDataGridView.Rows.Add(newRow);
                }
            }
            if (aspect.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", aspect.extends);
            }
            else if (aspect.extends?.Count == 1)
            {
                extendsTextBox.Text = aspect.extends[0];
            }
        }

        private void InducesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(inducesDataGridView.Rows[e.RowIndex].Cells[0].Value is string id))
            {
                return;
            }

            RecipeViewer rv = new RecipeViewer(Utilities.GetRecipe(id), null, null);
            rv.Show();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Aspects must have an ID.");
                return;
            }
            if (inducesDataGridView.Rows.Count > 1)
            {
                DisplayedAspect.induces = null;
                DisplayedAspect.induces_append = null;
                DisplayedAspect.induces_prepend = null;
                DisplayedAspect.induces_remove = null;
                foreach (DataGridViewRow row in inducesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (row.DefaultCellStyle.BackColor == Utilities.ListAppendColor)
                        {
                            if (DisplayedAspect.induces_append == null)
                            {
                                DisplayedAspect.induces_append = new List<Induces>();
                            }

                            DisplayedAspect.induces_append.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                        else if (row.DefaultCellStyle.BackColor == Utilities.ListPrependColor)
                        {
                            if (DisplayedAspect.induces_prepend == null)
                            {
                                DisplayedAspect.induces_prepend = new List<Induces>();
                            }

                            DisplayedAspect.induces_prepend.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                        else if (row.DefaultCellStyle.BackColor == Utilities.ListRemoveColor)
                        {
                            if (DisplayedAspect.induces_remove == null)
                            {
                                DisplayedAspect.induces_remove = new List<String>();
                            }

                            DisplayedAspect.induces_remove.Add(row.Cells[0].Value as string);
                        }
                        else
                        {
                            if (DisplayedAspect.induces == null)
                            {
                                DisplayedAspect.induces = new List<Induces>();
                            }

                            DisplayedAspect.induces.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                    }
                }
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedAspect);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.ID = idTextBox.Text;
            if (DisplayedAspect.ID == "")
            {
                DisplayedAspect.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.label = labelTextBox.Text;
            if (DisplayedAspect.label == "")
            {
                DisplayedAspect.label = null;
            }
        }

        private void IconTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.icon = iconTextBox.Text;
            if (Utilities.AspectImageExists(iconTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetAspectImage(iconTextBox.Text);
            }
            if (DisplayedAspect.icon == "")
            {
                DisplayedAspect.icon = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.description = descriptionTextBox.Text;
            if (DisplayedAspect.description == "")
            {
                DisplayedAspect.description = null;
            }
        }

        private void InducesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value as string : null;
            Induces induces = key != null ? inducesDictionary[key] : null;
            if (e.Row.DefaultCellStyle.BackColor == Utilities.ListAppendColor)
            {
                if (DisplayedAspect.induces_append == null)
                {
                    return;
                }

                if (DisplayedAspect.induces_append.Contains(induces))
                {
                    DisplayedAspect.induces_append.Remove(induces);
                }

                if (DisplayedAspect.induces_append.Count == 0)
                {
                    DisplayedAspect.induces_append = null;
                }
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListPrependColor)
            {
                if (DisplayedAspect.induces_prepend == null)
                {
                    return;
                }

                if (DisplayedAspect.induces_prepend.Contains(induces))
                {
                    DisplayedAspect.induces_prepend.Remove(induces);
                }

                if (DisplayedAspect.induces_prepend.Count == 0)
                {
                    DisplayedAspect.induces_prepend = null;
                }
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListRemoveColor)
            {
                if (DisplayedAspect.induces_remove == null)
                {
                    return;
                }

                if (DisplayedAspect.induces_remove.Contains(key))
                {
                    DisplayedAspect.induces_remove.Remove(key);
                }

                if (DisplayedAspect.induces_remove.Count == 0)
                {
                    DisplayedAspect.induces_remove = null;
                }
            }
            else
            {
                if (DisplayedAspect.induces == null)
                {
                    return;
                }

                if (DisplayedAspect.induces.Contains(induces))
                {
                    DisplayedAspect.induces.Remove(induces);
                }

                if (DisplayedAspect.induces.Count == 0)
                {
                    DisplayedAspect.induces = null;
                }
            }
        }

        private void SetAsPrependToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle.BackColor = Utilities.ListPrependColor;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle.BackColor = Utilities.ListPrependColor;
            }
        }

        private void SetAsAppendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle.BackColor = Utilities.ListAppendColor;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle.BackColor = Utilities.ListAppendColor;
            }
        }

        private void SetAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle.BackColor = Utilities.ListRemoveColor;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle.BackColor = Utilities.ListRemoveColor;
            }
        }

        private void IsHiddenCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (isHiddenCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedAspect.isHidden = true;
            }

            if (isHiddenCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedAspect.isHidden = false;
            }

            if (isHiddenCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedAspect.isHidden = null;
            }
        }

        private void NoartworkneededCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (noartworkneededCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedAspect.noartneeded = true;
            }

            if (noartworkneededCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedAspect.noartneeded = false;
            }

            if (noartworkneededCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedAspect.noartneeded = null;
            }
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.comments = commentTextBox.Text;
            if (DisplayedAspect.comments == "")
            {
                DisplayedAspect.comments = null;
            }
        }

        private void InheritsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.inherits = inheritsTextBox.Text;
            if (DisplayedAspect.inherits == "")
            {
                DisplayedAspect.inherits = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedAspect.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedAspect.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedAspect.deleted = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                DisplayedAspect.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedAspect.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void VerbIconTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedAspect.verbicon = !string.IsNullOrEmpty(verbIconTextBox.Text) ? verbIconTextBox.Text : null;
        }

        private void AspectViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedAspect);
        }
    }
}
