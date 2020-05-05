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
    public partial class RecipeViewer : Form
    {
        Recipe displayedRecipe;
        ModViewer currentMod;
        Dictionary<string, Recipe.RecipeLink> recipeLinks = new Dictionary<string, Recipe.RecipeLink>();
        Dictionary<string, Recipe.RecipeLink> alternativerecipeLinks = new Dictionary<string, Recipe.RecipeLink>();
        Dictionary<string, Recipe.Mutation> mutations = new Dictionary<string, Recipe.Mutation>();

        public RecipeViewer(Recipe recipe, ModViewer currentMod)
        {
            InitializeComponent();
            this.displayedRecipe = recipe;
            this.currentMod = currentMod;
            idTextBox.Text = recipe.id;
            labelTextBox.Text = recipe.label;
            actionIdTextBox.Text = recipe.actionId;
            endingTextBox.Text = recipe.ending;
            burnimageTextBox.Text = recipe.burnimage;
            craftableCheckBox.Checked = recipe.craftable;
            hintonlyCheckBox.Checked = recipe.hintonly;
            startdescriptionTextBox.Text = recipe.startdescription;
            descriptionTextBox.Text = recipe.description;
            showInternalDeckButton.Enabled = recipe.internalDeck != null ? true : false;
            showSlotButton.Enabled = recipe.slots != null ? true : false;
            if (recipe.requirements != null && !recipe.requirements.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in recipe.requirements.toDictionary())
                {
                    requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.extantreqs != null && !recipe.extantreqs.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in recipe.extantreqs.toDictionary())
                {
                    extantreqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.tablereqs != null && !recipe.tablereqs.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in recipe.tablereqs.toDictionary())
                {
                    tablereqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.effects != null && !recipe.effects.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in recipe.effects.toDictionary())
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.aspects != null && !recipe.aspects.isNull())
            {
                foreach (KeyValuePair<string, int> kvp in recipe.aspects.toDictionary())
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.deckeffect != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffect)
                {
                    deckeffectDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.alternativerecipes != null)
            {
                foreach (Recipe.RecipeLink rl in recipe.alternativerecipes)
                {
                    alternativerecipeLinks.Add(rl.id, rl);
                    alternativerecipesListBox.Items.Add(rl.id);
                }
            }
            if (recipe.linked != null)
            {
                foreach (Recipe.RecipeLink rl in recipe.linked)
                {
                    recipeLinks.Add(rl.id, rl);
                    linkedListBox.Items.Add(rl.id);
                }
            }
            if (recipe.mutations != null)
            {
                foreach (Recipe.Mutation mutation in recipe.mutations)
                {
                    mutations.Add(mutation.mutateAspectId, mutation);
                    mutationsListBox.Items.Add(mutation.mutateAspectId);
                }
            }
        }

        private void showInternalDeckButton_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(displayedRecipe.internalDeck, currentMod);
            dv.ShowDialog();
        }

        private void showSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(displayedRecipe.slots[0], currentMod);
            sv.ShowDialog();
        }

        private void alternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativerecipesListBox.SelectedItem == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(alternativerecipeLinks[alternativerecipesListBox.SelectedItem.ToString()], currentMod);
            rlv.ShowDialog();
        }

        private void linkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedListBox.SelectedItem == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(recipeLinks[linkedListBox.SelectedItem.ToString()], currentMod);
            rlv.ShowDialog();
        }

        private void mutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListBox.SelectedItem == null) return;
            MutationViewer mv = new MutationViewer(mutations[mutationsListBox.SelectedItem.ToString()], currentMod);
            mv.ShowDialog();
        }

        private void requirementsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects OR elements
            string id = requirementsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if (currentMod.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(currentMod.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }

        private void extantreqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can also be aspects or elements
            string id = extantreqsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
            else if (currentMod.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(currentMod.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }

        private void tablereqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be card elements
            string id = tablereqsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
        }

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be card elements
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(currentMod.getElement(id), currentMod);
                ev.ShowDialog();
            }
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be aspect elements
            string id = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(currentMod.getAspect(id), currentMod);
                av.ShowDialog();
            }
        }

        private void deckeffectDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be decks
            string id = deckeffectDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (currentMod.deckExists(id))
            {
                DeckViewer dv = new DeckViewer(currentMod.getDeck(id), currentMod);
                dv.ShowDialog();
            }
        }
    }
}
