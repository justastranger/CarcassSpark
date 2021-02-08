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
    public enum RecipeType
    {
        DEFAULT,
        NESTED,
        GENERATOR
    }

    public partial class RecipeViewer : Form
    {
        public Recipe displayedRecipe;
        bool editing;
        event EventHandler<Recipe> SuccessCallback;
        public ListViewItem associatedListViewItem;

        readonly Dictionary<Guid, RecipeLink> recipeLinks = new Dictionary<Guid, RecipeLink>();
        readonly Dictionary<Guid, RecipeLink> alternativerecipeLinks = new Dictionary<Guid, RecipeLink>();
        readonly Dictionary<Guid, Mutation> mutations = new Dictionary<Guid, Mutation>();

        public RecipeViewer(Recipe recipe, EventHandler<Recipe> SuccessCallback, ListViewItem item)
        {
            InitializeComponent();
            displayedRecipe = recipe;
            FillValues(recipe);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else SetEditingMode(false);
        }

        public RecipeViewer(Recipe recipe, EventHandler<Recipe> SuccessCallback, RecipeType recipeViewerType, ListViewItem item)
        {
            InitializeComponent();
            displayedRecipe = recipe;
            FillValues(recipe);
            associatedListViewItem = item;
            if (SuccessCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += SuccessCallback;
            }
            else
            {
                SetEditingMode(false);
            }

            SetViewerType(recipeViewerType);
        }

        void SetEditingMode(bool editing)
        {
            this.editing = editing;
            idTextBox.ReadOnly = !editing;
            labelTextBox.ReadOnly = !editing;
            actionIdTextBox.ReadOnly = !editing;
            endingTextBox.ReadOnly = !editing;
            burnimageTextBox.ReadOnly = !editing;
            startdescriptionTextBox.ReadOnly = !editing;
            descriptionTextBox.ReadOnly = !editing;
            commentsTextBox.ReadOnly = !editing;
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
            moveAltRecipeDownButton.Visible = editing;
            moveAltRecipeUpButton.Visible = editing;
            moveLinkedRecipeDownButton.Visible = editing;
            moveLinkedRecipeUpButton.Visible = editing;
            okButton.Visible = editing;
            signalImportantLoopCheckBox.Enabled = editing;
            purgeDataGridView.ReadOnly = !editing;
            purgeDataGridView.AllowUserToAddRows = editing;
            purgeDataGridView.AllowUserToDeleteRows = editing;
            haltVerbDataGridView.ReadOnly = !editing;
            haltVerbDataGridView.AllowUserToAddRows = editing;
            haltVerbDataGridView.AllowUserToDeleteRows = editing;
            deleteVerbDataGridView.ReadOnly = !editing;
            deleteVerbDataGridView.AllowUserToAddRows = editing;
            deleteVerbDataGridView.AllowUserToDeleteRows = editing;
            moveMutationUpButton.Visible = editing;
            moveMutationDownButton.Visible = editing;
            setAsExtendToolStripMenuItem.Visible = editing;
            setAsRemoveToolStripMenuItem.Visible = editing;
            deletedCheckBox.Enabled = editing;
            cancelButton.Text = editing ? "Cancel" : "Close";
            if (showSlotButton.Text == "New Slot")
            {
                showSlotButton.Enabled = editing;
            }
            if (showInternalDeckButton.Text == "New Deck")
            {
                showInternalDeckButton.Enabled = editing;
            }
        }

        void SetViewerType(RecipeType recipeViewerType)
        {
            switch (recipeViewerType)
            {
                case RecipeType.GENERATOR:
                    idLabel.ForeColor = Color.Red;
                    labelLabel.ForeColor = Color.Red;
                    startDescriptionLabel.ForeColor = Color.Red;
                    descriptionLabel.ForeColor = Color.Red;
                    requirementsLabel.ForeColor = Color.Red;
                    break;
                case RecipeType.NESTED:
                    break;
                case RecipeType.DEFAULT:
                    break;
            }
        }

        private void FillValues(Recipe recipe)
        {
            if (recipe.id != null) idTextBox.Text = recipe.id;
            if (recipe.label != null) labelTextBox.Text = recipe.label;
            if (recipe.actionId != null) actionIdTextBox.Text = recipe.actionId;
            if (recipe.ending != null) endingTextBox.Text = recipe.ending;
            if (recipe.burnimage != null) burnimageTextBox.Text = recipe.burnimage;
            if (recipe.craftable.HasValue) craftableCheckBox.Checked = recipe.craftable.Value;
            if (recipe.hintonly.HasValue) hintonlyCheckBox.Checked = recipe.hintonly.Value;
            if (recipe.signalimportantloop.HasValue) signalImportantLoopCheckBox.Checked = recipe.signalimportantloop.Value;
            if (recipe.startdescription != null) startdescriptionTextBox.Text = recipe.startdescription;
            if (recipe.description != null) descriptionTextBox.Text = recipe.description;
            if (recipe.portaleffect != null) portalEffectDomainUpDown.Text = recipe.portaleffect;
            if (recipe.signalendingflavour != null) signalEndingFlavourDomainUpDown.Text = recipe.signalendingflavour;
            if (recipe.maxexecutions.HasValue) maxExecutionsNumericUpDown.Value = recipe.maxexecutions.Value;
            if (recipe.warmup.HasValue) warmupNumericUpDown.Value = recipe.warmup.Value;
            if (recipe.internaldeck != null)
            {
                showInternalDeckButton.Text = "Show Deck";
            }
            else
            {
                showInternalDeckButton.Text = "New Deck";
            }
            if (recipe.slots?.Count > 0)
            {
                showSlotButton.Text = "Show Slot";
            }
            else
            {
                showSlotButton.Text = "New Slot";
            }
            if (recipe.deleted.HasValue) deletedCheckBox.Checked = recipe.deleted.Value;
            if (recipe.requirements != null && recipe.requirements.Count > 0)
            {
                // requirementsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, string> kvp in recipe.requirements)
                {
                    requirementsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.requirements_extend != null && recipe.requirements_extend.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in recipe.requirements_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(requirementsDataGridView, kvp.Key, kvp.Value);
                    requirementsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.requirements_remove != null && recipe.requirements_remove.Count > 0)
            {
                foreach (string removeId in recipe.requirements_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(requirementsDataGridView, removeId);
                    requirementsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.extantreqs != null && recipe.extantreqs.Count > 0)
            {
                // extantreqsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, string> kvp in recipe.extantreqs)
                {
                    extantreqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.extantreqs_extend != null && recipe.extantreqs_extend.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in recipe.extantreqs_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(extantreqsDataGridView, kvp.Key, kvp.Value);
                    extantreqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.extantreqs_remove != null && recipe.extantreqs_remove.Count > 0)
            {
                foreach (string removeId in recipe.extantreqs_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(extantreqsDataGridView, removeId);
                    extantreqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.tablereqs != null && recipe.tablereqs.Count > 0)
            {
                // tablereqsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, string> kvp in recipe.tablereqs)
                {
                    tablereqsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.tablereqs_extend != null && recipe.tablereqs_extend.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in recipe.tablereqs_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(tablereqsDataGridView, kvp.Key, kvp.Value);
                    tablereqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.tablereqs_remove != null && recipe.tablereqs_remove.Count > 0)
            {
                foreach (string removeId in recipe.tablereqs_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(tablereqsDataGridView, removeId);
                    tablereqsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.effects != null && recipe.effects.Count > 0)
            {
                // effectsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, string> kvp in recipe.effects)
                {
                    effectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.effects_extend != null && recipe.effects_extend.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in recipe.effects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(effectsDataGridView, kvp.Key, kvp.Value);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.effects_remove != null && recipe.effects_remove.Count > 0)
            {
                foreach (string removeId in recipe.effects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(effectsDataGridView, removeId);
                    effectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.aspects != null && recipe.aspects.Count > 0)
            {
                // aspectsDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.aspects)
                {
                    aspectsDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.aspects_extend != null && recipe.aspects_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.aspects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(aspectsDataGridView, kvp.Key, kvp.Value);
                    aspectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.aspects_remove != null && recipe.aspects_remove.Count > 0)
            {
                foreach (string removeId in recipe.aspects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(aspectsDataGridView, removeId);
                    aspectsDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deckeffects != null && recipe.deckeffects.Count > 0)
            {
                // deckeffectDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffects)
                {
                    deckeffectDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.deckeffects_extend != null && recipe.deckeffects_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.deckeffects_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(deckeffectDataGridView, kvp.Key, kvp.Value);
                    deckeffectDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deckeffects_remove != null && recipe.deckeffects_remove.Count > 0)
            {
                foreach (string removeId in recipe.deckeffects_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(deckeffectDataGridView, removeId);
                    deckeffectDataGridView.Rows.Add(row);
                }
            }
            if (recipe.alt != null && recipe.alt.Count > 0)
            {
                // alternativerecipeLinks.Clear();
                // alternativeRecipesListView.Items.Clear();
                foreach (RecipeLink rl in recipe.alt)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        Tag = Guid.NewGuid()
                    };
                    alternativerecipeLinks.Add((Guid)item.Tag, rl);
                    alternativeRecipesListView.Items.Add(item);
                }
            }
            if (recipe.alt_prepend != null && recipe.alt_prepend.Count > 0)
            {
                foreach (RecipeLink rl in recipe.alt_prepend)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = Guid.NewGuid()
                    };
                    alternativeRecipesListView.Items.Insert(0, item);
                    alternativerecipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.alt_append != null && recipe.alt_append.Count > 0)
            {
                foreach (RecipeLink rl in recipe.alt_append)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = Guid.NewGuid()
                    };
                    alternativeRecipesListView.Items.Insert(0, item);
                    alternativerecipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.alt_remove != null && recipe.alt_remove.Count > 0)
            {
                foreach (string rl in recipe.alt_remove)
                {
                    ListViewItem item = new ListViewItem(rl)
                    {
                        BackColor = Utilities.ListRemoveColor,
                        Tag = Guid.NewGuid()
                    };
                    alternativerecipeLinks.Add((Guid)item.Tag, new RecipeLink(rl));
                    alternativeRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.linked != null && recipe.linked.Count > 0)
            {
                // recipeLinks.Clear();
                // linkedRecipesListView.Items.Clear();
                foreach (RecipeLink rl in recipe.linked)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        Tag = Guid.NewGuid()
                    };
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.linked_prepend != null && recipe.linked_prepend.Count > 0)
            {
                // recipeLinks.Clear();
                // linkedRecipesListView.Items.Clear();
                // foreach (RecipeLink rl in recipe.linked)
                // {
                //     recipeLinks.Add(rl.id, rl);
                //     linkedRecipesListView.Items.Add(rl.id);
                // }
                foreach (RecipeLink rl in recipe.linked_prepend)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = Guid.NewGuid()
                    };
                    linkedRecipesListView.Items.Insert(0, item);
                    recipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.linked_append != null && recipe.linked_append.Count > 0)
            {
                foreach (RecipeLink rl in recipe.linked_append)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = Guid.NewGuid()
                    };
                    linkedRecipesListView.Items.Insert(0, item);
                    recipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.linked_remove != null && recipe.linked_remove.Count > 0)
            {
                foreach (string rl in recipe.linked_remove)
                {
                    // recipeLinks.Add(rl.id, rl);
                    ListViewItem item = new ListViewItem(rl)
                    {
                        BackColor = Utilities.ListRemoveColor,
                        Tag = Guid.NewGuid()
                    };
                    recipeLinks.Add((Guid)item.Tag, new RecipeLink(rl));
                    linkedRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.mutations != null && recipe.mutations.Count > 0)
            {

                // mutations.Clear();
                // mutationsListView.Items.Clear();
                foreach (Mutation mutation in recipe.mutations)
                {
                    ListViewItem item = new ListViewItem(mutation.mutate)
                    {
                        Tag = Guid.NewGuid()
                    };
                    mutations.Add((Guid)item.Tag, mutation);
                    mutationsListView.Items.Add(item);
                }
            }
            if (recipe.mutations_prepend != null && recipe.mutations_prepend.Count > 0)
            {

                // mutations.Clear();
                // mutationsListView.Items.Clear();
                foreach (Mutation mutation in recipe.mutations_prepend)
                {
                    ListViewItem item = new ListViewItem(mutation.mutate)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = Guid.NewGuid()
                    };
                    mutations.Add((Guid)item.Tag, mutation);
                    mutationsListView.Items.Add(item);
                }
            }
            if (recipe.mutations_append != null && recipe.mutations_append.Count > 0)
            {

                // mutations.Clear();
                // mutationsListView.Items.Clear();
                foreach (Mutation mutation in recipe.mutations_append)
                {
                    ListViewItem item = new ListViewItem(mutation.mutate)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = Guid.NewGuid()
                    };
                    mutations.Add((Guid)item.Tag, mutation);
                    mutationsListView.Items.Add(item);
                }
            }
            if (recipe.mutations_remove != null && recipe.mutations_remove.Count > 0)
            {
                foreach (string mutation in recipe.mutations_remove)
                {
                    // recipeLinks.Add(rl.id, rl);
                    ListViewItem item = new ListViewItem(mutation)
                    {
                        BackColor = Utilities.ListRemoveColor,
                        Tag = Guid.NewGuid()
                    };
                    mutations.Add((Guid)item.Tag, new Mutation(mutation));
                    linkedRecipesListView.Items.Insert(0, item);
                }
            }
            if (recipe.purge != null && recipe.purge.Count > 0)
            {
                // purgeDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.purge)
                {
                    purgeDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.purge_extend != null && recipe.purge_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.purge_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(purgeDataGridView, kvp.Key, kvp.Value);
                    purgeDataGridView.Rows.Add(row);
                }
            }
            if (recipe.purge_remove != null && recipe.purge_remove.Count > 0)
            {
                foreach (string removeId in recipe.purge_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(purgeDataGridView, removeId);
                    purgeDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deleteverb != null && recipe.deleteverb.Count > 0)
            {
                // deleteVerbDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.deleteverb)
                {
                    deleteVerbDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.deleteverb_extend != null && recipe.deleteverb_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.deleteverb_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(deleteVerbDataGridView, kvp.Key, kvp.Value);
                    deleteVerbDataGridView.Rows.Add(row);
                }
            }
            if (recipe.deleteverb_remove != null && recipe.deleteverb_remove.Count > 0)
            {
                foreach (string removeId in recipe.deleteverb_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(deleteVerbDataGridView, removeId);
                    deleteVerbDataGridView.Rows.Add(row);
                }
            }
            if (recipe.haltverb != null && recipe.haltverb.Count > 0)
            {
                // haltVerbDataGridView.Rows.Clear();
                foreach (KeyValuePair<string, int> kvp in recipe.haltverb)
                {
                    haltVerbDataGridView.Rows.Add(kvp.Key, kvp.Value);
                }
            }
            if (recipe.haltverb_extend != null && recipe.haltverb_extend.Count > 0)
            {
                foreach (KeyValuePair<string, int> kvp in recipe.haltverb_extend)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryExtendStyle
                    };
                    row.CreateCells(haltVerbDataGridView, kvp.Key, kvp.Value);
                    haltVerbDataGridView.Rows.Add(row);
                }
            }
            if (recipe.haltverb_remove != null && recipe.haltverb_remove.Count > 0)
            {
                foreach (string removeId in recipe.haltverb_remove)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        DefaultCellStyle = Utilities.DictionaryRemoveStyle
                    };
                    row.CreateCells(haltVerbDataGridView, removeId);
                    haltVerbDataGridView.Rows.Add(row);
                }
            }
            if (recipe.extends?.Count > 1)
            {
                extendsTextBox.Text = string.Join(",", recipe.extends);
            }
            else if (recipe.extends?.Count == 1)
            {
                extendsTextBox.Text = recipe.extends[0];
            }
        }

        private void ShowInternalDeckButton_Click(object sender, EventArgs e)
        {
            if (displayedRecipe.internaldeck == null && editing)
            {
                DeckViewer dv = new DeckViewer(new Deck(), InternalDeck_Assign, true, null);
                dv.Show();
            }
            else if (displayedRecipe.internaldeck is Deck deck)
            {
                DeckViewer dv = new DeckViewer(deck.Copy(), editing ? InternalDeck_Assign : (EventHandler<Deck>)null, true, null);
                dv.Show();
            }
        }

        private void InternalDeck_Assign(object sender, Deck result)
        {
            if (result is Deck)
            {
                displayedRecipe.internaldeck = result.Copy();
                showInternalDeckButton.Text = "Show Deck";
            }
            else
            {
                displayedRecipe.internaldeck = null;
                showInternalDeckButton.Text = "New Deck";
            }
        }

        private void ShowSlotButton_Click(object sender, EventArgs e)
        {
            if ((displayedRecipe.slots == null || displayedRecipe.slots.Count == 0) && editing)
            {
                SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.RECIPE);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot.Copy() };
                    showSlotButton.Text = "Show Slot";
                }
            }
            else if (displayedRecipe.slots?.Count == 1)
            {
                SlotViewer sv = new SlotViewer(displayedRecipe.slots[0].Copy(), editing, SlotType.RECIPE);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    displayedRecipe.slots = new List<Slot> { sv.displayedSlot.Copy() };
                }
            }
        }

        private void AlternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count == 0) return;
            Guid guid = (Guid)alternativeRecipesListView.SelectedItems[0].Tag;
            RecipeLink oldLink = alternativerecipeLinks[guid];
            RecipeLinkViewer rlv = new RecipeLinkViewer(oldLink.Copy(), editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                alternativerecipeLinks.Remove(guid);
                alternativerecipeLinks.Add(guid, rlv.displayedRecipeLink.Copy());
                if (alternativeRecipesListView.SelectedItems[0].Text != rlv.displayedRecipeLink.id)
                {
                    alternativeRecipesListView.SelectedItems[0].Text = rlv.displayedRecipeLink.id;
                }
                SaveAlternativeRecipes();
                // displayedRecipe.alternativerecipes[alternativeRecipesListView.SelectedIndices[0]] = rlv.displayedRecipeLink;
            }
        }

        private void LinkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0) return;
            Guid guid = (Guid)linkedRecipesListView.SelectedItems[0].Tag;
            RecipeLink oldLink = recipeLinks[guid];
            RecipeLinkViewer rlv = new RecipeLinkViewer(oldLink.Copy(), editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                recipeLinks.Remove(guid);
                recipeLinks.Add(guid, rlv.displayedRecipeLink.Copy());
                if (linkedRecipesListView.SelectedItems[0].Text != rlv.displayedRecipeLink.id)
                {
                    linkedRecipesListView.SelectedItems[0].Text = rlv.displayedRecipeLink.id;
                }
                SaveLinkedRecipes();
            }
        }

        private void MutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count == 0) return;
            Guid guid = (Guid)mutationsListView.SelectedItems[0].Tag;
            string oldId = mutationsListView.SelectedItems[0].Text;
            MutationViewer mv = new MutationViewer(mutations[guid], editing);
            mv.ShowDialog();
            if (mv.DialogResult == DialogResult.OK)
            {
                mutations.Remove(guid);
                mutations.Add(guid, mv.displayedMutation.Copy());
                if (oldId != mv.displayedMutation.mutate)
                {
                    mutationsListView.SelectedItems[0].Text = mv.displayedMutation.mutate;
                }
                SaveMutations();
                // displayedRecipe.mutations[mutationsListView.SelectedIndices[0]] = mv.displayedMutation;
            }
        }

        private void RequirementsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects OR elements, don't allow editing these from a recipe
            if (!(requirementsDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void ExtantreqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can also be aspects or elements, don't allow editing these from a recipe
            if (!(extantreqsDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void TablereqsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be elements or aspects, don't allow editing these from a recipe
            if (!(tablereqsDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void EffectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be elements or aspects, don't allow editing these from a recipe
            if (!(effectsDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
            else if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
        }

        private void AspectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects, don't allow editing these from a recipe
            if (!(aspectsDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
            else if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
        }

        private void DeckeffectDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can only be decks, don't allow editing these from a recipe
            if (!(deckeffectDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.DeckExists(id))
            {
                DeckViewer dv = new DeckViewer(Utilities.GetDeck(id), null, null);
                dv.Show();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text == null || idTextBox.Text == "")
            {
                MessageBox.Show("All Recipes must have an ID.");
                return;
            }
            if (actionIdTextBox.Text == null || actionIdTextBox.Text == "")
            {
                MessageBox.Show("All Recipes must have an Action/Verb ID unless they're extending a recipe with one.");
                // return;
            }
            if (requirementsDataGridView.RowCount > 1)
            {
                displayedRecipe.requirements = null;
                displayedRecipe.requirements_extend = null;
                displayedRecipe.requirements_remove = null;
                foreach (DataGridViewRow row in requirementsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.requirements_extend == null) displayedRecipe.requirements_extend = new Dictionary<string, string>();
                        displayedRecipe.requirements_extend[key] = value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.requirements_remove == null) displayedRecipe.requirements_remove = new List<string>();
                        if (!displayedRecipe.requirements_remove.Contains(key)) displayedRecipe.requirements_remove.Add(key);
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.requirements == null) displayedRecipe.requirements = new Dictionary<string, string>();
                        displayedRecipe.requirements[key] = value;
                    }
                }
            }
            if (extantreqsDataGridView.RowCount > 1)
            {
                displayedRecipe.extantreqs = null;
                displayedRecipe.extantreqs_extend = null;
                displayedRecipe.extantreqs_remove = null;
                foreach (DataGridViewRow row in extantreqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.extantreqs_extend == null) displayedRecipe.extantreqs_extend = new Dictionary<string, string>();
                        displayedRecipe.extantreqs_extend[key] = value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.extantreqs_remove == null) displayedRecipe.extantreqs_remove = new List<string>();
                        if (!displayedRecipe.extantreqs_remove.Contains(key)) displayedRecipe.extantreqs_remove.Add(key);
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.extantreqs == null) displayedRecipe.extantreqs = new Dictionary<string, string>();
                        displayedRecipe.extantreqs[key] = value;
                    }
                }
            }
            if (tablereqsDataGridView.RowCount > 1)
            {
                displayedRecipe.tablereqs = null;
                displayedRecipe.tablereqs_extend = null;
                displayedRecipe.tablereqs_remove = null;
                foreach (DataGridViewRow row in tablereqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.tablereqs_extend == null) displayedRecipe.tablereqs_extend = new Dictionary<string, string>();
                        displayedRecipe.tablereqs_extend[key] = value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.tablereqs_remove == null) displayedRecipe.tablereqs_remove = new List<string>();
                        if (!displayedRecipe.tablereqs_remove.Contains(key)) displayedRecipe.tablereqs_remove.Add(key);
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.tablereqs == null) displayedRecipe.tablereqs = new Dictionary<string, string>();
                        displayedRecipe.tablereqs[key] = value;
                    }
                }
            }
            if (effectsDataGridView.RowCount > 1)
            {
                displayedRecipe.effects = null;
                displayedRecipe.effects_extend = null;
                displayedRecipe.effects_remove = null;
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.effects_extend == null) displayedRecipe.effects_extend = new Dictionary<string, string>();
                        displayedRecipe.effects_extend[key] = value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.effects_remove == null) displayedRecipe.effects_remove = new List<string>();
                        if (!displayedRecipe.effects_remove.Contains(key)) displayedRecipe.effects_remove.Add(key);
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (displayedRecipe.effects == null) displayedRecipe.effects = new Dictionary<string, string>();
                        displayedRecipe.effects[key] = value;
                    }
                }
            }
            if (aspectsDataGridView.RowCount > 1)
            {
                displayedRecipe.aspects = null;
                displayedRecipe.aspects_extend = null;
                displayedRecipe.aspects_remove = null;
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
                displayedRecipe.deckeffects = null;
                displayedRecipe.deckeffects_extend = null;
                displayedRecipe.deckeffects_remove = null;
                foreach (DataGridViewRow row in deckeffectDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.deckeffects_extend == null) displayedRecipe.deckeffects_extend = new Dictionary<string, int>();
                        displayedRecipe.deckeffects_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.deckeffects_remove == null) displayedRecipe.deckeffects_remove = new List<string>();
                        if (!displayedRecipe.deckeffects_remove.Contains(key)) displayedRecipe.deckeffects_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.deckeffects == null) displayedRecipe.deckeffects = new Dictionary<string, int>();
                        displayedRecipe.deckeffects[key] = value.Value;
                    }
                }
            }
            if (purgeDataGridView.RowCount > 1)
            {
                displayedRecipe.purge = null;
                displayedRecipe.purge_extend = null;
                displayedRecipe.purge_remove = null;
                foreach (DataGridViewRow row in purgeDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.purge_extend == null) displayedRecipe.purge_extend = new Dictionary<string, int>();
                        displayedRecipe.purge_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.purge_remove == null) displayedRecipe.purge_remove = new List<string>();
                        if (!displayedRecipe.purge_remove.Contains(key)) displayedRecipe.purge_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.purge == null) displayedRecipe.purge = new Dictionary<string, int>();
                        displayedRecipe.purge[key] = value.Value;
                    }
                }
            }
            if (haltVerbDataGridView.RowCount > 1)
            {
                displayedRecipe.haltverb = null;
                displayedRecipe.haltverb_extend = null;
                displayedRecipe.haltverb_remove = null;
                foreach (DataGridViewRow row in haltVerbDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.haltverb_extend == null) displayedRecipe.haltverb_extend = new Dictionary<string, int>();
                        displayedRecipe.haltverb_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.haltverb_remove == null) displayedRecipe.haltverb_remove = new List<string>();
                        if (!displayedRecipe.haltverb_remove.Contains(key)) displayedRecipe.haltverb_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.haltverb == null) displayedRecipe.haltverb = new Dictionary<string, int>();
                        displayedRecipe.haltverb[key] = value.Value;
                    }
                }
            }
            if (deleteVerbDataGridView.RowCount > 1)
            {
                displayedRecipe.deleteverb = null;
                displayedRecipe.deleteverb_extend = null;
                displayedRecipe.deleteverb_remove = null;
                foreach (DataGridViewRow row in deleteVerbDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null) continue;
                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
                    {
                        if (displayedRecipe.deleteverb_extend == null) displayedRecipe.deleteverb_extend = new Dictionary<string, int>();
                        displayedRecipe.deleteverb_extend[key] = value.Value;
                    }
                    else if (row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
                    {
                        if (displayedRecipe.deleteverb_remove == null) displayedRecipe.deleteverb_remove = new List<string>();
                        if (!displayedRecipe.deleteverb_remove.Contains(key)) displayedRecipe.deleteverb_remove.Add(key);
                    }
                    else
                    {
                        if (displayedRecipe.deleteverb == null) displayedRecipe.deleteverb = new Dictionary<string, int>();
                        displayedRecipe.deleteverb[key] = value.Value;
                    }
                }
            }
            SaveAlternativeRecipes();
            SaveLinkedRecipes();
            SaveMutations();
            Close();
            SuccessCallback?.Invoke(this, displayedRecipe);
        }

        private void AddAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    alternativeRecipesListView.Items.Add(new ListViewItem(rlv.displayedRecipeLink.id) { Tag = newGuid });
                    alternativerecipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    //if (displayedRecipe.alternativerecipes != null) displayedRecipe.alternativerecipes.Add(rlv.displayedRecipeLink);
                    //else displayedRecipe.alternativerecipes = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveAlternativeRecipes();
                }
            }
        }
        
        private void PrependAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    alternativeRecipesListView.Items.Insert(0, item);
                    alternativerecipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    // if (displayedRecipe.alternativerecipes_prepend != null) displayedRecipe.alternativerecipes_prepend.Add(rlv.displayedRecipeLink);
                    // else displayedRecipe.alternativerecipes_prepend = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveAlternativeRecipes();
                }
            }
        }

        private void AppendAlternativeReipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = newGuid
                    };
                    alternativeRecipesListView.Items.Add(item);
                    alternativerecipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    // if (displayedRecipe.alternativerecipes_append != null) displayedRecipe.alternativerecipes_append.Add(rlv.displayedRecipeLink);
                    // else displayedRecipe.alternativerecipes_append = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveAlternativeRecipes();
                }
            }
        }

        private void AddLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.LINKED))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    linkedRecipesListView.Items.Add(new ListViewItem(rlv.displayedRecipeLink.id) { Tag = newGuid });
                    recipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    // if (displayedRecipe.linked != null) displayedRecipe.linked.Add(rlv.displayedRecipeLink);
                    // else displayedRecipe.linked = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveLinkedRecipes();
                }
            }
        }

        private void PrependLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.LINKED))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    linkedRecipesListView.Items.Insert(0, item);
                    recipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    // if (displayedRecipe.linked_prepend != null) displayedRecipe.linked_prepend.Add(rlv.displayedRecipeLink);
                    // else displayedRecipe.linked_prepend = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveLinkedRecipes();
                }
            }
        }

        private void AppendLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.LINKED))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.displayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add(newGuid, rlv.displayedRecipeLink);
                    // if (displayedRecipe.linked_append != null) displayedRecipe.linked_append.Add(rlv.displayedRecipeLink);
                    // else displayedRecipe.linked_append = new List<RecipeLink> { rlv.displayedRecipeLink };
                    SaveLinkedRecipes();
                }
            }
        }

        private void AddMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if (mv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(mv.displayedMutation.mutate)
                    {
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.displayedMutation);
                    // if (displayedRecipe.mutations != null) displayedRecipe.mutations.Add(mv.displayedMutation);
                    // else displayedRecipe.mutations = new List<Mutation> { mv.displayedMutation };
                    SaveMutations();
                }
            }
        }

        private void PrependMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if (mv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(mv.displayedMutation.mutate)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.displayedMutation);
                    // if (displayedRecipe.mutations_prepend != null) displayedRecipe.mutations_prepend.Add(mv.displayedMutation);
                    // else displayedRecipe.mutations_prepend = new List<Mutation> { mv.displayedMutation };
                    SaveMutations();
                }
            }
        }

        private void AppendMutationButton_Click(object sender, EventArgs e)
        {
            using (MutationViewer mv = new MutationViewer(new Mutation(), true))
            {
                mv.ShowDialog();
                if (mv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(mv.displayedMutation.mutate)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.displayedMutation);
                    // if (displayedRecipe.mutations_append != null) displayedRecipe.mutations_append.Add(mv.displayedMutation);
                    // else displayedRecipe.mutations_append = new List<Mutation> { mv.displayedMutation };
                    SaveMutations();
                }
            }
        }

        private void MaxExecutionsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipe.maxexecutions = Convert.ToInt32(maxExecutionsNumericUpDown.Value);
            if (maxExecutionsNumericUpDown.Value == 0) displayedRecipe.maxexecutions = null;
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (idTextBox.Text == "") displayedRecipe.id = null;
            else displayedRecipe.id = idTextBox.Text;
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (labelTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.label = labelTextBox.Text;
        }

        private void ActionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (actionIdTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.actionId = actionIdTextBox.Text;
        }

        private void EndingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (endingTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.ending = endingTextBox.Text;
        }

        private void BurnimageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (burnimageTextBox.Text == "") displayedRecipe.label = null;
            else displayedRecipe.burnimage = burnimageTextBox.Text;
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void WarmupNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            displayedRecipe.warmup = Convert.ToInt32(warmupNumericUpDown.Value);
            if (displayedRecipe.warmup == 0)
            {
                displayedRecipe.warmup = null;
            }
        }

        private void StartdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.startdescription = startdescriptionTextBox.Text;
            if (displayedRecipe.startdescription == "")
            {
                displayedRecipe.startdescription = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.description = descriptionTextBox.Text;
            if (displayedRecipe.description == "")
            {
                displayedRecipe.description = null;
            }
        }

        private void RequirementsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void ExtantreqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void TablereqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void EffectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void AspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
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

        private void DeckeffectDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.deckeffects_extend == null) return;
                if (displayedRecipe.deckeffects_extend.ContainsKey(key)) displayedRecipe.deckeffects_extend.Remove(key);
                if (displayedRecipe.deckeffects_extend.Count == 0) displayedRecipe.deckeffects_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.deckeffects_remove == null) return;
                if (displayedRecipe.deckeffects_remove.Contains(key)) displayedRecipe.deckeffects_remove.Remove(key);
                if (displayedRecipe.deckeffects_remove.Count == 0) displayedRecipe.deckeffects_remove = null;
            }
            else
            {
                if (displayedRecipe.deckeffects == null) return;
                if (displayedRecipe.deckeffects.ContainsKey(key)) displayedRecipe.deckeffects.Remove(key);
                if (displayedRecipe.deckeffects.Count == 0) displayedRecipe.deckeffects = null;
            }
        }

        private void RemoveAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count > 0)
            {   // need to remove it from the right location now that there's 4 of them
                ListViewItem item = alternativeRecipesListView.SelectedItems[0];
                Guid guid = (Guid)alternativeRecipesListView.SelectedItems[0].Tag;
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    // displayedRecipe.alternativerecipes_prepend.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    // displayedRecipe.alternativerecipes_append.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    // displayedRecipe.alternativerecipes_remove.Remove(value);
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else
                {
                    // displayedRecipe.alternativerecipes.Remove(alternativerecipeLinks[value]);
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                SaveAlternativeRecipes();
            }
        }

        private void RemoveLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count > 0)
            {
                ListViewItem item = linkedRecipesListView.SelectedItems[0];
                Guid guid = (Guid)linkedRecipesListView.SelectedItems[0].Tag;
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    // displayedRecipe.linked_prepend.Remove(recipeLinks[guid]);
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_prepend.Count == 0) displayedRecipe.linked_prepend = null;
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    // displayedRecipe.linked_append.Remove(recipeLinks[guid]);
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_append.Count == 0) displayedRecipe.linked_append = null;
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    // displayedRecipe.linked_remove.Remove(guid);
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked_remove.Count == 0) displayedRecipe.linked_remove = null;
                }
                else
                {
                    // displayedRecipe.linked.Remove(recipeLinks[guid]);
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                    if (displayedRecipe.linked.Count == 0) displayedRecipe.linked = null;
                }
                SaveLinkedRecipes();
            }
        }

        private void RemoveMutationButton_Click(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count > 0)
            {
                ListViewItem item = mutationsListView.SelectedItems[0];
                Guid guid = (Guid)mutationsListView.SelectedItems[0].Tag;
                if (item.BackColor == Utilities.ListPrependColor)
                {
                    // displayedRecipe.mutations_prepend.Remove(mutations[hashcode]);
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_prepend.Count == 0) displayedRecipe.mutations_prepend = null;
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    // displayedRecipe.mutations_append.Remove(mutations[hashcode]);
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_append.Count == 0) displayedRecipe.mutations_append = null;
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    // displayedRecipe.mutations_remove.Remove(hashcode);
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations_remove.Count == 0) displayedRecipe.mutations_remove = null;
                }
                else
                {
                    // displayedRecipe.mutations.Remove(mutations[hashcode]);
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                    if (displayedRecipe.mutations.Count == 0) displayedRecipe.mutations = null;
                }
                SaveMutations();
            }
        }

        private void SetAsExtendToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void SetAsRemoveToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void MoveAltRecipeUpButton_Click(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count == 0) return;
            // check to see if first item is selected
            if (alternativeRecipesListView.SelectedIndices[0] == 0) return;

            int selectedIndex = alternativeRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex--;
            ListViewItem selectedItem = alternativeRecipesListView.SelectedItems[0];
            alternativeRecipesListView.Items.Remove(selectedItem);
            alternativeRecipesListView.Items.Insert(affectedIndex-1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveAltRecipeDownButton_Click(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count == 0) return;
            // check to see if last item is selected
            if (alternativeRecipesListView.SelectedIndices[0] == alternativeRecipesListView.Items.Count-1) return;

            int selectedIndex = alternativeRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = alternativeRecipesListView.SelectedItems[0];
            alternativeRecipesListView.Items.Remove(selectedItem);
            alternativeRecipesListView.Items.Insert(affectedIndex+1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveLinkedRecipeUpButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0) return;
            // check to see if first item is selected
            if (linkedRecipesListView.SelectedIndices[0] == 0) return;

            int selectedIndex = linkedRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex--;
            ListViewItem selectedItem = linkedRecipesListView.SelectedItems[0];
            linkedRecipesListView.Items.Remove(selectedItem);
            linkedRecipesListView.Items.Insert(affectedIndex-1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveLinkedRecipeDownButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0) return;
            // check to see if last item is selected
            if (linkedRecipesListView.SelectedIndices[0] == linkedRecipesListView.Items.Count - 1) return;

            int selectedIndex = linkedRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = linkedRecipesListView.SelectedItems[0];
            linkedRecipesListView.Items.Remove(selectedItem);
            linkedRecipesListView.Items.Insert(affectedIndex+1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void SaveAlternativeRecipes()
        {
            displayedRecipe.alt = null;
            displayedRecipe.alt_prepend = null;
            displayedRecipe.alt_append = null;
            displayedRecipe.alt_remove = null;
            foreach (ListViewItem item in alternativeRecipesListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (displayedRecipe.alt_append == null) displayedRecipe.alt_append = new List<RecipeLink>();
                    displayedRecipe.alt_append.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (displayedRecipe.alt_prepend == null) displayedRecipe.alt_prepend = new List<RecipeLink>();
                    displayedRecipe.alt_prepend.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (displayedRecipe.alt_remove == null) displayedRecipe.alt_remove = new List<string>();
                    displayedRecipe.alt_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (displayedRecipe.alt == null) displayedRecipe.alt = new List<RecipeLink>();
                    displayedRecipe.alt.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
            }
        }

        private void SaveLinkedRecipes()
        {
            displayedRecipe.linked = null;
            displayedRecipe.linked_prepend = null;
            displayedRecipe.linked_append = null;
            displayedRecipe.linked_remove = null;
            foreach (ListViewItem item in linkedRecipesListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (displayedRecipe.linked_append == null) displayedRecipe.linked_append = new List<RecipeLink>();
                    displayedRecipe.linked_append.Add(recipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (displayedRecipe.linked_prepend == null) displayedRecipe.linked_prepend = new List<RecipeLink>();
                    displayedRecipe.linked_prepend.Add(recipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (displayedRecipe.linked_remove == null) displayedRecipe.linked_remove = new List<string>();
                    displayedRecipe.linked_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (displayedRecipe.linked == null) displayedRecipe.linked = new List<RecipeLink>();
                    displayedRecipe.linked.Add(recipeLinks[(Guid)item.Tag]);
                }
            }
        }

        private void PurgeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(purgeDataGridView.SelectedCells[0].Value is string id)) return;
            if (Utilities.AspectExists(id))
            {
                AspectViewer av = new AspectViewer(Utilities.GetAspect(id), null, null);
                av.Show();
            }
            else if (Utilities.ElementExists(id))
            {
                ElementViewer ev = new ElementViewer(Utilities.GetElement(id), null, null);
                ev.Show();
            }
        }

        private void HaltVerbDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(haltVerbDataGridView.Rows[e.RowIndex].Cells[0].Value is string id)) return;
            if (Utilities.VerbExists(id))
            {
                VerbViewer vv = new VerbViewer(Utilities.GetVerb(id), null, null);
                vv.Show();
            }
        }

        private void DeleteVerbDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(deleteVerbDataGridView.Rows[e.RowIndex].Cells[0].Value is string id)) return;
            if (Utilities.VerbExists(id))
            {
                VerbViewer vv = new VerbViewer(Utilities.GetVerb(id), null, null);
                vv.Show();
            }
        }

        private void DeleteVerbDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.deleteverb_extend == null) return;
                if (displayedRecipe.deleteverb_extend.ContainsKey(key)) displayedRecipe.deleteverb_extend.Remove(key);
                if (displayedRecipe.deleteverb_extend.Count == 0) displayedRecipe.deleteverb_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.deleteverb_remove == null) return;
                if (displayedRecipe.deleteverb_remove.Contains(key)) displayedRecipe.deleteverb_remove.Remove(key);
                if (displayedRecipe.deleteverb_remove.Count == 0) displayedRecipe.deleteverb_remove = null;
            }
            else
            {
                if (displayedRecipe.deleteverb == null) return;
                if (displayedRecipe.deleteverb.ContainsKey(key)) displayedRecipe.deleteverb.Remove(key);
                if (displayedRecipe.deleteverb.Count == 0) displayedRecipe.deleteverb = null;
            }
        }

        private void HaltVerbDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.haltverb_extend == null) return;
                if (displayedRecipe.haltverb_extend.ContainsKey(key)) displayedRecipe.haltverb_extend.Remove(key);
                if (displayedRecipe.haltverb_extend.Count == 0) displayedRecipe.haltverb_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.haltverb_remove == null) return;
                if (displayedRecipe.haltverb_remove.Contains(key)) displayedRecipe.haltverb_remove.Remove(key);
                if (displayedRecipe.haltverb_remove.Count == 0) displayedRecipe.haltverb_remove = null;
            }
            else
            {
                if (displayedRecipe.haltverb == null) return;
                if (displayedRecipe.haltverb.ContainsKey(key)) displayedRecipe.haltverb.Remove(key);
                if (displayedRecipe.haltverb.Count == 0) displayedRecipe.haltverb = null;
            }
        }

        private void PurgeDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (e.Row.DefaultCellStyle == Utilities.DictionaryExtendStyle)
            {

                if (displayedRecipe.purge_extend == null) return;
                if (displayedRecipe.purge_extend.ContainsKey(key)) displayedRecipe.purge_extend.Remove(key);
                if (displayedRecipe.purge_extend.Count == 0) displayedRecipe.purge_extend = null;
            }
            else if (e.Row.DefaultCellStyle == Utilities.DictionaryRemoveStyle)
            {

                if (displayedRecipe.purge_remove == null) return;
                if (displayedRecipe.purge_remove.Contains(key)) displayedRecipe.purge_remove.Remove(key);
                if (displayedRecipe.purge_remove.Count == 0) displayedRecipe.purge_remove = null;
            }
            else
            {
                if (displayedRecipe.purge == null) return;
                if (displayedRecipe.purge.ContainsKey(key)) displayedRecipe.purge.Remove(key);
                if (displayedRecipe.purge.Count == 0) displayedRecipe.purge = null;
            }
        }

        private void MoveMutationUpButton_Click(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count == 0) return;
            // check to see if first item is selected
            if (mutationsListView.SelectedIndices[0] == 0) return;

            int selectedIndex = mutationsListView.SelectedIndices[0];
            int affectedIndex = selectedIndex--;
            ListViewItem selectedItem = mutationsListView.SelectedItems[0];
            mutationsListView.Items.Remove(selectedItem);
            mutationsListView.Items.Insert(affectedIndex - 1, selectedItem);
            // TODO refactor mutation storage/handling so I can save it like linked and alternate recipes
            SaveMutations();
        }

        private void MoveMutationDownButton_Click(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count == 0) return;
            // check to see if last item is selected
            if (mutationsListView.SelectedIndices[0] == mutationsListView.Items.Count - 1) return;

            int selectedIndex = mutationsListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = mutationsListView.SelectedItems[0];
            mutationsListView.Items.Remove(selectedItem);
            mutationsListView.Items.Insert(affectedIndex + 1, selectedItem);
            SaveMutations();
        }

        private void SaveMutations()
        {
            displayedRecipe.mutations = null;
            displayedRecipe.mutations_prepend = null;
            displayedRecipe.mutations_append = null;
            displayedRecipe.mutations_remove = null;
            foreach (ListViewItem item in mutationsListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (displayedRecipe.mutations_append == null) displayedRecipe.mutations_append = new List<Mutation>();
                    displayedRecipe.mutations_append.Add(mutations[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (displayedRecipe.mutations_prepend == null) displayedRecipe.mutations_prepend = new List<Mutation>();
                    displayedRecipe.mutations_prepend.Add(mutations[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (displayedRecipe.mutations_remove == null) displayedRecipe.mutations_remove = new List<string>();
                    displayedRecipe.mutations_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (displayedRecipe.mutations == null) displayedRecipe.mutations = new List<Mutation>();
                    displayedRecipe.mutations.Add(mutations[(Guid)item.Tag]);
                }
            }
        }

        private void SignalEndingFlavourDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            displayedRecipe.signalendingflavour = signalEndingFlavourDomainUpDown.Text;
            if (displayedRecipe.signalendingflavour == "")
            {
                displayedRecipe.signalendingflavour = null;
            }
        }

        private void PortalEffectDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            displayedRecipe.portaleffect = portalEffectDomainUpDown.Text;
            if (displayedRecipe.portaleffect == "")
            {
                displayedRecipe.portaleffect = null;
            }
        }

        private void CraftableCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (craftableCheckBox.CheckState == CheckState.Checked) displayedRecipe.craftable = true;
            if (craftableCheckBox.CheckState == CheckState.Unchecked) displayedRecipe.craftable = false;
            if (craftableCheckBox.CheckState == CheckState.Indeterminate) displayedRecipe.craftable = null;
        }

        private void HintonlyCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (hintonlyCheckBox.CheckState == CheckState.Checked) displayedRecipe.hintonly = true;
            if (hintonlyCheckBox.CheckState == CheckState.Unchecked) displayedRecipe.hintonly = false;
            if (hintonlyCheckBox.CheckState == CheckState.Indeterminate) displayedRecipe.hintonly = null;
        }

        private void SignalImportantLoopCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (signalImportantLoopCheckBox.CheckState == CheckState.Checked) displayedRecipe.signalimportantloop = true;
            if (signalImportantLoopCheckBox.CheckState == CheckState.Unchecked) displayedRecipe.signalimportantloop = false;
            if (signalImportantLoopCheckBox.CheckState == CheckState.Indeterminate) displayedRecipe.signalimportantloop = null;
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            displayedRecipe.comments = commentsTextBox.Text;
            if (displayedRecipe.comments == "")
            {
                displayedRecipe.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked) displayedRecipe.deleted = true;
            if (deletedCheckBox.CheckState == CheckState.Unchecked) displayedRecipe.deleted = false;
            if (deletedCheckBox.CheckState == CheckState.Indeterminate) displayedRecipe.deleted = null;
        }

        private void ExtendsTextBox_TextChanged_1(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                displayedRecipe.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                if (extendsTextBox.Text != "") displayedRecipe.extends = new List<string> { extendsTextBox.Text };
                else displayedRecipe.extends = null;
            }
        }
    }
}
