using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{

    public enum RecipeLinkType
    {
        Alt,
        Linked
    }

    public partial class RecipeLinkViewer : Form
    {
        public RecipeLink DisplayedRecipeLink;
        private bool editing;

        public RecipeLinkViewer(RecipeLink recipeLink, bool? editing)
        {
            InitializeComponent();
            DisplayedRecipeLink = recipeLink;
            SetEditingMode(editing.HasValue && editing.Value);
        }

        public RecipeLinkViewer(RecipeLink recipeLink, bool? editing, RecipeLinkType type)
        {
            InitializeComponent();
            DisplayedRecipeLink = recipeLink;
            SetEditingMode(editing.HasValue && editing.Value);

            if (type == RecipeLinkType.Linked)
            {
                SetLinked();
            }
        }

        private void FillValues(RecipeLink recipeLink)
        {
            idTextBox.Text = recipeLink.id;
            if (recipeLink.chance.HasValue)
            {
                chanceNumericUpDown.Value = recipeLink.chance.Value;
            }

            if (recipeLink.additional.HasValue)
            {
                additionalCheckBox.Checked = recipeLink.additional.Value;
            }

            if (recipeLink.challenges != null)
            {
                foreach (KeyValuePair<string, string> kvp in recipeLink.challenges)
                {
                    challengesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipeLink.expulsion != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipeLink.expulsion.filter)
                {
                    expulsionDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
                if (recipeLink.expulsion.limit.HasValue)
                {
                    totalExpulsionLimitNumericUpDown.Value = recipeLink.expulsion.limit.Value;
                }
            }
        }

        private void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            chanceNumericUpDown.Enabled = editing;
            additionalCheckBox.Enabled = editing;
            challengesDataGridView.ReadOnly = !editing;
            challengesDataGridView.AllowUserToAddRows = editing;
            challengesDataGridView.AllowUserToDeleteRows = editing;
            expulsionDataGridView.ReadOnly = !editing;
            expulsionDataGridView.AllowUserToAddRows = editing;
            expulsionDataGridView.AllowUserToDeleteRows = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void SetAlt()
        {

        }

        private void SetLinked()
        {
            additionalCheckBox.Visible = false;
            expulsionDataGridView.Visible = false;
            expulsionLabel.Visible = false;
            expulsionTotalLimitLabel.Visible = false;
            totalExpulsionLimitNumericUpDown.Visible = false;
        }

        private void OpenRecipeButton_Click(object sender, EventArgs e)
        {
            if (Utilities.RecipeExists(idTextBox.Text))
            {
                RecipeViewer rv = new RecipeViewer(Utilities.GetRecipe(idTextBox.Text), null, null);
                rv.Show();
            }
            else
            {
                MessageBox.Show("That recipe does not currently exist.");
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (challengesDataGridView.RowCount > 1)
            {
                DisplayedRecipeLink.challenges = new Dictionary<string, string>();
                foreach (DataGridViewRow row in challengesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        DisplayedRecipeLink.challenges.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                    }
                }
            }
            if (expulsionDataGridView.RowCount > 1)
            {
                DisplayedRecipeLink.expulsion = new Expulsion(Convert.ToInt32(totalExpulsionLimitNumericUpDown.Value) > 0 ? Convert.ToInt32(totalExpulsionLimitNumericUpDown.Value) : 1);
                foreach (DataGridViewRow row in expulsionDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        DisplayedRecipeLink.expulsion.filter.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                    }
                }
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipeLink.id = idTextBox.Text;
            if (DisplayedRecipeLink.id == "")
            {
                DisplayedRecipeLink.id = null;
                // idTextBox.BackColor = Color.White;
                RecipeIDErrorProvider.SetError(idTextBox, string.Empty);
            }
            else
            {
                RecipeIDErrorProvider.SetError(idTextBox,
                    Utilities.RecipeExists(DisplayedRecipeLink.id) ? string.Empty : "Recipe ID Not Found");
            }
        }

        private void ChanceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DisplayedRecipeLink.chance = Convert.ToInt32(chanceNumericUpDown.Value);
            if (DisplayedRecipeLink.chance == 0)
            {
                DisplayedRecipeLink.chance = null;
            }
        }

        private void AdditionalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplayedRecipeLink.additional = additionalCheckBox.Checked;
            if (!DisplayedRecipeLink.additional.Value)
            {
                DisplayedRecipeLink.additional = null;
            }
        }

        private void ChallengesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (DisplayedRecipeLink.challenges.ContainsKey(e.Row.Cells[0].Value.ToString()))
            {
                DisplayedRecipeLink.challenges.Remove(e.Row.Cells[0].Value.ToString());
            }

            if (DisplayedRecipeLink.challenges.Count == 0)
            {
                DisplayedRecipeLink.challenges = null;
            }
        }

        private void ExpulsionsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = expulsionDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            switch (Utilities.GetIdType(id))
            {
                case "aspect":
                    AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                    av.Show();
                    break;

                case "element":
                    ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                    ev.Show();
                    break;
            }
        }

        private void ExpulsionDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (DisplayedRecipeLink.expulsion.filter.ContainsKey(e.Row.Cells[0].Value.ToString()))
            {
                DisplayedRecipeLink.expulsion.filter.Remove(e.Row.Cells[0].Value.ToString());
            }

            if (DisplayedRecipeLink.expulsion.filter.Count == 0)
            {
                DisplayedRecipeLink.expulsion = null;
            }
        }

        private void ChallengesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AspectViewer av = new AspectViewer(Utilities.GetAspect(challengesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()), null, null);
            av.Show();
        }

        private void TotalExpulsionLimitNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DisplayedRecipeLink.expulsion != null)
            {
                DisplayedRecipeLink.expulsion.limit = Convert.ToInt32(totalExpulsionLimitNumericUpDown.Value);
            }
        }

        private void RecipeLinkViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedRecipeLink);
        }
    }
}
