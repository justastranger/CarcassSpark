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
using CarcassSpark.ObjectViewers;

namespace CarcassSpark.ObjectViewers
{
    public partial class SlotViewer : Form
    {
        public Slot displayedSlot;
        public bool editing;
        SlotType? slotType;
        
        public enum SlotType
        {
            ELEMENT,
            VERB,
            RECIPE
        }

        public SlotViewer(Slot slot, bool? editing, SlotType? slotType)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            fillValues(slot);
            if (slotType.HasValue) this.slotType = slotType.Value;
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        public SlotViewer(Slot slot, bool? editing)
        {
            InitializeComponent();
            this.displayedSlot = slot;
            fillValues(slot);
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

            if (slotType.HasValue)
            {
                switch (slotType)
                {
                    case SlotType.ELEMENT:
                        consumesCheckBox.Visible = true;
                        actionIdTextBox.Visible = true;
                        actionIdLabel.Visible = true;
                        break;
                    case SlotType.RECIPE:
                        greedyCheckBox.Visible = true;
                        consumesCheckBox.Visible = true;
                        break;
                    case SlotType.VERB:
                        greedyCheckBox.Visible = true;
                        consumesCheckBox.Visible = true;
                        break;
                }
            }

            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            actionIdTextBox.ReadOnly = !editing;
            greedyCheckBox.Enabled = editing;
            consumesCheckBox.Enabled = editing;
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
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), null);
                ev.Show();
            }
            else if(Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), null);
                av.Show();
            }
        }

        private void forbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), null);
                ev.Show();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), null);
                av.Show();
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
            if (displayedSlot.id == "")
            {
                displayedSlot.id = null;
            }
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.label = labelTextBox.Text;
            if (displayedSlot.label == "")
            {
                displayedSlot.label = null;
            }
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.description = descriptionTextBox.Text;
            if (displayedSlot.description == "")
            {
                displayedSlot.description = null;
            }
        }

        private void actionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedSlot.actionId = actionIdTextBox.Text;
            if (displayedSlot.actionId == "")
            {
                displayedSlot.actionId = null;
            }
        }

        private void greedyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedSlot.greedy = greedyCheckBox.Checked;
            if (!displayedSlot.greedy.Value)
            {
                displayedSlot.greedy = null;
            }
        }

        private void requiredDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedSlot.required.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedSlot.required.Remove(e.Row.Cells[0].Value.ToString());
            if (displayedSlot.required.Count == 0) displayedSlot.required = null;
        }

        private void consumesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedSlot.consumes = consumesCheckBox.Checked;
            if (!displayedSlot.consumes.Value)
            {
                displayedSlot.consumes = null;
            }
        }

        private void forbiddenDataGridView_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string id = forbiddenDataGridView.SelectedCells[0].Value as String;
            if (id == null)
            {
                return;
            }
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), null);
                ev.Show();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), null);
                av.Show();
            }
        }

        private void requiredDataGridView_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string id = requiredDataGridView.SelectedCells[0].Value as String;
            if (id == null) return;
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), null);
                ev.Show();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), null);
                av.Show();
            }
        }

        private void forbiddenDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedSlot.forbidden.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedSlot.forbidden.Remove(e.Row.Cells[0].Value.ToString());
            if (displayedSlot.forbidden.Count == 0) displayedSlot.forbidden = null;
        }
    }
}
