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
    public partial class EndingViewer : Form
    {
        public Ending displayedEnding;
        bool editing;
        event EventHandler<Ending> SuccessCallback;

        public EndingViewer(Ending ending, EventHandler<Ending> SuccessCallback)
        {
            InitializeComponent();
            displayedEnding = ending;
            fillValues(ending);
            if (SuccessCallback != null)
            {
                setEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
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
            if (ending.achievement != null) achievementTextBox.Text = ending.achievement;
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
            if (displayedEnding.id == null || displayedEnding.label == null || displayedEnding.image == null || displayedEnding.flavour == null || displayedEnding.description == null || displayedEnding.anim == null || displayedEnding.achievement == null)
            {
                MessageBox.Show("All values must be filled for the Ending to be valid.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
            SuccessCallback?.Invoke(this, displayedEnding);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void achievementTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.achievement = achievementTextBox.Text;
        }
    }
}
