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
    public partial class MutationViewer : Form
    {
        public Mutation displayedMutation;
        bool editing;

        public MutationViewer(Mutation mutation, bool? editing)
        {
            InitializeComponent();
            displayedMutation = mutation;
            FillValues(mutation);
            if (editing.HasValue) SetEditingMode(editing.Value);
            else SetEditingMode(false);
        }

        void FillValues(Mutation mutation)
        {
            if (mutation.filterOnAspectId != null) filterTextBox.Text = mutation.filterOnAspectId;
            if (mutation.mutateAspectId != null) mutateAspectIdTextBox.Text = mutation.mutateAspectId;
            if (mutation.mutationLevel.HasValue) levelNumericUpDown.Value = mutation.mutationLevel.Value;
            if (mutation.additive.HasValue) additiveCheckBox.Checked = mutation.additive.Value;
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            filterTextBox.ReadOnly = !editing;
            mutateAspectIdTextBox.ReadOnly = !editing;
            levelNumericUpDown.Enabled = editing;
            additiveCheckBox.Enabled = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedMutation.filterOnAspectId = filterTextBox.Text;
            if (displayedMutation.filterOnAspectId == "")
            {
                displayedMutation.filterOnAspectId = null;
            }
        }

        private void MutateAspectIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedMutation.mutateAspectId = mutateAspectIdTextBox.Text;
            if (displayedMutation.mutateAspectId == "")
            {
                displayedMutation.mutateAspectId = null;
            }
        }

        private void LevelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedMutation.mutationLevel = Convert.ToInt32(levelNumericUpDown.Value);
            if (displayedMutation.mutationLevel == 0)
            {
                displayedMutation.mutationLevel = null;
            }
        }

        private void AdditiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedMutation.additive = additiveCheckBox.Checked;
            if (!displayedMutation.additive.Value)
            {
                displayedMutation.additive = null;
            }
        }
    }
}
