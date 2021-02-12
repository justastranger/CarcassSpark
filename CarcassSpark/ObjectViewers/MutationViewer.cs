using CarcassSpark.ObjectTypes;
using System;
using System.Windows.Forms;

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
            if (editing.HasValue)
            {
                SetEditingMode(editing.Value);
            }
            else
            {
                SetEditingMode(false);
            }
        }

        void FillValues(Mutation mutation)
        {
            if (mutation.filter != null)
            {
                filterTextBox.Text = mutation.filter;
            }

            if (mutation.mutate != null)
            {
                mutateAspectIdTextBox.Text = mutation.mutate;
            }

            if (mutation.level.HasValue)
            {
                levelNumericUpDown.Value = mutation.level.Value;
            }

            if (mutation.additive.HasValue)
            {
                additiveCheckBox.Checked = mutation.additive.Value;
            }
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
            displayedMutation.filter = filterTextBox.Text;
            if (displayedMutation.filter == "")
            {
                displayedMutation.filter = null;
            }
        }

        private void MutateAspectIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedMutation.mutate = mutateAspectIdTextBox.Text;
            if (displayedMutation.mutate == "")
            {
                displayedMutation.mutate = null;
            }
        }

        private void LevelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedMutation.level = Convert.ToInt32(levelNumericUpDown.Value);
            if (displayedMutation.level == 0)
            {
                displayedMutation.level = null;
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

        private void MutationViewer_Shown(object sender, EventArgs e)
        {
            FillValues(displayedMutation);
        }
    }
}
