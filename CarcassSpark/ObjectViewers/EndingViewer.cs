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
            FillValues(ending);
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
        }

        void FillValues(Ending ending)
        {
            if (ending.id != null) idTextBox.Text = ending.id;
            if (ending.label != null) labelTextBox.Text = ending.label;
            if (ending.image != null) imageTextBox.Text = ending.image;
            if (ending.id != null && ending.image == null) pictureBox1.Image = Utilities.GetEndingImage(ending.id);
            else if (ending.image != null) pictureBox1.Image = Utilities.GetEndingImage(ending.image);
            if (ending.flavour != null) endindFlavourComboBox.Text = ending.flavour;
            if (ending.anim != null) animComboBox.Text = ending.anim;
            if (ending.description != null) descriptionTextBox.Text = ending.description;
            if (ending.comments != null) commentsTextBox.Text = ending.comments;
            if (ending.achievement != null) achievementTextBox.Text = ending.achievement;
            if (ending.deleted.HasValue) deletedCheckBox.Checked = ending.deleted.Value;
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            imageTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
            endindFlavourComboBox.Enabled = editing;
            animComboBox.Enabled = editing;
            achievementTextBox.ReadOnly = !editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            deletedCheckBox.Enabled = editing;
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.id = idTextBox.Text;
            if (displayedEnding.id == "")
            {
                displayedEnding.id = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.label = labelTextBox.Text;
            if (displayedEnding.label == "")
            {
                displayedEnding.label = null;
            }
        }

        private void ImageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.image = imageTextBox.Text;
            if (Utilities.GetEndingImage(imageTextBox.Text) != null)
            {
                pictureBox1.Image = Utilities.GetEndingImage(imageTextBox.Text);
            }
            if (displayedEnding.image == "")
            {
                displayedEnding.image = null;
            }
        }
        
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.description = descriptionTextBox.Text;
            if (displayedEnding.description == "")
            {
                displayedEnding.description = null;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (displayedEnding.id == null || displayedEnding.label == null || displayedEnding.image == null || displayedEnding.flavour == null || displayedEnding.description == null || displayedEnding.anim == null || displayedEnding.achievement == null)
            {
                MessageBox.Show("All values must be filled for the Ending to be valid.");
                return;
            }
            Close();
            SuccessCallback?.Invoke(this, displayedEnding);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AchievementTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.achievement = achievementTextBox.Text;
            if (displayedEnding.achievement == "")
            {
                displayedEnding.achievement = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedEnding.comments = commentsTextBox.Text;
            if (displayedEnding.comments == "")
            {
                displayedEnding.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedEnding.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedEnding.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedEnding.deleted = null;
        }

        private void EndindFlavourComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayedEnding.flavour = endindFlavourComboBox.Text;
            if (displayedEnding.flavour == "")
            {
                displayedEnding.flavour = null;
            }
        }

        private void AnimComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayedEnding.anim = animComboBox.Text;
            if (displayedEnding.anim == "")
            {
                displayedEnding.anim = null;
            }
        }
    }
}
