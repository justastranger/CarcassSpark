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
        public Recipe displayedRecipe;
        bool editing;

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
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            actionIdTextBox.ReadOnly = !editing;
            endingTextBox.ReadOnly = !editing;
            burnimageTextBox.ReadOnly = !editing;
            extendsTextBox.ReadOnly = !editing;
            startdescriptionTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            craftableCheckBox.Enabled = editing;
            hintonlyCheckBox.Enabled = editing;
            maxExecutionsNumericUpDown.Enabled = editing;
            warmupNumericUpDown.Enabled = editing;
            requirementsDataGridView.ReadOnly = !editing;
            requirementsDataGridView.AllowUserToAddRows = editing;
            requirementsDataGridView.AllowUserToDeleteRows = editing;
            extantreqsDataGridView.ReadOnly = !editing;
            extantreqsDataGridView.AllowUserToAddRows = editing;
            extantreqsDataGridView.AllowUserToDeleteRows = editing;
            tablereqsDataGridView.ReadOnly = !editing;
            tablereqsDataGridView.AllowUserToAddRows = editing;
            tablereqsDataGridView.AllowUserToDeleteRows = editing;
            effectsDataGridView.ReadOnly = !editing;
            effectsDataGridView.AllowUserToAddRows = editing;
            effectsDataGridView.AllowUserToDeleteRows = editing;
            aspectsDataGridView.ReadOnly = !editing;
            aspectsDataGridView.AllowUserToAddRows = editing;
            aspectsDataGridView.AllowUserToDeleteRows = editing;
            deckeffectDataGridView.ReadOnly = !editing;
            deckeffectDataGridView.AllowUserToAddRows = editing;
            deckeffectDataGridView.AllowUserToDeleteRows = editing;
            addAlternativeRecipeButton.Visible = editing;
            addLinkedRecipeButton.Visible = editing;
            addMutationButton.Visible = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            showSlotButton.Enabled = showSlotButton.Enabled ? true : editing;
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
            if (recipe.maxexecutions.HasValue) maxExecutionsNumericUpDown.Value = recipe.maxexecutions.Value;
            if (recipe.warmup.HasValue) warmupNumericUpDown.Value = recipe.warmup.Value;
            showInternalDeckButton.Enabled = recipe.internalDeck != null ? true : false;
            showSlotButton.Enabled = recipe.slots != null ? true : false;
            if (recipe.requirements != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.requirements)
                {
                    requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.extantreqs != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.extantreqs)
                {
                    extantreqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.tablereqs != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.tablereqs)
                {
                    tablereqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.effects != null)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.effects)
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
            if (displayedRecipe.internalDeck == null && editing)
            {
                DeckViewer dv = new DeckViewer(new Deck(), true, true);
                dv.ShowDialog();
                if(dv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.internalDeck = dv.displayedDeck;
                }
            } else if (displayedRecipe.internalDeck != null)
            {
                DeckViewer dv = new DeckViewer(displayedRecipe.internalDeck, editing, true);
                dv.ShowDialog();
                if(dv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.internalDeck = dv.displayedDeck;
                }
            }
        }

        private void showSlotButton_Click(object sender, EventArgs e)
        {
            if (displayedRecipe.slots == null && editing)
            {
                SlotViewer sv = new SlotViewer(new Slot(), true);
                sv.ShowDialog();
                if (sv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot };
                }
            }
            else if (displayedRecipe.slots != null)
            {
                SlotViewer sv = new SlotViewer(displayedRecipe.slots[0], editing);
                sv.ShowDialog();
                if (sv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot };
                }
            }
        }

        private void alternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativerecipesListBox.SelectedItem == null || editing) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(alternativerecipeLinks[alternativerecipesListBox.SelectedItem.ToString()], false);
            rlv.ShowDialog();
        }

        private void linkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedListBox.SelectedItem == null || editing) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(recipeLinks[linkedListBox.SelectedItem.ToString()], false);
            rlv.ShowDialog();
        }

        private void mutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListBox.SelectedItem == null || editing) return;
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (requirementsDataGridView.RowCount > 1)
            {
                displayedRecipe.requirements = new Dictionary<string, int>();
                foreach (DataGridViewRow row in requirementsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.requirements.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (extantreqsDataGridView.RowCount > 1)
            {
                displayedRecipe.extantreqs = new Dictionary<string, int>();
                foreach (DataGridViewRow row in extantreqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.extantreqs.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (tablereqsDataGridView.RowCount > 1)
            {
                displayedRecipe.tablereqs = new Dictionary<string, int>();
                foreach (DataGridViewRow row in tablereqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.tablereqs.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (effectsDataGridView.RowCount > 1)
            {
                displayedRecipe.effects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.effects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (aspectsDataGridView.RowCount > 1)
            {
                displayedRecipe.aspects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.aspects.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            if (deckeffectDataGridView.RowCount > 1)
            {
                displayedRecipe.deckeffect = new Dictionary<string, int>();
                foreach (DataGridViewRow row in deckeffectDataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null) displayedRecipe.deckeffect.Add(row.Cells[0].Value.ToString(), Convert.ToInt32(row.Cells[1].Value));
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void addAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new Recipe.RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    alternativerecipesListBox.Items.Add(rlv.displayedRecipeLink.id);
                    alternativerecipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.alternativerecipes != null) displayedRecipe.alternativerecipes.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.alternativerecipes = new List<Recipe.RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void addLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new Recipe.RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    linkedListBox.Items.Add(rlv.displayedRecipeLink.id);
                    recipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.linked != null) displayedRecipe.linked.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.linked = new List<Recipe.RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void addMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Recipe.Mutation(), true))
            {
                mv.ShowDialog();
                if(mv.DialogResult == DialogResult.OK)
                {
                    mutationsListBox.Items.Add(mv.displayedMutation.filterOnAspectId);
                    mutations.Add(mv.displayedMutation.filterOnAspectId, mv.displayedMutation);
                    if (displayedRecipe.mutations != null) displayedRecipe.mutations.Add(mv.displayedMutation);
                    else displayedRecipe.mutations = new List<Recipe.Mutation> { mv.displayedMutation };
                }
            }
        }

        private void maxExecutionsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipe.maxexecutions = Convert.ToInt32(maxExecutionsNumericUpDown.Value);
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.label = labelTextBox.Text;
        }

        private void actionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.actionId = actionIdTextBox.Text;
        }

        private void endingTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.ending = endingTextBox.Text;
        }

        private void burnimageTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.burnimage = burnimageTextBox.Text;
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.extends = new List<string> { extendsTextBox.Text };
        }

        private void craftableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedRecipe.craftable = craftableCheckBox.Checked;
        }

        private void hintonlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            displayedRecipe.hintonly = hintonlyCheckBox.Checked;
        }

        private void warmupNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipe.warmup = Convert.ToInt32(warmupNumericUpDown.Value);
        }
    }
}
