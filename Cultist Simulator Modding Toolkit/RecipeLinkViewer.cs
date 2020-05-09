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

        private void challengesDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedRecipeLink.challenges.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedRecipeLink.challenges.Remove(e.Row.Cells[0].Value.ToString());
            if (displayedRecipeLink.challenges.Count == 0) displayedRecipeLink.challenges = null;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = expulsionsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            switch (Utilities.getIdType(id))
            {
                case "aspect":
                    AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                    av.ShowDialog();
                    break;

                case "element":
                    ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                    ev.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (displayedRecipeLink.expulsions.ContainsKey(e.Row.Cells[0].Value.ToString())) displayedRecipeLink.expulsions.Remove(e.Row.Cells[0].Value.ToString());
            if (displayedRecipeLink.expulsions.Count == 0) displayedRecipeLink.expulsions = null;
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value != null && e.Row.Cells[1].Value != null)
            {
                displayedRecipeLink.expulsions.Add(e.Row.Cells[0].Value.ToString(), Convert.ToInt32(e.Row.Cells[1].Value));
            }
        }

        private void challengesDataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value != null && e.Row.Cells[1].Value != null)
            {
                displayedRecipeLink.challenges.Add(e.Row.Cells[0].Value.ToString(), e.Row.Cells[1].Value.ToString());
            }
        }

        private void challengesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AspectViewer av = new AspectViewer(Utilities.getAspect(challengesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()), false);
            av.ShowDialog();
        }
    }
}
