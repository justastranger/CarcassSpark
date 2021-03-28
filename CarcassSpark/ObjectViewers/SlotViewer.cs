using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public enum SlotType
    {
        Element,
        Verb,
        Recipe
    }

    public partial class SlotViewer : Form
    {
        public Slot DisplayedSlot;
        public bool Editing;
        private readonly SlotType? slotType;


        public SlotViewer(Slot slot, bool? editing, SlotType? slotType)
        {
            InitializeComponent();
            DisplayedSlot = slot;
            if (slotType.HasValue)
            {
                this.slotType = slotType.Value;
            }

            SetEditingMode(editing.HasValue && editing.Value);
        }

        public SlotViewer(Slot slot, bool? editing)
        {
            InitializeComponent();
            DisplayedSlot = slot;
            SetEditingMode(editing.HasValue && editing.Value);
        }

        private void FillValues(Slot slot)
        {
            idTextBox.Text = slot.id;
            labelTextBox.Text = slot.label;
            descriptionTextBox.Text = slot.description;
            actionIdTextBox.Text = slot.actionId;
            if (slot.greedy.HasValue)
            {
                greedyCheckBox.Checked = slot.greedy.Value;
            }

            if (slot.consumes.HasValue)
            {
                consumesCheckBox.Checked = slot.consumes.Value;
            }

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

        private void SetEditingMode(bool editing)
        {
            this.Editing = editing;

            if (slotType.HasValue)
            {
                switch (slotType)
                {
                    case SlotType.Element:
                        consumesCheckBox.Visible = true;
                        actionIdTextBox.Visible = true;
                        actionIdLabel.Visible = true;
                        break;
                    case SlotType.Recipe:
                        greedyCheckBox.Visible = true;
                        consumesCheckBox.Visible = true;
                        break;
                    case SlotType.Verb:
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

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (requiredDataGridView.RowCount > 1)
            {
                DisplayedSlot.required = new Dictionary<string, int>();
                foreach (DataGridViewRow row in requiredDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        DisplayedSlot.required.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                    }
                }
            }
            if (forbiddenDataGridView.RowCount > 1)
            {
                DisplayedSlot.forbidden = new Dictionary<string, int>();
                foreach (DataGridViewRow row in forbiddenDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        DisplayedSlot.forbidden.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                    }
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSlot.id = idTextBox.Text;
            if (DisplayedSlot.id == "")
            {
                DisplayedSlot.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSlot.label = labelTextBox.Text;
            if (DisplayedSlot.label == "")
            {
                DisplayedSlot.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSlot.description = descriptionTextBox.Text;
            if (DisplayedSlot.description == "")
            {
                DisplayedSlot.description = null;
            }
        }

        private void ActionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedSlot.actionId = actionIdTextBox.Text;
            if (DisplayedSlot.actionId == "")
            {
                DisplayedSlot.actionId = null;
            }
        }

        private void GreedyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplayedSlot.greedy = greedyCheckBox.Checked;
            if (!DisplayedSlot.greedy.Value)
            {
                DisplayedSlot.greedy = null;
            }
        }

        private void RequiredDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            // if anything's null, then nothing was committed and we can just let it get deleted without doing any more work
            if (e.Row.Cells[0] == null || e.Row.Cells[1] == null || DisplayedSlot.required == null)
            {
                return;
            }

            if (DisplayedSlot.required.ContainsKey(e.Row.Cells[0].Value.ToString()))
            {
                DisplayedSlot.required.Remove(e.Row.Cells[0].Value.ToString());
            }

            if (DisplayedSlot.required.Count == 0)
            {
                DisplayedSlot.required = null;
            }
        }

        private void ConsumesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplayedSlot.consumes = consumesCheckBox.Checked;
            if (!DisplayedSlot.consumes.Value)
            {
                DisplayedSlot.consumes = null;
            }
        }

        private void ForbiddenDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(forbiddenDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }
            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void RequiredDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(requiredDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void ForbiddenDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            // if anything's null, then nothing was committed and we can just let it get deleted without doing any more work
            if (e.Row.Cells[0] == null || e.Row.Cells[1] == null || DisplayedSlot.forbidden == null)
            {
                return;
            }

            if (DisplayedSlot.forbidden.ContainsKey(e.Row.Cells[0].Value.ToString()))
            {
                DisplayedSlot.forbidden.Remove(e.Row.Cells[0].Value.ToString());
            }

            if (DisplayedSlot.forbidden.Count == 0)
            {
                DisplayedSlot.forbidden = null;
            }
        }

        private void SlotViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedSlot);
        }
    }
}
