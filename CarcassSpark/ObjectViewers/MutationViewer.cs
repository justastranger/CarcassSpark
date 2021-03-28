using CarcassSpark.ObjectTypes;
using System;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class MutationViewer : Form
    {
        public Mutation DisplayedMutation;
        private bool editing;

        public MutationViewer(Mutation mutation, bool? editing)
        {
            InitializeComponent();
            DisplayedMutation = mutation;
            SetEditingMode(editing.HasValue && editing.Value);
        }

        private void FillValues(Mutation mutation)
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

        private void SetEditingMode(bool editing)
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
            DisplayedMutation.filter = filterTextBox.Text;
            if (DisplayedMutation.filter == "")
            {
                DisplayedMutation.filter = null;
            }
        }

        private void MutateAspectIdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedMutation.mutate = mutateAspectIdTextBox.Text;
            if (DisplayedMutation.mutate == "")
            {
                DisplayedMutation.mutate = null;
            }
        }

        private void LevelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DisplayedMutation.level = Convert.ToInt32(levelNumericUpDown.Value);
            if (DisplayedMutation.level == 0)
            {
                DisplayedMutation.level = null;
            }
        }

        private void AdditiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplayedMutation.additive = additiveCheckBox.Checked;
            if (!DisplayedMutation.additive.Value)
            {
                DisplayedMutation.additive = null;
            }
        }

        private void MutationViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedMutation);
        }
    }
}
