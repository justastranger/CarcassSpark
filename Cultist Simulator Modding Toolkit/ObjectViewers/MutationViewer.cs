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
using CultistSimulatorModdingToolkit.ObjectViewers;

namespace CultistSimulatorModdingToolkit.ObjectViewers
{
    public partial class MutationViewer : Form
    {
        public Mutation displayedMutation;
        bool editing;

        public MutationViewer(Mutation mutation, bool? editing)
        {
            InitializeComponent();
            displayedMutation = mutation;
            fillValues(mutation);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Mutation mutation)
        {
            if (mutation.filterOnAspectId != null) filterTextBox.Text = mutation.filterOnAspectId;
            if (mutation.mutateAspectId != null) mutateAspectIdTextBox.Text = mutation.mutateAspectId;
            if (mutation.mutationLevel.HasValue) levelNumericUpDown.Value = mutation.mutationLevel.Value;
            if (mutation.additive.HasValue) additiveCheckBox.Checked = mutation.additive.Value;
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            filterTextBox.ReadOnly = !editing;
            mutateAspectIdTextBox.ReadOnly = !editing;
            levelNumericUpDown.Enabled = editing;
            additiveCheckBox.Enabled = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedMutation.filterOnAspectId = filterTextBox.Text;
        }

        private void mutateAspectIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedMutation.mutateAspectId = mutateAspectIdTextBox.Text;
        }

        private void levelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedMutation.mutationLevel = Convert.ToInt32(levelNumericUpDown.Value);
        }

        private void additiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedMutation.additive = additiveCheckBox.Checked;
        }
    }
}
