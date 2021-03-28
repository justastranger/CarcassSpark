using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class EndingViewer : Form, IGameObjectViewer
    {
        public Ending DisplayedEnding;
        private bool editing;

        private event EventHandler<Ending> SuccessCallback;
        public ListViewItem associatedListViewItem;

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public EndingViewer(Ending ending, EventHandler<Ending> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedEnding = ending;
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

        private void FillValues(Ending ending)
        {
            if (ending.ID != null)
            {
                idTextBox.Text = ending.ID;
            }

            if (ending.label != null)
            {
                labelTextBox.Text = ending.label;
            }

            if (ending.image != null)
            {
                imageTextBox.Text = ending.image;
                if (Utilities.EndingImageExists(ending.image))
                {
                    pictureBox1.Image = Utilities.GetEndingImage(ending.image);
                }
            }
            else if (Utilities.EndingImageExists(ending.ID))
            {
                pictureBox1.Image = Utilities.GetEndingImage(ending.ID);
            }
            if (ending.flavour != null)
            {
                endindFlavourComboBox.Text = ending.flavour;
            }

            if (ending.anim != null)
            {
                animComboBox.Text = ending.anim;
            }

            if (ending.description != null)
            {
                descriptionTextBox.Text = ending.description;
            }

            if (ending.comments != null)
            {
                commentsTextBox.Text = ending.comments;
            }

            if (ending.achievement != null)
            {
                achievementTextBox.Text = ending.achievement;
            }

            if (ending.deleted.HasValue)
            {
                deletedCheckBox.Checked = ending.deleted.Value;
            }

            if (ending.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", ending.extends);
            }
            else if (ending.extends?.Count == 1)
            {
                extendsTextBox.Text = ending.extends[0];
            }
        }

        private void SetEditingMode(bool editing)
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
            DisplayedEnding.ID = idTextBox.Text;
            if (DisplayedEnding.ID == "")
            {
                DisplayedEnding.ID = null;
            }
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.label = labelTextBox.Text;
            if (DisplayedEnding.label == "")
            {
                DisplayedEnding.label = null;
            }
        }

        private void ImageTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.image = imageTextBox.Text;
            if (Utilities.EndingImageExists(imageTextBox.Text))
            {
                pictureBox1.Image = Utilities.GetEndingImage(imageTextBox.Text);
            }
            if (DisplayedEnding.image == "")
            {
                DisplayedEnding.image = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.description = descriptionTextBox.Text;
            if (DisplayedEnding.description == "")
            {
                DisplayedEnding.description = null;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (DisplayedEnding.ID == null || DisplayedEnding.label == null || DisplayedEnding.image == null || DisplayedEnding.flavour == null || DisplayedEnding.description == null || DisplayedEnding.anim == null)// || displayedEnding.achievement == null)
            {
                MessageBox.Show("All values (except achievement) must be filled for the Ending to be valid.");
                return;
            }
            Close();
            SuccessCallback?.Invoke(this, DisplayedEnding);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AchievementTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.achievement = achievementTextBox.Text;
            if (DisplayedEnding.achievement == "")
            {
                DisplayedEnding.achievement = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.comments = commentsTextBox.Text;
            if (DisplayedEnding.comments == "")
            {
                DisplayedEnding.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedEnding.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedEnding.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedEnding.deleted = null;
            }
        }

        private void EndindFlavourComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayedEnding.flavour = endindFlavourComboBox.Text;
            if (DisplayedEnding.flavour == "")
            {
                DisplayedEnding.flavour = null;
            }
        }

        private void AnimComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayedEnding.anim = animComboBox.Text;
            if (DisplayedEnding.anim == "")
            {
                DisplayedEnding.anim = null;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedEnding.extends = extendsTextBox.Text.Contains(",") ? extendsTextBox.Text.Split(',').ToList() : new List<string> { extendsTextBox.Text };
        }

        private void EndingViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedEnding);
        }
    }
}
