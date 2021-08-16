using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;

namespace CarcassSpark.Tools
{
    public partial class RecipeSelector : Form
    {
        private List<Recipe> recipesList;
        public string SelectedRecipeID = null;

        public RecipeSelector()
        {
            InitializeComponent();

            recipesList = Utilities.GetRecipes();

            foreach (var recipe in recipesList)
            {
                recipesListBox.Items.Add(recipe.ID);
            }
        }

        private void RecipesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedRecipeID = recipesListBox.SelectedItem as string;
        }

        private void RecipesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectedRecipeID = recipesListBox.SelectedItem as string;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListBox.BeginUpdate();
            recipesListBox.Items.Clear();

            string[] filteredRecipes = SearchRecipeIDs(recipesList, filterTextBox.Text);

            recipesListBox.Items.AddRange(filteredRecipes);

            recipesListBox.EndUpdate();
        }

        private string[] SearchRecipeIDs(List<Recipe> recipesList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from recipe in recipesList.AsParallel()
                    where (recipe.ID != null && regex.IsMatch(recipe.ID))
                          || (recipe.label != null && regex.IsMatch(recipe.label))
                          || (recipe.description != null && regex.IsMatch(recipe.description))
                          || (recipe.startdescription != null && regex.IsMatch(recipe.startdescription))
                          || (recipe.comments != null && regex.IsMatch(recipe.comments))
                    select recipe.ID).ToArray();
            }
            catch (ArgumentException)
            {
                return (from recipe in recipesList.AsParallel()
                    where (recipe.ID != null && recipe.ID.Contains(searchPattern))
                          || (recipe.label != null && recipe.label.Contains(searchPattern))
                          || (recipe.description != null && recipe.description.Contains(searchPattern))
                          || (recipe.startdescription != null && recipe.startdescription.Contains(searchPattern))
                          || (recipe.comments != null && recipe.comments.Contains(searchPattern))
                    select recipe.ID).ToArray();
            }

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(SelectedRecipeID))
            {
                MessageBox.Show("Please select a recipe.");
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SelectedRecipeID = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
