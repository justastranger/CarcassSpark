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
    public partial class EndingViewer : Form
    {
        public Ending displayedEnding;
        bool editing;

        public EndingViewer(Ending ending, bool? editing)
        {
            InitializeComponent();
            displayedEnding = ending;
            fillValues(ending);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Ending ending)
        {
            if (ending.id != null) idTextBox.Text = ending.id;
            if (ending.label != null) labelTextBox.Text = ending.label;
            if (ending.image != null) imageTextBox.Text = ending.image;
            if (ending.id != null && ending.image == null) pictureBox1.Image = Utilities.getEndingImage(ending.id);
            else if (ending.image != null) pictureBox1.Image = Utilities.getEndingImage(ending.image);
            if (ending.flavour != null) flavourDomainUpDown.Text = ending.flavour;
            if (ending.anim != null) animDomainUpDown.Text = ending.anim;
            if (ending.description != null) descriptionTextBox.Text = ending.description;
        }

        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            imageTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            flavourDomainUpDown.ReadOnly = !editing;
            flavourDomainUpDown.Enabled = editing;
            animDomainUpDown.ReadOnly = !editing;
            animDomainUpDown.Enabled = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.label = labelTextBox.Text;
        }

        private void imageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.image = imageTextBox.Text;
            if (Utilities.getEndingImage(imageTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.getEndingImage(imageTextBox.Text);
            }
        }

        private void flavourDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            displayedEnding.flavour = flavourDomainUpDown.Text;
        }

        private void animDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            displayedEnding.anim = animDomainUpDown.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.description = descriptionTextBox.Text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Endings must have an ID");
                return;
            }
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
