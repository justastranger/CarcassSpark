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

        // private readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

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
            if (Utilities.VerbImageExists(verb.id))
            {
                pictureBox1.Image = Utilities.GetVerbImage(verb.id);
            }
            if (verb.label != null) labelTextBox.Text = verb.label;
            if (verb.atStart.HasValue) atStartCheckBox.Checked = verb.atStart.Value;
            if (verb.description != null) descriptionTextBox.Text = verb.description;
            if (verb.deleted.HasValue) deletedCheckBox.Checked = verb.deleted.Value;
            if (verb.slot != null)
            {
                slotsListView.Items.Add(verb.slot.id);
                // slots.Add(verb.slot.id, verb.slot);
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
            SlotViewer sv = new SlotViewer(displayedVerb.slot, editing);
            if (sv.ShowDialog() == DialogResult.OK)
            {
                displayedVerb.slot = sv.displayedSlot;
            }
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
                displayedVerb.slot = sv.displayedSlot;
                // slots.Add(sv.displayedSlot.id, sv.displayedSlot);
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
            if (Utilities.VerbImageExists(idTextBox.Text))
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
            displayedVerb.slot = null;
            slotsListView.Items.Remove(slotsListView.SelectedItems[0]);
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
