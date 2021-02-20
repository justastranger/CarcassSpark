using CarcassSpark.ObjectTypes;
using CarcassSpark.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class TabbedModViewer : Form
    {
        private ModViewerTabControl SelectedModViewer;

        public TabbedModViewer()
        {
            // this should always be first before messing with any components like the Folder Browser Dialog
            InitializeComponent();
            // This is necessary to ensure we have a reference point for the game and the game's assemblies

        }

        private void InitializeTabs()
        {
            CreateNewModViewerTab(Utilities.DirectoryToVanillaContent, true, false);
            if (Settings.settings["previousMods"] != null && Settings.settings["loadPreviousMods"] != null && Settings.settings["loadPreviousMods"].ToObject<bool>())
            {
                if (((JArray)Settings.settings["previousMods"]).Count() > 0)
                {
                    foreach (string path in Settings.GetPreviousMods())
                    {
                        CreateNewModViewerTab(path, false, false);
                    }
                }
            }
            if (!SelectedModViewer.isVanilla)
            {

            }
            toggleEditModeToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && SelectedModViewer.editMode;
            saveSplitterLocationsToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && (SelectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);
        }
        private void CreateNewModViewerTab(string folder, bool isVanilla, bool newMod)
        {
            try
            {
                SelectedModViewer = new ModViewerTabControl(folder, isVanilla, newMod);
                SelectedModViewer.MarkDirtyEventHandler += MarkTabDirty;
                TabPage newPage = new TabPage(SelectedModViewer.Content.GetName());
                newPage.Controls.Add(SelectedModViewer);
                newPage.Name = SelectedModViewer.Content.GetName();
                ModViewerTabs.TabPages.Add(newPage);
                ModViewerTabs.SelectTab(newPage);
            }
            catch
            {

            }
        }

        private void CreateNewModViewerTab(ModViewerTabControl mvtc)
        {
            SelectedModViewer = mvtc;
            TabPage newPage = new TabPage(mvtc.Content.GetName());
            newPage.Controls.Add(mvtc);
            newPage.Name = mvtc.Content.GetName();
            ModViewerTabs.TabPages.Add(newPage);
            ModViewerTabs.SelectTab(newPage);
        }

        private void OpenModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = (Settings.settings["previousMods"] != null && Settings.settings["previousMods"].Count() > 0) ? Settings.settings["previousMods"].Last.ToString() : AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                try
                {
                    ModViewerTabControl mvtc = new ModViewerTabControl(location, false, false);
                    mvtc.MarkDirtyEventHandler += MarkTabDirty;
                    CreateNewModViewerTab(mvtc);
                    if (Settings.HasPreviousMods())
                    {
                        Settings.AddPreviousMod(location);
                    }
                    else
                    {
                        Settings.InitPreviousMods(location);
                    }
                    Settings.SaveSettings();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Opening Mod");
                    return;
                }
            }
        }

        private void NewModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                try
                {
                    ModViewerTabControl mvtc = new ModViewerTabControl(location, false, true);
                    mvtc.MarkDirtyEventHandler += MarkTabDirty;
                    CreateNewModViewerTab(mvtc);
                    if (Settings.HasPreviousMods())
                    {
                        Settings.AddPreviousMod(location);
                    }
                    else
                    {
                        Settings.InitPreviousMods(location);
                    }
                    Settings.SaveSettings();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Creating Mod");
                }
            }
        }

        private void CloseModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SelectedModViewer.IsDirty && SelectedModViewer.editMode)
            {
                if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and close the mod.",
                    "You have unsaved changes",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            Utilities.ContentSources.Remove(SelectedModViewer.Content.GetName());
            // ((JArray)Settings.settings["previousMods"]).Remove(SelectedModViewer.Content.currentDirectory);
            Settings.RemovePreviousMod(SelectedModViewer.Content.currentDirectory);
            ModViewerTabs.TabPages.Remove(ModViewerTabs.SelectedTab);
        }

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void SaveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't save vanilla content.");
                return;
            }
            SelectedModViewer.SaveMod();
        }

        private void ModViewerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModViewerTabs.SelectedTab == null)
            {
                SelectedModViewer = null;
                return;
            }
            SelectedModViewer = (ModViewerTabControl)ModViewerTabs.SelectedTab.Controls[0];

            toggleEditModeToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && SelectedModViewer.editMode;
            toggleAutosaveToolStripMenuItem.Checked = !SelectedModViewer.isVanilla && (SelectedModViewer.Content.GetCustomManifestBool("AutoSave") ?? false);
            saveSplitterLocationsToolStripMenuItem.Checked = SelectedModViewer.isVanilla ? (Settings.settings.ContainsKey("saveWidths") && Settings.settings["saveWidths"].ToObject<bool>()) : (SelectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);

            toggleEditModeToolStripMenuItem.Enabled = !SelectedModViewer.isVanilla;
            toggleAutosaveToolStripMenuItem.Enabled = !SelectedModViewer.isVanilla;
        }

        private void SaveModToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = SelectedModViewer.Content.currentDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.SaveMod(folderBrowserDialog.SelectedPath);
            }
        }

        private void OpenSynopsisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("There is no synopsis for vanilla content.");
                return;
            }

            SynopsisViewer mv = new SynopsisViewer(SelectedModViewer.Content.synopsis.Copy());
            if (mv.ShowDialog() == DialogResult.OK)
            {
                if (SelectedModViewer.Content.synopsis.name != mv.displayedSynopsis.name)
                {
                    SelectedModViewer.Parent.Name = mv.displayedSynopsis.name;
                }
                SelectedModViewer.Content.synopsis = mv.displayedSynopsis.Copy();
                SelectedModViewer.SaveMod();
            }
        }

        private void ReloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.LoadContent();
        }

        private void ToggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Edit Mode for vanilla content.");
                return;
            }
            toggleEditModeToolStripMenuItem.Checked = !toggleEditModeToolStripMenuItem.Checked;
            SelectedModViewer.Content.CustomManifest["EditMode"] = toggleEditModeToolStripMenuItem.Checked;
            SelectedModViewer.SetEditingMode(toggleEditModeToolStripMenuItem.Checked);
        }

        private void ToggleAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Autosave for vanilla content.");
                return;
            }
            toggleAutosaveToolStripMenuItem.Checked = !toggleAutosaveToolStripMenuItem.Checked;
            SelectedModViewer.Content.SetCustomManifestProperty("AutoSave", toggleAutosaveToolStripMenuItem.Checked);
        }

        private void AspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            AspectViewer av = new AspectViewer(new Aspect(), SelectedModViewer.AspectsList_Add, null);
            av.Show();
        }

        private void ElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            ElementViewer ev = new ElementViewer(new Element(), SelectedModViewer.ElementsList_Add, null);
            ev.Show();
        }

        private void RecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            RecipeViewer rv = new RecipeViewer(new Recipe(), SelectedModViewer.RecipesList_Add, null);
            rv.Show();
        }

        private void DeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            DeckViewer dv = new DeckViewer(new Deck(), SelectedModViewer.DecksList_Add, null);
            dv.Show();
        }

        private void LegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            LegacyViewer lv = new LegacyViewer(new Legacy(), SelectedModViewer.LegaciesList_Add, null);
            lv.Show();
        }

        private void EndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            EndingViewer ev = new EndingViewer(new Ending(), SelectedModViewer.EndingsList_Add, null);
            ev.Show();
        }

        private void VerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            VerbViewer vv = new VerbViewer(new Verb(), SelectedModViewer.VerbsList_Add, null);
            vv.Show();
        }

        private void AspectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.aspectsListView.Items.ContainsKey(deserializedAspect.id))
                    {
                        MessageBox.Show("Aspect already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.aspectsListView.Groups["aspects"] ?? new ListViewGroup("aspects", "aspects");
                        if (!SelectedModViewer.aspectsListView.Groups.Contains(group))
                        {
                            SelectedModViewer.aspectsListView.Groups.Add(group);
                        }
                        SelectedModViewer.aspectsListView.Items.Add(new ListViewItem(deserializedAspect.id) { Tag = guid, Group = group });
                    }
                    deserializedAspect.filename = "aspects";
                    SelectedModViewer.Content.Aspects[guid] = deserializedAspect;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deserializing Aspect: " + ex.ToString());
                }
            }
        }

        private void ElementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Element deserializedElement = JsonConvert.DeserializeObject<Element>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.elementsListView.Items.ContainsKey(deserializedElement.id))
                    {
                        MessageBox.Show("Element already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.elementsListView.Groups["elements"] ?? new ListViewGroup("elements", "elements");
                        if (!SelectedModViewer.elementsListView.Groups.Contains(group))
                        {
                            SelectedModViewer.elementsListView.Groups.Add(group);
                        }
                        SelectedModViewer.elementsListView.Items.Add(new ListViewItem(deserializedElement.id) { Tag = guid, Group = group });
                    }
                    deserializedElement.filename = "elements";
                    SelectedModViewer.Content.Elements[guid] = deserializedElement;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Element");
                }
            }
        }

        private void RecipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.recipesListView.Items.ContainsKey(deserializedRecipe.id))
                    {
                        MessageBox.Show("Recipe already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.recipesListView.Groups["recipes"] ?? new ListViewGroup("recipes", "recipes");
                        if (!SelectedModViewer.recipesListView.Groups.Contains(group))
                        {
                            SelectedModViewer.recipesListView.Groups.Add(group);
                        }
                        SelectedModViewer.recipesListView.Items.Add(new ListViewItem(deserializedRecipe.id) { Tag = guid, Group = group });
                    }
                    deserializedRecipe.filename = "recipes";
                    SelectedModViewer.Content.Recipes[guid] = deserializedRecipe;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Recipe");
                }
            }
        }

        private void DeckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.decksListView.Items.ContainsKey(deserializedDeck.id))
                    {
                        MessageBox.Show("Deck already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.decksListView.Groups["decks"] ?? new ListViewGroup("decks", "decks");
                        if (!SelectedModViewer.decksListView.Groups.Contains(group))
                        {
                            SelectedModViewer.decksListView.Groups.Add(group);
                        }
                        SelectedModViewer.decksListView.Items.Add(new ListViewItem(deserializedDeck.id) { Tag = guid, Group = group });
                    }
                    deserializedDeck.filename = "decks";
                    SelectedModViewer.Content.Decks[guid] = deserializedDeck;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Deck");
                }
            }
        }

        private void LegacyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.legaciesListView.Items.ContainsKey(deserializedLegacy.id))
                    {
                        MessageBox.Show("Legacy already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.legaciesListView.Groups["legacies"] ?? new ListViewGroup("legacies", "legacies");
                        if (!SelectedModViewer.legaciesListView.Groups.Contains(group))
                        {
                            SelectedModViewer.legaciesListView.Groups.Add(group);
                        }
                        SelectedModViewer.legaciesListView.Items.Add(new ListViewItem(deserializedLegacy.id) { Tag = guid, Group = group });
                    }
                    deserializedLegacy.filename = "legacies";
                    SelectedModViewer.Content.Legacies[guid] = deserializedLegacy;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Legacy");
                }
            }
        }

        private void EndingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.endingsListView.Items.ContainsKey(deserializedEnding.id))
                    {
                        MessageBox.Show("Ending already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.endingsListView.Groups["endings"] ?? new ListViewGroup("endings", "endings");
                        if (!SelectedModViewer.endingsListView.Groups.Contains(group))
                        {
                            SelectedModViewer.endingsListView.Groups.Add(group);
                        }
                        SelectedModViewer.endingsListView.Items.Add(new ListViewItem(deserializedEnding.id) { Tag = guid, Group = group });
                    }
                    deserializedEnding.filename = "endings";
                    SelectedModViewer.Content.Endings[guid] = deserializedEnding;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Ending");
                }
            }
        }

        private void VerbToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.verbsListView.Items.ContainsKey(deserializedVerb.id))
                    {
                        MessageBox.Show("Verb already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = SelectedModViewer.verbsListView.Groups["verbs"] ?? new ListViewGroup("verbs", "verbs");
                        if (!SelectedModViewer.verbsListView.Groups.Contains(group))
                        {
                            SelectedModViewer.verbsListView.Groups.Add(group);
                        }
                        SelectedModViewer.verbsListView.Items.Add(new ListViewItem(deserializedVerb.id) { Tag = guid, Group = group });
                    }
                    deserializedVerb.filename = "verbs";
                    SelectedModViewer.Content.Verbs[guid] = deserializedVerb;
                    SelectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Verb");
                }
            }
        }

        private void FromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            if (!Clipboard.ContainsText())
            {
                return;
            }

            JsonEditor je = new JsonEditor(Clipboard.GetText());
            if (je.ShowDialog() == DialogResult.OK)
            {
                Guid guid = Guid.NewGuid();
                ListViewGroup listViewGroup;
                SelectedModViewer.MarkDirty();
                switch (je.objectType)
                {
                    case "Aspect":
                        listViewGroup = SelectedModViewer.aspectsListView.Groups["aspects"] ?? new ListViewGroup("aspects", "aspects");
                        if (!SelectedModViewer.aspectsListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.aspectsListView.Groups.Add(listViewGroup);
                        }
                        Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                        deserializedAspect.filename = "aspects";
                        SelectedModViewer.Content.Aspects[guid] = deserializedAspect;
                        if (!SelectedModViewer.aspectsListView.Items.ContainsKey(deserializedAspect.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedAspect.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.aspectsListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Element":
                        listViewGroup = SelectedModViewer.elementsListView.Groups["elements"] ?? new ListViewGroup("elements", "elements");
                        if (!SelectedModViewer.elementsListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.elementsListView.Groups.Add(listViewGroup);
                        }
                        Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                        deserializedElement.filename = "elements";
                        SelectedModViewer.Content.Elements[guid] = deserializedElement;
                        if (!SelectedModViewer.elementsListView.Items.ContainsKey(deserializedElement.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedElement.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.elementsListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Recipe":
                        listViewGroup = SelectedModViewer.recipesListView.Groups["recipes"] ?? new ListViewGroup("recipes", "recipes");
                        if (!SelectedModViewer.recipesListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.recipesListView.Groups.Add(listViewGroup);
                        }
                        Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                        deserializedRecipe.filename = "recipes";
                        SelectedModViewer.Content.Recipes[guid] = deserializedRecipe;
                        if (!SelectedModViewer.recipesListView.Items.ContainsKey(deserializedRecipe.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedRecipe.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.recipesListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Deck":
                        listViewGroup = SelectedModViewer.decksListView.Groups["decks"] ?? new ListViewGroup("decks", "decks");
                        if (SelectedModViewer.decksListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.decksListView.Groups.Add(listViewGroup);
                        }
                        Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                        deserializedDeck.filename = "decks";
                        SelectedModViewer.Content.Decks[guid] = deserializedDeck;
                        if (!SelectedModViewer.decksListView.Items.ContainsKey(deserializedDeck.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedDeck.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.decksListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Legacy":
                        listViewGroup = SelectedModViewer.legaciesListView.Groups["legacies"] ?? new ListViewGroup("legacies", "legacies");
                        if (SelectedModViewer.legaciesListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.legaciesListView.Groups.Add(listViewGroup);
                        }
                        Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                        deserializedLegacy.filename = "legacies";
                        SelectedModViewer.Content.Legacies[guid] = deserializedLegacy;
                        if (!SelectedModViewer.legaciesListView.Items.ContainsKey(deserializedLegacy.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedLegacy.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.legaciesListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Ending":
                        listViewGroup = SelectedModViewer.endingsListView.Groups["endings"] ?? new ListViewGroup("endings", "endings");
                        if (SelectedModViewer.endingsListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.endingsListView.Groups.Add(listViewGroup);
                        }
                        Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                        deserializedEnding.filename = "endings";
                        SelectedModViewer.Content.Endings[guid] = deserializedEnding;
                        if (!SelectedModViewer.endingsListView.Items.ContainsKey(deserializedEnding.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedEnding.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.endingsListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    case "Verb":
                        listViewGroup = SelectedModViewer.verbsListView.Groups["verbs"] ?? new ListViewGroup("verbs", "verbs");
                        if (SelectedModViewer.verbsListView.Groups.Contains(listViewGroup))
                        {
                            SelectedModViewer.verbsListView.Groups.Add(listViewGroup);
                        }
                        Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                        deserializedVerb.filename = "verbs";
                        SelectedModViewer.Content.Verbs[guid] = deserializedVerb;
                        if (!SelectedModViewer.verbsListView.Items.ContainsKey(deserializedVerb.id))
                        {
                            ListViewItem item = new ListViewItem(deserializedVerb.id) { Tag = guid, Group = listViewGroup };
                            SelectedModViewer.verbsListView.Items.Add(item);
                            // listViewGroup.Items.Add(item);
                        }
                        break;
                    default:
                        MessageBox.Show("I'm not sure what you selected or how, but that was an invalid choice.", "Unknown Object Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
        }

        private void SummonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            SummonCreator sc = new SummonCreator();
            if (sc.ShowDialog() == DialogResult.OK)
            {
                ListViewGroup defaultElementsGroup = SelectedModViewer.elementsListView.Groups["elements"] ?? new ListViewGroup("elements", "elements");
                if (!SelectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    SelectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid baseGuid = Guid.NewGuid();
                ListViewItem baseSummon = new ListViewItem(sc.baseSummon.id) { Tag = baseGuid, Group = defaultElementsGroup };
                SelectedModViewer.elementsListView.Items.Add(baseSummon);
                sc.baseSummon.filename = "elements";
                SelectedModViewer.Content.Elements.Add(baseGuid, sc.baseSummon.Copy());

                Guid preGuid = Guid.NewGuid();
                ListViewItem preSummon = new ListViewItem(sc.preSummon.id) { Tag = preGuid, Group = defaultElementsGroup };
                SelectedModViewer.elementsListView.Items.Add(preSummon);
                sc.preSummon.filename = "elements";
                SelectedModViewer.Content.Elements.Add(preGuid, sc.preSummon.Copy());

                ListViewGroup defaultRecipesGroup = SelectedModViewer.elementsListView.Groups["recipes"] ?? new ListViewGroup("recipes", "recipes");
                if (!SelectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    SelectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid startGuid = Guid.NewGuid();
                ListViewItem startSummon = new ListViewItem(sc.startSummon.id) { Tag = startGuid, Group = defaultRecipesGroup };
                SelectedModViewer.recipesListView.Items.Add(startSummon);
                sc.startSummon.filename = "recipes";
                SelectedModViewer.Content.Recipes.Add(startGuid, sc.startSummon.Copy());

                Guid succeedGuid = Guid.NewGuid();
                ListViewItem succeedSummon = new ListViewItem(sc.succeedSummon.id) { Tag = succeedGuid, Group = defaultRecipesGroup };
                SelectedModViewer.recipesListView.Items.Add(succeedSummon);
                sc.succeedSummon.filename = "recipes";
                SelectedModViewer.Content.Recipes.Add(succeedGuid, sc.succeedSummon.Copy());
            }
        }

        private void ImageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                return;
            }
            ImageImporter ii = new ImageImporter();
            if (ii.ShowDialog() == DialogResult.OK)
            {
                switch (ii.displayedImageType.ToLower())
                {
                    case "aspect":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\aspects\\" + ii.displayedFileName, true);
                        break;
                    case "element":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\elements\\" + ii.displayedFileName, true);
                        break;
                    case "ending":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\endings\\" + ii.displayedFileName, true);
                        break;
                    case "legacy":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\legacies\\" + ii.displayedFileName, true);
                        break;
                    case "verb":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\verbs\\" + ii.displayedFileName, true);
                        break;
                }
                MessageBox.Show("Imported " + ii.displayedImageType + " image.");
            }
        }

        private void JSONCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonCleaner jc = new JsonCleaner();
            jc.ShowDialog();
        }

        private void SaveSplitterLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool @checked = saveSplitterLocationsToolStripMenuItem.Checked;
            saveSplitterLocationsToolStripMenuItem.Checked = !@checked;
            if (!@checked)
            {
                if (!SelectedModViewer.isVanilla)
                {
                    SelectedModViewer.Content.SetCustomManifestProperty("saveWidths", !@checked);
                    SelectedModViewer.Content.SetCustomManifestProperty("widths", new List<int>() {
                        SelectedModViewer.tableLayoutPanel2.Size.Width,
                        SelectedModViewer.tableLayoutPanel3.Size.Width,
                        SelectedModViewer.tableLayoutPanel4.Size.Width,
                        SelectedModViewer.tableLayoutPanel5.Size.Width,
                        SelectedModViewer.tableLayoutPanel6.Size.Width,
                        SelectedModViewer.tableLayoutPanel7.Size.Width,
                        SelectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                }
                else
                {
                    Settings.settings["saveWidths"] = !@checked;
                    Settings.settings["widths"] = JToken.FromObject(new List<int>() {
                        SelectedModViewer.tableLayoutPanel2.Size.Width,
                        SelectedModViewer.tableLayoutPanel3.Size.Width,
                        SelectedModViewer.tableLayoutPanel4.Size.Width,
                        SelectedModViewer.tableLayoutPanel5.Size.Width,
                        SelectedModViewer.tableLayoutPanel6.Size.Width,
                        SelectedModViewer.tableLayoutPanel7.Size.Width,
                        SelectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                    Settings.SaveSettings();
                }

            }
            else
            {
                if (!SelectedModViewer.isVanilla)
                {
                    SelectedModViewer.Content.CustomManifest.Remove("saveWidths");
                }
                else
                {
                    Settings.settings.Remove("saveWidths");
                    Settings.SaveSettings();
                }
            }
            SelectedModViewer.SaveManifests(SelectedModViewer.Content.currentDirectory);
        }

        private void CulturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CulturesViewer cv = new CulturesViewer(SelectedModViewer.Content.Cultures, SelectedModViewer.editMode);
            if (cv.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.Content.Cultures = cv.displayedCultures.ToDictionary(entry => Guid.NewGuid(), entry => entry.Value.Copy());
            }
        }

        private void AssetBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssetBrowser ab = new AssetBrowser();
            ab.Show();
        }

        private void TemplateManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager();
            templateManager.Show();
        }

        private void AboutToolStripButton_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void ModViewerTabs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ModViewerTabs_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string path in files)
                {
                    if (Path.HasExtension(path))
                    {   // We are only interested in paths, if there's an extension then it's a file so we'll skip it
                        continue;
                    }
                    // Then we check for a synopsis or a manifest to see if we're actually looking at a valid mod folder
                    string synopsisPath = Path.Combine(path, "synopsis.json");
                    string manifestPath = Path.Combine(path, "manifest.json");
                    if (File.Exists(synopsisPath) || File.Exists(manifestPath))
                    {
                        if (Settings.HasPreviousMods())
                        {
                            Settings.AddPreviousMod(path);
                        }
                        else
                        {
                            Settings.InitPreviousMods(path);
                        }
                        CreateNewModViewerTab(path, false, false);
                    }
                    else
                    {
                        MessageBox.Show("There is not a 'synopsis.json' file in this folder.");
                    }
                }
            }
        }

        private void OpenInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "Explorer.exe",
                Arguments = SelectedModViewer.Content.currentDirectory
            };
            Process.Start(startInfo);
        }

        public void MarkTabDirty(object modViewerTab, bool v)
        {
            TabPage tabPage = (TabPage)((ModViewerTabControl)modViewerTab).Parent;
            tabPage.Text = v ? tabPage.Name + "*" : tabPage.Name;
        }

        private void ModViewerTabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                // Thanks, Samuel from StackOverflow for this 12 year old solution
                TabPage tab = ModViewerTabs.TabPages.Cast<TabPage>().Where((t, i) => ModViewerTabs.GetTabRect(i).Contains(e.Location)).First();
                // Selecting a tab fires an event handler that'll update SelectedModViewer, so we can just use that variable
                ModViewerTabs.SelectTab(tab);
                // TODO deduplicate the code below
                if (SelectedModViewer.isVanilla)
                {
                    MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (SelectedModViewer.IsDirty && SelectedModViewer.editMode)
                {
                    if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and close the mod.",
                        "You have unsaved changes",
                        MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                }
                Utilities.ContentSources.Remove(SelectedModViewer.Content.GetName());
                Settings.RemovePreviousMod(SelectedModViewer.Content.currentDirectory);
                SelectedModViewer.Dispose();
                ModViewerTabs.TabPages.Remove(tab);
            }
        }

        private void aspectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("aspects");
        }

        private void elementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("elements");
        }

        private void recipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("recipes");
        }

        private void decksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("decks");
        }

        private void legaciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("legacies");
        }

        private void endingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("endings");
        }

        private void verbsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.Content.ResetHiddenGroups("verbs");
		}
		
        private void TabbedModViewer_Shown(object sender, EventArgs e)
        {
            if (Settings.settings["GamePath"] == null)
            {
                // If installed in the game's folder, save the user the hassle and just use that install
                if (File.Exists("cultistsimulator.exe"))
                {
                    Settings.settings["GamePath"] = AppDomain.CurrentDomain.BaseDirectory;
                    Settings.settings["portable"] = false;
                    Settings.SaveSettings();
                    InitializeTabs();
                }
                else
                {
                    // Otherwise, make them select the game's installation folder
                    MessageBox.Show("Please select your Cultist Simulator game directory.");
                    folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Check to see if the game's actually installed there
                        if (File.Exists(folderBrowserDialog.SelectedPath + "/cultistsimulator.exe"))
                        {
                            Settings.settings["portable"] = true;
                            Settings.settings["GamePath"] = folderBrowserDialog.SelectedPath;
                            Settings.SaveSettings();
                            InitializeTabs();
                        }
                        else
                        {
                            MessageBox.Show("cultistsimulator.exe not found in that folder, please select your install folder.", "Wrong Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        // Didn't open the games folder
                        MessageBox.Show("No directory selected, exiting.");
                        Application.Exit();
                        return;
                    }
                }
            }
            else
            {
                InitializeTabs();
            }
        }
    }
}