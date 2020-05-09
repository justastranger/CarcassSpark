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
    public partial class VerbViewer : Form
    {
        public Verb displayedVerb;
        bool editing;
        Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        public VerbViewer(Verb verb, bool? editing)
        {
            InitializeComponent();
            displayedVerb = verb;
            fillValues(verb);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }
        
        void fillValues(Verb verb)
        {
            idTextBox.Text = verb.id;
            if (Utilities.getVerbImage(verb.id) != null)
            {
                pictureBox1.Image = Utilities.getVerbImage(verb.id);
            }
            labelTextBox.Text = verb.label;
            atStartCheckBox.Checked = verb.atStart;
            descriptionTextBox.Text = verb.description;
            if (verb.slots != null)
            {
                foreach (Slot slot in verb.slots)
                {
                    slotsListBox.Items.Add(slot.id);
                    slots.Add(slot.id, slot);
                }
            }
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            atStartCheckBox.Enabled = editing;
            descriptionTextBox.ReadOnly = !editing;
            okButton.Visible = editing;
            addSlotButton.Visible = editing;
            removeButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void slotsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListBox.SelectedItem == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListBox.SelectedItem.ToString()], editing);
            sv.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void addSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(new Slot(), true);
            sv.ShowDialog();
            if(sv.DialogResult == DialogResult.OK)
            {
                if (displayedVerb.slots == null)
                {
                    displayedVerb.slots = new List<Slot>();
                }
                displayedVerb.slots.Add(sv.displayedSlot);
                slots.Add(sv.displayedSlot.id, sv.displayedSlot);
                slotsListBox.Items.Add(sv.displayedSlot.id);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.id = idTextBox.Text;
            if (Utilities.getVerbImage(idTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getVerbImage(idTextBox.Text);
            }
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.label = labelTextBox.Text;
        }

        private void atStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedVerb.atStart = atStartCheckBox.Checked;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.description = descriptionTextBox.Text;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (slots.ContainsKey(slotsListBox.SelectedItem.ToString()))
            {
                displayedVerb.slots.Remove(slots[slotsListBox.SelectedItem.ToString()]);
                slots.Remove(slotsListBox.SelectedItem.ToString());
                slotsListBox.Items.Remove(slotsListBox.SelectedItem);
            }
        }
    }
}
