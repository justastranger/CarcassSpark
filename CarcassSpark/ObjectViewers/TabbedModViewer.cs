extern alias CultistSimulator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;
using CarcassSpark.DictionaryViewers;
using CarcassSpark.Flowchart;
using CarcassSpark.Tools;
using System.Text.RegularExpressions;

namespace CarcassSpark.ObjectViewers
{
    public partial class TabbedModViewer : Form
    {
        TabPage VanillaTab = new TabPage("Vanilla");

        ModViewerTabControl SelectedModViewer;

        public TabbedModViewer()
        {
            InitializeComponent();
            SelectedModViewer = new ModViewerTabControl(Utilities.directoryToVanillaContent, true, false);
            VanillaTab.Controls.Add(SelectedModViewer);
            ModViewerTabs.TabPages.Add(VanillaTab);
            ModViewerTabs.SelectTab(VanillaTab);
        }

        private void openModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modFolderBrowserDialog.SelectedPath = (Settings.settings["previousMod"] != null) ? Settings.settings["previousMod"].ToString() : AppDomain.CurrentDomain.BaseDirectory;
            if (modFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = modFolderBrowserDialog.SelectedPath;
                ModViewerTabControl mvtc = null;
                try
                {
                    mvtc = new ModViewerTabControl(location, false, false);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Opening Mod");
                }
                if (mvtc != null)
                {
                    TabPage newPage = new TabPage(mvtc.Content.getName());
                    Settings.settings["previousMod"] = mvtc.Content.currentDirectory;
                    newPage.Controls.Add(mvtc);
                    ModViewerTabs.TabPages.Add(newPage);
                    Settings.saveSettings();
                    ModViewerTabs.SelectTab(newPage);
                }
            }
        }

        private void newModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modFolderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            if (modFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = modFolderBrowserDialog.SelectedPath;
                ModViewerTabControl mvtc = new ModViewerTabControl(location, false, true);
                TabPage newPage = new TabPage(mvtc.Content.getName());
                Settings.settings["previousMod"] = mvtc.Content.currentDirectory;
                newPage.Controls.Add(mvtc);
                ModViewerTabs.TabPages.Add(newPage);
                Settings.saveSettings();
                ModViewerTabs.SelectTab(newPage);
            }
        }

        private void closeModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Utilities.ContentSources.Remove(SelectedModViewer.Content.getName());
            ModViewerTabs.TabPages.Remove(ModViewerTabs.SelectedTab);
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void saveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't save vanilla content.");
                return;
            }
            SelectedModViewer.saveMod();
        }

        private void ModViewerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModViewerTabs.SelectedTab == null)
            {
                SelectedModViewer = null;
                return;
            }
            SelectedModViewer = (ModViewerTabControl)ModViewerTabs.SelectedTab.Controls[0];
            toggleEditModeToolStripMenuItem.Checked = SelectedModViewer.editMode;
        }

        private void saveModToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modFolderBrowserDialog.SelectedPath = SelectedModViewer.Content.currentDirectory;
            if (modFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.saveMod(modFolderBrowserDialog.SelectedPath);
            }
        }

        private void openManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla) return;
            ManifestViewer mv = new ManifestViewer(SelectedModViewer.Content.manifest);
            if (mv.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.Content.manifest = mv.displayedManifest;
                SelectedModViewer.saveMod();
            }
        }

        private void reloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedModViewer.refreshContent();
        }

        private void toggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Edit Mode for vanilla content.");
                return;
            }
            toggleEditModeToolStripMenuItem.Checked = !toggleEditModeToolStripMenuItem.Checked;
            SelectedModViewer.Content.CustomManifest["EditMode"] = toggleEditModeToolStripMenuItem.Checked;
            SelectedModViewer.setEditingMode(toggleEditModeToolStripMenuItem.Checked);
        }

        private void toggleAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedModViewer.isVanilla)
            {
                MessageBox.Show("Can't enable Autosave for vanilla content.");
                return;
            }
            toggleAutosaveToolStripMenuItem.Checked = !toggleAutosaveToolStripMenuItem.Checked;
            SelectedModViewer.Content.CustomManifest["AutoSave"] = toggleAutosaveToolStripMenuItem.Checked;
        }

        private void aspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspectViewer av = new AspectViewer(new Aspect(), SelectedModViewer.aspectsList_Add);
            av.Show();
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), SelectedModViewer.elementsList_Add);
            ev.Show();
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), SelectedModViewer.recipesList_Add);
            rv.Show();
        }

        private void deckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), SelectedModViewer.decksList_Add);
            dv.Show();
        }

        private void legacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), SelectedModViewer.legaciesList_Add);
            lv.Show();
        }

        private void endingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), SelectedModViewer.endingsList_Add);
            ev.Show();
        }

        private void verbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), SelectedModViewer.verbsList_Add);
            vv.Show();
        }

        private void aspectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.aspectsListBox.Items.Contains(deserializedAspect.id))
                    {
                        MessageBox.Show("Aspect already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.aspectsListBox.Items.Add(deserializedAspect.id);
                    }
                    SelectedModViewer.Content.Aspects[deserializedAspect.id] = deserializedAspect;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Aspect");
                }
            }
        }

        private void elementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Element deserializedElement = JsonConvert.DeserializeObject<Element>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.elementsListBox.Items.Contains(deserializedElement.id))
                    {
                        MessageBox.Show("Element already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.elementsListBox.Items.Add(deserializedElement.id);
                    }
                    SelectedModViewer.Content.Elements[deserializedElement.id] = deserializedElement;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Element");
                }
            }
        }

        private void recipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.recipesListBox.Items.Contains(deserializedRecipe.id))
                    {
                        MessageBox.Show("Recipe already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.recipesListBox.Items.Add(deserializedRecipe.id);
                    }
                    SelectedModViewer.Content.Recipes[deserializedRecipe.id] = deserializedRecipe;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Recipe");
                }
            }
        }

        private void deckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.decksListBox.Items.Contains(deserializedDeck.id))
                    {
                        MessageBox.Show("Deck already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.decksListBox.Items.Add(deserializedDeck.id);
                    }
                    SelectedModViewer.Content.Decks[deserializedDeck.id] = deserializedDeck;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Deck");
                }
            }
        }

        private void legacyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.legaciesListBox.Items.Contains(deserializedLegacy.id))
                    {
                        MessageBox.Show("Legacy already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.legaciesListBox.Items.Add(deserializedLegacy.id);
                    }
                    SelectedModViewer.Content.Legacies[deserializedLegacy.id] = deserializedLegacy;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Legacy");
                }
            }
        }

        private void endingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.endingsListBox.Items.Contains(deserializedEnding.id))
                    {
                        MessageBox.Show("Ending already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.endingsListBox.Items.Add(deserializedEnding.id);
                    }
                    SelectedModViewer.Content.Endings[deserializedEnding.id] = deserializedEnding;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Ending");
                }
            }
        }

        private void verbToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (SelectedModViewer.verbsListBox.Items.Contains(deserializedVerb.id))
                    {
                        MessageBox.Show("Verb already exists, overwriting.");
                    }
                    else
                    {
                        SelectedModViewer.verbsListBox.Items.Add(deserializedVerb.id);
                    }
                    SelectedModViewer.Content.Verbs[deserializedVerb.id] = deserializedVerb;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Verb");
                }
            }
        }

        private void fromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText()) return;
            JsonEditor je = new JsonEditor(Clipboard.GetText());
            if (je.ShowDialog() == DialogResult.OK)
            {
                switch (je.objectType)
                {
                    case "Aspect":
                        Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                        SelectedModViewer.Content.Aspects[deserializedAspect.id] = deserializedAspect;
                        if (!SelectedModViewer.aspectsListBox.Items.Contains(deserializedAspect.id))
                        {
                            SelectedModViewer.aspectsListBox.Items.Add(deserializedAspect.id);
                        }
                        break;
                    case "Element":
                        Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                        SelectedModViewer.Content.Elements[deserializedElement.id] = deserializedElement;
                        if (!SelectedModViewer.elementsListBox.Items.Contains(deserializedElement.id))
                        {
                            SelectedModViewer.elementsListBox.Items.Add(deserializedElement.id);
                        }
                        break;
                    case "Recipe":
                        Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                        SelectedModViewer.Content.Recipes[deserializedRecipe.id] = deserializedRecipe;
                        if (!SelectedModViewer.recipesListBox.Items.Contains(deserializedRecipe.id))
                        {
                            SelectedModViewer.recipesListBox.Items.Add(deserializedRecipe.id);
                        }
                        break;
                    case "Deck":
                        Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                        SelectedModViewer.Content.Decks[deserializedDeck.id] = deserializedDeck;
                        if (!SelectedModViewer.decksListBox.Items.Contains(deserializedDeck.id))
                        {
                            SelectedModViewer.decksListBox.Items.Add(deserializedDeck.id);
                        }
                        break;
                    case "Legacy":
                        Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                        SelectedModViewer.Content.Legacies[deserializedLegacy.id] = deserializedLegacy;
                        if (!SelectedModViewer.legaciesListBox.Items.Contains(deserializedLegacy.id))
                        {
                            SelectedModViewer.legaciesListBox.Items.Add(deserializedLegacy.id);
                        }
                        break;
                    case "Ending":
                        Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                        SelectedModViewer.Content.Endings[deserializedEnding.id] = deserializedEnding;
                        if (!SelectedModViewer.endingsListBox.Items.Contains(deserializedEnding.id))
                        {
                            SelectedModViewer.endingsListBox.Items.Add(deserializedEnding.id);
                        }
                        break;
                    case "Verb":
                        Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                        SelectedModViewer.Content.Verbs[deserializedVerb.id] = deserializedVerb;
                        if (!SelectedModViewer.verbsListBox.Items.Contains(deserializedVerb.id))
                        {
                            SelectedModViewer.verbsListBox.Items.Add(deserializedVerb.id);
                        }
                        break;
                    default:
                        MessageBox.Show("I'm not sure what you selected or how, but that was an invalid choice.", "Unknown Object Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
        }

        private void summonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SummonCreator sc = new SummonCreator();
            if (sc.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.elementsListBox.Items.Add(sc.baseSummon.id);
                SelectedModViewer.Content.Elements.Add(sc.baseSummon.id, sc.baseSummon);

                SelectedModViewer.elementsListBox.Items.Add(sc.preSummon.id);
                SelectedModViewer.Content.Elements.Add(sc.preSummon.id, sc.preSummon);

                SelectedModViewer.recipesListBox.Items.Add(sc.startSummon.id);
                SelectedModViewer.Content.Recipes.Add(sc.startSummon.id, sc.startSummon);

                SelectedModViewer.recipesListBox.Items.Add(sc.succeedSummon.id);
                SelectedModViewer.Content.Recipes.Add(sc.succeedSummon.id, sc.succeedSummon);
            }
        }

        private void imageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageImporter ii = new ImageImporter();
            if (ii.ShowDialog() == DialogResult.OK)
            {
                switch (ii.displayedImageType.ToLower())
                {
                    case "aspect":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\icons40\\aspects\\" + ii.displayedFileName, true);
                        break;
                    case "element":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\elementArt\\" + ii.displayedFileName, true);
                        break;
                    case "ending":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\endingArt\\" + ii.displayedFileName, true);
                        break;
                    case "legacy":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\icons100\\legacies\\" + ii.displayedFileName, true);
                        break;
                    case "verb":
                        File.Copy(ii.displayedImagePath, SelectedModViewer.Content.currentDirectory + "\\images\\icons100\\verbs\\" + ii.displayedFileName, true);
                        break;
                }
                MessageBox.Show("Imported " + ii.displayedImageType + " image.");
            }
        }
    }
}