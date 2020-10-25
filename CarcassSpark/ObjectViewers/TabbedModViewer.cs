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
        ModViewerTabControl SelectedModViewer;

        public TabbedModViewer()
        {
            // this should always be first before messing with any components like the Folder Browser Dialog
            InitializeComponent();
            // This is necessary to ensure we have a reference point for the game and the game's assemblies
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

        private void InitializeTabs()
        {
            CreateNewModViewerTab(Utilities.DirectoryToVanillaContent, true, false);
            if (Settings.settings["previousMods"] != null && Settings.settings["loadPreviousMods"] != null && Settings.settings["loadPreviousMods"].ToObject<bool>())
            {
                if (((JArray)Settings.settings["previousMods"]).Count() > 0)
                    foreach (string path in Settings.GetPreviousMods())
                    { 
                        CreateNewModViewerTab(path, false, false);
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
            SelectedModViewer = new ModViewerTabControl(folder, isVanilla, newMod);
            if (SelectedModViewer.valid)
            {
                TabPage newPage = new TabPage(SelectedModViewer.Content.GetName());
                newPage.Controls.Add(SelectedModViewer);
                ModViewerTabs.TabPages.Add(newPage);
                ModViewerTabs.SelectTab(newPage);
            } 
            else
            {
                SelectedModViewer.Dispose();
            }
        }

        private void CreateNewModViewerTab(ModViewerTabControl mvtc)
        {
            SelectedModViewer = mvtc;
            TabPage newPage = new TabPage(SelectedModViewer.Content.GetName());
            newPage.Controls.Add(SelectedModViewer);
            ModViewerTabs.TabPages.Add(newPage);
            ModViewerTabs.SelectTab(newPage);
        }

        private void OpenModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = (Settings.settings["previousMods"] != null && Settings.settings["previousMods"].Count() > 0) ? Settings.settings["previousMods"].Last.ToString() : AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                ModViewerTabControl mvtc = null;
                try
                {
                    mvtc = new ModViewerTabControl(location, false, false);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Opening Mod");
                }
                if (mvtc != null && mvtc.valid)
                {
                    CreateNewModViewerTab(mvtc);
                    if (Settings.settings["previousMods"] is JArray array) array.Add(JToken.FromObject(mvtc.Content.currentDirectory));
                    else if (Settings.settings["previousMods"] == null) Settings.settings["previousMods"] = new JArray(mvtc.Content.currentDirectory);
                    Settings.SaveSettings();
                }
            }
        }

        private void NewModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string location = folderBrowserDialog.SelectedPath;
                ModViewerTabControl mvtc = null;
                try
                {
                    mvtc = new ModViewerTabControl(location, false, true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Creating Mod");
                }
                if (mvtc != null && mvtc.valid)
                {
                    CreateNewModViewerTab(mvtc);
                    if (Settings.settings["previousMods"] is JArray array) array.Add(JToken.FromObject(mvtc.Content.currentDirectory));
                    else if (Settings.settings["previousMods"] == null) Settings.settings["previousMods"] = new JArray(mvtc.Content.currentDirectory);
                    Settings.SaveSettings();
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
            if (SelectedModViewer.isVanilla) return;
            SynopsisViewer mv = new SynopsisViewer(SelectedModViewer.Content.synopsis);
            if (mv.ShowDialog() == DialogResult.OK)
            {
                SelectedModViewer.Content.synopsis = mv.displayedSynopsis;
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
            AspectViewer av = new AspectViewer(new Aspect(), SelectedModViewer.AspectsList_Add);
            av.Show();
        }

        private void ElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), SelectedModViewer.ElementsList_Add);
            ev.Show();
        }

        private void RecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), SelectedModViewer.RecipesList_Add);
            rv.Show();
        }

        private void DeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), SelectedModViewer.DecksList_Add);
            dv.Show();
        }

        private void LegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), SelectedModViewer.LegaciesList_Add);
            lv.Show();
        }

        private void EndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), SelectedModViewer.EndingsList_Add);
            ev.Show();
        }

        private void VerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), SelectedModViewer.VerbsList_Add);
            vv.Show();
        }

        private void AspectToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void ElementToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void RecipeToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void DeckToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void LegacyToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void EndingToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void VerbToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void FromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void SummonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ImageImporterToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void JSONCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JsonCleaner jc = new JsonCleaner();
            jc.ShowDialog();
        }

        private void saveSplitterLocationsToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}