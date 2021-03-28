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
        private ModViewerTabControl selectedModViewer;

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
                if (((JArray)Settings.settings["previousMods"]).Any())
                {
                    foreach (string path in Settings.GetPreviousMods())
                    {
                        CreateNewModViewerTab(path, false, false);
                    }
                }
            }
            if (!selectedModViewer.IsVanilla)
            {

            }
            toggleEditModeToolStripMenuItem.Checked = !selectedModViewer.IsVanilla && selectedModViewer.EditMode;
            saveSplitterLocationsToolStripMenuItem.Checked = !selectedModViewer.IsVanilla && (selectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);
        }
        private void CreateNewModViewerTab(string folder, bool isVanilla, bool newMod)
        {
            try
            {
                selectedModViewer = new ModViewerTabControl(folder, isVanilla, newMod);
                selectedModViewer.MarkDirtyEventHandler += MarkTabDirty;
                TabPage newPage = new TabPage(selectedModViewer.Content.GetName());
                newPage.Controls.Add(selectedModViewer);
                newPage.Name = selectedModViewer.Content.GetName();
                ModViewerTabs.TabPages.Add(newPage);
                ModViewerTabs.SelectTab(newPage);
            }
            catch
            {

            }
        }

        private void CreateNewModViewerTab(ModViewerTabControl mvtc)
        {
            selectedModViewer = mvtc;
            TabPage newPage = new TabPage(mvtc.Content.GetName());
            newPage.Controls.Add(mvtc);
            newPage.Name = mvtc.Content.GetName();
            ModViewerTabs.TabPages.Add(newPage);
            ModViewerTabs.SelectTab(newPage);
        }

        private void CloseMod()
        {
            if (selectedModViewer.IsVanilla)
            {
                MessageBox.Show("Carcass Spark will not close Vanilla content.", "Close Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedModViewer.IsDirty && selectedModViewer.EditMode)
            {
                if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and close the mod.",
                    "You have unsaved changes",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            Utilities.ContentSources.Remove(selectedModViewer.Content.ToString());
            Settings.RemovePreviousMod(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.Dispose();
            ModViewerTabs.TabPages.Remove(ModViewerTabs.SelectedTab);
        }

        private void OpenModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = (Settings.settings["previousMods"] != null && Settings.settings["previousMods"].Any()) ? Settings.settings["previousMods"].Last?.ToString() : AppDomain.CurrentDomain.BaseDirectory;
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
            CloseMod();
        }

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void SaveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                MessageBox.Show("Can't save vanilla content.");
                return;
            }
            selectedModViewer.SaveMod();
        }

        private void ModViewerTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModViewerTabs.SelectedTab == null)
            {
                selectedModViewer = null;
                return;
            }
            selectedModViewer = (ModViewerTabControl)ModViewerTabs.SelectedTab.Controls[0];

            toggleEditModeToolStripMenuItem.Checked = !selectedModViewer.IsVanilla && selectedModViewer.EditMode;
            toggleAutosaveToolStripMenuItem.Checked = !selectedModViewer.IsVanilla && (selectedModViewer.Content.GetCustomManifestBool("AutoSave") ?? false);
            saveSplitterLocationsToolStripMenuItem.Checked = selectedModViewer.IsVanilla ? (Settings.settings.ContainsKey("saveWidths") && Settings.settings["saveWidths"].ToObject<bool>()) : (selectedModViewer.Content.GetCustomManifestBool("saveWidths") ?? false);

            toggleEditModeToolStripMenuItem.Enabled = !selectedModViewer.IsVanilla;
            toggleAutosaveToolStripMenuItem.Enabled = !selectedModViewer.IsVanilla;
        }

        private void SaveModToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = selectedModViewer.Content.CurrentDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedModViewer.SaveMod(folderBrowserDialog.SelectedPath);
            }
        }

        private void OpenSynopsisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                MessageBox.Show("There is no synopsis for vanilla content.");
                return;
            }

            SynopsisViewer mv = new SynopsisViewer(selectedModViewer.Content.Synopsis.Copy());
            if (mv.ShowDialog() == DialogResult.OK)
            {
                if (selectedModViewer.Content.GetName() != mv.DisplayedSynopsis.name)
                {
                    selectedModViewer.Parent.Name = mv.DisplayedSynopsis.name;
                }
                selectedModViewer.Content.Synopsis = mv.DisplayedSynopsis.Copy();
                selectedModViewer.SaveMod();
            }
        }

        private void ReloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.LoadContent();
        }

        private void ToggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                MessageBox.Show("Can't enable Edit Mode for vanilla content.");
                return;
            }
            toggleEditModeToolStripMenuItem.Checked = !toggleEditModeToolStripMenuItem.Checked;
            selectedModViewer.Content.SetEditMode(toggleEditModeToolStripMenuItem.Checked);
            selectedModViewer.SetEditingMode(toggleEditModeToolStripMenuItem.Checked);
        }

        private void ToggleAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                MessageBox.Show("Can't enable Autosave for vanilla content.");
                return;
            }
            toggleAutosaveToolStripMenuItem.Checked = !toggleAutosaveToolStripMenuItem.Checked;
            selectedModViewer.Content.SetCustomManifestProperty("AutoSave", toggleAutosaveToolStripMenuItem.Checked);
        }

        #region "Create New" events

        private void AspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Aspect>(selectedModViewer.AspectsList_Add);
        }

        private void ElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Element>(selectedModViewer.ElementsList_Add);
        }

        private void RecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Recipe>(selectedModViewer.RecipesList_Add);
        }

        private void DeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Deck>(selectedModViewer.DecksList_Add);
        }

        private void LegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Legacy>(selectedModViewer.LegaciesList_Add);
        }

        private void EndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Ending>(selectedModViewer.EndingsList_Add);
        }

        private void VerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGameObject<Verb>(selectedModViewer.VerbsList_Add);
        }

        private void CreateNewGameObject<T>(EventHandler<T> successCallback) where T: IGameObject, new()
        {
            if(!selectedModViewer.IsVanilla)
            {
                IGameObjectViewer gov = Utilities.GetViewer(new T(), successCallback);
                gov.Show();
            }
        }

        #endregion
        #region "Import" events

        private void AspectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Aspects);
        }

        private void ElementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Elements);
        }

        private void RecipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Recipes);
        }

        private void DeckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Decks);
        }

        private void LegacyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Legacies);
        }

        private void EndingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Endings);
        }

        private void VerbToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportGameObject(selectedModViewer.Content.Verbs);
        }

        private void ImportGameObject<T>(ContentGroup<T> cg) where T : IGameObject
        {
            if (selectedModViewer.IsVanilla)
            {
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    T deserializedT = JsonConvert.DeserializeObject<T>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    ListView listView = selectedModViewer.ListViews[cg.DisplayName];
                    if (listView.Items.ContainsKey(deserializedT.ID))
                    {
                        MessageBox.Show(cg.DisplayName + " already exists, overwriting.");
                    }
                    else
                    {
                        ListViewGroup group = listView.Groups[cg.Filename] ?? new ListViewGroup(cg.Filename, cg.Filename);
                        if (!listView.Groups.Contains(group))
                        {
                            listView.Groups.Add(group);
                        }
                        listView.Items.Add(new ListViewItem(deserializedT.ID) { Tag = guid, Group = group });
                    }
                    deserializedT.Filename = cg.Filename;
                    cg[guid] = deserializedT;
                    selectedModViewer.MarkDirty();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing " + cg.DisplayName);
                }
            }
        }

        private void FromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
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
                switch (je.ObjectType)
                {
                    case "Aspect":
                        FromClipboardOnOkay(selectedModViewer.Content.Aspects, je.ObjectText);
                        break;
                    case "Element":
                        FromClipboardOnOkay(selectedModViewer.Content.Elements, je.ObjectText);
                        break;
                    case "Recipe":
                        FromClipboardOnOkay(selectedModViewer.Content.Recipes, je.ObjectText);
                        break;
                    case "Deck":
                        FromClipboardOnOkay(selectedModViewer.Content.Decks, je.ObjectText);
                        break;
                    case "Legacy":
                        FromClipboardOnOkay(selectedModViewer.Content.Legacies, je.ObjectText);
                        break;
                    case "Ending":
                        FromClipboardOnOkay(selectedModViewer.Content.Endings, je.ObjectText);
                        break;
                    case "Verb":
                        FromClipboardOnOkay(selectedModViewer.Content.Verbs, je.ObjectText);
                        break;
                    default:
                        MessageBox.Show("I'm not sure what you selected or how, but that was an invalid choice.", "Unknown Object Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                }
                selectedModViewer.MarkDirty();
            }
        }

        private void FromClipboardOnOkay<T>(ContentGroup<T> cg, string objectText) where T : IGameObject
        {
            Guid guid = Guid.NewGuid();
            ListViewGroup listViewGroup = selectedModViewer.ListViews[cg.Filename].Groups[cg.Filename] ?? new ListViewGroup(cg.Filename, cg.Filename);
            if (selectedModViewer.ListViews[cg.Filename].Groups.Contains(listViewGroup))
            {
                selectedModViewer.ListViews[cg.Filename].Groups.Add(listViewGroup);
            }
            T deserializedGameObject = JsonConvert.DeserializeObject<T>(objectText);
            deserializedGameObject.Filename = cg.Filename;
            cg[guid] = deserializedGameObject;
            if (!selectedModViewer.ListViews[cg.Filename].Items.ContainsKey(deserializedGameObject.ID))
            {
                ListViewItem item = new ListViewItem(deserializedGameObject.ID) { Tag = guid, Group = listViewGroup };
                selectedModViewer.ListViews[cg.Filename].Items.Add(item);
                // listViewGroup.Items.Add(item);
            }
        }

        #endregion

        private void SummonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                return;
            }
            SummonCreator sc = new SummonCreator();
            if (sc.ShowDialog() == DialogResult.OK)
            {
                ListViewGroup defaultElementsGroup = selectedModViewer.elementsListView.Groups["elements"] ?? new ListViewGroup("elements", "elements");
                if (!selectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    selectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid baseGuid = Guid.NewGuid();
                ListViewItem baseSummon = new ListViewItem(sc.BaseSummon.id) { Tag = baseGuid, Group = defaultElementsGroup };
                selectedModViewer.elementsListView.Items.Add(baseSummon);
                sc.BaseSummon.filename = "elements";
                selectedModViewer.Content.Elements.Add(baseGuid, sc.BaseSummon.Copy());

                Guid preGuid = Guid.NewGuid();
                ListViewItem preSummon = new ListViewItem(sc.PreSummon.id) { Tag = preGuid, Group = defaultElementsGroup };
                selectedModViewer.elementsListView.Items.Add(preSummon);
                sc.PreSummon.filename = "elements";
                selectedModViewer.Content.Elements.Add(preGuid, sc.PreSummon.Copy());

                ListViewGroup defaultRecipesGroup = selectedModViewer.elementsListView.Groups["recipes"] ?? new ListViewGroup("recipes", "recipes");
                if (!selectedModViewer.elementsListView.Groups.Contains(defaultElementsGroup))
                {
                    selectedModViewer.elementsListView.Groups.Add(defaultElementsGroup);
                }

                Guid startGuid = Guid.NewGuid();
                ListViewItem startSummon = new ListViewItem(sc.StartSummon.id) { Tag = startGuid, Group = defaultRecipesGroup };
                selectedModViewer.recipesListView.Items.Add(startSummon);
                sc.StartSummon.filename = "recipes";
                selectedModViewer.Content.Recipes.Add(startGuid, sc.StartSummon.Copy());

                Guid succeedGuid = Guid.NewGuid();
                ListViewItem succeedSummon = new ListViewItem(sc.SucceedSummon.id) { Tag = succeedGuid, Group = defaultRecipesGroup };
                selectedModViewer.recipesListView.Items.Add(succeedSummon);
                sc.SucceedSummon.filename = "recipes";
                selectedModViewer.Content.Recipes.Add(succeedGuid, sc.SucceedSummon.Copy());
            }
        }

        private void ImageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedModViewer.IsVanilla)
            {
                return;
            }
            ImageImporter ii = new ImageImporter();
            if (ii.ShowDialog() == DialogResult.OK)
            {
                switch (ii.DisplayedImageType.ToLower())
                {
                    case "aspect":
                        File.Copy(ii.DisplayedImagePath, selectedModViewer.Content.CurrentDirectory + "\\images\\aspects\\" + ii.DisplayedFileName, true);
                        break;
                    case "element":
                        File.Copy(ii.DisplayedImagePath, selectedModViewer.Content.CurrentDirectory + "\\images\\elements\\" + ii.DisplayedFileName, true);
                        break;
                    case "ending":
                        File.Copy(ii.DisplayedImagePath, selectedModViewer.Content.CurrentDirectory + "\\images\\endings\\" + ii.DisplayedFileName, true);
                        break;
                    case "legacy":
                        File.Copy(ii.DisplayedImagePath, selectedModViewer.Content.CurrentDirectory + "\\images\\legacies\\" + ii.DisplayedFileName, true);
                        break;
                    case "verb":
                        File.Copy(ii.DisplayedImagePath, selectedModViewer.Content.CurrentDirectory + "\\images\\verbs\\" + ii.DisplayedFileName, true);
                        break;
                }
                MessageBox.Show("Imported " + ii.DisplayedImageType + " image.");
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
                if (!selectedModViewer.IsVanilla)
                {
                    selectedModViewer.Content.SetCustomManifestProperty("saveWidths", !@checked);
                    selectedModViewer.Content.SetCustomManifestProperty("widths", new List<int>() {
                        selectedModViewer.tableLayoutPanel2.Size.Width,
                        selectedModViewer.tableLayoutPanel3.Size.Width,
                        selectedModViewer.tableLayoutPanel4.Size.Width,
                        selectedModViewer.tableLayoutPanel5.Size.Width,
                        selectedModViewer.tableLayoutPanel6.Size.Width,
                        selectedModViewer.tableLayoutPanel7.Size.Width,
                        selectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                }
                else
                {
                    Settings.settings["saveWidths"] = !@checked;
                    Settings.settings["widths"] = JToken.FromObject(new List<int>() {
                        selectedModViewer.tableLayoutPanel2.Size.Width,
                        selectedModViewer.tableLayoutPanel3.Size.Width,
                        selectedModViewer.tableLayoutPanel4.Size.Width,
                        selectedModViewer.tableLayoutPanel5.Size.Width,
                        selectedModViewer.tableLayoutPanel6.Size.Width,
                        selectedModViewer.tableLayoutPanel7.Size.Width,
                        selectedModViewer.tableLayoutPanel8.Size.Width,
                    });
                    Settings.SaveSettings();
                }

            }
            else
            {
                if (!selectedModViewer.IsVanilla)
                {
                    selectedModViewer.Content.GetCustomManifest().Remove("saveWidths");
                }
                else
                {
                    Settings.settings.Remove("saveWidths");
                    Settings.SaveSettings();
                }
            }
            selectedModViewer.SaveManifests(selectedModViewer.Content.CurrentDirectory);
        }

        private void CulturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CulturesViewer cv = new CulturesViewer(selectedModViewer.Content.Cultures.GameObjects, selectedModViewer.EditMode);
            if (cv.ShowDialog() == DialogResult.OK)
            {
                selectedModViewer.Content.Cultures.GameObjects = cv.DisplayedCultures.ToDictionary(entry => Guid.NewGuid(), entry => entry.Value.Copy());
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
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
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
                Arguments = selectedModViewer.Content.CurrentDirectory
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

                CloseMod();
            }
        }

        #region "Unhide Groups" events

        private void AspectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Aspects.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Aspects);
        }

        private void ElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Elements.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Elements);
        }

        private void RecipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Recipes.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Recipes);
        }

        private void DecksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Decks.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Decks);
        }

        private void LegaciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Legacies.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Legacies);
        }

        private void EndingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Endings.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Endings);
        }

        private void VerbsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedModViewer.Content.ResetHiddenGroups(selectedModViewer.Content.Verbs.Filename);
            selectedModViewer.SaveCustomManifest(selectedModViewer.Content.CurrentDirectory);
            selectedModViewer.ReloadListView(selectedModViewer.Content.Verbs);
        }

        #endregion

        private void TabbedModViewer_Shown(object sender, EventArgs e)
        {
            if (Settings.settings["GamePath"] == null)
            {
                // If installed in the game's folder, save the user the hassle and just use that install
                if (File.Exists("cultistsimulator.exe"))
                {
                    Settings.settings["GamePath"] = AppDomain.CurrentDomain.BaseDirectory;
                    Settings.settings["portable"] = false;
                }
                else
                {
                    // Otherwise, make them select the game's installation folder
                    MessageBox.Show("Please select your Cultist Simulator game directory.");
                    folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    {
                        // Didn't open the games folder
                        MessageBox.Show("No directory selected, exiting.");
                        Application.Exit();
                        return;
                    }

                    // Check to see if the game's actually installed there
                    if (!File.Exists(folderBrowserDialog.SelectedPath + "/cultistsimulator.exe"))
                    {
                        MessageBox.Show("cultistsimulator.exe not found in that folder, please select your install folder.", "Wrong Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                    
                    Settings.settings["portable"] = true;
                    Settings.settings["GamePath"] = folderBrowserDialog.SelectedPath;
                }
                Settings.SaveSettings();
            }

            InitializeTabs();
        }

        private void hiddenGroupManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HiddenGroupManager hiddenGroupManager = new HiddenGroupManager(selectedModViewer.Content.GetHiddenGroupsDictionary());
            if (hiddenGroupManager.ShowDialog() == DialogResult.OK)
            {
                selectedModViewer.Content.SetHiddenGroups(hiddenGroupManager.HiddenGroups);
                selectedModViewer.SaveCustomManifest();
                selectedModViewer.LoadContent();
            }
        }
    }
}