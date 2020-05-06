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
        Recipe.RecipeLink displayedRecipeLink;

        public RecipeLinkViewer(Recipe.RecipeLink recipeLink, bool? editing)
        {
            InitializeComponent();
            displayedRecipeLink = recipeLink;
            idTextBox.Text = recipeLink.id;
            chanceNumericUpDown.Value = recipeLink.chance;
            additionalCheckBox.Checked = recipeLink.additional;
            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void setEditingMode(bool editing)
        {
            idTextBox.Enabled = editing;
            chanceNumericUpDown.Enabled = editing;
            additionalCheckBox.Enabled = editing;
            dataGridView1.ReadOnly = !editing;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(idTextBox.Text), false);
            rv.ShowDialog();
        }
    }
}
