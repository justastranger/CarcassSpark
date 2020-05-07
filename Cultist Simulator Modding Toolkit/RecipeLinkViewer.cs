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
    public partial class RecipeLinkViewer : Form
    {
        public Recipe.RecipeLink displayedRecipeLink;
        bool editing;

        public RecipeLinkViewer(Recipe.RecipeLink recipeLink, bool? editing)
        {
            InitializeComponent();
            displayedRecipeLink = recipeLink;
            fillValues(recipeLink);
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void fillValues(Recipe.RecipeLink recipeLink)
        {
            idTextBox.Text = recipeLink.id;
            if (recipeLink.chance.HasValue) chanceNumericUpDown.Value = recipeLink.chance.Value;
            if (recipeLink.additional.HasValue) additionalCheckBox.Checked = recipeLink.additional.Value;
            if (recipeLink.challenges != null)
            {
                foreach (KeyValuePair<string, string> kvp in recipeLink.challenges)
                {
                    challengesDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
        }
        
        void setEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            chanceNumericUpDown.Enabled = editing;
            additionalCheckBox.Enabled = editing;
            challengesDataGridView.ReadOnly = !editing;
            challengesDataGridView.AllowUserToAddRows = editing;
            challengesDataGridView.AllowUserToDeleteRows = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
        }

        private void openRecipeButton_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(idTextBox.Text), false);
            rv.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (challengesDataGridView.RowCount > 1)
            {
                displayedRecipeLink.challenges = new Dictionary<string, string>();
                foreach (DataGridViewRow row in challengesDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1] != null)
                    {
                        displayedRecipeLink.challenges.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipeLink.id = idTextBox.Text;
        }

        private void chanceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipeLink.chance = Convert.ToInt32(chanceNumericUpDown.Value);
        }

        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedRecipeLink.additional = additionalCheckBox.Checked;
        }
    }
}
