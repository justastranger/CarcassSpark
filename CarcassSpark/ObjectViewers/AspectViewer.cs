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
    public partial class AspectViewer : Form
    {
        public Aspect displayedAspect;
        Dictionary<string, Induces> inducesDictionary;
        event EventHandler<Aspect> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public AspectViewer(Aspect aspect, EventHandler<Aspect> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            displayedAspect = aspect;
            associatedListViewItem = item;
            FillValues(aspect);
            if (SuccessCallback != null)
            {
                this.SuccessCallback += SuccessCallback;
                SetEditingMode(true);
            }
            else
            {
                SetEditingMode(false);
            }
        }
        
        void SetEditingMode(bool editing)
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
        }

        public void FillValues(Aspect aspect)
        {
            if (aspect.id != null) idTextBox.Text = aspect.id;
            if (aspect.label != null) labelTextBox.Text = aspect.label;
            if ((!aspect.noartneeded.HasValue) || aspect.noartneeded.Value == false)
            {
                if (aspect.icon != null)
                {
                    iconTextBox.Text = aspect.icon;
                    if (Utilities.AspectImageExists(aspect.icon)) pictureBox1.Image = Utilities.GetAspectImage(aspect.icon);
                }
                else if (Utilities.AspectImageExists(aspect.id))
                {
                    pictureBox1.Image = Utilities.GetAspectImage(aspect.id);
                }
            }
            if (aspect.description != null) descriptionTextBox.Text = aspect.description;
            if (aspect.isHidden.HasValue) isHiddenCheckBox.Checked = aspect.isHidden.Value;
            if (aspect.noartneeded.HasValue) noartworkneededCheckBox.Checked = aspect.noartneeded.Value;
            if (aspect.inherits != null) inheritsTextBox.Text = aspect.inherits;
            if (aspect.comments != null) commentTextBox.Text = aspect.comments;
            if (aspect.deleted.HasValue) deletedCheckBox.Checked = aspect.deleted.Value;
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
            if (aspect.induces_prepend != null)
            {
                foreach (Induces induces in aspect.induces_prepend)
                {
                    inducesDictionary.Add(induces.id, induces);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(inducesDataGridView, induces.id, induces.chance, induces.additional ?? false);
                    newRow.DefaultCellStyle.BackColor = Utilities.ListPrependColor;
                    inducesDataGridView.Rows.Insert(0, newRow);
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
            if (!(inducesDataGridView.Rows[e.RowIndex].Cells[0].Value is string id)) return;
            RecipeViewer rv = new RecipeViewer(Utilities.GetRecipe(id), null, null);
            rv.Show();
        }
        
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Aspects must have an ID.");
                return;
            }
            if (inducesDataGridView.Rows.Count > 1) {
                displayedAspect.induces = null;
                displayedAspect.induces_append = null;
                displayedAspect.induces_prepend = null;
                displayedAspect.induces_remove = null;
                foreach (DataGridViewRow row in inducesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (row.DefaultCellStyle.BackColor == Utilities.ListAppendColor)
                        {
                            if (displayedAspect.induces_append == null) displayedAspect.induces_append = new List<Induces>();
                            displayedAspect.induces_append.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                        else if (row.DefaultCellStyle.BackColor == Utilities.ListPrependColor)
                        {
                            if (displayedAspect.induces_prepend == null) displayedAspect.induces_prepend = new List<Induces>();
                            displayedAspect.induces_prepend.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                        else if (row.DefaultCellStyle.BackColor == Utilities.ListRemoveColor)
                        {
                            if (displayedAspect.induces_remove == null) displayedAspect.induces_remove = new List<String>();
                            displayedAspect.induces_remove.Add(row.Cells[0].Value as string);
                        }
                        else
                        {
                            if (displayedAspect.induces == null) displayedAspect.induces = new List<Induces>();
                            displayedAspect.induces.Add(new Induces(row.Cells[0].Value as string, Convert.ToInt32(row.Cells[1].Value), Convert.ToBoolean(row.Cells[2].Value), null));
                        }
                    }
                }
            }
            Close();
            SuccessCallback?.Invoke(this, displayedAspect);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.id = idTextBox.Text;
            if (displayedAspect.id == "")
            {
                displayedAspect.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.label = labelTextBox.Text;
            if (displayedAspect.label == "")
            {
                displayedAspect.label = null;
            }
        }

        private void IconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.icon = iconTextBox.Text;
            if (Utilities.AspectImageExists(iconTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetAspectImage(iconTextBox.Text);
            }
            if (displayedAspect.icon == "")
            {
                displayedAspect.icon = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.description = descriptionTextBox.Text;
            if (displayedAspect.description == "")
            {
                displayedAspect.description = null;
            }
        }

        private void InducesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value as string : null;
            Induces induces = key != null ? inducesDictionary[key] : null;
            if (e.Row.DefaultCellStyle.BackColor == Utilities.ListAppendColor)
            {
                if (displayedAspect.induces_append == null) return;
                if (displayedAspect.induces_append.Contains(induces)) displayedAspect.induces_append.Remove(induces);
                if (displayedAspect.induces_append.Count == 0) displayedAspect.induces_append = null;
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListPrependColor)
            {
                if (displayedAspect.induces_prepend == null) return;
                if (displayedAspect.induces_prepend.Contains(induces)) displayedAspect.induces_prepend.Remove(induces);
                if (displayedAspect.induces_prepend.Count == 0) displayedAspect.induces_prepend = null;
            }
            else if (e.Row.DefaultCellStyle.BackColor == Utilities.ListRemoveColor)
            {
                if (displayedAspect.induces_remove == null) return;
                if (displayedAspect.induces_remove.Contains(key)) displayedAspect.induces_remove.Remove(key);
                if (displayedAspect.induces_remove.Count == 0) displayedAspect.induces_remove = null;
            }
            else
            {
                if (displayedAspect.induces == null) return;
                if (displayedAspect.induces.Contains(induces)) displayedAspect.induces.Remove(induces);
                if (displayedAspect.induces.Count == 0) displayedAspect.induces = null;
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
            if (isHiddenCheckBox.CheckState == CheckState.Checked) displayedAspect.isHidden = true;
            if (isHiddenCheckBox.CheckState == CheckState.Unchecked) displayedAspect.isHidden = false;
            if (isHiddenCheckBox.CheckState == CheckState.Indeterminate) displayedAspect.isHidden = null;
        }

        private void NoartworkneededCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (noartworkneededCheckBox.CheckState == CheckState.Checked) displayedAspect.noartneeded = true;
            if (noartworkneededCheckBox.CheckState == CheckState.Unchecked) displayedAspect.noartneeded = false;
            if (noartworkneededCheckBox.CheckState == CheckState.Indeterminate) displayedAspect.noartneeded = null;
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.comments = commentTextBox.Text;
            if (displayedAspect.comments == "") displayedAspect.comments = null;
        }

        private void InheritsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedAspect.inherits = inheritsTextBox.Text;
            if (displayedAspect.inherits == "") displayedAspect.inherits = null;
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedAspect.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedAspect.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedAspect.deleted = null;
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedAspect.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                if (extendsTextBox.Text != "") displayedAspect.extends = new List<string> { extendsTextBox.Text };
                else displayedAspect.extends = null;
            }
        }
    }
}
