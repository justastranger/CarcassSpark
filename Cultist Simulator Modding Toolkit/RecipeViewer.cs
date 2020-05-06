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

        Dictionary<string, Recipe.RecipeLink> recipeLinks = new Dictionary<string, Recipe.RecipeLink>();
        Dictionary<string, Recipe.RecipeLink> alternativerecipeLinks = new Dictionary<string, Recipe.RecipeLink>();
        Dictionary<string, Recipe.Mutation> mutations = new Dictionary<string, Recipe.Mutation>();

        public RecipeViewer(Recipe recipe, bool? editing)
        {
            InitializeComponent();
            this.displayedRecipe = recipe;
            if (recipe.extends != null)
            {
                extendsTextBox.Text = recipe.extends[0];
                Recipe extendedRecipe = Utilities.getRecipe(recipe.extends[0]);
                fillValues(extendedRecipe);
            }
            fillValues(recipe);

            if (editing.HasValue) setEditingMode(editing.Value);
            else setEditingMode(false);
        }

        void setEditingMode(bool editing)
        {
            idTextBox.Enabled = editing;
            labelTextBox.Enabled = editing;
            actionIdTextBox.Enabled = editing;
            endingTextBox.Enabled = editing;
            burnimageTextBox.Enabled = editing;
            craftableCheckBox.Enabled = editing;
            hintonlyCheckBox.Enabled = editing;
            extendsTextBox.Enabled = editing;
            maxExecutionsTextBox.Enabled = editing;
            warmupTextBox.Enabled = editing;
            requirementsDataGridView.ReadOnly = !editing;
            extantreqsDataGridView.ReadOnly = !editing;
            tablereqsDataGridView.ReadOnly = !editing;
            effectsDataGridView.ReadOnly = !editing;
            aspectsDataGridView.ReadOnly = !editing;
            deckeffectDataGridView.ReadOnly = !editing;
        }

        private void fillValues(Recipe recipe)
        {
            if (recipe.id != null) idTextBox.Text = recipe.id;
            if (recipe.label != null) labelTextBox.Text = recipe.label;
            if (recipe.actionId != null) actionIdTextBox.Text = recipe.actionId;
            if (recipe.ending != null) endingTextBox.Text = recipe.ending;
            if (recipe.burnimage != null) burnimageTextBox.Text = recipe.burnimage;
            if (recipe.craftable.HasValue) craftableCheckBox.Checked = recipe.craftable.Value;
            if (recipe.hintonly.HasValue) hintonlyCheckBox.Checked = recipe.hintonly.Value;
            if (recipe.startdescription != null) startdescriptionTextBox.Text = recipe.startdescription;
            if (recipe.description != null) descriptionTextBox.Text = recipe.description;
            if (recipe.maxexecutions.HasValue) maxExecutionsTextBox.Text = Convert.ToString(recipe.maxexecutions.Value);
            if (recipe.warmup.HasValue) warmupTextBox.Text = Convert.ToString(recipe.warmup.Value);
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
            if (recipe.aspects != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.aspects)
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
            DeckViewer dv = new DeckViewer(displayedRecipe.internalDeck, false);
            dv.ShowDialog();
        }

        private void showSlotButton_Click(object sender, EventArgs e)
        {
            SlotViewer sv = new SlotViewer(displayedRecipe.slots[0], false);
            sv.ShowDialog();
        }

        private void alternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativerecipesListBox.SelectedItem == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(alternativerecipeLinks[alternativerecipesListBox.SelectedItem.ToString()], false);
            rlv.ShowDialog();
        }

        private void linkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedListBox.SelectedItem == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(recipeLinks[linkedListBox.SelectedItem.ToString()], false);
            rlv.ShowDialog();
        }

        private void mutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListBox.SelectedItem == null) return;
            MutationViewer mv = new MutationViewer(mutations[mutationsListBox.SelectedItem.ToString()], false);
            mv.ShowDialog();
        }

        private void requirementsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects OR elements
            string id = requirementsDataGridView.SelectedCells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void extantreqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can also be aspects or elements
            string id = extantreqsDataGridView.SelectedCells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
            else if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void tablereqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be card elements
            string id = tablereqsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
        }

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be card elements
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.elementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.getElement(id), false);
                ev.ShowDialog();
            }
        }

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be aspect elements
            string id = aspectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.aspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.getAspect(id), false);
                av.ShowDialog();
            }
        }

        private void deckeffectDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be decks
            string id = deckeffectDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (Utilities.deckExists(id))
            {
                DeckViewer dv = new DeckViewer(Utilities.getDeck(id), false);
                dv.ShowDialog();
            }
        }
    }
}
