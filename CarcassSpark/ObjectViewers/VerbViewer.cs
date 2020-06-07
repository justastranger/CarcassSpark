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
    public partial class VerbViewer : Form
    {
        public Verb displayedVerb;
        bool editing;
        event EventHandler<Verb> SuccessCallback;

        Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        public VerbViewer(Verb verb, EventHandler<Verb> SuccessCallback)
        {
            InitializeComponent();
            displayedVerb = verb;
            fillValues(verb);
            if (SuccessCallback != null)
            {
                setEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
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
            if (verb.atStart.HasValue) atStartCheckBox.Checked = verb.atStart.Value;
            descriptionTextBox.Text = verb.description;
            if (verb.slots != null)
            {
                foreach (Slot slot in verb.slots)
                {
                    slotsListView.Items.Add(slot.id);
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
            if (slotsListView.SelectedItems == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListView.SelectedItems[0].Text], editing);
            sv.Show();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Verbs must have an ID");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
            SuccessCallback?.Invoke(this, displayedVerb);
        }

        private void addSlotButton_Click(object sender, EventArgs e)
        {
            if (slotsListView.Items.Count == 1)
            {
                MessageBox.Show("Currently, only one slot is supported by Verbs at this time.");
                return;
            }
            SlotViewer sv = new SlotViewer(new Slot(), true, SlotViewer.SlotType.VERB);
            sv.ShowDialog();
            if(sv.DialogResult == DialogResult.OK)
            {
                if (displayedVerb.slots == null)
                {
                    displayedVerb.slots = new List<Slot>();
                }
                displayedVerb.slots.Add(sv.displayedSlot);
                slots.Add(sv.displayedSlot.id, sv.displayedSlot);
                slotsListView.Items.Add(sv.displayedSlot.id);
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
            if (displayedVerb.id == "")
            {
                displayedVerb.id = null;
            }
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.label = labelTextBox.Text;
            if (displayedVerb.label == "")
            {
                displayedVerb.label = null;
            }
        }

        private void atStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedVerb.atStart = atStartCheckBox.Checked;
            if (!displayedVerb.atStart.Value)
            {
                displayedVerb.atStart = null;
            }
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.description = descriptionTextBox.Text;
            if (displayedVerb.description == "")
            {
                displayedVerb.description = null;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (slots.ContainsKey(slotsListView.SelectedItems[0].Text.ToString()))
            {
                displayedVerb.slots.Remove(slots[slotsListView.SelectedItems[0].Text.ToString()]);
                slots.Remove(slotsListView.SelectedItems[0].Text.ToString());
                slotsListView.Items.Remove(slotsListView.SelectedItems[0]);
            }
        }
    }
}
