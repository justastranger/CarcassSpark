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

        private readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        public VerbViewer(Verb verb, EventHandler<Verb> SuccessCallback)
        {
            InitializeComponent();
            displayedVerb = verb;
            FillValues(verb);
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
        }
        
        void FillValues(Verb verb)
        {
            if (verb.id != null) idTextBox.Text = verb.id;
            if (Utilities.GetVerbImage(verb.id) != null)
            {
                pictureBox1.Image = Utilities.GetVerbImage(verb.id);
            }
            if (verb.label != null) labelTextBox.Text = verb.label;
            if (verb.atStart.HasValue) atStartCheckBox.Checked = verb.atStart.Value;
            if (verb.description != null) descriptionTextBox.Text = verb.description;
            if (verb.deleted.HasValue) deletedCheckBox.Checked = verb.deleted.Value;
            if (verb.slots != null && verb.slots.Count > 0)
            {
                if (verb.slots.Count > 1) MessageBox.Show("Cultist Simulator does not currently support Verbs with more than one Starting Slot. Carcass Spark will add them, but only so you can remove them. The game will not as long as there is more than one slot.", "Error: Too Many Slots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                foreach (Slot slot in verb.slots)
                {
                    slotsListView.Items.Add(slot.id);
                    slots.Add(slot.id, slot);
                }
            }
            if (verb.extends != null && verb.extends.Count > 0) extendsTextBox.Text = verb.extends[0];
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            atStartCheckBox.Enabled = editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            okButton.Visible = editing;
            addSlotButton.Visible = editing;
            removeButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
        }

        private void SlotsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (slotsListView.SelectedItems == null) return;
            SlotViewer sv = new SlotViewer(slots[slotsListView.SelectedItems[0].Text], editing);
            sv.Show();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Verbs must have an ID");
                return;
            }
            Close();
            SuccessCallback?.Invoke(this, displayedVerb);
        }

        private void AddSlotButton_Click(object sender, EventArgs e)
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.id = idTextBox.Text;
            if (Utilities.GetVerbImage(idTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.GetVerbImage(idTextBox.Text);
            }
            if (displayedVerb.id == "")
            {
                displayedVerb.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.label = labelTextBox.Text;
            if (displayedVerb.label == "")
            {
                displayedVerb.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.description = descriptionTextBox.Text;
            if (displayedVerb.description == "")
            {
                displayedVerb.description = null;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (slots.ContainsKey(slotsListView.SelectedItems[0].Text.ToString()))
            {
                displayedVerb.slots.Remove(slots[slotsListView.SelectedItems[0].Text.ToString()]);
                slots.Remove(slotsListView.SelectedItems[0].Text.ToString());
                slotsListView.Items.Remove(slotsListView.SelectedItems[0]);
            }
        }

        private void AtStartCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (atStartCheckBox.CheckState == CheckState.Checked) displayedVerb.atStart = true;
            if (atStartCheckBox.CheckState == CheckState.Unchecked) displayedVerb.atStart = false;
            if (atStartCheckBox.CheckState == CheckState.Indeterminate) displayedVerb.atStart = null;
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.comments = commentsTextBox.Text;
            if (displayedVerb.comments == "")
            {
                displayedVerb.comments = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.extends = new List<string>() { extendsTextBox.Text };
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedVerb.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedVerb.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedVerb.deleted = null;
        }
    }
}
