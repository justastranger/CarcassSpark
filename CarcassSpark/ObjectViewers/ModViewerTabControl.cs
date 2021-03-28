using CarcassSpark.DictionaryViewers;
using CarcassSpark.Flowchart;
using CarcassSpark.ObjectTypes;
using CarcassSpark.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CarcassSpark.ObjectViewers
{
    public partial class ModViewerTabControl : UserControl
    {
        public bool IsVanilla, EditMode, IsValid = false;

        public EventHandler<bool> MarkDirtyEventHandler;

        public bool IsDirty { get; private set; } = false;
        public Dictionary<string, ListView> ListViews { get; set; } = new Dictionary<string, ListView>();

        public ContentSource Content = new ContentSource();

        public ModViewerTabControl(string location, bool isVanilla, bool newMod)
        {
            InitializeComponent();

            ListViews["aspects"] = aspectsListView;
            ListViews["elements"] = elementsListView;
            ListViews["recipes"] = recipesListView;
            ListViews["decks"] = decksListView;
            ListViews["legacies"] = legaciesListView;
            ListViews["endings"] = endingsListView;
            ListViews["verbs"] = verbsListView;

            Dock = DockStyle.Fill;
            Content.CurrentDirectory = location;
            saveFileDialog.InitialDirectory = location;
            openFileDialog.InitialDirectory = location;
            this.IsVanilla = isVanilla;
            if (!isVanilla && newMod)
            {
                // if the user cancels the synopsis creation for a new mod, invalidate the control and abort loading.
                if (!CreateSynopsis())
                {
                    throw new Exception("New Mod Canceled");
                }
            }
            // if loading content is successful
            if (LoadContent())
            {
                SetEditingMode(Content.GetCustomManifestBool("EditMode") ?? !isVanilla);
                Utilities.ContentSources.Add(isVanilla ? "Vanilla" : Content.ToString(), Content);
            }
            else
            {
                throw new Exception("Loading Failed");
                // MessageBox.Show("Failed to load content source.");
            }
        }

        public void SetEditingMode(bool editing)
        {
            EditMode = editing;
            deleteSelectedAspectToolStripMenuItem.Enabled = editing;
            deleteSelectedElementToolStripMenuItem.Enabled = editing;
            deleteSelectedRecipeToolStripMenuItem.Enabled = editing;
            deleteSelectedDeckToolStripMenuItem.Enabled = editing;
            deleteSelectedLegacyToolStripMenuItem.Enabled = editing;
            deleteSelectedEndingToolStripMenuItem.Enabled = editing;
            deleteSelectedVerbToolStripMenuItem.Enabled = editing;
            duplicateSelectedAspectToolStripMenuItem.Enabled = editing;
            duplicateSelectedElementToolStripMenuItem.Enabled = editing;
            duplicateSelectedRecipeToolStripMenuItem.Enabled = editing;
            duplicateSelectedDeckToolStripMenuItem.Enabled = editing;
            duplicateSelectedLegacyToolStripMenuItem.Enabled = editing;
            duplicateSelectedEndingToolStripMenuItem.Enabled = editing;
            duplicateSelectedVerbToolStripMenuItem.Enabled = editing;
            newAspectToolStripMenuItem.Enabled = editing;
            newElementToolStripMenuItem.Enabled = editing;
            newRecipeToolStripMenuItem.Enabled = editing;
            newDeckToolStripMenuItem.Enabled = editing;
            newLegacyToolStripMenuItem.Enabled = editing;
            newEndingToolStripMenuItem.Enabled = editing;
            newVerbToolStripMenuItem.Enabled = editing;
            setGroupAspectToolStripMenuItem.Enabled = editing;
            setGroupDeckToolStripMenuItem.Enabled = editing;
            setGroupElementToolStripMenuItem.Enabled = editing;
            setGroupEndingToolStripMenuItem.Enabled = editing;
            setGroupLegacyToolStripMenuItem.Enabled = editing;
            setGroupRecipeToolStripMenuItem.Enabled = editing;
            setGroupVerbToolStripMenuItem.Enabled = editing;
        }

        #region Saving and Loading

        public bool LoadContent()
        {
            if (IsDirty && EditMode)
            {
                if (MessageBox.Show("You WILL lose any unsaved changes you've made. Click OK to discard changes and reload content.",
                    "You have unsaved changes",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return false;
                }
            }

            aspectsListView.Items.Clear();
            Content.Aspects.Clear();
            elementsListView.Items.Clear();
            Content.Elements.Clear();
            recipesListView.Items.Clear();
            Content.Recipes.Clear();
            decksListView.Items.Clear();
            Content.Decks.Clear();
            legaciesListView.Items.Clear();
            Content.Legacies.Clear();
            endingsListView.Items.Clear();
            Content.Endings.Clear();
            verbsListView.Items.Clear();
            Content.Verbs.Clear();
            try
            {
                // If there is no synopsis, try to create one. If no synopsis ends up loaded or created, return false so the tab can be canceled
                if(IsVanilla)
                {
                    Content.Synopsis = new Synopsis("Vanilla", "Weather Factory", null, "Content from Cultist Simulator", null);
                }
                else if(!CheckForSynopsis() || !Directory.Exists(Content.CurrentDirectory + "\\content\\"))
                {
                    return false;
                }

                IEnumerable<string> files = Directory.EnumerateFiles(Content.CurrentDirectory + (IsVanilla ? "" : "\\content\\"), "*.json", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        LoadFile(fs, file);
                    }
                }

                if (!IsVanilla)
                {
                    // mod loaded successfully
                    MarkDirty(false);
                }

                return true;
            }
            // mod failed to load catastrophically
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace, "Content Source Loading Failed");
            }
            return false;
        }

        public bool CreateSynopsis()
        {
            SynopsisViewer mv = new SynopsisViewer(new Synopsis());
            if (mv.ShowDialog() == DialogResult.OK)
            {
                Content.Synopsis = mv.DisplayedSynopsis;
                SaveMod();
                return true;
            }
            return false;
        }

        public bool CheckForSynopsis()
        {
            if (File.Exists(Content.CurrentDirectory + "/CarcassSpark.Manifest.json"))
            {
                using (FileStream fs = new FileStream(Content.CurrentDirectory + "/CarcassSpark.Manifest.json", FileMode.Open))
                {
                    LoadCustomManifest(fs);
                }
            }
            string manifestPath = Content.CurrentDirectory + "/manifest.json";
            string synopsisPath = Content.CurrentDirectory + "/synopsis.json";
            // if manifest.json still exists, load it, save it as synopsis.json, then delete manifest.json
            if (File.Exists(manifestPath))
            {
                using (FileStream fs = new FileStream(manifestPath, FileMode.Open))
                {
                    LoadSynopsis(fs);
                }
                SaveManifests(Content.CurrentDirectory);
                File.Delete(manifestPath);
                return true;
            }
            // otherwise if we have synopsis.json, load that
            else if (File.Exists(synopsisPath))
            {
                using (FileStream fs = new FileStream(synopsisPath, FileMode.Open))
                {
                    LoadSynopsis(fs);
                }
                return true;
            }
            // no manifest so we'll try to make one
            else if (MessageBox.Show("synopsis.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // return true if everything's going good; otherwise, return false so I can abort the creation of the tab
                return CreateSynopsis();
            }
            else
            {
                return false;
            }
        }

        public void LoadSynopsis(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.Synopsis = JsonConvert.DeserializeObject<Synopsis>(new StreamReader(file).ReadToEnd());
            Text = Content.GetName();
        }

        public void LoadCustomManifest(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.SetCustomManifest(JsonConvert.DeserializeObject<JObject>(new StreamReader(file).ReadToEnd()));
        }

        public void LoadWidths()
        {
            List<int> widths;
            if (!IsVanilla)
            {
                widths = Content.GetCustomManifestListInt("widths");
            }
            else
            {   // This part looks way uglier than the part above because I didn't make getter functions for the Settings, only Custom Manifests :(
                widths = Settings.settings.ContainsKey("widths") ? Settings.settings["widths"]?.ToObject<List<int>>() : null;
            }

            if (widths != null)
            {
                tableLayoutPanel2.Size = new Size(widths[0], tableLayoutPanel2.Size.Height);
                tableLayoutPanel3.Size = new Size(widths[1], tableLayoutPanel3.Size.Height);
                tableLayoutPanel4.Size = new Size(widths[2], tableLayoutPanel4.Size.Height);
                tableLayoutPanel5.Size = new Size(widths[3], tableLayoutPanel5.Size.Height);
                tableLayoutPanel6.Size = new Size(widths[4], tableLayoutPanel6.Size.Height);
                tableLayoutPanel7.Size = new Size(widths[5], tableLayoutPanel7.Size.Height);
                tableLayoutPanel8.Size = new Size(widths[6], tableLayoutPanel8.Size.Height);
            }
        }

        public void SaveWidths()
        {
            if (!IsVanilla)
            {
                Content.SetCustomManifestProperty("widths", new List<int>() {
                        tableLayoutPanel2.Size.Width,
                        tableLayoutPanel3.Size.Width,
                        tableLayoutPanel4.Size.Width,
                        tableLayoutPanel5.Size.Width,
                        tableLayoutPanel6.Size.Width,
                        tableLayoutPanel7.Size.Width,
                        tableLayoutPanel8.Size.Width,
                    });
                SaveCustomManifest(Content.CurrentDirectory);
            }
            else
            {
                Settings.settings["widths"] = JToken.FromObject(new List<int>() {
                        tableLayoutPanel2.Size.Width,
                        tableLayoutPanel3.Size.Width,
                        tableLayoutPanel4.Size.Width,
                        tableLayoutPanel5.Size.Width,
                        tableLayoutPanel6.Size.Width,
                        tableLayoutPanel7.Size.Width,
                        tableLayoutPanel8.Size.Width,
                    });
                Settings.SaveSettings();
            }
        }

        public void LoadFile(FileStream file, string filePath)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (!string.IsNullOrEmpty(fileText))
            {
                JToken parsedJToken = JsonConvert.DeserializeObject<JObject>(fileText).First;
                string fileType = parsedJToken.Path;
                bool isGroupHidden = false;

                Dictionary<string, List<string>> hiddenGroups = Content.GetHiddenGroupsDictionary();//CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                if (hiddenGroups != null && fileName != null && hiddenGroups.ContainsKey(fileType) && hiddenGroups[fileType] != null)
                {
                    isGroupHidden = hiddenGroups[fileType].Contains(fileName);
                } // */

                switch (fileType)
                {
                case "elements":
                    bool isAspectHidden = false;
                    if (hiddenGroups != null && hiddenGroups.ContainsKey("aspects") && hiddenGroups["aspects"] != null)
                    {
                        isAspectHidden = hiddenGroups["aspects"].Contains(fileName);
                    }

                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        if (token["xtriggers"] != null)
                        {
                            foreach (var jToken in token["xtriggers"])
                            {
                                var xtrigger = (JProperty) jToken;
                                if (xtrigger.Value as JArray != null)
                                {
                                    xtrigger.Value = xtrigger.Value as JArray;
                                }
                                else if (xtrigger.Value as JObject != null)
                                {
                                    xtrigger.Value = new JArray(xtrigger.Value);
                                }
                                else if (xtrigger.Value.Value<string>() != null)
                                {
                                    xtrigger.Value = JArray.FromObject(new List<XTrigger> { new XTrigger(xtrigger.Value.Value<string>()) });
                                }
                            }
                        }
                        
                        if (token["isAspect"] != null || (token["extends"] != null && Utilities.AspectExists(token["id"].ToString())))
                        {
                            AddItemToContentAndListView(token, Content.Aspects, aspectsListView, fileName, isAspectHidden);
                        }
                        else
                        {
                            AddItemToContentAndListView(token, Content.Elements, elementsListView, fileName, isGroupHidden);
                        }
                    }
                    return;
                case "recipes":
                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        AddItemToContentAndListView(token, Content.Recipes, recipesListView, fileName, isGroupHidden);
                    }
                    return;
                case "decks":
                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        AddItemToContentAndListView(token, Content.Decks, decksListView, fileName, isGroupHidden);
                    }
                    return;
                case "legacies":
                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        AddItemToContentAndListView(token, Content.Legacies, legaciesListView, fileName, isGroupHidden);
                    }
                    return;
                case "endings":
                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        AddItemToContentAndListView(token, Content.Endings, endingsListView, fileName, isGroupHidden);
                    }
                    return;
                case "verbs":
                    foreach (JToken token in parsedJToken.First.ToArray())
                    {
                        AddItemToContentAndListView(token, Content.Verbs, verbsListView, fileName, isGroupHidden);
                    }
                    return;
                case "cultures":
                    foreach (JToken culture in parsedJToken.First.ToArray())
                    {
                        Culture deserializedCulture = culture.ToObject<Culture>();
                        Content.Cultures.Add(deserializedCulture.Guid, deserializedCulture);
                    }
                    return;
                // Default case is empty.
                }
            }
        }

        private void AddItemToContentAndListView<T>(JToken token, ContentGroup<T> contentGroup, ListView listView, string fileName, bool isGroupHidden) where T : IGameObject
        {
            T deserializedObject = token.ToObject<T>();
            contentGroup.Add(deserializedObject.Guid, deserializedObject);
            if (!isGroupHidden)
            {
                ListViewItem lviCurr = new ListViewItem(deserializedObject.ID)
                {
                    Tag = deserializedObject.Guid,
                    Name = deserializedObject.ID
                };
                listView.Items.Add(lviCurr);
                if (listView.Groups[fileName] == null)
                {
                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                    listView.Groups.Add(listViewGroup);
                    lviCurr.Group = listViewGroup;
                }
                else
                {
                    lviCurr.Group = listView.Groups[fileName];
                }
            }
            deserializedObject.Filename = fileName;
        }

        private void CreateDirectories(string modLocation)
        {
            if (!Directory.Exists(modLocation + "/content/"))
            {
                Directory.CreateDirectory(modLocation + "/content/");
            }
            if (!Directory.Exists(modLocation + "/images/elements/"))
            {
                Directory.CreateDirectory(modLocation + "/images/elements/");
            }
            if (!Directory.Exists(modLocation + "/images/elements/anim/"))
            {
                Directory.CreateDirectory(modLocation + "/images/elements/anim/");
            }
            if (!Directory.Exists(modLocation + "/images/endings/"))
            {
                Directory.CreateDirectory(modLocation + "/images/endings/");
            }
            if (!Directory.Exists(modLocation + "/images/aspects/"))
            {
                Directory.CreateDirectory(modLocation + "/images/aspects/");
            }
            if (!Directory.Exists(modLocation + "/images/legacies/"))
            {
                Directory.CreateDirectory(modLocation + "/images/legacies/");
            }
            if (!Directory.Exists(modLocation + "/images/verbs/"))
            {
                Directory.CreateDirectory(modLocation + "/images/verbs/");
            }
            if (!Directory.Exists(modLocation + "/images/verbs/anim/"))
            {
                Directory.CreateDirectory(modLocation + "/images/verbs/anim/");
            }
            if (!Directory.Exists(modLocation + "/images/statusbaricons/"))
            {
                Directory.CreateDirectory(modLocation + "/images/statusbaricons/");
            }
            if (!Directory.Exists(modLocation + "/images/ui/"))
            {
                Directory.CreateDirectory(modLocation + "/images/ui/");
            }
        }

        private void ClearContentFolder(string modLocation)
        {
            foreach (string file in Directory.EnumerateFiles(modLocation + "/content/", "*.json"))
            {
                File.Delete(file);
            }
        }

        public void SaveMod()
        {
            SaveMod(Content.CurrentDirectory);
        }

        public void SaveMod(string location)
        {
            CreateDirectories(location);
            ClearContentFolder(location);
            SaveType(Content.Aspects, "elements", location);
            SaveType(Content.Elements, "elements", location);
            SaveType(Content.Recipes, "recipes", location);
            SaveType(Content.Decks, "decks", location);
            SaveType(Content.Legacies, "legacies", location);
            SaveType(Content.Endings, "endings", location);
            SaveType(Content.Verbs, "verbs", location);
            if (Content.Cultures.Count > 0)
            {
                // TODO fix this, I'm *pretty* sure it doesn't save cultures correctly. It looks like they need to be saved individually, in subfolders, with a blank json file named after the culture's ID?
                JObject cultures = new JObject
                {
                    ["cultures"] = JArray.FromObject(Content.Cultures.Values)
                };
                string culturesJson = JsonConvert.SerializeObject(cultures, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/cultures.json", FileMode.Create))))
                {
                    jtw.WriteRaw(culturesJson);
                }
            }
            SaveManifests(location);
            MarkDirty(false);
        }

        private void SaveType<T>(ContentGroup<T> contentGroup, string gameType, string location) where T : IGameObject
        {
            if (contentGroup.Count > 0)
            {
                Dictionary<string, List<T>> sortedGameObjects = new Dictionary<string, List<T>>();

                foreach (T gameObject in contentGroup.Values)
                {
                    if (!sortedGameObjects.ContainsKey(gameObject.Filename))
                    {
                        sortedGameObjects[gameObject.Filename] = new List<T>();
                    }
                    sortedGameObjects[gameObject.Filename].Add(gameObject);
                }

                foreach (KeyValuePair<string, List<T>> keyValuePair in sortedGameObjects)
                {
                    JObject gameObjects = new JObject
                    {
                        [gameType] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedGameObjects = JsonConvert.SerializeObject(gameObjects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedGameObjects);
                    }
                }
            }
        }

        public void SaveManifests(string location)
        {
            if (IsVanilla)
            {
                return;
            }

            string synopsisJson = JsonConvert.SerializeObject(Content.Synopsis, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/synopsis.json", FileMode.Create))))
            {
                jtw.WriteRaw(synopsisJson);
            }
            SaveCustomManifest(location);
        }

        public void SaveCustomManifest()
        {
            SaveCustomManifest(Content.CurrentDirectory);
        }

        public void SaveCustomManifest(string location)
        {
            if (IsVanilla)
            {
                return;
            }

            if (Content.GetCustomManifest().Count > 0)
            {
                string customManifestJson = JsonConvert.SerializeObject(Content.GetCustomManifest(), Formatting.Indented);
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/CarcassSpark.Manifest.json", FileMode.Create))))
                {
                    jtw.WriteRaw(customManifestJson);
                }
            }
            else if (File.Exists(location + "/CarcassSpark.Manifest.json"))
            {
                File.Delete(location + "/CarcassSpark.Manifest.json");
            }
        }

        #endregion
        #region "Double-Click" events

        private void AspectListView_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) aspectsListView.SelectedItems[0].Tag;
                AspectViewer av = new AspectViewer(Content.Aspects.Get(id).Copy(), EditMode ? (EventHandler<Aspect>) AspectsList_Assign : null, aspectsListView.SelectedItems[0]);
                av.Show();
            }
        }

        private void ElementsListView_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) elementsListView.SelectedItems[0].Tag;
                ElementViewer ev = new ElementViewer(Content.Elements.Get(id).Copy(), EditMode ? (EventHandler<Element>) ElementsList_Assign : null, elementsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void RecipesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) recipesListView.SelectedItems[0].Tag;
                RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(id).Copy(), EditMode ? (EventHandler<Recipe>) RecipesList_Assign : null, recipesListView.SelectedItems[0]);
                rv.Show();
            }
        }

        private void DecksListView_DoubleClick(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) decksListView.SelectedItems[0].Tag;
                DeckViewer dv = new DeckViewer(Content.Decks.Get(id).Copy(), EditMode ? (EventHandler<Deck>) DecksList_Assign : null, decksListView.SelectedItems[0]);
                dv.Show();
            }
        }

        private void EndingsListView_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) endingsListView.SelectedItems[0].Tag;
                EndingViewer ev = new EndingViewer(Content.Endings.Get(id).Copy(), EditMode ? (EventHandler<Ending>) EndingsList_Assign : null, endingsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void LegaciesListView_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid) legaciesListView.SelectedItems[0].Tag;
                LegacyViewer lv = new LegacyViewer(Content.Legacies.Get(id).Copy(), EditMode ? (EventHandler<Legacy>) LegaciesList_Assign : null, legaciesListView.SelectedItems[0]);
                lv.Show();
            }
        }

        private void VerbsListView_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count >= 1)
            {
                Guid id = (Guid)verbsListView.SelectedItems[0].Tag;
                VerbViewer vv = new VerbViewer(Content.Verbs.Get(id).Copy(), EditMode ? (EventHandler<Verb>)VerbsList_Assign : null, verbsListView.SelectedItems[0]);
                vv.Show();
            }
        }

        #endregion
        #region "Assign to List" events

        private void AspectsList_Assign(object sender, Aspect result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Aspects, result);
        }

        private void ElementsList_Assign(object sender, Element result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Elements, result);
        }

        private void RecipesList_Assign(object sender, Recipe result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Recipes, result);
        }

        private void DecksList_Assign(object sender, Deck result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Decks, result);
        }

        private void EndingsList_Assign(object sender, Ending result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Endings, result);
        }

        private void LegaciesList_Assign(object sender, Legacy result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Legacies, result);
        }

        private void VerbsList_Assign(object sender, Verb result)
        {
            AssignToList((IGameObjectViewer) sender, Content.Verbs, result);
        }

        private void AssignToList<T>(IGameObjectViewer sender, ContentGroup<T> cg, T result) where T:IGameObject
        {
            T resultCopy = result.Copy<T>();
            if ((Guid)sender.AssociatedListViewItem.Tag != result.Guid)
            {
                cg.Remove((Guid)sender.AssociatedListViewItem.Tag);
                sender.AssociatedListViewItem.Tag = result.Guid;
                resultCopy.Filename = sender.AssociatedListViewItem.Group.Name;
                cg.Add(result.Guid, resultCopy);
            }
            if (sender.AssociatedListViewItem.Text != result.ID)
            {
                sender.AssociatedListViewItem.Text = result.ID;
            }
            MarkDirty();
        }

        #endregion
        #region "Search Text Box" events

        private void AspectsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Aspects, aspectsSearchTextBox.Text, SearchAspects);
        }

        private void ElementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Elements, elementsSearchTextBox.Text, SearchElements);
        }

        private void RecipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Recipes, recipesSearchTextBox.Text, SearchRecipes);
        }

        private void DecksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Decks, decksSearchTextBox.Text, SearchDecks);
        }

        private void LegaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Legacies, legaciesSearchTextBox.Text, SearchLegacies);
        }

        private void EndingsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Endings, endingsSearchTextBox.Text, SearchEndings);
        }

        private void VerbsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged(Content.Verbs, verbsSearchTextBox.Text, SearchVerbs);
        }

        private void SearchTextBox_TextChanged<T>(ContentGroup<T> contentGroup, string newText, Func<List<T>, string, T[]> func) where T : IGameObject
        {
            ListView listView = ListViews[contentGroup.Filename];
            listView.BeginUpdate();
            listView.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            string[] hiddenGroups = Content.GetHiddenGroups(contentGroup.Filename) ?? new string[] { }; //.GetCustomManifest()["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            T[] itemsToAdd = (newText != "") ? func(contentGroup.Values.ToList(), newText) : contentGroup.Values.ToArray();
            foreach (T gameObject in itemsToAdd)
            {
                bool isGroupHidden = hiddenGroups.Contains(gameObject.Filename);
                if (!isGroupHidden)
                {
                    ListViewGroup group = listView.Groups[gameObject.Filename] == null
                        ? new ListViewGroup(gameObject.Filename, gameObject.Filename)
                        : listView.Groups[gameObject.Filename];
                    ListViewItem item = new ListViewItem(gameObject.ID) { Tag = gameObject.Guid, Group = group, Name = gameObject.ID };
                    // group.Items.Add(item);
                    if (!listView.Groups.Contains(group))
                    {
                        listView.Groups.Add(group);
                    }
                    items.Add(item);
                }
            }
            listView.Items.AddRange(items.ToArray());
            listView.EndUpdate();
        }

        private string[] SearchKeys(List<string> keysList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from id in keysList
                    where regex.IsMatch(id)
                    select id).ToArray();
        }

        private Aspect[] SearchAspects(List<Aspect> aspectsList, string searchPattern)
        {
            try
            {

                Regex regex = new Regex(searchPattern);
                return (from aspect in aspectsList.AsParallel()
                        where (aspect.ID != null && regex.IsMatch(aspect.ID))
                           || (aspect.label != null && regex.IsMatch(aspect.label))
                           || (aspect.description != null && regex.IsMatch(aspect.description))
                           || (aspect.comments != null && regex.IsMatch(aspect.comments))
                        select aspect).ToArray();
            }
            catch (ArgumentException)
            {
                return (from aspect in aspectsList.AsParallel()
                        where (aspect.ID != null && aspect.ID.Contains(searchPattern))
                           || (aspect.label != null && aspect.label.Contains(searchPattern))
                           || (aspect.description != null && aspect.description.Contains(searchPattern))
                           || (aspect.comments != null && aspect.comments.Contains(searchPattern))
                        select aspect).ToArray();
            }
        }

        private Element[] SearchElements(List<Element> elementsList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from element in elementsList.AsParallel()
                        where (element.ID != null && regex.IsMatch(element.ID))
                           || (element.label != null && regex.IsMatch(element.label))
                           || (element.comments != null && regex.IsMatch(element.comments))
                        select element).ToArray();
            }
            catch (ArgumentException)
            {
                return (from element in elementsList.AsParallel()
                        where (element.ID != null && element.ID.Contains(searchPattern))
                           || (element.label != null && element.label.Contains(searchPattern))
                           || (element.comments != null && element.comments.Contains(searchPattern))
                        select element).ToArray();
            }

        }

        private Recipe[] SearchRecipes(List<Recipe> recipesList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from recipe in recipesList.AsParallel()
                        where (recipe.ID != null && regex.IsMatch(recipe.ID))
                           || (recipe.label != null && regex.IsMatch(recipe.label))
                           || (recipe.description != null && regex.IsMatch(recipe.description))
                           || (recipe.startdescription != null && regex.IsMatch(recipe.startdescription))
                           || (recipe.comments != null && regex.IsMatch(recipe.comments))
                        select recipe).ToArray();
            }
            catch (ArgumentException)
            {
                return (from recipe in recipesList.AsParallel()
                        where (recipe.ID != null && recipe.ID.Contains(searchPattern))
                           || (recipe.label != null && recipe.label.Contains(searchPattern))
                           || (recipe.description != null && recipe.description.Contains(searchPattern))
                           || (recipe.startdescription != null && recipe.startdescription.Contains(searchPattern))
                           || (recipe.comments != null && recipe.comments.Contains(searchPattern))
                        select recipe).ToArray();
            }

        }

        private Deck[] SearchDecks(List<Deck> decksList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from deck in decksList.AsParallel()
                        where (deck.ID != null && regex.IsMatch(deck.ID))
                           || (deck.label != null && regex.IsMatch(deck.label))
                           || (deck.description != null && regex.IsMatch(deck.description))
                           || (deck.comments != null && regex.IsMatch(deck.comments))
                        select deck).ToArray();
            }
            catch (ArgumentException)
            {
                return (from deck in decksList.AsParallel()
                        where (deck.ID != null && deck.ID.Contains(searchPattern))
                           || (deck.label != null && deck.label.Contains(searchPattern))
                           || (deck.description != null && deck.description.Contains(searchPattern))
                           || (deck.comments != null && deck.comments.Contains(searchPattern))
                        select deck).ToArray();
            }
        }

        private Legacy[] SearchLegacies(List<Legacy> recipesList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from legacy in recipesList.AsParallel()
                        where (legacy.ID != null && regex.IsMatch(legacy.ID))
                           || (legacy.label != null && regex.IsMatch(legacy.label))
                           || (legacy.description != null && regex.IsMatch(legacy.description))
                           || (legacy.startdescription != null && regex.IsMatch(legacy.startdescription))
                           || (legacy.comments != null && regex.IsMatch(legacy.comments))
                        select legacy).ToArray();
            }
            catch (ArgumentException)
            {
                return (from legacy in recipesList.AsParallel()
                        where (legacy.ID != null && legacy.ID.Contains(searchPattern))
                           || (legacy.label != null && legacy.label.Contains(searchPattern))
                           || (legacy.description != null && legacy.description.Contains(searchPattern))
                           || (legacy.startdescription != null && legacy.startdescription.Contains(searchPattern))
                           || (legacy.comments != null && legacy.comments.Contains(searchPattern))
                        select legacy).ToArray();
            }
        }

        private Ending[] SearchEndings(List<Ending> recipesList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from ending in recipesList.AsParallel()
                        where (ending.ID != null && regex.IsMatch(ending.ID))
                           || (ending.label != null && regex.IsMatch(ending.label))
                           || (ending.description != null && regex.IsMatch(ending.description))
                           || (ending.comments != null && regex.IsMatch(ending.comments))
                        select ending).ToArray();
            }
            catch (ArgumentException)
            {
                return (from ending in recipesList.AsParallel()
                        where (ending.ID != null && ending.ID.Contains(searchPattern))
                           || (ending.label != null && ending.label.Contains(searchPattern))
                           || (ending.description != null && ending.description.Contains(searchPattern))
                           || (ending.comments != null && ending.comments.Contains(searchPattern))
                        select ending).ToArray();
            }
        }

        private Verb[] SearchVerbs(List<Verb> recipesList, string searchPattern)
        {
            try
            {
                Regex regex = new Regex(searchPattern);
                return (from verb in recipesList.AsParallel()
                        where (verb.ID != null && regex.IsMatch(verb.ID))
                           || (verb.label != null && regex.IsMatch(verb.label))
                           || (verb.description != null && regex.IsMatch(verb.description))
                           || (verb.comments != null && regex.IsMatch(verb.comments))
                        select verb).ToArray();
            }
            catch (ArgumentException)
            {
                return (from verb in recipesList.AsParallel()
                        where (verb.ID != null && verb.ID.Contains(searchPattern))
                           || (verb.label != null && verb.label.Contains(searchPattern))
                           || (verb.description != null && verb.description.Contains(searchPattern))
                           || (verb.comments != null && verb.comments.Contains(searchPattern))
                        select verb).ToArray();
            }
        }

        #endregion
        #region "Search for..." events

        private void ElementsWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count != 1 || Content.Elements.Count == 0)
            {
                return;
            }

            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.aspects != null && element.aspects.ContainsKey(id))
                {
                    tmp[element.Guid] = element;
                }
                else if (element.aspects_extend != null && element.aspects_extend.ContainsKey(id))
                {
                    tmp[element.Guid] = element;
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatReactWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count != 1 || Content.Elements.Count == 0)
            {
                return;
            }

            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null && element.xtriggers.ContainsKey(id))
                {
                    tmp[element.Guid] = element;
                }
                else if (element.xtriggers_extend != null && element.xtriggers_extend.ContainsKey(id))
                {
                    tmp[element.Guid] = element;
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void RecipesRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null)
                {
                    if (recipe.requirements.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.requirements.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
                else if (recipe.requirements_extend != null)
                {
                    if (recipe.requirements_extend.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.requirements_extend.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
                if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.extantreqs.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
                else if (recipe.extantreqs_extend != null)
                {
                    if (recipe.extantreqs_extend.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.extantreqs_extend.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
                if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.tablereqs.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
                else if (recipe.tablereqs_extend != null)
                {
                    if (recipe.tablereqs_extend.ContainsKey(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                    else if (recipe.tablereqs_extend.ContainsValue(id))
                    {
                        tmp[recipe.Guid] = recipe;
                    }
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatProduceThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.aspects != null && recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.aspects_extend != null && recipe.aspects_extend.ContainsKey(id) && recipe.aspects_extend[id] > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.effects != null && recipe.effects.ContainsKey(id) && Convert.ToInt32(recipe.effects[id]) > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.effects_extend != null && recipe.effects_extend.ContainsKey(id) && Convert.ToInt32(recipe.effects_extend[id]) > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void SlotsRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count != 1 || Content.Elements.Count == 0)
            {
                return;
            }

            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values.Where((element)=>element.HasSlots()))
            {
                if (element.AllSlotsWhere((slot) => slot.required != null && slot.required.ContainsKey(id)).Any())
                {
                    tmp[element.Guid] = element;
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatDecayIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count != 1 || Content.Elements.Count == 0)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.decayTo == id)
                {
                    tmp[element.Guid] = element;
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void ElementsThatXtriggerIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null)
                {
                    foreach (KeyValuePair<string, List<XTrigger>> xtrigger in element.xtriggers)
                    {
                        foreach (XTrigger xtriggereffect in xtrigger.Value)
                        {
                            if (xtriggereffect.ID == id)
                            {
                                tmp[element.Guid] = element;
                            }
                        }
                    }
                }
                else if (element.xtriggers_extend != null)
                {
                    foreach (KeyValuePair<string, List<XTrigger>> xtrigger in element.xtriggers_extend)
                    {
                        foreach (XTrigger xtriggereffect in xtrigger.Value)
                        {
                            if (xtriggereffect.ID == id)
                            {
                                tmp[element.Guid] = element;
                            }
                        }
                    }
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        private void RecipesThatRequireThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null && recipe.requirements.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.requirements_extend != null && recipe.requirements_extend.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                if (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.extantreqs_extend != null && recipe.extantreqs_extend.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                if (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.tablereqs_extend != null && recipe.tablereqs_extend.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatProduceThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.effects != null && (recipe.effects.ContainsKey(id) || recipe.effects.ContainsValue(id)))
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.effects_extend != null && (recipe.effects_extend.ContainsKey(id) || recipe.effects_extend.ContainsValue(id)))
                {
                    tmp[recipe.Guid] = recipe;
                }
                if (recipe.aspects != null && recipe.aspects.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.aspects_extend != null && recipe.aspects_extend.ContainsKey(id))
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void DecksThatContainThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count != 1 || Content.Decks.Count == 0)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Deck> tmp = new Dictionary<Guid, Deck>();
            foreach (Deck deck in Content.Decks.Values)
            {
                if (deck.spec != null && deck.spec.Contains(id))
                {
                    tmp[deck.Guid] = deck;
                }
                else if (deck.spec_prepend != null && deck.spec_prepend.Contains(id))
                {
                    tmp[deck.Guid] = deck;
                }
                else if (deck.spec_append != null && deck.spec_append.Contains(id))
                {
                    tmp[deck.Guid] = deck;
                }
            }
            if (tmp.Count > 0)
            {
                DecksDictionaryResults ddr = new DecksDictionaryResults(tmp);
                ddr.Show();
            }
        }

        private void LegaciesThatStartWithThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count != 1 || Content.Legacies.Count == 0)
            {
                return;
            }

            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<Guid, Legacy> tmp = new Dictionary<Guid, Legacy>();
            foreach (Legacy legacy in Content.Legacies.Values)
            {
                if (legacy.effects != null && legacy.effects.ContainsKey(id))
                {
                    tmp[legacy.Guid] = legacy;
                }
                else if (legacy.effects_extend != null && legacy.effects_extend.ContainsKey(id))
                {
                    tmp[legacy.Guid] = legacy;
                }
            }
            if (tmp.Count > 0)
            {
                LegaciesDictionaryResults ldr = new LegaciesDictionaryResults(tmp);
                ldr.Show();
            }
        }

        private void RecipesThatLinkToThisRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = recipesListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.linked != null)
                {
                    foreach (RecipeLink link in recipe.linked)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
                else if (recipe.linked_prepend != null)
                {
                    foreach (RecipeLink link in recipe.linked_prepend)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
                else if (recipe.linked_append != null)
                {
                    foreach (RecipeLink link in recipe.linked_append)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
                if (recipe.alt != null)
                {
                    foreach (RecipeLink link in recipe.alt)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
                else if (recipe.alt_prepend != null)
                {
                    foreach (RecipeLink link in recipe.alt_prepend)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
                else if (recipe.alt_append != null)
                {
                    foreach (RecipeLink link in recipe.alt_append)
                    {
                        if (link.ID == id)
                        {
                            tmp[recipe.Guid] = recipe;
                        }
                    }
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatDrawFromThisDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = decksListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.deckeffects != null && recipe.deckeffects.ContainsKey(id) && recipe.deckeffects[id] > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
                else if (recipe.deckeffects_extend != null && recipe.deckeffects_extend.ContainsKey(id) && recipe.deckeffects_extend[id] > 0)
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatCauseThisEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = endingsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.ending != null && recipe.ending == id)
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void RecipesThatUseThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count != 1 || Content.Recipes.Count == 0)
            {
                return;
            }

            string id = verbsListView.SelectedItems[0].Text;
            Dictionary<Guid, Recipe> tmp = new Dictionary<Guid, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.actionId == id)
                {
                    tmp[recipe.Guid] = recipe;
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void ElementsWithSlotsForThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count != 1 || Content.Elements.Count == 0)
            {
                return;
            }

            string id = verbsListView.SelectedItems[0].Text;
            Dictionary<Guid, Element> tmp = new Dictionary<Guid, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.slots != null)
                {
                    foreach (Slot slot in element.slots)
                    {
                        if (slot.actionId == id)
                        {
                            tmp[element.Guid] = element;
                        }
                    }
                }
                else if (element.slots_prepend != null)
                {
                    foreach (Slot slot in element.slots_prepend)
                    {
                        if (slot.actionId == id)
                        {
                            tmp[element.Guid] = element;
                        }
                    }
                }
                else if (element.slots_append != null)
                {
                    foreach (Slot slot in element.slots_append)
                    {
                        if (slot.actionId == id)
                        {
                            tmp[element.Guid] = element;
                        }
                    }
                }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.Show();
            }
        }

        #endregion
        #region "Delete Selected" events

        private void DeleteSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedAspectToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Aspects);
            }
        }

        private void DeleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedElementToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Elements);
            }
        }

        private void DeleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedRecipeToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Recipes);
            }
        }

        private void DeleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedDeckToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Decks);
            }
        }

        private void DeleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedLegacyToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Legacies);
            }
        }

        private void DeleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedEndingToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Endings);
            }
        }

        private void DeleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deleteSelectedVerbToolStripMenuItem.Enabled)
            {
                DeleteSelected(Content.Verbs);
            }
        }

        public DialogResult ConfirmDelete(string id)
        {
            if (id == null)
            {
                return MessageBox.Show("Are you sure you'd like to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
            }

            return MessageBox.Show("Are you sure you'd like to delete " + id + "?", "Delete Item", MessageBoxButtons.YesNo);
        }

        public void Deleted(string id)
        {
            MessageBox.Show(id + "has been deleted.");
        }

        private void DeleteSelected<T>(ContentGroup<T> cg) where T : IGameObject
        {
            if (ListViews[cg.Filename].SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = ListViews[cg.Filename].SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                ListViews[cg.Filename].Items.Remove(listViewItem);
                cg.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        #endregion
        #region "Mouse Down" events

        private void AspectsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(aspectsListView, e.Button);
        }

        private void ElementsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(elementsListView, e.Button);
        }

        private void RecipesListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(recipesListView, e.Button);
        }

        private void DecksListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(decksListView, e.Button);
        }

        private void LegaciesListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(legaciesListView, e.Button);
        }

        private void EndingsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(endingsListView, e.Button);
        }

        private void VerbsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView_MouseDown(verbsListView, e.Button);
        }

        private void ListView_MouseDown(ListView listView, MouseButtons button)
        {
            if (button == MouseButtons.Right)
            {
                listView.SelectedIndices.Clear();
                listView.Select();
                Point point = listView.PointToClient(Cursor.Position);
                if (listView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        #endregion
        #region "Open Selected JSON" events

        private void OpenSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Aspects);
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Elements);
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Recipes);
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Decks);
        }

        private void OpenSelectedLegaciesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Legacies);
        }

        private void OpenSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Endings);
        }

        private void OpenSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJson(Content.Verbs);
        }

        private void OpenSelectedJson<T>(ContentGroup<T> cg) where T : IGameObject
        {
            ListView lv = ListViews[cg.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)lv.SelectedItems[0].Tag;
            T gameObjectToEdit = cg.Get(guid);
            if (gameObjectToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(gameObjectToEdit), true, !EditMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                T deserializedGameObject = JsonConvert.DeserializeObject<T>(je.ObjectText);
                if (!deserializedGameObject.Equals(gameObjectToEdit))
                {
                    // cg.Remove(lv.SelectedItems[0].Tag.ToString());
                    deserializedGameObject.Filename = lv.SelectedItems[0].Group.Name;
                    cg[guid] = deserializedGameObject;
                    lv.SelectedItems[0].Text = deserializedGameObject.ID;
                    MarkDirty();
                }
                else
                {
                    // cg[lv.SelectedItems[0].Tag.ToString()] = deserializedGameObject.Copy();
                }
            }
        }

        #endregion
        #region "Duplicate Selected" events

        private void DuplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedAspectToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Aspects);
            }
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedElementToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Elements);
            }
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedRecipeToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Recipes);
            }
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedDeckToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Decks);
            }
        }

        private void DuplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedLegacyToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Legacies);
            }
        }

        private void DuplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedEndingToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Endings);
            }
        }

        private void DuplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedVerbToolStripMenuItem.Enabled)
            {
                DuplicateSelectedGameObject(Content.Verbs);
            }
        }

        private void DuplicateSelectedGameObject<T>(ContentGroup<T> contentGroup) where T : IGameObject
        {
            ListView lv = ListViews[contentGroup.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }
            T newGameObject = contentGroup[(Guid)lv.SelectedItems[0].Tag].Copy<T>();
            ListViewGroup group = lv.SelectedItems[0].Group;
            string id = newGameObject.ID;
            if (lv.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (lv.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newGameObject.ID = id;
            newGameObject.Filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newGameObject.ID) { Tag = newGuid, Group = group, Name = newGameObject.ID };
            lv.Items.Add(newItem);
            // group.Items.Add(newItem);
            contentGroup.Add(newGuid, newGameObject);
            MarkDirty();
        }

        #endregion
        #region "Export Selected" events

        private void ExportSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Aspects);
        }

        private void ExportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Elements);
        }

        private void ExportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Recipes);
        }

        private void ExportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Decks);
        }

        private void ExportSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Legacies);
        }

        private void ExportSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Endings);
        }

        private void ExportSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedGameObject(Content.Verbs);
        }

        private void ExportSelectedGameObject<T>(ContentGroup<T> cg) where T : IGameObject
        {
            ListView lv = ListViews[cg.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }

            T exportedGameObject = cg.Get((Guid)lv.SelectedItems[0].Tag);
            if (exportedGameObject == null)
            {
                return;
            }
            
            string json = JsonConvert.SerializeObject(exportedGameObject, Formatting.Indented);
            saveFileDialog.FileName = exportedGameObject.GetType().Name + "_" + exportedGameObject.ID + ".json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(saveFileDialog.OpenFile())))
                {
                    jtw.WriteRaw(json);
                }
            }
        }

        #endregion
        #region "Copy Selected JSON to Clipboard" events

        private void CopySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Aspects);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Elements);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Recipes);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Decks);
        }

        private void CopySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Legacies);
        }

        private void CopySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Endings);
        }

        private void CopySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJsonToClipboard(Content.Verbs);
        }

        private void CopySelectedJsonToClipboard<T>(ContentGroup<T> contentGroup) where T : IGameObject
        {
            ListView lv = ListViews[contentGroup.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }

            T exportedT = contentGroup.Get((Guid)lv.SelectedItems[0].Tag);
            if (exportedT == null)
            {
                return;
            }

            Clipboard.SetText(Utilities.SerializeObject(exportedT));
        }

        #endregion
        #region "Create New" events

        private void NewAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspectViewer av = new AspectViewer(new Aspect(), AspectsList_Add, null);
            av.Show();
        }

        private void NewElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), ElementsList_Add, null);
            ev.Show();
        }

        private void NewRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), RecipesList_Add, null);
            rv.Show();
        }

        private void NewDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), DecksList_Add, null);
            dv.Show();
        }

        private void NewLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), LegaciesList_Add, null);
            lv.Show();
        }

        private void NewEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), EndingsList_Add, null);
            ev.Show();
        }

        private void NewVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), VerbsList_Add, null);
            vv.Show();
        }

        #endregion
        #region "Add to List" events

        public void AspectsList_Add(object sender, Aspect result)
        {
            AddToList(Content.Aspects, result);
        }

        public void ElementsList_Add(object sender, Element result)
        {
            AddToList(Content.Elements, result);
        }

        public void RecipesList_Add(object sender, Recipe result)
        {
            AddToList(Content.Recipes, result);
        }

        public void DecksList_Add(object sender, Deck result)
        {
            AddToList(Content.Decks, result);
        }

        public void LegaciesList_Add(object sender, Legacy result)
        {
            AddToList(Content.Legacies, result);
        }

        public void EndingsList_Add(object sender, Ending result)
        {
            AddToList(Content.Endings, result);
        }

        public void VerbsList_Add(object sender, Verb result)
        {
            /*
            Guid guid = Guid.NewGuid();
            Verb newVerb = result.Copy();
            newVerb.Filename = "verbs";
            Content.Verbs[guid] = newVerb;
            ListViewGroup defaultVerbsGroup;
            if (verbsListView.Groups["verbs"] == null)
            {
                defaultVerbsGroup = new ListViewGroup("verbs", "verbs");
                verbsListView.Groups.Add(defaultVerbsGroup);
            }
            else
            {
                defaultVerbsGroup = verbsListView.Groups["verbs"];
            }
            ListViewItem newVerbEntry = new ListViewItem(result.ID) { Tag = guid, Group = defaultVerbsGroup, Name = result.ID };
            // defaultVerbsGroup.Items.Add(newVerbEntry);
            verbsListView.Items.Add(newVerbEntry);
            MarkDirty();
            */
            AddToList(Content.Verbs, result);
        }

        private void AddToList<T>(ContentGroup<T> contentGroup, T result) where T : IGameObject
        {
            ListView listView = ListViews[contentGroup.Filename];
            Guid guid = Guid.NewGuid();
            T newGameObject = result.Copy<T>();
            newGameObject.Filename = contentGroup.Filename;
            contentGroup[guid] = newGameObject;
            ListViewGroup defaultGroup;
            if (listView.Groups[contentGroup.Filename] == null)
            {
                defaultGroup = new ListViewGroup(contentGroup.Filename, contentGroup.Filename);
                listView.Groups.Add(defaultGroup);
            }
            else
            {
                defaultGroup = listView.Groups[contentGroup.Filename];
            }
            ListViewItem newEntry = new ListViewItem(result.ID) { Tag = guid, Group = defaultGroup, Name = result.ID };
            // defaultGroup.Items.Add(newEntry);
            listView.Items.Add(newEntry);
            MarkDirty();
        }

        #endregion
        #region Splitter widths

        private void ModViewerTabControl_VisibleChanged(object sender, EventArgs e)
        {
            LoadWidths();
        }

        private void Splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter4_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter5_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter6_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        private void Splitter7_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SaveWidths();
        }

        #endregion
        #region "Key Down" events

        private void AspectsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && aspectsListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)aspectsListView.SelectedItems[0].Tag;
                AspectViewer av = new AspectViewer(Content.Aspects.Get(guid).Copy(), EditMode ? (EventHandler<Aspect>)AspectsList_Assign : null, aspectsListView.SelectedItems[0]);
                av.Show();
            }
        }

        private void ElementsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && elementsListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)elementsListView.SelectedItems[0].Tag;
                ElementViewer ev = new ElementViewer(Content.Elements.Get(guid).Copy(), EditMode ? (EventHandler<Element>)ElementsList_Assign : null, elementsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void RecipesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && recipesListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)recipesListView.SelectedItems[0].Tag;
                RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(guid).Copy(), EditMode ? (EventHandler<Recipe>)RecipesList_Assign : null, recipesListView.SelectedItems[0]);
                rv.Show();
            }
        }

        private void DecksListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && decksListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)decksListView.SelectedItems[0].Tag;
                DeckViewer dv = new DeckViewer(Content.Decks.Get(guid).Copy(), EditMode ? (EventHandler<Deck>)DecksList_Assign : null, decksListView.SelectedItems[0]);
                dv.Show();
            }
        }

        private void LegaciesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && legaciesListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)legaciesListView.SelectedItems[0].Tag;
                LegacyViewer lv = new LegacyViewer(Content.Legacies.Get(guid).Copy(), EditMode ? (EventHandler<Legacy>)LegaciesList_Assign : null, legaciesListView.SelectedItems[0]);
                lv.Show();
            }
        }

        private void EndingsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && endingsListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)endingsListView.SelectedItems[0].Tag;
                EndingViewer ev = new EndingViewer(Content.Endings.Get(guid).Copy(), EditMode ? (EventHandler<Ending>)EndingsList_Assign : null, endingsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void VerbsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && verbsListView.SelectedItems.Count >= 1)
            {
                Guid guid = (Guid)verbsListView.SelectedItems[0].Tag;
                VerbViewer vv = new VerbViewer(Content.Verbs.Get(guid).Copy(), EditMode ? (EventHandler<Verb>)VerbsList_Assign : null, verbsListView.SelectedItems[0]);
                vv.Show();
            }
        }

        #endregion
        #region "Set Group" events

        private void SetGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupAspectToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Aspects);
            }
        }

        private void SetGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupElementToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Elements);
            }
        }

        private void SetGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupRecipeToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Recipes);
            }
        }

        private void SetGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupDeckToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Decks);
            }
        }

        private void SetGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupLegacyToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Legacies);
            }
        }

        private void SetGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupEndingToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Endings);
            }
        }

        private void SetGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setGroupVerbToolStripMenuItem.Enabled)
            {
                SetGroup(Content.Verbs);
            }
        }

        public void SetGroup<T>(ContentGroup<T> cg) where T : IGameObject
        {
            ListView lv = ListViews[cg.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = lv.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in lv.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup(cg.DisplayName), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.Group;
                foreach (ListView listView in ListViews.Values)
                {
                    // TODO: Make this respect the ContentSource, rather than the ListView.
                    // We REALLY don't want people making groups that exist for other types.
                    if (listView != lv && listView.Groups[newGroup] != null)
                    {
                        MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (!string.IsNullOrEmpty(currentGroup))
                    {
                        lv.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (lv.Groups[newGroup] == null)
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        lv.Groups.Add(listViewGroup);
                    }
                    lv.Groups[newGroup].Items.Add(selectedItem);
                    cg.Get((Guid)selectedItem.Tag).Filename = newGroup;
                    MarkDirty();
                }
                Content.SetRecentGroup(cg.DisplayName, newGroup);
            }
        }

        #endregion
        #region "Use Template" events

        private void UseTemplateAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Aspect));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Aspect templateAspect = JsonConvert.DeserializeObject<Aspect>(templateJson);
                AspectViewer av = new AspectViewer(templateAspect, AspectsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Element));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Element templateElement = JsonConvert.DeserializeObject<Element>(templateJson);
                ElementViewer av = new ElementViewer(templateElement, ElementsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateRecipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Recipe));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Recipe templateRecipe = JsonConvert.DeserializeObject<Recipe>(templateJson);
                RecipeViewer av = new RecipeViewer(templateRecipe, RecipesList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Deck));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Deck templateDeck = JsonConvert.DeserializeObject<Deck>(templateJson);
                DeckViewer av = new DeckViewer(templateDeck, DecksList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Legacy));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Legacy templateLegacy = JsonConvert.DeserializeObject<Legacy>(templateJson);
                LegacyViewer av = new LegacyViewer(templateLegacy, LegaciesList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Ending));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Ending templateEnding = JsonConvert.DeserializeObject<Ending>(templateJson);
                EndingViewer av = new EndingViewer(templateEnding, EndingsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.Selecting, typeof(Verb));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.SelectedItem.Tag.ToString();
                Verb templateVerb = JsonConvert.DeserializeObject<Verb>(templateJson);
                VerbViewer av = new VerbViewer(templateVerb, VerbsList_Add, null);
                av.Show();
            }
        }

        #endregion
        #region "Hide Group" events

        private void HideGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("aspects");
        }

        private void HideGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("elements");
        }

        private void HideGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("recipes");
        }

        private void HideGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("decks");
        }

        private void HideGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("legacies");
        }

        private void HideGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("endings");
        }

        private void HideGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideGroup("verbs");
        }

        private void HideGroup(string listViewKey)
        {
            ListView listView = ListViews[listViewKey];
            if (listView.SelectedItems.Count < 1)
            {
                return;
            }

            if(IsDirty)
            {
                MessageBox.Show("Save or discard your unsaved changes, then try again.",
                "You have unsaved changes!",
                MessageBoxButtons.OK);
                return;
            }
            else if (EditMode && MessageBox.Show("You WILL lose any unsaved changes you've made to this group. Are you sure you want to hide it?",
                "Last chance to save!",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            ListViewGroup group = listView.SelectedItems[0].Group;
            while (group.Items.Count > 0)
            {
                ListViewItem item = group.Items[0];
                group.Items.Remove(item);
                listView.Items.Remove(item);
            }

            Content.SetHiddenGroup(listViewKey, group.Name);
            SaveCustomManifest(Content.CurrentDirectory);
        }

        #endregion

        private void ViewAsFlowchartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)recipesListView.SelectedItems[0].Tag;
            RecipeFlowchartViewer rfv = new RecipeFlowchartViewer(Content.Recipes.Get(id).Copy());
            rfv.Show();
        }

        private void AutosaveTimer_Tick(object sender, EventArgs e)
        {
            SaveMod();
        }

        private void ModViewerTabControl_Load(object sender, EventArgs e)
        {

        }

        private void MarkDirty(bool v)
        {
            IsDirty = v;
            MarkDirtyEventHandler?.Invoke(this, IsDirty);
        }

        public void MarkDirty()
        {
            MarkDirty(true);
        }

        public bool GroupExistsAsHidden(string newGroup)
        {
            bool isGroupHidden = false;
            Dictionary<string, List<string>> hiddenGroups = Content.GetHiddenGroupsDictionary();//CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            if (hiddenGroups != null)
            {
                foreach (string key in hiddenGroups.Keys)
                {
                    isGroupHidden |= hiddenGroups[key].Contains(newGroup) | hiddenGroups[key].Contains(newGroup + ".json");
                }
            }
            return isGroupHidden;
        }

        // This should only ever get called from the reset hidden groups button!
        public void ReloadListView<T>(ContentGroup<T> contentGroup) where T : IGameObject
        {
            ListView listView = ListViews[contentGroup.Filename];
            listView.BeginUpdate();
            listView.Items.Clear();
            List<ListViewItem> itemsToAdd = new List<ListViewItem>();

            foreach (T entity in contentGroup.Values)
            {
                ListViewGroup group = listView.Groups[entity.Filename] ?? new ListViewGroup(entity.Filename, entity.Filename);
                if (!listView.Groups.Contains(group))
                {
                    listView.Groups.Add(group);
                }
                ListViewItem listViewItem = new ListViewItem(entity.ID) { Tag = entity.Guid, Group = group, Name = entity.ID };
                itemsToAdd.Add(listViewItem);
            }

            listView.Items.AddRange(itemsToAdd.ToArray());
            listView.EndUpdate();
        }
    }
}
