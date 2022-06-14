using CarcassSpark.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public enum RecipeType
    {
        Default,
        Nested,
        Generator
    }

    public partial class RecipeViewer : Form, IGameObjectViewer
    {
        public Recipe DisplayedRecipe;
        private bool editing;

        private event EventHandler<Recipe> SuccessCallback;
        public ListViewItem associatedListViewItem;
        private readonly Dictionary<Guid, RecipeLink> recipeLinks = new Dictionary<Guid, RecipeLink>();
        private readonly Dictionary<Guid, RecipeLink> alternativerecipeLinks = new Dictionary<Guid, RecipeLink>();
        private readonly Dictionary<Guid, Mutation> mutations = new Dictionary<Guid, Mutation>();

        public ListViewItem AssociatedListViewItem { get => associatedListViewItem; set => associatedListViewItem=value; }

        public RecipeViewer(Recipe recipe, EventHandler<Recipe> successCallback, ListViewItem item)
        {
            InitializeComponent();
            DisplayedRecipe = recipe;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
        }

        public RecipeViewer(Recipe recipe, EventHandler<Recipe> successCallback, RecipeType recipeViewerType, ListViewItem item)
        {
            InitializeComponent();
            DisplayedRecipe = recipe;
            associatedListViewItem = item;
            if (successCallback != null)
            {
                SetEditingMode(true);
                this.SuccessCallback += successCallback;
            }
            else
            {
                SetEditingMode(false);
            }
            SetViewerType(recipeViewerType);
        }

        private void SetEditingMode(bool editing)
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

        private void SetViewerType(RecipeType recipeViewerType)
        {
            switch (recipeViewerType)
            {
                case RecipeType.Generator:
                    idLabel.ForeColor = Color.Red;
                    labelLabel.ForeColor = Color.Red;
                    startDescriptionLabel.ForeColor = Color.Red;
                    descriptionLabel.ForeColor = Color.Red;
                    requirementsLabel.ForeColor = Color.Red;
                    break;
                case RecipeType.Nested:
                    break;
                case RecipeType.Default:
                    break;
            }
        }

        private void FillValues(Recipe recipe)
        {
            if (recipe.ID != null)
            {
                idTextBox.Text = recipe.ID;
            }

            if (recipe.label != null)
            {
                labelTextBox.Text = recipe.label;
            }

            if (recipe.actionId != null)
            {
                actionIdTextBox.Text = recipe.actionId;
            }

            if (recipe.ending != null)
            {
                endingTextBox.Text = recipe.ending;
            }

            if (recipe.burnimage != null)
            {
                burnimageTextBox.Text = recipe.burnimage;
            }

            if (recipe.craftable.HasValue)
            {
                craftableCheckBox.Checked = recipe.craftable.Value;
            }

            if (recipe.hintonly.HasValue)
            {
                hintonlyCheckBox.Checked = recipe.hintonly.Value;
            }

            if (recipe.signalimportantloop.HasValue)
            {
                signalImportantLoopCheckBox.Checked = recipe.signalimportantloop.Value;
            }

            if (recipe.startdescription != null)
            {
                startdescriptionTextBox.Text = recipe.startdescription;
            }

            if (recipe.description != null)
            {
                descriptionTextBox.Text = recipe.description;
            }

            if (recipe.comments != null)
            {
                commentsTextBox.Text = recipe.comments;
            }

            if (recipe.portaleffect != null)
            {
                portalEffectDomainUpDown.Text = recipe.portaleffect;
            }

            if (recipe.signalendingflavour != null)
            {
                signalEndingFlavourDomainUpDown.Text = recipe.signalendingflavour;
            }

            if (recipe.maxexecutions.HasValue)
            {
                maxExecutionsNumericUpDown.Value = recipe.maxexecutions.Value;
            }

            if (recipe.warmup.HasValue)
            {
                warmupNumericUpDown.Value = recipe.warmup.Value;
            }

            showInternalDeckButton.Text = recipe.internaldeck != null ? "Show Deck" : "New Deck";
            showSlotButton.Text = recipe.slots?.Count > 0 ? "Show Slot" : "New Slot";
            if (recipe.deleted.HasValue)
            {
                deletedCheckBox.Checked = recipe.deleted.Value;
            }

            if (recipe.requirements != null && recipe.requirements.Count > 0)
            {
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
            if (recipe.alt_prepend != null && recipe.alt_prepend.Count > 0)
            {
                foreach (RecipeLink rl in recipe.alt_prepend)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = Guid.NewGuid()
                    };
                    alternativeRecipesListView.Items.Add(item);
                    alternativerecipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.alt != null && recipe.alt.Count > 0)
            {
                foreach (RecipeLink rl in recipe.alt)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        Tag = Guid.NewGuid()
                    };
                    alternativeRecipesListView.Items.Add(item);
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
                    alternativeRecipesListView.Items.Add(item);
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
                    alternativeRecipesListView.Items.Add(item);
                }
            }
            if (recipe.linked_prepend != null && recipe.linked_prepend.Count > 0)
            {
                foreach (RecipeLink rl in recipe.linked_prepend)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = Guid.NewGuid()
                    };
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.linked != null && recipe.linked.Count > 0)
            {
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
            if (recipe.linked_append != null && recipe.linked_append.Count > 0)
            {
                foreach (RecipeLink rl in recipe.linked_append)
                {
                    ListViewItem item = new ListViewItem(rl.id)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = Guid.NewGuid()
                    };
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add((Guid)item.Tag, rl);
                }
            }
            if (recipe.linked_remove != null && recipe.linked_remove.Count > 0)
            {
                foreach (string rl in recipe.linked_remove)
                {
                    ListViewItem item = new ListViewItem(rl)
                    {
                        BackColor = Utilities.ListRemoveColor,
                        Tag = Guid.NewGuid()
                    };
                    recipeLinks.Add((Guid)item.Tag, new RecipeLink(rl));
                    linkedRecipesListView.Items.Add(item);
                }
            }
            if (recipe.mutations_prepend != null && recipe.mutations_prepend.Count > 0)
            {
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
            if (recipe.mutations != null && recipe.mutations.Count > 0)
            {
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
            if (recipe.mutations_append != null && recipe.mutations_append.Count > 0)
            {
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
                    ListViewItem item = new ListViewItem(mutation)
                    {
                        BackColor = Utilities.ListRemoveColor,
                        Tag = Guid.NewGuid()
                    };
                    mutations.Add((Guid)item.Tag, new Mutation(mutation));
                    mutationsListView.Items.Add(item);
                }
            }
            if (recipe.purge != null && recipe.purge.Count > 0)
            {
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
            if (DisplayedRecipe.internaldeck == null && editing)
            {
                DeckViewer dv = new DeckViewer(new Deck(), InternalDeck_Assign, true, null);
                dv.Show();
            }
            else if (DisplayedRecipe.internaldeck is Deck deck)
            {
                DeckViewer dv = new DeckViewer(deck.Copy(), editing ? InternalDeck_Assign : (EventHandler<Deck>)null, true, null);
                dv.Show();
            }
        }

        private void InternalDeck_Assign(object sender, Deck result)
        {
            if (result != null)
            {
                DisplayedRecipe.internaldeck = result.Copy();
                showInternalDeckButton.Text = "Show Deck";
            }
            else
            {
                DisplayedRecipe.internaldeck = null;
                showInternalDeckButton.Text = "New Deck";
            }
        }

        private void ShowSlotButton_Click(object sender, EventArgs e)
        {
            if ((DisplayedRecipe.slots == null || DisplayedRecipe.slots.Count == 0) && editing)
            {
                SlotViewer sv = new SlotViewer(new Slot(), true, SlotType.Recipe);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    DisplayedRecipe.slots = new List<Slot> { sv.DisplayedSlot.Copy() };
                    showSlotButton.Text = "Show Slot";
                }
            }
            else if (DisplayedRecipe.slots?.Count == 1)
            {
                SlotViewer sv = new SlotViewer(DisplayedRecipe.slots[0].Copy(), editing, SlotType.Recipe);
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    DisplayedRecipe.slots = new List<Slot> { sv.DisplayedSlot.Copy() };
                }
            }
        }

        private void AlternativerecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }

            Guid guid = (Guid)alternativeRecipesListView.SelectedItems[0].Tag;
            RecipeLink oldLink = alternativerecipeLinks[guid];
            RecipeLinkViewer rlv = new RecipeLinkViewer(oldLink.Copy(), editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                alternativerecipeLinks.Remove(guid);
                alternativerecipeLinks.Add(guid, rlv.DisplayedRecipeLink.Copy());
                if (alternativeRecipesListView.SelectedItems[0].Text != rlv.DisplayedRecipeLink.id)
                {
                    alternativeRecipesListView.SelectedItems[0].Text = rlv.DisplayedRecipeLink.id;
                }
                SaveAlternativeRecipes();
            }
        }

        private void LinkedListBox_DoubleClick(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }

            Guid guid = (Guid)linkedRecipesListView.SelectedItems[0].Tag;
            RecipeLink oldLink = recipeLinks[guid];
            RecipeLinkViewer rlv = new RecipeLinkViewer(oldLink.Copy(), editing);
            rlv.ShowDialog();
            if (rlv.DialogResult == DialogResult.OK)
            {
                recipeLinks.Remove(guid);
                recipeLinks.Add(guid, rlv.DisplayedRecipeLink.Copy());
                if (linkedRecipesListView.SelectedItems[0].Text != rlv.DisplayedRecipeLink.id)
                {
                    linkedRecipesListView.SelectedItems[0].Text = rlv.DisplayedRecipeLink.id;
                }
                SaveLinkedRecipes();
            }
        }

        private void MutationsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count == 0)
            {
                return;
            }

            Guid guid = (Guid)mutationsListView.SelectedItems[0].Tag;
            string oldId = mutationsListView.SelectedItems[0].Text;
            MutationViewer mv = new MutationViewer(mutations[guid], editing);
            mv.ShowDialog();
            if (mv.DialogResult == DialogResult.OK)
            {
                mutations.Remove(guid);
                mutations.Add(guid, mv.DisplayedMutation.Copy());
                if (oldId != mv.DisplayedMutation.mutate)
                {
                    mutationsListView.SelectedItems[0].Text = mv.DisplayedMutation.mutate;
                }
                SaveMutations();
            }
        }

        private void RequirementsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // can be aspects OR elements, don't allow editing these from a recipe
            if (!(requirementsDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(extantreqsDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(tablereqsDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(effectsDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(aspectsDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(deckeffectDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("All Recipes must have an ID.");
                return;
            }
            if (string.IsNullOrEmpty(actionIdTextBox.Text))
            {
                MessageBox.Show("All Recipes must have an Action/Verb ID unless they're extending a recipe with one.");
                // return;
            }
            if (requirementsDataGridView.RowCount > 1)
            {
                DisplayedRecipe.requirements = null;
                DisplayedRecipe.requirements_extend = null;
                DisplayedRecipe.requirements_remove = null;
                foreach (DataGridViewRow row in requirementsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.requirements_extend == null)
                        {
                            DisplayedRecipe.requirements_extend = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.requirements_extend[key] = value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.requirements_remove == null)
                        {
                            DisplayedRecipe.requirements_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.requirements_remove.Contains(key))
                        {
                            DisplayedRecipe.requirements_remove.Add(key);
                        }
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.requirements == null)
                        {
                            DisplayedRecipe.requirements = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.requirements[key] = value;
                    }
                }
            }
            if (extantreqsDataGridView.RowCount > 1)
            {
                DisplayedRecipe.extantreqs = null;
                DisplayedRecipe.extantreqs_extend = null;
                DisplayedRecipe.extantreqs_remove = null;
                foreach (DataGridViewRow row in extantreqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.extantreqs_extend == null)
                        {
                            DisplayedRecipe.extantreqs_extend = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.extantreqs_extend[key] = value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.extantreqs_remove == null)
                        {
                            DisplayedRecipe.extantreqs_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.extantreqs_remove.Contains(key))
                        {
                            DisplayedRecipe.extantreqs_remove.Add(key);
                        }
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.extantreqs == null)
                        {
                            DisplayedRecipe.extantreqs = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.extantreqs[key] = value;
                    }
                }
            }
            if (tablereqsDataGridView.RowCount > 1)
            {
                DisplayedRecipe.tablereqs = null;
                DisplayedRecipe.tablereqs_extend = null;
                DisplayedRecipe.tablereqs_remove = null;
                foreach (DataGridViewRow row in tablereqsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.tablereqs_extend == null)
                        {
                            DisplayedRecipe.tablereqs_extend = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.tablereqs_extend[key] = value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.tablereqs_remove == null)
                        {
                            DisplayedRecipe.tablereqs_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.tablereqs_remove.Contains(key))
                        {
                            DisplayedRecipe.tablereqs_remove.Add(key);
                        }
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.tablereqs == null)
                        {
                            DisplayedRecipe.tablereqs = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.tablereqs[key] = value;
                    }
                }
            }
            if (effectsDataGridView.RowCount > 1)
            {
                DisplayedRecipe.effects = null;
                DisplayedRecipe.effects_extend = null;
                DisplayedRecipe.effects_remove = null;
                foreach (DataGridViewRow row in effectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    string value;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.effects_extend == null)
                        {
                            DisplayedRecipe.effects_extend = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.effects_extend[key] = value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.effects_remove == null)
                        {
                            DisplayedRecipe.effects_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.effects_remove.Contains(key))
                        {
                            DisplayedRecipe.effects_remove.Add(key);
                        }
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                        if (DisplayedRecipe.effects == null)
                        {
                            DisplayedRecipe.effects = new Dictionary<string, string>();
                        }

                        DisplayedRecipe.effects[key] = value;
                    }
                }
            }
            if (aspectsDataGridView.RowCount > 1)
            {
                DisplayedRecipe.aspects = null;
                DisplayedRecipe.aspects_extend = null;
                DisplayedRecipe.aspects_remove = null;
                foreach (DataGridViewRow row in aspectsDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        if (DisplayedRecipe.aspects_extend == null)
                        {
                            DisplayedRecipe.aspects_extend = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.aspects_extend[key] = value.Value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.aspects_remove == null)
                        {
                            DisplayedRecipe.aspects_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.aspects_remove.Contains(key))
                        {
                            DisplayedRecipe.aspects_remove.Add(key);
                        }
                    }
                    else
                    {
                        if (DisplayedRecipe.aspects == null)
                        {
                            DisplayedRecipe.aspects = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.aspects[key] = value.Value;
                    }
                }
            }
            if (deckeffectDataGridView.RowCount > 1)
            {
                DisplayedRecipe.deckeffects = null;
                DisplayedRecipe.deckeffects_extend = null;
                DisplayedRecipe.deckeffects_remove = null;
                foreach (DataGridViewRow row in deckeffectDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        if (DisplayedRecipe.deckeffects_extend == null)
                        {
                            DisplayedRecipe.deckeffects_extend = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.deckeffects_extend[key] = value.Value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.deckeffects_remove == null)
                        {
                            DisplayedRecipe.deckeffects_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.deckeffects_remove.Contains(key))
                        {
                            DisplayedRecipe.deckeffects_remove.Add(key);
                        }
                    }
                    else
                    {
                        if (DisplayedRecipe.deckeffects == null)
                        {
                            DisplayedRecipe.deckeffects = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.deckeffects[key] = value.Value;
                    }
                }
            }
            if (purgeDataGridView.RowCount > 1)
            {
                DisplayedRecipe.purge = null;
                DisplayedRecipe.purge_extend = null;
                DisplayedRecipe.purge_remove = null;
                foreach (DataGridViewRow row in purgeDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        if (DisplayedRecipe.purge_extend == null)
                        {
                            DisplayedRecipe.purge_extend = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.purge_extend[key] = value.Value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.purge_remove == null)
                        {
                            DisplayedRecipe.purge_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.purge_remove.Contains(key))
                        {
                            DisplayedRecipe.purge_remove.Add(key);
                        }
                    }
                    else
                    {
                        if (DisplayedRecipe.purge == null)
                        {
                            DisplayedRecipe.purge = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.purge[key] = value.Value;
                    }
                }
            }
            if (haltVerbDataGridView.RowCount > 1)
            {
                DisplayedRecipe.haltverb = null;
                DisplayedRecipe.haltverb_extend = null;
                DisplayedRecipe.haltverb_remove = null;
                foreach (DataGridViewRow row in haltVerbDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        if (DisplayedRecipe.haltverb_extend == null)
                        {
                            DisplayedRecipe.haltverb_extend = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.haltverb_extend[key] = value.Value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.haltverb_remove == null)
                        {
                            DisplayedRecipe.haltverb_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.haltverb_remove.Contains(key))
                        {
                            DisplayedRecipe.haltverb_remove.Add(key);
                        }
                    }
                    else
                    {
                        if (DisplayedRecipe.haltverb == null)
                        {
                            DisplayedRecipe.haltverb = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.haltverb[key] = value.Value;
                    }
                }
            }
            if (deleteVerbDataGridView.RowCount > 1)
            {
                DisplayedRecipe.deleteverb = null;
                DisplayedRecipe.deleteverb_extend = null;
                DisplayedRecipe.deleteverb_remove = null;
                foreach (DataGridViewRow row in deleteVerbDataGridView.Rows)
                {
                    if (row.Cells[0].Value == null)
                    {
                        continue;
                    }

                    string key = row.Cells[0].Value.ToString();
                    int? value = row.Cells[1].Value != null ? Convert.ToInt32(row.Cells[1].Value) : (int?)null;
                    if (Equals(row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
                    {
                        if (DisplayedRecipe.deleteverb_extend == null)
                        {
                            DisplayedRecipe.deleteverb_extend = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.deleteverb_extend[key] = value.Value;
                    }
                    else if (Equals(row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
                    {
                        if (DisplayedRecipe.deleteverb_remove == null)
                        {
                            DisplayedRecipe.deleteverb_remove = new List<string>();
                        }

                        if (!DisplayedRecipe.deleteverb_remove.Contains(key))
                        {
                            DisplayedRecipe.deleteverb_remove.Add(key);
                        }
                    }
                    else
                    {
                        if (DisplayedRecipe.deleteverb == null)
                        {
                            DisplayedRecipe.deleteverb = new Dictionary<string, int>();
                        }

                        DisplayedRecipe.deleteverb[key] = value.Value;
                    }
                }
            }
            SaveAlternativeRecipes();
            SaveLinkedRecipes();
            SaveMutations();
            Close();
            SuccessCallback?.Invoke(this, DisplayedRecipe);
        }

        private void AddAlternativeRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    alternativeRecipesListView.Items.Add(new ListViewItem(rlv.DisplayedRecipeLink.id) { Tag = newGuid });
                    alternativerecipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
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
                    ListViewItem item = new ListViewItem(rlv.DisplayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    alternativeRecipesListView.Items.Insert(0, item);
                    alternativerecipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
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
                    ListViewItem item = new ListViewItem(rlv.DisplayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = newGuid
                    };
                    alternativeRecipesListView.Items.Add(item);
                    alternativerecipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
                    SaveAlternativeRecipes();
                }
            }
        }

        private void AddLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.Linked))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    linkedRecipesListView.Items.Add(new ListViewItem(rlv.DisplayedRecipeLink.id) { Tag = newGuid });
                    recipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
                    SaveLinkedRecipes();
                }
            }
        }

        private void PrependLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.Linked))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.DisplayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    linkedRecipesListView.Items.Insert(0, item);
                    recipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
                    SaveLinkedRecipes();
                }
            }
        }

        private void AppendLinkedRecipeButton_Click(object sender, EventArgs e)
        {
            using (RecipeLinkViewer rlv = new RecipeLinkViewer(new RecipeLink(), true, RecipeLinkType.Linked))
            {
                rlv.ShowDialog();
                if (rlv.DialogResult == DialogResult.OK)
                {
                    Guid newGuid = Guid.NewGuid();
                    ListViewItem item = new ListViewItem(rlv.DisplayedRecipeLink.id)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    linkedRecipesListView.Items.Add(item);
                    recipeLinks.Add(newGuid, rlv.DisplayedRecipeLink);
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
                    ListViewItem item = new ListViewItem(mv.DisplayedMutation.mutate)
                    {
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.DisplayedMutation);
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
                    ListViewItem item = new ListViewItem(mv.DisplayedMutation.mutate)
                    {
                        BackColor = Utilities.ListPrependColor,
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.DisplayedMutation);
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
                    ListViewItem item = new ListViewItem(mv.DisplayedMutation.mutate)
                    {
                        BackColor = Utilities.ListAppendColor,
                        Tag = newGuid
                    };
                    mutationsListView.Items.Add(item);
                    mutations.Add(newGuid, mv.DisplayedMutation);
                    // if (displayedRecipe.mutations_append != null) displayedRecipe.mutations_append.Add(mv.displayedMutation);
                    // else displayedRecipe.mutations_append = new List<Mutation> { mv.displayedMutation };
                    SaveMutations();
                }
            }
        }

        private void MaxExecutionsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.maxexecutions = Convert.ToInt32(maxExecutionsNumericUpDown.Value);
            if (maxExecutionsNumericUpDown.Value == 0)
            {
                DisplayedRecipe.maxexecutions = null;
            }
        }

        private void IdTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.ID = idTextBox.Text == "" ? null : idTextBox.Text;
        }

        private void LabelTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.label = labelTextBox.Text == "" ? null : labelTextBox.Text;
        }

        private void ActionIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (actionIdTextBox.Text == "")
            {
                DisplayedRecipe.label = null;
            }
            else
            {
                DisplayedRecipe.actionId = actionIdTextBox.Text;
            }
        }

        private void EndingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (endingTextBox.Text == "")
            {
                DisplayedRecipe.label = null;
            }
            else
            {
                DisplayedRecipe.ending = endingTextBox.Text;
            }
        }

        private void BurnimageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (burnimageTextBox.Text == "")
            {
                DisplayedRecipe.label = null;
            }
            else
            {
                DisplayedRecipe.burnimage = burnimageTextBox.Text;
            }
        }

        private void ExtendsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void WarmupNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.warmup = Convert.ToInt32(warmupNumericUpDown.Value);
            if (DisplayedRecipe.warmup == 0)
            {
                DisplayedRecipe.warmup = null;
            }
        }

        private void StartdescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.startdescription = startdescriptionTextBox.Text;
            if (DisplayedRecipe.startdescription == "")
            {
                DisplayedRecipe.startdescription = null;
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.description = descriptionTextBox.Text;
            if (DisplayedRecipe.description == "")
            {
                DisplayedRecipe.description = null;
            }
        }

        private void RequirementsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.requirements_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.requirements_extend.ContainsKey(key))
                {
                    DisplayedRecipe.requirements_extend.Remove(key);
                }

                if (DisplayedRecipe.requirements_extend.Count == 0)
                {
                    DisplayedRecipe.requirements_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.requirements_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.requirements_remove.Contains(key))
                {
                    DisplayedRecipe.requirements_remove.Remove(key);
                }

                if (DisplayedRecipe.requirements_remove.Count == 0)
                {
                    DisplayedRecipe.requirements_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.requirements == null)
                {
                    return;
                }

                if (DisplayedRecipe.requirements.ContainsKey(key))
                {
                    DisplayedRecipe.requirements.Remove(key);
                }

                if (DisplayedRecipe.requirements.Count == 0)
                {
                    DisplayedRecipe.requirements = null;
                }
            }
        }

        private void ExtantreqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.extantreqs_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.extantreqs_extend.ContainsKey(key))
                {
                    DisplayedRecipe.extantreqs_extend.Remove(key);
                }

                if (DisplayedRecipe.extantreqs_extend.Count == 0)
                {
                    DisplayedRecipe.extantreqs_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.extantreqs_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.extantreqs_remove.Contains(key))
                {
                    DisplayedRecipe.extantreqs_remove.Remove(key);
                }

                if (DisplayedRecipe.extantreqs_remove.Count == 0)
                {
                    DisplayedRecipe.extantreqs_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.extantreqs == null)
                {
                    return;
                }

                if (DisplayedRecipe.extantreqs.ContainsKey(key))
                {
                    DisplayedRecipe.extantreqs.Remove(key);
                }

                if (DisplayedRecipe.extantreqs.Count == 0)
                {
                    DisplayedRecipe.extantreqs = null;
                }
            }
        }

        private void TablereqsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.tablereqs_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.tablereqs_extend.ContainsKey(key))
                {
                    DisplayedRecipe.tablereqs_extend.Remove(key);
                }

                if (DisplayedRecipe.tablereqs_extend.Count == 0)
                {
                    DisplayedRecipe.tablereqs_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.tablereqs_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.tablereqs_remove.Contains(key))
                {
                    DisplayedRecipe.tablereqs_remove.Remove(key);
                }

                if (DisplayedRecipe.tablereqs_remove.Count == 0)
                {
                    DisplayedRecipe.tablereqs_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.tablereqs == null)
                {
                    return;
                }

                if (DisplayedRecipe.tablereqs.ContainsKey(key))
                {
                    DisplayedRecipe.tablereqs.Remove(key);
                }

                if (DisplayedRecipe.tablereqs.Count == 0)
                {
                    DisplayedRecipe.tablereqs = null;
                }
            }
        }

        private void EffectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.effects_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.effects_extend.ContainsKey(key))
                {
                    DisplayedRecipe.effects_extend.Remove(key);
                }

                if (DisplayedRecipe.effects_extend.Count == 0)
                {
                    DisplayedRecipe.effects_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.effects_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.effects_remove.Contains(key))
                {
                    DisplayedRecipe.effects_remove.Remove(key);
                }

                if (DisplayedRecipe.effects_remove.Count == 0)
                {
                    DisplayedRecipe.effects_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.effects == null)
                {
                    return;
                }

                if (DisplayedRecipe.effects.ContainsKey(key))
                {
                    DisplayedRecipe.effects.Remove(key);
                }

                if (DisplayedRecipe.effects.Count == 0)
                {
                    DisplayedRecipe.effects = null;
                }
            }
        }

        private void AspectsDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.aspects_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.aspects_extend.ContainsKey(key))
                {
                    DisplayedRecipe.aspects_extend.Remove(key);
                }

                if (DisplayedRecipe.aspects_extend.Count == 0)
                {
                    DisplayedRecipe.aspects_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.aspects_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.aspects_remove.Contains(key))
                {
                    DisplayedRecipe.aspects_remove.Remove(key);
                }

                if (DisplayedRecipe.aspects_remove.Count == 0)
                {
                    DisplayedRecipe.aspects_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.aspects == null)
                {
                    return;
                }

                if (DisplayedRecipe.aspects.ContainsKey(key))
                {
                    DisplayedRecipe.aspects.Remove(key);
                }

                if (DisplayedRecipe.aspects.Count == 0)
                {
                    DisplayedRecipe.aspects = null;
                }
            }
        }

        private void DeckeffectDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.deckeffects_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.deckeffects_extend.ContainsKey(key))
                {
                    DisplayedRecipe.deckeffects_extend.Remove(key);
                }

                if (DisplayedRecipe.deckeffects_extend.Count == 0)
                {
                    DisplayedRecipe.deckeffects_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.deckeffects_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.deckeffects_remove.Contains(key))
                {
                    DisplayedRecipe.deckeffects_remove.Remove(key);
                }

                if (DisplayedRecipe.deckeffects_remove.Count == 0)
                {
                    DisplayedRecipe.deckeffects_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.deckeffects == null)
                {
                    return;
                }

                if (DisplayedRecipe.deckeffects.ContainsKey(key))
                {
                    DisplayedRecipe.deckeffects.Remove(key);
                }

                if (DisplayedRecipe.deckeffects.Count == 0)
                {
                    DisplayedRecipe.deckeffects = null;
                }
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
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    alternativerecipeLinks.Remove(guid);
                    alternativeRecipesListView.Items.Remove(alternativeRecipesListView.SelectedItems[0]);
                }
                else
                {
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
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
                }
                else
                {
                    recipeLinks.Remove(guid);
                    linkedRecipesListView.Items.Remove(linkedRecipesListView.SelectedItems[0]);
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
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListAppendColor)
                {
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
                }
                else
                {
                    mutations.Remove(guid);
                    mutationsListView.Items.Remove(mutationsListView.SelectedItems[0]);
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
            if (alternativeRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if first item is selected
            if (alternativeRecipesListView.SelectedIndices[0] == 0)
            {
                return;
            }

            int selectedIndex = alternativeRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex--;
            ListViewItem selectedItem = alternativeRecipesListView.SelectedItems[0];
            alternativeRecipesListView.Items.Remove(selectedItem);
            alternativeRecipesListView.Items.Insert(affectedIndex - 1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveAltRecipeDownButton_Click(object sender, EventArgs e)
        {
            if (alternativeRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if last item is selected
            if (alternativeRecipesListView.SelectedIndices[0] == alternativeRecipesListView.Items.Count - 1)
            {
                return;
            }

            int selectedIndex = alternativeRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = alternativeRecipesListView.SelectedItems[0];
            alternativeRecipesListView.Items.Remove(selectedItem);
            alternativeRecipesListView.Items.Insert(affectedIndex + 1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveLinkedRecipeUpButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if first item is selected
            if (linkedRecipesListView.SelectedIndices[0] == 0)
            {
                return;
            }

            int selectedIndex = linkedRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex--;
            ListViewItem selectedItem = linkedRecipesListView.SelectedItems[0];
            linkedRecipesListView.Items.Remove(selectedItem);
            linkedRecipesListView.Items.Insert(affectedIndex - 1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void MoveLinkedRecipeDownButton_Click(object sender, EventArgs e)
        {
            if (linkedRecipesListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if last item is selected
            if (linkedRecipesListView.SelectedIndices[0] == linkedRecipesListView.Items.Count - 1)
            {
                return;
            }

            int selectedIndex = linkedRecipesListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = linkedRecipesListView.SelectedItems[0];
            linkedRecipesListView.Items.Remove(selectedItem);
            linkedRecipesListView.Items.Insert(affectedIndex + 1, selectedItem);
            SaveAlternativeRecipes();
        }

        private void SaveAlternativeRecipes()
        {
            DisplayedRecipe.alt = null;
            DisplayedRecipe.alt_prepend = null;
            DisplayedRecipe.alt_append = null;
            DisplayedRecipe.alt_remove = null;
            foreach (ListViewItem item in alternativeRecipesListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (DisplayedRecipe.alt_append == null)
                    {
                        DisplayedRecipe.alt_append = new List<RecipeLink>();
                    }

                    DisplayedRecipe.alt_append.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (DisplayedRecipe.alt_prepend == null)
                    {
                        DisplayedRecipe.alt_prepend = new List<RecipeLink>();
                    }

                    DisplayedRecipe.alt_prepend.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (DisplayedRecipe.alt_remove == null)
                    {
                        DisplayedRecipe.alt_remove = new List<string>();
                    }

                    DisplayedRecipe.alt_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (DisplayedRecipe.alt == null)
                    {
                        DisplayedRecipe.alt = new List<RecipeLink>();
                    }

                    DisplayedRecipe.alt.Add(alternativerecipeLinks[(Guid)item.Tag]);
                }
            }
        }

        private void SaveLinkedRecipes()
        {
            DisplayedRecipe.linked = null;
            DisplayedRecipe.linked_prepend = null;
            DisplayedRecipe.linked_append = null;
            DisplayedRecipe.linked_remove = null;
            foreach (ListViewItem item in linkedRecipesListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (DisplayedRecipe.linked_append == null)
                    {
                        DisplayedRecipe.linked_append = new List<RecipeLink>();
                    }

                    DisplayedRecipe.linked_append.Add(recipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (DisplayedRecipe.linked_prepend == null)
                    {
                        DisplayedRecipe.linked_prepend = new List<RecipeLink>();
                    }

                    DisplayedRecipe.linked_prepend.Add(recipeLinks[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (DisplayedRecipe.linked_remove == null)
                    {
                        DisplayedRecipe.linked_remove = new List<string>();
                    }

                    DisplayedRecipe.linked_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (DisplayedRecipe.linked == null)
                    {
                        DisplayedRecipe.linked = new List<RecipeLink>();
                    }

                    DisplayedRecipe.linked.Add(recipeLinks[(Guid)item.Tag]);
                }
            }
        }

        private void PurgeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(purgeDataGridView.SelectedCells[0].Value is string id))
            {
                return;
            }

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
            if (!(haltVerbDataGridView.Rows[e.RowIndex].Cells[0].Value is string id))
            {
                return;
            }

            if (Utilities.VerbExists(id))
            {
                VerbViewer vv = new VerbViewer(Utilities.GetVerb(id), null, null);
                vv.Show();
            }
        }

        private void DeleteVerbDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(deleteVerbDataGridView.Rows[e.RowIndex].Cells[0].Value is string id))
            {
                return;
            }

            if (Utilities.VerbExists(id))
            {
                VerbViewer vv = new VerbViewer(Utilities.GetVerb(id), null, null);
                vv.Show();
            }
        }

        private void DeleteVerbDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.deleteverb_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.deleteverb_extend.ContainsKey(key))
                {
                    DisplayedRecipe.deleteverb_extend.Remove(key);
                }

                if (DisplayedRecipe.deleteverb_extend.Count == 0)
                {
                    DisplayedRecipe.deleteverb_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.deleteverb_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.deleteverb_remove.Contains(key))
                {
                    DisplayedRecipe.deleteverb_remove.Remove(key);
                }

                if (DisplayedRecipe.deleteverb_remove.Count == 0)
                {
                    DisplayedRecipe.deleteverb_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.deleteverb == null)
                {
                    return;
                }

                if (DisplayedRecipe.deleteverb.ContainsKey(key))
                {
                    DisplayedRecipe.deleteverb.Remove(key);
                }

                if (DisplayedRecipe.deleteverb.Count == 0)
                {
                    DisplayedRecipe.deleteverb = null;
                }
            }
        }

        private void HaltVerbDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.haltverb_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.haltverb_extend.ContainsKey(key))
                {
                    DisplayedRecipe.haltverb_extend.Remove(key);
                }

                if (DisplayedRecipe.haltverb_extend.Count == 0)
                {
                    DisplayedRecipe.haltverb_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.haltverb_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.haltverb_remove.Contains(key))
                {
                    DisplayedRecipe.haltverb_remove.Remove(key);
                }

                if (DisplayedRecipe.haltverb_remove.Count == 0)
                {
                    DisplayedRecipe.haltverb_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.haltverb == null)
                {
                    return;
                }

                if (DisplayedRecipe.haltverb.ContainsKey(key))
                {
                    DisplayedRecipe.haltverb.Remove(key);
                }

                if (DisplayedRecipe.haltverb.Count == 0)
                {
                    DisplayedRecipe.haltverb = null;
                }
            }
        }

        private void PurgeDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells[0].Value == null) return;
            string key = e.Row.Cells[1].Value != null ? e.Row.Cells[0].Value.ToString() : null;
            if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryExtendStyle))
            {

                if (DisplayedRecipe.purge_extend == null)
                {
                    return;
                }

                if (DisplayedRecipe.purge_extend.ContainsKey(key))
                {
                    DisplayedRecipe.purge_extend.Remove(key);
                }

                if (DisplayedRecipe.purge_extend.Count == 0)
                {
                    DisplayedRecipe.purge_extend = null;
                }
            }
            else if (Equals(e.Row.DefaultCellStyle, Utilities.DictionaryRemoveStyle))
            {

                if (DisplayedRecipe.purge_remove == null)
                {
                    return;
                }

                if (DisplayedRecipe.purge_remove.Contains(key))
                {
                    DisplayedRecipe.purge_remove.Remove(key);
                }

                if (DisplayedRecipe.purge_remove.Count == 0)
                {
                    DisplayedRecipe.purge_remove = null;
                }
            }
            else
            {
                if (DisplayedRecipe.purge == null)
                {
                    return;
                }

                if (DisplayedRecipe.purge.ContainsKey(key))
                {
                    DisplayedRecipe.purge.Remove(key);
                }

                if (DisplayedRecipe.purge.Count == 0)
                {
                    DisplayedRecipe.purge = null;
                }
            }
        }

        private void MoveMutationUpButton_Click(object sender, EventArgs e)
        {
            if (mutationsListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if first item is selected
            if (mutationsListView.SelectedIndices[0] == 0)
            {
                return;
            }

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
            if (mutationsListView.SelectedItems.Count == 0)
            {
                return;
            }
            // check to see if last item is selected
            if (mutationsListView.SelectedIndices[0] == mutationsListView.Items.Count - 1)
            {
                return;
            }

            int selectedIndex = mutationsListView.SelectedIndices[0];
            int affectedIndex = selectedIndex++;
            ListViewItem selectedItem = mutationsListView.SelectedItems[0];
            mutationsListView.Items.Remove(selectedItem);
            mutationsListView.Items.Insert(affectedIndex + 1, selectedItem);
            SaveMutations();
        }

        private void SaveMutations()
        {
            DisplayedRecipe.mutations = null;
            DisplayedRecipe.mutations_prepend = null;
            DisplayedRecipe.mutations_append = null;
            DisplayedRecipe.mutations_remove = null;
            foreach (ListViewItem item in mutationsListView.Items)
            {
                if (item.BackColor == Utilities.ListAppendColor)
                {
                    if (DisplayedRecipe.mutations_append == null)
                    {
                        DisplayedRecipe.mutations_append = new List<Mutation>();
                    }

                    DisplayedRecipe.mutations_append.Add(mutations[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListPrependColor)
                {
                    if (DisplayedRecipe.mutations_prepend == null)
                    {
                        DisplayedRecipe.mutations_prepend = new List<Mutation>();
                    }

                    DisplayedRecipe.mutations_prepend.Add(mutations[(Guid)item.Tag]);
                }
                else if (item.BackColor == Utilities.ListRemoveColor)
                {
                    if (DisplayedRecipe.mutations_remove == null)
                    {
                        DisplayedRecipe.mutations_remove = new List<string>();
                    }

                    DisplayedRecipe.mutations_remove.Add(item.Tag.ToString());
                }
                else
                {
                    if (DisplayedRecipe.mutations == null)
                    {
                        DisplayedRecipe.mutations = new List<Mutation>();
                    }

                    DisplayedRecipe.mutations.Add(mutations[(Guid)item.Tag]);
                }
            }
        }

        private void SignalEndingFlavourDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.signalendingflavour = signalEndingFlavourDomainUpDown.Text;
            if (DisplayedRecipe.signalendingflavour == "")
            {
                DisplayedRecipe.signalendingflavour = null;
            }
        }

        private void PortalEffectDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.portaleffect = portalEffectDomainUpDown.Text;
            if (DisplayedRecipe.portaleffect == "")
            {
                DisplayedRecipe.portaleffect = null;
            }
        }

        private void CraftableCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (craftableCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedRecipe.craftable = true;
            }

            if (craftableCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedRecipe.craftable = false;
            }

            if (craftableCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedRecipe.craftable = null;
            }
        }

        private void HintonlyCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (hintonlyCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedRecipe.hintonly = true;
            }

            if (hintonlyCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedRecipe.hintonly = false;
            }

            if (hintonlyCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedRecipe.hintonly = null;
            }
        }

        private void SignalImportantLoopCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (signalImportantLoopCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedRecipe.signalimportantloop = true;
            }

            if (signalImportantLoopCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedRecipe.signalimportantloop = false;
            }

            if (signalImportantLoopCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedRecipe.signalimportantloop = null;
            }
        }

        private void CommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayedRecipe.comments = commentsTextBox.Text;
            if (DisplayedRecipe.comments == "")
            {
                DisplayedRecipe.comments = null;
            }
        }

        private void DeletedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (deletedCheckBox.CheckState == CheckState.Checked)
            {
                DisplayedRecipe.deleted = true;
            }

            if (deletedCheckBox.CheckState == CheckState.Unchecked)
            {
                DisplayedRecipe.deleted = false;
            }

            if (deletedCheckBox.CheckState == CheckState.Indeterminate)
            {
                DisplayedRecipe.deleted = null;
            }
        }

        private void ExtendsTextBox_TextChanged_1(object sender, EventArgs e)
        {
            if (extendsTextBox.Text.Contains(","))
            {
                DisplayedRecipe.extends = extendsTextBox.Text.Split(',').ToList();
            }
            else
            {
                DisplayedRecipe.extends = extendsTextBox.Text != "" ? new List<string> { extendsTextBox.Text } : null;
            }
        }

        private void RecipeViewer_Shown(object sender, EventArgs e)
        {
            FillValues(DisplayedRecipe);
        }
    }
}
