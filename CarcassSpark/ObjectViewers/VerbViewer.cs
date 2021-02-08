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
        public ListViewItem associatedListViewItem;

        // private readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        public VerbViewer(Verb verb, EventHandler<Verb> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            displayedVerb = verb;
            FillValues(verb);
            associatedListViewItem = item;
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
            if (verb.description != null) descriptionTextBox.Text = verb.description;
            if (verb.deleted.HasValue) deletedCheckBox.Checked = verb.deleted.Value;
            if (verb.slot != null)
            {
                addSlotButton.Text = "Open Slot";
                removeButton.Enabled = true;
            }
            else
            {
                removeButton.Enabled = false;
            }
            if (verb.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", verb.extends);
            }
            else if (verb.extends?.Count == 1)
            {
                extendsTextBox.Text = verb.extends[0];
            }
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            okButton.Visible = editing;
            // addSlotButton.Visible = editing;
            removeButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
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
            if (displayedVerb.slot == null)
            {
                SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.VERB);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    displayedVerb.slot = sv.displayedSlot;
                    addSlotButton.Text = "Open Slot";
                    removeButton.Enabled = true;
                }
            }
            else
            {
                SlotViewer sv = new SlotViewer(displayedVerb.slot.Copy(), editing, SlotType.VERB);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    displayedVerb.slot = sv.displayedSlot;
                    removeButton.Enabled = true;
                }
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
            if (displayedVerb.slot != null)
            {
                displayedVerb.slot = null;
                addSlotButton.Text = "Add Slot";
                removeButton.Enabled = false;
            }
            else
            {
                MessageBox.Show("There is no slot");
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedVerb.comments = commentsTextBox.Text;
            if (displayedVerb.comments == "")
            {
                displayedVerb.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedVerb.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedVerb.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedVerb.deleted = null;
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedVerb.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                if (extendsTextBox.Text != "") displayedVerb.extends = new List<string> { extendsTextBox.Text };
                else displayedVerb.extends = null;
            }
        }
    }
}
