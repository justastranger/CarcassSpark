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
    public partial class MutationViewer : Form
    {
        public Recipe.Mutation displayedMutation;
        bool editing;

        public MutationViewer(Recipe.Mutation mutation, bool? editing)
        {
            InitializeComponent();
            displayedMutation = mutation;
            fillValues(mutation);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Recipe.Mutation mutation)
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
    }
}
