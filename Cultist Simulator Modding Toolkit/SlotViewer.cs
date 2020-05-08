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
    public partial class SlotViewer : Form
    {
        public Slot displayedSlot;
        public bool editing, recipeSlot;

        public SlotViewer(Slot slot, bool? editing, bool? recipeSlot)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            fillValues(slot);
            if (recipeSlot.HasValue) this.recipeSlot = recipeSlot.Value;
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        public SlotViewer(Slot slot, bool? editing)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            fillValues(slot);
            recipeSlot = false;
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Slot slot)
        {
            idTextBox.Text = slot.id;
            labelTextBox.Text = slot.label;
            descriptionTextBox.Text = slot.description;
            actionIdTextBox.Text = slot.actionId;
            if (slot.greedy.HasValue) greedyCheckBox.Checked = slot.greedy.Value;
            if (slot.consumes.HasValue) consumesCheckBox.Checked = slot.consumes.Value;
            if (slot.required != null)
            {
                foreach (KeyValuePair<string, int> req in slot.required)
                {
                    requiredDataGridView.Rows.Add(req.Key, req.Value);
                }
            }
            if (slot.forbidden != null)
            {
                foreach (KeyValuePair<string, int> req in slot.forbidden)
                {
                    forbiddenDataGridView.Rows.Add(req.Key, req.Value);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            actionIdTextBox.Visible = !recipeSlot;
            actionIdLabel.Visible = !recipeSlot;
            actionIdTextBox.ReadOnly = !editing;
            greedyCheckBox.Enabled = editing;
            greedyCheckBox.Visible = recipeSlot;
            consumesCheckBox.Enabled = editing;
            consumesCheckBox.Visible = !recipeSlot;
            requiredDataGridView.AllowUserToAddRows = editing;
            requiredDataGridView.AllowUserToDeleteRows = editing;
            requiredDataGridView.ReadOnly = !editing;
            forbiddenDataGridView.AllowUserToAddRows = editing;
            forbiddenDataGridView.AllowUserToDeleteRows = editing;
            forbiddenDataGridView.ReadOnly = !editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void requiredDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = requiredDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if(Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void forbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (requiredDataGridView.RowCount > 1)
            {
                displayedSlot.required = new Dictionary<string, int>();
                foreach (DataGridViewRow row in requiredDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedSlot.required.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (forbiddenDataGridView.RowCount > 1)
            {
                displayedSlot.forbidden = new Dictionary<string, int>();
                foreach (DataGridViewRow row in forbiddenDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedSlot.forbidden.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
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

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.label = labelTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.description = descriptionTextBox.Text;
        }

        private void actionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.actionId = actionIdTextBox.Text;
        }

        private void greedyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedSlot.greedy = greedyCheckBox.Checked;
        }

        private void requiredDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedSlot.required.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedSlot.required.Remove(e.Row.Cells[0].Value.ToString());
        }

        private void consumesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedSlot.consumes = consumesCheckBox.Checked;
        }

        private void forbiddenDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedSlot.forbidden.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedSlot.forbidden.Remove(e.Row.Cells[0].Value.ToString());
        }
    }
}
