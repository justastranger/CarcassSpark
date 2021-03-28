using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class VerbViewer : Form, IGameObjectViewer
    {
        public Verb DisplayedVerb;
        private bool editing;

        private event EventHandler<Verb> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        // private readonly Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        public VerbViewer(Verb verb, EventHandler<Verb> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedVerb = verb;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
        }

        private void FillValues(Verb verb)
        {
            if (verb.ID != null)
            {
                idTextBox.Text = verb.ID;
            }

            if (Utilities.VerbImageExists(verb.ID))
            {
                pictureBox1.Image = Utilities.GetVerbImage(verb.ID);
            }
            if (verb.label != null)
            {
                labelTextBox.Text = verb.label;
            }

            if (verb.description != null)
            {
                descriptionTextBox.Text = verb.description;
            }

            if (verb.deleted.HasValue)
            {
                deletedCheckBox.Checked = verb.deleted.Value;
            }

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
            if (verb.comments != null)
            {
                commentsTextBox.Text = verb.comments;
            }
        }

        private void SetEditingMode(bool editing)
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
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Verbs must have an ID");
                return;
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedVerb);
        }

        private void AddSlotButton_Click(object sender, EventArgs e)
        {
            if (DisplayedVerb.slot == null)
            {
                SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.Verb);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    DisplayedVerb.slot = sv.DisplayedSlot;
                    addSlotButton.Text = "Open Slot";
                    removeButton.Enabled = true;
                }
            }
            else
            {
                SlotViewer sv = new SlotViewer(DisplayedVerb.slot.Copy(), editing, SlotType.Verb);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    DisplayedVerb.slot = sv.DisplayedSlot;
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
            DisplayedVerb.ID = idTextBox.Text;
            if (Utilities.VerbImageExists(idTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetVerbImage(idTextBox.Text);
            }
            if (DisplayedVerb.ID == "")
            {
                DisplayedVerb.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedVerb.label = labelTextBox.Text;
            if (DisplayedVerb.label == "")
            {
                DisplayedVerb.label = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedVerb.description = descriptionTextBox.Text;
            if (DisplayedVerb.description == "")
            {
                DisplayedVerb.description = null;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (DisplayedVerb.slot != null)
            {
                DisplayedVerb.slot = null;
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
            DisplayedVerb.comments = !string.IsNullOrEmpty(commentsTextBox.Text) ? commentsTextBox.Text : null;
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedVerb.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedVerb.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedVerb.deleted = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                DisplayedVerb.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedVerb.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void VerbViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedVerb);
        }
    }
}
