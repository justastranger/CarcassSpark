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
        ModViewer currentMod;

        public RecipeLinkViewer(Recipe.RecipeLink recipeLink, ModViewer currentMod)
        {
            InitializeComponent();
            displayedRecipeLink = recipeLink;
            this.currentMod = currentMod;
            idTextBox.Text = recipeLink.id;
            chanceNumericUpDown.Value = recipeLink.chance;
            additionalCheckBox.Checked = recipeLink.additional;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(Utilities.getRecipe(idTextBox.Text), currentMod);
            rv.ShowDialog();
        }
    }
}
