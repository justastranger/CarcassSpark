using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CultistSimulatorModdingToolkit.ObjectTypes;

namespace CultistSimulatorModdingToolkit.ObjectViewers
{
    public partial class ElementViewer : Form
    {

        Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
        public Element displayedElement;
        bool editing;

        public ElementViewer(Element element, bool? editing)
        {
            InitializeComponent();
            displayedElement = element;
            if (element.extends != null)
            {
                //extendsTextBox.Text = element.extends[0]; // afaik extends should only ever be an array of a single string
                //Element extendedElement = Utilities.getElement(element.extends[0]);
                //fillValues(extendedElement);
            }
            fillValues(element);
            if (editing.HasValue)
            {
                setEditingMode(editing.Value);
            }
            else
            {
                setEditingMode(false);
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            iconTextBox.ReadOnly = !editing;
            animFramesNumericUpDown.Enabled = editing;
            lifetimeNumericUpDown.Enabled = editing;
            decayToTextBox.ReadOnly = !editing;
            uniqueCheckBox.Enabled = editing;
            uniquenessgroupTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            extendsTextBox.ReadOnly = !editing;
            xtriggersDataGridView.AllowUserToAddRows = editing;
            xtriggersDataGridView.AllowUserToDeleteRows = editing;
            xtriggersDataGridView.ReadOnly = !editing;
            aspectsDataGridView.AllowUserToAddRows = editing;
            aspectsDataGridView.AllowUserToDeleteRows = editing;
            aspectsDataGridView.ReadOnly = !editing;
            okButton.Visible = editing;
            addSlotButton.Visible = editing;
            removeSlotButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void fillValues(Element element)
        {
            if (element.id != null)
            {
                idTextBox.Text = element.id;
                pictureBox1.Image = Utilities.getElementImage(element.id);
            }
            if (element.label != null) labelTextBox.Text = element.label;
            if (element.icon != null)
            {
                iconTextBox.Text = element.icon;
                pictureBox1.Image = Utilities.getElementImage(element.icon);
            }
            if (element.animframes.HasValue) animFramesNumericUpDown.Value = element.animframes.Value;
            if (element.lifetime.HasValue) lifetimeNumericUpDown.Value = element.lifetime.Value;
            if (element.decayTo != null) decayToTextBox.Text = element.decayTo;
            if (element.unique.HasValue) uniqueCheckBox.Checked = element.unique.Value;
            if (element.uniquenessgroup != null) uniquenessgroupTextBox.Text = element.uniquenessgroup;
            if (element.description != null) descriptionTextBox.Text = element.description;
            if (element.slots != null)
            {
                foreach (Slot slot in element.slots)
                {
                    slots[slot.id] = slot;
                    slotsListBox.Items.Add(slot.id);
                }
            }
            if (element.xtriggers != null)
            {
                Dictionary<string, string> xtriggers = element.xtriggers;
                foreach (KeyValuePair<string, string> kvp in xtriggers)
                {
                    xtriggersDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (element.aspects != null)
            {
                Dictionary<string, int> aspects = element.aspects;
                foreach (KeyValuePair<string, int> kvp in aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, Convert.ToString(kvp.Value));
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListBox.SelectedItem == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListBox.SelectedItem.ToString()], editing);
            sv.ShowDialog();
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string aspectID = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            AspectViewer av = new AspectViewer(Utilities.getAspect(aspectID), editing);
            av.ShowDialog();
        }

        private void xtriggersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = xtriggersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), editing);
                ev.Show();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), editing);
                av.ShowDialog();
            }
            else
            {
                MessageBox.Show("XTrigger catalyst and result must both be either aspects or elements.", "What the hell is this?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Elements must have an ID");
                return;
            }
            if (xtriggersDataGridView.Rows.Count > 1) {
                displayedElement.xtriggers = new Dictionary<string, string>();
                foreach (DataGridViewRow row in xtriggersDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedElement.xtriggers.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                }
            }
            if (aspectsDataGridView.Rows.Count > 1) {
                displayedElement.aspects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedElement.aspects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            DialogResult = DialogResult.OK;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.id = idTextBox.Text;
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.extends = new string[] { extendsTextBox.Text };
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.label = labelTextBox.Text;
        }

        private void iconTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.icon = iconTextBox.Text;
            if (Utilities.getElementImage(iconTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getElementImage(iconTextBox.Text);
            }
        }

        private void uniquenessgroupTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.uniquenessgroup = uniquenessgroupTextBox.Text;
        }

        private void decayToTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.decayTo = decayToTextBox.Text;
        }

        private void lifetimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedElement.lifetime = Convert.ToInt32(lifetimeNumericUpDown.Value);
        }

        private void animFramesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedElement.animframes = Convert.ToInt32(animFramesNumericUpDown.Value);
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedElement.description = descriptionTextBox.Text;
        }

        private void addSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(new Slot(), true);
            DialogResult dr = sv.ShowDialog();
            if (dr == DialogResult.OK)
            {
                slots.Add(sv.displayedSlot.id, sv.displayedSlot);
                slotsListBox.Items.Add(sv.displayedSlot.id);
                if (displayedElement.slots == null) displayedElement.slots = new List<Slot>();
                displayedElement.slots.Add(sv.displayedSlot);
            }
        }
    }
}
