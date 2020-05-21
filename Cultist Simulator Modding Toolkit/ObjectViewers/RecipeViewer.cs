using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;

namespace CarcassSpark.ObjectViewers
{
    public partial class RecipeViewer : Form
    {
        public Recipe displayedRecipe;
        bool editing;

        Dictionary<string, RecipeLink> recipeLinks = new Dictionary<string, RecipeLink>();
        Dictionary<string, RecipeLink> alternativerecipeLinks = new Dictionary<string, RecipeLink>();
        Dictionary<string, Mutation> mutations = new Dictionary<string, Mutation>();

        public RecipeViewer(Recipe recipe, bool? editing)
        {
            InitializeComponent();
            displayedRecipe = recipe;
            
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
            signalEndingFlavourDomainUpDown.ReadOnly = !editing;
            portalEffectDomainUpDown.ReadOnly = !editing;
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
            removeAlternativeRecipeButton.Visible = editing;
            addLinkedRecipeButton.Visible = editing;
            removeLinkedRecipeButton.Visible = editing;
            addMutationButton.Visible = editing;
            removeMutationButton.Visible = editing;
            prependAlternativeRecipeButton.Visible = editing;
            prependLinkedRecipeButton.Visible = editing;
            prependMutationButton.Visible = editing;
            appendAlternativeReipeButton.Visible = editing;
            appendLinkedRecipeButton.Visible = editing;
            appendMutationButton.Visible = editing;
            okButton.Visible = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            if (!showSlotButton.Enabled) showSlotButton.Enabled = editing;
            if (!showInternalDeckButton.Enabled) showInternalDeckButton.Enabled = editing;
        }

        private void fillValues(Recipe recipe)
        {
            if (recipe.extends != null)
            {
                extendsTextBox.Text = recipe.extends[0];
                //Recipe extendedRecipe = Utilities.getRecipe(recipe.extends[0]);
                //fillValues(extendedRecipe);
            }
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
            if (recipe.internalDeck != null) showInternalDeckButton.Enabled = true;
            else showInternalDeckButton.Enabled = false;
            if (recipe.slots != null) showSlotButton.Enabled = true;
            else showSlotButton.Enabled = false;
            if (recipe.requirements != null && recipe.requirements.Count > 0)
            {
                requirementsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.requirements)
                {
                    requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.requirements_extend != null && recipe.requirements_extend.Count > 0)
            {
                //requirementsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.requirements_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(requirementsDataGridView, kvp.Key, kvp.Value);
                    requirementsDataGridView.Rows.Add(row);
                    //requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.requirements_remove != null && recipe.requirements_remove.Count > 0)
            {
                //requirementsDataGridView.Rows.Clear();
                foreach (string removeId in recipe.requirements_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(requirementsDataGridView, removeId);
                    requirementsDataGridView.Rows.Add(row);
                    //requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.extantreqs != null && recipe.extantreqs.Count > 0)
            {
                extantreqsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.extantreqs)
                {
                    extantreqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.extantreqs_extend != null && recipe.extantreqs_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.extantreqs_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(extantreqsDataGridView, kvp.Key, kvp.Value);
                    extantreqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.extantreqs_remove != null && recipe.extantreqs_remove.Count > 0)
            {
                foreach (string removeId in recipe.extantreqs_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(extantreqsDataGridView, removeId);
                    extantreqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.tablereqs != null && recipe.tablereqs.Count > 0)
            {
                tablereqsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.tablereqs)
                {
                    tablereqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.tablereqs_extend != null && recipe.tablereqs_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.tablereqs_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(tablereqsDataGridView, kvp.Key, kvp.Value);
                    tablereqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.tablereqs_remove != null && recipe.tablereqs_remove.Count > 0)
            {
                foreach (string removeId in recipe.tablereqs_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(tablereqsDataGridView, removeId);
                    tablereqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.effects != null && recipe.effects.Count > 0)
            {
                effectsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.effects)
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.effects_extend != null && recipe.effects_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.effects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(effectsDataGridView, kvp.Key, kvp.Value);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.effects_remove != null && recipe.effects_remove.Count > 0)
            {
                foreach (string removeId in recipe.effects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(effectsDataGridView, removeId);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.aspects != null && recipe.aspects.Count > 0)
            {
                aspectsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.aspects_extend != null && recipe.aspects_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.aspects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(aspectsDataGridView, kvp.Key, kvp.Value);
                    aspectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.aspects_remove != null && recipe.aspects_remove.Count > 0)
            {
                foreach (string removeId in recipe.aspects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(aspectsDataGridView, removeId);
                    aspectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deckeffect != null && recipe.deckeffect.Count > 0)
            {
                deckeffectDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffect)
                {
                    deckeffectDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.deckeffect_extend != null && recipe.deckeffect_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffect_extend)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
                    row.CreateCells(deckeffectDataGridView, kvp.Key, kvp.Value);
                    deckeffectDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deckeffect_remove != null && recipe.deckeffect_remove.Count > 0)
            {
                foreach (string removeId in recipe.deckeffect_remove)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
                    row.CreateCells(deckeffectDataGridView, removeId);
                    deckeffectDataGridView.Rows.Add(row);
                }
            }
            if (recipe.alternativerecipes != null && recipe.alternativerecipes.Count > 0)
            {
                alternativerecipeLinks.Clear();
                alternativeRecipesListView.Items.Clear();
                foreach (RecipeLink rl in recipe.alternativerecipes)
                {
                    alternativerecipeLinks.Add(rl.id, rl);
                    alternativeRecipesListView.Items.Add(rl.id);
                }
            }
            if (recipe.alternativerecipes_prepend != null && recipe.alternativerecipes_prepend.Count > 0)
            {
                //alternativerecipeLinks.Clear();
                //alternativerecipesListBox.Items.Clear();
                foreach (RecipeLink rl in recipe.alternativerecipes_prepend)
                {
                    alternativerecipeLinks.Add(rl.id, rl);
                    ListViewItem item = new ListViewItem(rl.id);
                    item.BackColor = Utilities.ListPrependColor;
                    alternativeRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.alternativerecipes_append != null && recipe.alternativerecipes_append.Count > 0)
            {
                //alternativerecipeLinks.Clear();
                //alternativerecipesListBox.Items.Clear();
                foreach (RecipeLink rl in recipe.alternativerecipes_append)
                {
                    alternativerecipeLinks.Add(rl.id, rl);
                    ListViewItem item = new ListViewItem(rl.id);
                    item.BackColor = Utilities.ListAppendColor;
                    alternativeRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.alternativerecipes_remove != null && recipe.alternativerecipes_remove.Count > 0)
            {
                //alternativerecipeLinks.Clear();
                //alternativerecipesListBox.Items.Clear();
                foreach (string rl in recipe.alternativerecipes_remove)
                {
                    //alternativerecipeLinks.Add(rl.id, rl);
                    ListViewItem item = new ListViewItem(rl);
                    item.BackColor = Utilities.ListRemoveColor;
                    alternativeRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.linked != null && recipe.linked.Count > 0)
            {
                recipeLinks.Clear();
                linkedRecipesListView.Items.Clear();
                foreach (RecipeLink rl in recipe.linked)
                {
                    recipeLinks.Add(rl.id, rl);
                    linkedRecipesListView.Items.Add(rl.id);
                }
            }
            if (recipe.mutations != null && recipe.mutations.Count > 0)
            {

                mutations.Clear();
                mutationsListView.Items.Clear();
                foreach (Mutation mutation in recipe.mutations)
                {
                    mutations.Add(mutation.mutateAspectId, mutation);
                    mutationsListView.Items.Add(mutation.mutateAspectId);
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
                SlotViewer sv = new SlotViewer(new Slot(), true, SlotViewer.SlotType.RECIPE);
                sv.ShowDialog();
                if (sv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot };
                }
            }
            else if (displayedRecipe.slots != null)
            {
                SlotViewer sv = new SlotViewer(displayedRecipe.slots[0], editing, SlotViewer.SlotType.RECIPE);
                sv.ShowDialog();
                if (sv.DialogResult == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot };
                }
            }
        }

        private void alternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(alternativerecipeLinks[alternativeRecipesListView.SelectedItems[0].Text.ToString()], editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                alternativerecipeLinks.Remove(alternativeRecipesListView.SelectedItems[0].ToString());
                alternativeRecipesListView.Items[alternativeRecipesListView.SelectedIndices[0]].Text = rlv.displayedRecipeLink.id;
                alternativerecipeLinks[rlv.displayedRecipeLink.id] = rlv.displayedRecipeLink;
                displayedRecipe.alternativerecipes[alternativeRecipesListView.SelectedIndices[0]] = rlv.displayedRecipeLink;

            }
        }

        private void linkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems == null) return;
            RecipeLinkViewer rlv = new RecipeLinkViewer(recipeLinks[linkedRecipesListView.SelectedItems[0].Text.ToString()], editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                recipeLinks.Remove(linkedRecipesListView.SelectedItems.ToString());
                linkedRecipesListView.Items[linkedRecipesListView.SelectedIndices[0]].Text = rlv.displayedRecipeLink.id;
                recipeLinks[rlv.displayedRecipeLink.id] = rlv.displayedRecipeLink;
                displayedRecipe.linked[linkedRecipesListView.SelectedIndices[0]] = rlv.displayedRecipeLink;

            }
        }

        private void mutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems == null) return;
            MutationViewer mv = new MutationViewer(mutations[mutationsListView.SelectedItems[0].Text.ToString()], editing);
            mv.ShowDialog();
            if (mv.DialogResult == DialogResult.OK)
            {
                mutations.Remove(mv.displayedMutation.mutateAspectId);
                mutationsListView.Items[mutationsListView.SelectedIndices[0]].Text = mv.displayedMutation.mutateAspectId;
                mutations.Add(mv.displayedMutation.mutateAspectId, mv.displayedMutation);
                displayedRecipe.mutations[mutationsListView.SelectedIndices[0]] = mv.displayedMutation;
            }
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
        { // can be elements or aspects
            string id = tablereqsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
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

        private void effectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be elements or aspects
            string id = effectsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
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

        private void aspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects
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
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Recipes must have an ID");
                return;
            }
            if (requirementsDataGridView.RowCount > 1)
            {
                //displayedRecipe.requirements = new Dictionary<string, int>();
                foreach (DataGridViewRow row in requirementsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.requirements_extend == null) displayedRecipe.requirements_extend = new Dictionary<string, int>();
                        displayedRecipe.requirements_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.requirements_remove == null) displayedRecipe.requirements_remove = new List<string>();
                        if (!displayedRecipe.requirements_remove.Contains(key)) displayedRecipe.requirements_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.requirements == null) displayedRecipe.requirements = new Dictionary<string, int>();
                        displayedRecipe.requirements[key] = value.Value;
                    }
                }
            }
            if (extantreqsDataGridView.RowCount > 1)
            {
                //displayedRecipe.extantreqs = new Dictionary<string, int>();
                foreach (DataGridViewRow row in extantreqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.extantreqs_extend == null) displayedRecipe.extantreqs_extend = new Dictionary<string, int>();
                        displayedRecipe.extantreqs_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.extantreqs_remove == null) displayedRecipe.extantreqs_remove = new List<string>();
                        if (!displayedRecipe.extantreqs_remove.Contains(key)) displayedRecipe.extantreqs_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.extantreqs == null) displayedRecipe.extantreqs = new Dictionary<string, int>();
                        displayedRecipe.extantreqs[key] = value.Value;
                    }
                }
            }
            if (tablereqsDataGridView.RowCount > 1)
            {
                //displayedRecipe.tablereqs = new Dictionary<string, int>();
                foreach (DataGridViewRow row in tablereqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.tablereqs_extend == null) displayedRecipe.tablereqs_extend = new Dictionary<string, int>();
                        displayedRecipe.tablereqs_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.tablereqs_remove == null) displayedRecipe.tablereqs_remove = new List<string>();
                        if (!displayedRecipe.tablereqs_remove.Contains(key)) displayedRecipe.tablereqs_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.tablereqs == null) displayedRecipe.tablereqs = new Dictionary<string, int>();
                        displayedRecipe.tablereqs[key] = value.Value;
                    }
                }
            }
            if (effectsDataGridView.RowCount > 1)
            {
                //displayedRecipe.effects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.effects_extend == null) displayedRecipe.effects_extend = new Dictionary<string, int>();
                        displayedRecipe.effects_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.effects_remove == null) displayedRecipe.effects_remove = new List<string>();
                        if (!displayedRecipe.effects_remove.Contains(key)) displayedRecipe.effects_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.effects == null) displayedRecipe.effects = new Dictionary<string, int>();
                        displayedRecipe.effects[key] = value.Value;
                    }
                }
            }
            if (aspectsDataGridView.RowCount > 1)
            {
                //displayedRecipe.aspects = new Dictionary<string, int>();
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.aspects_extend == null) displayedRecipe.aspects_extend = new Dictionary<string, int>();
                        displayedRecipe.aspects_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.aspects_remove == null) displayedRecipe.aspects_remove = new List<string>();
                        if (!displayedRecipe.aspects_remove.Contains(key)) displayedRecipe.aspects_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.aspects == null) displayedRecipe.aspects = new Dictionary<string, int>();
                        displayedRecipe.aspects[key] = value.Value;
                    }
                }
            }
            if (deckeffectDataGridView.RowCount > 1)
            {
                //displayedRecipe.deckeffect = new Dictionary<string, int>();
                foreach (DataGridViewRow row in deckeffectDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.deckeffect_extend == null) displayedRecipe.deckeffect_extend = new Dictionary<string, int>();
                        displayedRecipe.deckeffect_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.deckeffect_remove == null) displayedRecipe.deckeffect_remove = new List<string>();
                        if (!displayedRecipe.deckeffect_remove.Contains(key)) displayedRecipe.deckeffect_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.deckeffect == null) displayedRecipe.deckeffect = new Dictionary<string, int>();
                        displayedRecipe.deckeffect[key] = value.Value;
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void addAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    alternativeRecipesListView.Items.Add(rlv.displayedRecipeLink.id);
                    alternativerecipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.alternativerecipes != null) displayedRecipe.alternativerecipes.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.alternativerecipes = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }
        
        private void prependAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id);
                    item.BackColor = Utilities.ListPrependColor;
                    alternativeRecipesListView.Items.Insert(0, item);
                    alternativerecipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.alternativerecipes_prepend != null) displayedRecipe.alternativerecipes_prepend.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.alternativerecipes_prepend = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void appendAlternativeReipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id);
                    item.BackColor = Utilities.ListAppendColor;
                    alternativeRecipesListView.Items.Add(item);
                    alternativerecipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.alternativerecipes_append != null) displayedRecipe.alternativerecipes_append.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.alternativerecipes_append = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void addLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    linkedRecipesListView.Items.Add(rlv.displayedRecipeLink.id);
                    recipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.linked != null) displayedRecipe.linked.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.linked = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void prependLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id);
                    item.BackColor = Utilities.ListPrependColor;
                    linkedRecipesListView.Items.Insert(0, item);
                    recipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.linked_prepend != null) displayedRecipe.linked_prepend.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.linked_prepend = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void appendLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id);
                    item.BackColor = Utilities.ListPrependColor;
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add(rlv.displayedRecipeLink.id, rlv.displayedRecipeLink);
                    if (displayedRecipe.linked_append != null) displayedRecipe.linked_append.Add(rlv.displayedRecipeLink);
                    else displayedRecipe.linked_append = new List<RecipeLink> { rlv.displayedRecipeLink };
                }
            }
        }

        private void addMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if(mv.DialogResult == DialogResult.OK)
                {
                    mutationsListView.Items.Add(mv.displayedMutation.mutateAspectId);
                    mutations.Add(mv.displayedMutation.mutateAspectId, mv.displayedMutation);
                    if (displayedRecipe.mutations != null) displayedRecipe.mutations.Add(mv.displayedMutation);
                    else displayedRecipe.mutations = new List<Mutation> { mv.displayedMutation };
                }
            }
        }

        private void prependMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if (mv.DialogResult == DialogResult.OK)
                {
                    mutationsListView.Items.Add(mv.displayedMutation.mutateAspectId);
                    mutations.Add(mv.displayedMutation.mutateAspectId, mv.displayedMutation);
                    if (displayedRecipe.mutations_prepend != null) displayedRecipe.mutations_prepend.Add(mv.displayedMutation);
                    else displayedRecipe.mutations_prepend = new List<Mutation> { mv.displayedMutation };
                }
            }
        }

        private void appendMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if (mv.DialogResult == DialogResult.OK)
                {
                    mutationsListView.Items.Add(mv.displayedMutation.mutateAspectId);
                    mutations.Add(mv.displayedMutation.mutateAspectId, mv.displayedMutation);
                    if (displayedRecipe.mutations_append != null) displayedRecipe.mutations_append.Add(mv.displayedMutation);
                    else displayedRecipe.mutations_append = new List<Mutation> { mv.displayedMutation };
                }
            }
        }

        private void maxExecutionsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipe.maxexecutions = Convert.ToInt32(maxExecutionsNumericUpDown.Value);
            if (maxExecutionsNumericUpDown.Value == 0) displayedRecipe.maxexecutions = null;
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            if (idTextBox.Text == "") displayedRecipe.id = null;
            else displayedRecipe.id = idTextBox.Text;
        }

        private void labelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (labelTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.label = labelTextBox.Text;
        }

        private void actionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (actionIdTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.actionId = actionIdTextBox.Text;
        }

        private void endingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (endingTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.ending = endingTextBox.Text;
        }

        private void burnimageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (burnimageTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.burnimage = burnimageTextBox.Text;
        }

        private void extendsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (extendsTextBox.Text == "") displayedRecipe.extends = null;
            else displayedRecipe.extends = new List<string> { extendsTextBox.Text };
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

        private void startdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.startdescription = startdescriptionTextBox.Text;
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.description = descriptionTextBox.Text;
        }

        private void requirementsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {   
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.requirements_extend == null) return;
                if (displayedRecipe.requirements_extend.ContainsKey(key)) displayedRecipe.requirements_extend.Remove(key);
                if (displayedRecipe.requirements_extend.Count == 0) displayedRecipe.requirements_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.requirements_remove == null) return;
                if (displayedRecipe.requirements_remove.Contains(key)) displayedRecipe.requirements_remove.Remove(key);
                if (displayedRecipe.requirements_remove.Count == 0) displayedRecipe.requirements_remove = null;
            }
            else
            {
                if (displayedRecipe.requirements == null) return;
                if (displayedRecipe.requirements.ContainsKey(key)) displayedRecipe.requirements.Remove(key);
                if (displayedRecipe.requirements.Count == 0) displayedRecipe.requirements = null;
            }
        }

        private void extantreqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.extantreqs_extend == null) return;
                if (displayedRecipe.extantreqs_extend.ContainsKey(key)) displayedRecipe.extantreqs_extend.Remove(key);
                if (displayedRecipe.extantreqs_extend.Count == 0) displayedRecipe.extantreqs_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.extantreqs_remove == null) return;
                if (displayedRecipe.extantreqs_remove.Contains(key)) displayedRecipe.extantreqs_remove.Remove(key);
                if (displayedRecipe.extantreqs_remove.Count == 0) displayedRecipe.extantreqs_remove = null;
            }
            else
            {
                if (displayedRecipe.extantreqs == null) return;
                if (displayedRecipe.extantreqs.ContainsKey(key)) displayedRecipe.extantreqs.Remove(key);
                if (displayedRecipe.extantreqs.Count == 0) displayedRecipe.extantreqs = null;
            }
        }

        private void tablereqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.tablereqs_extend == null) return;
                if (displayedRecipe.tablereqs_extend.ContainsKey(key)) displayedRecipe.tablereqs_extend.Remove(key);
                if (displayedRecipe.tablereqs_extend.Count == 0) displayedRecipe.tablereqs_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.tablereqs_remove == null) return;
                if (displayedRecipe.tablereqs_remove.Contains(key)) displayedRecipe.tablereqs_remove.Remove(key);
                if (displayedRecipe.tablereqs_remove.Count == 0) displayedRecipe.tablereqs_remove = null;
            }
            else
            {
                if (displayedRecipe.tablereqs == null) return;
                if (displayedRecipe.tablereqs.ContainsKey(key)) displayedRecipe.tablereqs.Remove(key);
                if (displayedRecipe.tablereqs.Count == 0) displayedRecipe.tablereqs = null;
            }
        }

        private void effectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.effects_extend == null) return;
                if (displayedRecipe.effects_extend.ContainsKey(key)) displayedRecipe.effects_extend.Remove(key);
                if (displayedRecipe.effects_extend.Count == 0) displayedRecipe.effects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.effects_remove == null) return;
                if (displayedRecipe.effects_remove.Contains(key)) displayedRecipe.effects_remove.Remove(key);
                if (displayedRecipe.effects_remove.Count == 0) displayedRecipe.effects_remove = null;
            }
            else
            {
                if (displayedRecipe.effects == null) return;
                if (displayedRecipe.effects.ContainsKey(key)) displayedRecipe.effects.Remove(key);
                if (displayedRecipe.effects.Count == 0) displayedRecipe.effects = null;
            }
        }

        private void aspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.aspects_extend == null) return;
                if (displayedRecipe.aspects_extend.ContainsKey(key)) displayedRecipe.aspects_extend.Remove(key);
                if (displayedRecipe.aspects_extend.Count == 0) displayedRecipe.aspects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.aspects_remove == null) return;
                if (displayedRecipe.aspects_remove.Contains(key)) displayedRecipe.aspects_remove.Remove(key);
                if (displayedRecipe.aspects_remove.Count == 0) displayedRecipe.aspects_remove = null;
            }
            else
            {
                if (displayedRecipe.aspects == null) return;
                if (displayedRecipe.aspects.ContainsKey(key)) displayedRecipe.aspects.Remove(key);
                if (displayedRecipe.aspects.Count == 0) displayedRecipe.aspects = null;
            }
        }

        private void deckeffectDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.deckeffect_extend == null) return;
                if (displayedRecipe.deckeffect_extend.ContainsKey(key)) displayedRecipe.deckeffect_extend.Remove(key);
                if (displayedRecipe.deckeffect_extend.Count == 0) displayedRecipe.deckeffect_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.deckeffect_remove == null) return;
                if (displayedRecipe.deckeffect_remove.Contains(key)) displayedRecipe.deckeffect_remove.Remove(key);
                if (displayedRecipe.deckeffect_remove.Count == 0) displayedRecipe.deckeffect_remove = null;
            }
            else
            {
                if (displayedRecipe.deckeffect == null) return;
                if (displayedRecipe.deckeffect.ContainsKey(key)) displayedRecipe.deckeffect.Remove(key);
                if (displayedRecipe.deckeffect.Count == 0) displayedRecipe.deckeffect = null;
            }
        }

        private void removeAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count > 0)
            {   // need to remove it from the right location now that there's 4 of them
                ListViewItem item = alternativeRecipesListView.SelectedItems[0];
                string value = alternativeRecipesListView.SelectedItems[0].Text.ToString();
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    displayedRecipe.alternativerecipes_prepend.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(value);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.alternativerecipes_prepend.Count == 0) displayedRecipe.alternativerecipes_prepend = null;
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    displayedRecipe.alternativerecipes_append.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(value);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.alternativerecipes_append.Count == 0) displayedRecipe.alternativerecipes_append = null;
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    displayedRecipe.alternativerecipes_remove.Remove(value);
                    alternativerecipeLinks.Remove(value);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.alternativerecipes_remove.Count == 0) displayedRecipe.alternativerecipes_remove = null;
                }
                else
                {
                    displayedRecipe.alternativerecipes.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(value);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.alternativerecipes.Count == 0) displayedRecipe.alternativerecipes = null;
                }
            }
        }

        private void removeLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count > 0)
            {
                ListViewItem item = linkedRecipesListView.SelectedItems[0];
                string value = linkedRecipesListView.SelectedItems[0].Text.ToString();
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    displayedRecipe.linked_prepend.Remove(recipeLinks[value]);
                    recipeLinks.Remove(value);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_prepend.Count == 0) displayedRecipe.linked_prepend = null;
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    displayedRecipe.linked_append.Remove(recipeLinks[value]);
                    recipeLinks.Remove(value);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_append.Count == 0) displayedRecipe.linked_append = null;
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    displayedRecipe.linked_remove.Remove(value);
                    recipeLinks.Remove(value);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_remove.Count == 0) displayedRecipe.linked_remove = null;
                }
                else
                {
                    displayedRecipe.linked.Remove(recipeLinks[value]);
                    recipeLinks.Remove(value);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked.Count == 0) displayedRecipe.linked = null;
                }
            }
        }

        private void removeMutationButton_Click(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count > 0)
            {
                ListViewItem item = mutationsListView.SelectedItems[0];
                string value = mutationsListView.SelectedItems[0].Text.ToString();
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    displayedRecipe.mutations_prepend.Remove(mutations[value]);
                    mutations.Remove(value);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_prepend.Count == 0) displayedRecipe.mutations_prepend = null;
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    displayedRecipe.mutations_append.Remove(mutations[value]);
                    mutations.Remove(value);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_append.Count == 0) displayedRecipe.mutations_append = null;
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    displayedRecipe.mutations_remove.Remove(value);
                    mutations.Remove(value);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_remove.Count == 0) displayedRecipe.mutations_remove = null;
                }
                else
                {
                    displayedRecipe.mutations.Remove(mutations[value]);
                    mutations.Remove(value);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations.Count == 0) displayedRecipe.mutations = null;
                }
            }
        }

        private void setAsExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryExtendStyle;
            }
        }

        private void setAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView affectedDataGridView = (DataGridView)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (affectedDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.SelectedRows[0];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
            else if (affectedDataGridView.SelectedCells.Count > 0)
            {
                DataGridViewRow row = affectedDataGridView.Rows[affectedDataGridView.SelectedCells[0].RowIndex];
                row.DefaultCellStyle = Utilities.DictionaryRemoveStyle;
            }
        }
    }
}
