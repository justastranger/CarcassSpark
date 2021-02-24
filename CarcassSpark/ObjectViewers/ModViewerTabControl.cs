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
        public bool isVanilla, editMode, isValid = false;

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
            Content.currentDirectory = location;
            saveFileDialog.InitialDirectory = location;
            openFileDialog.InitialDirectory = location;
            this.isVanilla = isVanilla;
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
                Utilities.ContentSources.Add(isVanilla ? "Vanilla" : Content.synopsis.name, Content);
            }
            else
            {
                throw new Exception("Loading Failed");
                // MessageBox.Show("Failed to load content source.");
            }
        }

        public void SetEditingMode(bool editing)
        {
            editMode = editing;
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

        public bool LoadContent()
        {
            if (IsDirty && editMode)
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
                if(isVanilla)
                {
                    Content.synopsis = new Synopsis("Vanilla", "Weather Factory", null, "Content from Cultist Simulator", null);
                }
                else if(!CheckForSynopsis() || !Directory.Exists(Content.currentDirectory + "\\content\\"))
                {
                    return false;
                }

                IEnumerable<string> files = Directory.EnumerateFiles(Content.currentDirectory + (isVanilla ? "" : "\\content\\"), "*.json", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        LoadFile(fs, file);
                    }
                }

                if (!isVanilla)
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
                Content.synopsis = mv.displayedSynopsis;
                SaveMod();
                return true;
            }
            return false;
        }

        public bool CheckForSynopsis()
        {
            if (File.Exists(Content.currentDirectory + "/CarcassSpark.Manifest.json"))
            {
                using (FileStream fs = new FileStream(Content.currentDirectory + "/CarcassSpark.Manifest.json", FileMode.Open))
                {
                    LoadCustomManifest(fs);
                }
            }
            string manifestPath = Content.currentDirectory + "/manifest.json";
            string synopsisPath = Content.currentDirectory + "/synopsis.json";
            // if manifest.json still exists, load it, save it as synopsis.json, then delete manifest.json
            if (File.Exists(manifestPath))
            {
                using (FileStream fs = new FileStream(manifestPath, FileMode.Open))
                {
                    LoadSynopsis(fs);
                }
                SaveManifests(Content.currentDirectory);
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
            else
            {
                // no manifest so we'll try to make one
                if (MessageBox.Show("synopsis.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // return true if everything's going good
                    if (CreateSynopsis())
                    {
                        return true;
                    }
                    // otherwise return false so I can abort the creation of the tab
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public void LoadSynopsis(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.synopsis = JsonConvert.DeserializeObject<Synopsis>(new StreamReader(file).ReadToEnd());
            Text = Content.synopsis.name;
        }

        public void LoadCustomManifest(FileStream file)
        {
            // string fileText = new StreamReader(file).ReadToEnd();
            // Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.SetCustomManifest(JsonConvert.DeserializeObject<JObject>(new StreamReader(file).ReadToEnd()));
        }

        public void LoadWidths()
        {
            if (!isVanilla)
            {
                List<int> widths = Content.GetCustomManifestListInt("widths");
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
            else
            {   // This part looks way uglier than the part above because I didn't make getter functions for the Settings, only Custom Manifests :(
                List<int> widths = Settings.settings.ContainsKey("widths") ? Settings.settings["widths"].ToObject<List<int>>() : null;
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
        }

        public void SaveWidths()
        {
            if (!isVanilla)
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
                SaveCustomManifest(Content.currentDirectory);
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
            if (fileText != "" && fileText != null)
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
                            foreach (JProperty xtrigger in token["xtriggers"])
                            {
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
                        Content.Cultures.Add(deserializedCulture.guid, deserializedCulture);
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
            SaveMod(Content.currentDirectory);
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
            if (isVanilla)
            {
                return;
            }

            string synopsisJson = JsonConvert.SerializeObject(Content.synopsis, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/synopsis.json", FileMode.Create))))
            {
                jtw.WriteRaw(synopsisJson);
            }
            SaveCustomManifest(location);
        }

        public void SaveCustomManifest()
        {
            SaveCustomManifest(Content.currentDirectory);
        }

        public void SaveCustomManifest(string location)
        {
            if (isVanilla)
            {
                return;
            }

            if (Content.GetCustomManifest().Count > 0)
            {
                string CustomManifestJson = JsonConvert.SerializeObject(Content.GetCustomManifest(), Formatting.Indented);
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/CarcassSpark.Manifest.json", FileMode.Create))))
                {
                    jtw.WriteRaw(CustomManifestJson);
                }
            }
            else if (File.Exists(location + "/CarcassSpark.Manifest.json"))
            {
                File.Delete(location + "/CarcassSpark.Manifest.json");
            }
        }

        private void AspectListView_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)aspectsListView.SelectedItems[0].Tag;
            if (editMode)
            {
                AspectViewer av = new AspectViewer(Content.Aspects.Get(id).Copy(), AspectsList_Assign, aspectsListView.SelectedItems[0]);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(Content.Aspects.Get(id).Copy(), null, aspectsListView.SelectedItems[0]);
                av.Show();
            }
        }

        private void AspectsList_Assign(object sender, Aspect result)
        {
            AspectViewer aspectViewer = (AspectViewer)sender;
            if ((Guid)aspectViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Aspects.Remove((Guid)aspectViewer.associatedListViewItem.Tag);
                aspectViewer.associatedListViewItem.Tag = result.guid;
                Aspect newAspect = result.Copy();
                newAspect.filename = aspectViewer.associatedListViewItem.Group.Name;
                Content.Aspects.Add(result.guid, newAspect);
            }
            if (aspectViewer.associatedListViewItem.Text != result.id)
            {
                aspectViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void DecksListView_DoubleClick(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)decksListView.SelectedItems[0].Tag;
            if (editMode)
            {
                DeckViewer dv = new DeckViewer(Content.Decks.Get(id).Copy(), DecksList_Assign, decksListView.SelectedItems[0]);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(Content.Decks.Get(id).Copy(), null, decksListView.SelectedItems[0]);
                dv.Show();
            }
        }

        private void DecksList_Assign(object sender, Deck result)
        {
            DeckViewer deckViewer = (DeckViewer)sender;
            if ((Guid)deckViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Decks.Remove((Guid)deckViewer.associatedListViewItem.Tag);
                deckViewer.associatedListViewItem.Tag = result.guid;
                Deck newDeck = result.Copy();
                newDeck.filename = deckViewer.associatedListViewItem.Group.Name;
                Content.Decks.Add(result.guid, newDeck);
            }
            if (deckViewer.associatedListViewItem.Text != result.id)
            {
                deckViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void ElementsListView_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)elementsListView.SelectedItems[0].Tag;
            if (editMode)
            {
                ElementViewer ev = new ElementViewer(Content.Elements.Get(id).Copy(), ElementsList_Assign, elementsListView.SelectedItems[0]);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(Content.Elements.Get(id).Copy(), null, elementsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void ElementsList_Assign(object sender, Element result)
        {
            ElementViewer elementViewer = (ElementViewer)sender;
            if ((Guid)elementViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Elements.Remove((Guid)elementViewer.associatedListViewItem.Tag);
                elementViewer.associatedListViewItem.Tag = result.guid;
                Element newElement = result.Copy();
                newElement.filename = elementViewer.associatedListViewItem.Group.Name;
                Content.Elements.Add(result.guid, newElement);
            }
            if (elementViewer.associatedListViewItem.Text != result.id)
            {
                elementViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void EndingsListView_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)endingsListView.SelectedItems[0].Tag;
            if (editMode)
            {
                EndingViewer ev = new EndingViewer(Content.Endings.Get(id).Copy(), EndingsList_Assign, endingsListView.SelectedItems[0]);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(Content.Endings.Get(id).Copy(), null, endingsListView.SelectedItems[0]);
                ev.Show();
            }
        }

        private void EndingsList_Assign(object sender, Ending result)
        {
            EndingViewer endingViewer = (EndingViewer)sender;
            if ((Guid)endingViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Endings.Remove((Guid)endingViewer.associatedListViewItem.Tag);
                endingViewer.associatedListViewItem.Tag = result.guid;
                Ending newEnding = result.Copy();
                newEnding.filename = endingViewer.associatedListViewItem.Group.Name;
                Content.Endings.Add(result.guid, newEnding);
            }
            if (endingViewer.associatedListViewItem.Text != result.id)
            {
                endingViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void LegaciesListView_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)legaciesListView.SelectedItems[0].Tag;
            if (editMode)
            {
                LegacyViewer lv = new LegacyViewer(Content.Legacies.Get(id).Copy(), LegaciesList_Assign, legaciesListView.SelectedItems[0]);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(Content.Legacies.Get(id).Copy(), null, legaciesListView.SelectedItems[0]);
                lv.Show();
            }
        }

        private void LegaciesList_Assign(object sender, Legacy result)
        {
            LegacyViewer legacyViewer = (LegacyViewer)sender;
            if ((Guid)legacyViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Legacies.Remove((Guid)legacyViewer.associatedListViewItem.Tag);
                legacyViewer.associatedListViewItem.Tag = result.guid;
                Legacy newLegacy = result.Copy();
                newLegacy.filename = legacyViewer.associatedListViewItem.Group.Name;
                Content.Legacies.Add(result.guid, newLegacy);
            }
            if (legacyViewer.associatedListViewItem.Text != result.id)
            {
                legacyViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void RecipesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)recipesListView.SelectedItems[0].Tag;
            if (editMode)
            {
                RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(id).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(id).Copy(), null, recipesListView.SelectedItems[0]);
                rv.Show();
            }
        }

        private void RecipesList_Assign(object sender, Recipe result)
        {
            RecipeViewer recipeViewer = (RecipeViewer)sender;
            if ((Guid)recipeViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Recipes.Remove((Guid)recipeViewer.associatedListViewItem.Tag);
                recipeViewer.associatedListViewItem.Tag = result.guid;
                Recipe newRecipe = result.Copy();
                newRecipe.filename = recipeViewer.associatedListViewItem.Group.Name;
                Content.Recipes.Add(result.guid, newRecipe);
            }
            if (recipeViewer.associatedListViewItem.Text != result.id)
            {
                recipeViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        private void VerbsListView_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid id = (Guid)verbsListView.SelectedItems[0].Tag;
            if (editMode)
            {
                VerbViewer vv = new VerbViewer(Content.Verbs.Get(id).Copy(), VerbsList_Assign, verbsListView.SelectedItems[0]);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(Content.Verbs.Get(id).Copy(), null, verbsListView.SelectedItems[0]);
                vv.Show();
            }
        }

        private void VerbsList_Assign(object sender, Verb result)
        {
            VerbViewer verbViewer = (VerbViewer)sender;
            if ((Guid)verbViewer.associatedListViewItem.Tag != result.guid)
            {
                Content.Verbs.Remove((Guid)verbViewer.associatedListViewItem.Tag);
                verbViewer.associatedListViewItem.Tag = result.guid;
                Verb newVerb = result.Copy();
                newVerb.filename = verbViewer.associatedListViewItem.Group.Name;
                Content.Verbs.Add(result.guid, newVerb);
            }
            if (verbViewer.associatedListViewItem.Text != result.id)
            {
                verbViewer.associatedListViewItem.Text = result.id;
            }
            MarkDirty();
        }

        #region "Search Text Box Text Changed" events

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

        private void SearchTextBox_TextChanged<T>(ContentGroup<T> contentGroup, string NewText, Func<List<T>, string, T[]> func) where T : IGameObject
        {
            ListView listView = ListViews[contentGroup.Filename];
            listView.BeginUpdate();
            listView.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            string[] hiddenGroups = Content.GetHiddenGroups(contentGroup.Filename); //.GetCustomManifest()["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            T[] itemsToAdd = (NewText != "") ? func(contentGroup.Values.ToList(), NewText) : contentGroup.Values.ToArray();
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
                        where (aspect.id != null && regex.IsMatch(aspect.id))
                           || (aspect.label != null && regex.IsMatch(aspect.label))
                           || (aspect.description != null && regex.IsMatch(aspect.description))
                           || (aspect.comments != null && regex.IsMatch(aspect.comments))
                        select aspect).ToArray();
            }
            catch (ArgumentException)
            {
                return (from aspect in aspectsList.AsParallel()
                        where (aspect.id != null && aspect.id.Contains(searchPattern))
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
                        where (element.id != null && regex.IsMatch(element.id))
                           || (element.label != null && regex.IsMatch(element.label))
                           || (element.comments != null && regex.IsMatch(element.comments))
                        select element).ToArray();
            }
            catch (ArgumentException)
            {
                return (from element in elementsList.AsParallel()
                        where (element.id != null && element.id.Contains(searchPattern))
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
                        where (recipe.id != null && regex.IsMatch(recipe.id))
                           || (recipe.label != null && regex.IsMatch(recipe.label))
                           || (recipe.description != null && regex.IsMatch(recipe.description))
                           || (recipe.startdescription != null && regex.IsMatch(recipe.startdescription))
                           || (recipe.comments != null && regex.IsMatch(recipe.comments))
                        select recipe).ToArray();
            }
            catch (ArgumentException)
            {
                return (from recipe in recipesList.AsParallel()
                        where (recipe.id != null && recipe.id.Contains(searchPattern))
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
                        where (deck.id != null && regex.IsMatch(deck.id))
                           || (deck.label != null && regex.IsMatch(deck.label))
                           || (deck.description != null && regex.IsMatch(deck.description))
                           || (deck.comments != null && regex.IsMatch(deck.comments))
                        select deck).ToArray();
            }
            catch (ArgumentException)
            {
                return (from deck in decksList.AsParallel()
                        where (deck.id != null && deck.id.Contains(searchPattern))
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
                        where (legacy.id != null && regex.IsMatch(legacy.id))
                           || (legacy.label != null && regex.IsMatch(legacy.label))
                           || (legacy.description != null && regex.IsMatch(legacy.description))
                           || (legacy.startdescription != null && regex.IsMatch(legacy.startdescription))
                           || (legacy.comments != null && regex.IsMatch(legacy.comments))
                        select legacy).ToArray();
            }
            catch (ArgumentException)
            {
                return (from legacy in recipesList.AsParallel()
                        where (legacy.id != null && legacy.id.Contains(searchPattern))
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
                        where (ending.id != null && regex.IsMatch(ending.id))
                           || (ending.label != null && regex.IsMatch(ending.label))
                           || (ending.description != null && regex.IsMatch(ending.description))
                           || (ending.comments != null && regex.IsMatch(ending.comments))
                        select ending).ToArray();
            }
            catch (ArgumentException)
            {
                return (from ending in recipesList.AsParallel()
                        where (ending.id != null && ending.id.Contains(searchPattern))
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
                        where (verb.id != null && regex.IsMatch(verb.id))
                           || (verb.label != null && regex.IsMatch(verb.label))
                           || (verb.description != null && regex.IsMatch(verb.description))
                           || (verb.comments != null && regex.IsMatch(verb.comments))
                        select verb).ToArray();
            }
            catch (ArgumentException)
            {
                return (from verb in recipesList.AsParallel()
                        where (verb.id != null && verb.id.Contains(searchPattern))
                           || (verb.label != null && verb.label.Contains(searchPattern))
                           || (verb.description != null && verb.description.Contains(searchPattern))
                           || (verb.comments != null && verb.comments.Contains(searchPattern))
                        select verb).ToArray();
            }
        }

        #endregion

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
                    tmp[element.guid] = element;
                }
                else if (element.aspects_extend != null && element.aspects_extend.ContainsKey(id))
                {
                    tmp[element.guid] = element;
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
                    tmp[element.guid] = element;
                }
                else if (element.xtriggers_extend != null && element.xtriggers_extend.ContainsKey(id))
                {
                    tmp[element.guid] = element;
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
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.requirements.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                }
                else if (recipe.requirements_extend != null)
                {
                    if (recipe.requirements_extend.ContainsKey(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.requirements_extend.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                }
                if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.extantreqs.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                }
                else if (recipe.extantreqs_extend != null)
                {
                    if (recipe.extantreqs_extend.ContainsKey(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.extantreqs_extend.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                }
                if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.tablereqs.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                }
                else if (recipe.tablereqs_extend != null)
                {
                    if (recipe.tablereqs_extend.ContainsKey(id))
                    {
                        tmp[recipe.guid] = recipe;
                    }
                    else if (recipe.tablereqs_extend.ContainsValue(id))
                    {
                        tmp[recipe.guid] = recipe;
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
                if (recipe.aspects != null && (recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.aspects_extend != null && (recipe.aspects_extend.ContainsKey(id) && recipe.aspects_extend[id] > 0))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.effects != null && (recipe.effects.ContainsKey(id) && Convert.ToInt32(recipe.effects[id]) > 0))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.effects_extend != null && (recipe.effects_extend.ContainsKey(id) && Convert.ToInt32(recipe.effects_extend[id]) > 0))
                {
                    tmp[recipe.guid] = recipe;
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
            foreach (Element element in Content.Elements.Values)
            {
                if (element.slots != null)
                {
                    foreach (Slot slot in element.slots)
                    {
                        if (slot.required.ContainsKey(id))
                        {
                            tmp[element.guid] = element;
                        }
                    }
                }
                else if (element.slots_prepend != null)
                {
                    foreach (Slot slot in element.slots_prepend)
                    {
                        if (slot.required.ContainsKey(id))
                        {
                            tmp[element.guid] = element;
                        }
                    }
                }
                else if (element.slots_append != null)
                {
                    foreach (Slot slot in element.slots_append)
                    {
                        if (slot.required.ContainsKey(id))
                        {
                            tmp[element.guid] = element;
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
                    tmp[element.guid] = element;
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
                            if (xtriggereffect.id == id)
                            {
                                tmp[element.guid] = element;
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
                            if (xtriggereffect.id == id)
                            {
                                tmp[element.guid] = element;
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
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.requirements_extend != null && recipe.requirements_extend.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
                }
                if (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.extantreqs_extend != null && recipe.extantreqs_extend.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
                }
                if (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.tablereqs_extend != null && recipe.tablereqs_extend.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
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
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.effects_extend != null && (recipe.effects_extend.ContainsKey(id) || recipe.effects_extend.ContainsValue(id)))
                {
                    tmp[recipe.guid] = recipe;
                }
                if (recipe.aspects != null && recipe.aspects.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.aspects_extend != null && recipe.aspects_extend.ContainsKey(id))
                {
                    tmp[recipe.guid] = recipe;
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
                    tmp[deck.guid] = deck;
                }
                else if (deck.spec_prepend != null && deck.spec_prepend.Contains(id))
                {
                    tmp[deck.guid] = deck;
                }
                else if (deck.spec_append != null && deck.spec_append.Contains(id))
                {
                    tmp[deck.guid] = deck;
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
                    tmp[legacy.guid] = legacy;
                }
                else if (legacy.effects_extend != null && legacy.effects_extend.ContainsKey(id))
                {
                    tmp[legacy.guid] = legacy;
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
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
                        }
                    }
                }
                else if (recipe.linked_prepend != null)
                {
                    foreach (RecipeLink link in recipe.linked_prepend)
                    {
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
                        }
                    }
                }
                else if (recipe.linked_append != null)
                {
                    foreach (RecipeLink link in recipe.linked_append)
                    {
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
                        }
                    }
                }
                if (recipe.alt != null)
                {
                    foreach (RecipeLink link in recipe.alt)
                    {
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
                        }
                    }
                }
                else if (recipe.alt_prepend != null)
                {
                    foreach (RecipeLink link in recipe.alt_prepend)
                    {
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
                        }
                    }
                }
                else if (recipe.alt_append != null)
                {
                    foreach (RecipeLink link in recipe.alt_append)
                    {
                        if (link.id == id)
                        {
                            tmp[recipe.guid] = recipe;
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
                    tmp[recipe.guid] = recipe;
                }
                else if (recipe.deckeffects_extend != null && recipe.deckeffects_extend.ContainsKey(id) && recipe.deckeffects_extend[id] > 0)
                {
                    tmp[recipe.guid] = recipe;
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
                    tmp[recipe.guid] = recipe;
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
                    tmp[recipe.guid] = recipe;
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
                            tmp[element.guid] = element;
                        }
                    }
                }
                else if (element.slots_prepend != null)
                {
                    foreach (Slot slot in element.slots_prepend)
                    {
                        if (slot.actionId == id)
                        {
                            tmp[element.guid] = element;
                        }
                    }
                }
                else if (element.slots_append != null)
                {
                    foreach (Slot slot in element.slots_append)
                    {
                        if (slot.actionId == id)
                        {
                            tmp[element.guid] = element;
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

        private void SaveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = Content.currentDirectory;
            if (saveToFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                SaveMod(saveToFolderBrowserDialog.SelectedPath);
            }
        }

        private void AutosaveTimer_Tick(object sender, EventArgs e)
        {
            SaveMod();
        }

        private void DeleteSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedAspectToolStripMenuItem.Enabled)
            {
                return;
            }

            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = aspectsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                aspectsListView.Items.Remove(listViewItem);
                Content.Aspects.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedElementToolStripMenuItem.Enabled)
            {
                return;
            }

            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = elementsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                elementsListView.Items.Remove(listViewItem);
                Content.Elements.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedRecipeToolStripMenuItem.Enabled)
            {
                return;
            }

            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = recipesListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                recipesListView.Items.Remove(listViewItem);
                Content.Recipes.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedDeckToolStripMenuItem.Enabled)
            {
                return;
            }

            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = decksListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                decksListView.Items.Remove(listViewItem);
                Content.Decks.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedLegacyToolStripMenuItem.Enabled)
            {
                return;
            }

            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = legaciesListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                legaciesListView.Items.Remove(listViewItem);
                Content.Legacies.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedEndingToolStripMenuItem.Enabled)
            {
                return;
            }

            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = endingsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                endingsListView.Items.Remove(listViewItem);
                Content.Endings.Remove((Guid)listViewItem.Tag);
                MarkDirty();
            }
        }

        private void DeleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!deleteSelectedVerbToolStripMenuItem.Enabled)
            {
                return;
            }

            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem listViewItem = verbsListView.SelectedItems[0];
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                verbsListView.Items.Remove(listViewItem);
                Content.Verbs.Remove((Guid)listViewItem.Tag);
                MarkDirty();
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

        private void AspectsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                aspectsListView.SelectedIndices.Clear();
                aspectsListView.Select();
                Point point = aspectsListView.PointToClient(Cursor.Position);
                if (aspectsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void ElementsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                elementsListView.SelectedIndices.Clear();
                elementsListView.Select();
                Point point = elementsListView.PointToClient(Cursor.Position);
                if (elementsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void RecipesListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                recipesListView.SelectedIndices.Clear();
                recipesListView.Select();
                Point point = recipesListView.PointToClient(Cursor.Position);
                if (recipesListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void DecksListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                decksListView.SelectedIndices.Clear();
                decksListView.Select();
                Point point = decksListView.PointToClient(Cursor.Position);
                if (decksListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void LegaciesListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                legaciesListView.SelectedIndices.Clear();
                legaciesListView.Select();
                Point point = legaciesListView.PointToClient(Cursor.Position);
                if (legaciesListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void EndingsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                endingsListView.SelectedIndices.Clear();
                endingsListView.Select();
                Point point = endingsListView.PointToClient(Cursor.Position);
                if (endingsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void VerbsListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                verbsListView.SelectedIndices.Clear();
                verbsListView.Select();
                Point point = verbsListView.PointToClient(Cursor.Position);
                if (verbsListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        #region "Open Selected JSON" events

        private void OpenSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Aspects);
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Elements);
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Recipes);
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Decks);
        }

        private void OpenSelectedLegaciesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Legacies);
        }

        private void OpenSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Endings);
        }

        private void OpenSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedJSON(Content.Verbs);
        }

        private void OpenSelectedJSON<T>(ContentGroup<T> cg) where T : IGameObject
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

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(gameObjectToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                T deserializedGameObject = JsonConvert.DeserializeObject<T>(je.objectText);
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
                Aspect newGameObject = Content.Aspects[(Guid)aspectsListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Aspects, newGameObject);
            }
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedElementToolStripMenuItem.Enabled)
            {
                Element newGameObject = Content.Elements[(Guid) elementsListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Elements, newGameObject);
            }
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedRecipeToolStripMenuItem.Enabled)
            {
                Recipe newGameObject = Content.Recipes[(Guid) recipesListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Recipes, newGameObject);
            }
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedDeckToolStripMenuItem.Enabled)
            {
                Deck newGameObject = Content.Decks[(Guid) decksListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Decks, newGameObject);
            }
        }

        private void DuplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedLegacyToolStripMenuItem.Enabled)
            {
                Legacy newGameObject = Content.Legacies[(Guid) legaciesListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Legacies, newGameObject);
            }
        }

        private void DuplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedEndingToolStripMenuItem.Enabled)
            {
                Ending newGameObject = Content.Endings[(Guid) verbsListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Endings, newGameObject);
            }
        }

        private void DuplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (duplicateSelectedVerbToolStripMenuItem.Enabled)
            {
                Verb newGameObject = Content.Verbs[(Guid)verbsListView.SelectedItems[0].Tag].Copy();
                DuplicateSelectedGameObject(Content.Verbs, newGameObject);
            }
        }

        private void DuplicateSelectedGameObject<T>(ContentGroup<T> contentGroup, T newGameObject) where T : IGameObject
        {
            ListView lv = ListViews[contentGroup.Filename];
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }
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
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Aspect exportedAspect = Content.Aspects.Get((Guid)aspectsListView.SelectedItems[0].Tag);
            if (exportedAspect == null)
            {
                return;
            }

            ExportObject(exportedAspect, exportedAspect.id);
        }

        private void ExportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Element exportedElement = Content.Elements.Get((Guid)elementsListView.SelectedItems[0].Tag);
            if (exportedElement == null)
            {
                return;
            }

            ExportObject(exportedElement, exportedElement.id);
        }

        private void ExportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Recipe exportedRecipe = Content.Recipes.Get((Guid)recipesListView.SelectedItems[0].Tag);
            if (exportedRecipe == null)
            {
                return;
            }

            ExportObject(exportedRecipe, exportedRecipe.id);
        }

        private void ExportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            Deck exportedDeck = Content.Decks.Get((Guid)decksListView.SelectedItems[0].Tag);
            if (exportedDeck == null)
            {
                return;
            }

            ExportObject(exportedDeck, exportedDeck.id);
        }

        private void ExportSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Legacy exportedLegacy = Content.Legacies.Get((Guid)legaciesListView.SelectedItems[0].Tag);
            if (exportedLegacy == null)
            {
                return;
            }

            ExportObject(exportedLegacy, exportedLegacy.id);
        }

        private void ExportSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Ending exportedEnding = Content.Endings.Get((Guid)endingsListView.SelectedItems[0].Tag);
            if (exportedEnding == null)
            {
                return;
            }

            ExportObject(exportedEnding, exportedEnding.id);
        }

        private void ExportSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Verb exportedVerb = Content.Verbs.Get((Guid)verbsListView.SelectedItems[0].Tag);
            ExportObject(exportedVerb, exportedVerb.id);
        }

        private void ExportObject(object objectToExport, string id)
        {
            string JSON = JsonConvert.SerializeObject(objectToExport, Formatting.Indented);
            saveFileDialog.FileName = objectToExport.GetType().Name + "_" + id + ".json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(saveFileDialog.OpenFile())))
                {
                    jtw.WriteRaw(JSON);
                }
            }
        }

        public void CopyObjectJSONToClipboard(object objectToExport)
        {
            Clipboard.SetText(Utilities.SerializeObject(objectToExport));
        }
        #endregion
        #region "Copy Selected JSON to Clipboard" events

        private void CopySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Aspects);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Elements);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Recipes);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Decks);
        }

        private void CopySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Legacies);
        }

        private void CopySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Endings);
        }

        private void CopySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedJSONToClipboard(Content.Verbs);
        }

        private void CopySelectedJSONToClipboard<T>(ContentGroup<T> contentGroup) where T : IGameObject
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
        #region "New" events

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
        #region Add to List events

        public void AspectsList_Add(object sender, Aspect result)
        {
            Guid guid = Guid.NewGuid();
            Aspect newAspect = result.Copy();
            newAspect.filename = "aspects";
            Content.Aspects[guid] = newAspect;
            ListViewGroup defaultAspectsGroup;
            if (aspectsListView.Groups["aspects"] == null)
            {
                defaultAspectsGroup = new ListViewGroup("aspects", "aspects");
                aspectsListView.Groups.Add(defaultAspectsGroup);
            }
            else
            {
                defaultAspectsGroup = aspectsListView.Groups["aspects"];
            }
            ListViewItem newAspectEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultAspectsGroup, Name = result.id };
            // defaultAspectsGroup.Items.Add(newAspectEntry);
            aspectsListView.Items.Add(newAspectEntry);
            MarkDirty();
        }

        public void ElementsList_Add(object sender, Element result)
        {
            Guid guid = Guid.NewGuid();
            Element newElement = result.Copy();
            newElement.filename = "elements";
            Content.Elements[guid] = newElement;
            ListViewGroup defaultElementsGroup;
            if (elementsListView.Groups["elements"] == null)
            {
                defaultElementsGroup = new ListViewGroup("elements", "elements");
                elementsListView.Groups.Add(defaultElementsGroup);
            }
            else
            {
                defaultElementsGroup = elementsListView.Groups["elements"];
            }
            ListViewItem newElementEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultElementsGroup, Name = result.id };
            // defaultElementsGroup.Items.Add(newElementEntry);
            elementsListView.Items.Add(newElementEntry);
            MarkDirty();
        }

        public void RecipesList_Add(object sender, Recipe result)
        {
            Guid guid = Guid.NewGuid();
            Recipe newRecipe = result.Copy();
            newRecipe.filename = "recipes";
            Content.Recipes[guid] = newRecipe;
            ListViewGroup defaultRecipesGroup;
            if (recipesListView.Groups["recipes"] == null)
            {
                defaultRecipesGroup = new ListViewGroup("recipes", "recipes");
                recipesListView.Groups.Add(defaultRecipesGroup);
            }
            else
            {
                defaultRecipesGroup = recipesListView.Groups["recipes"];
            }
            ListViewItem newRecipeEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultRecipesGroup, Name = result.id };
            // defaultRecipesGroup.Items.Add(newRecipeEntry);
            recipesListView.Items.Add(newRecipeEntry);
            MarkDirty();
        }

        public void DecksList_Add(object sender, Deck result)
        {
            Guid guid = Guid.NewGuid();
            Deck newDeck = result.Copy();
            newDeck.filename = "decks";
            Content.Decks[guid] = newDeck;
            ListViewGroup defaultDecksGroup;
            if (decksListView.Groups["decks"] == null)
            {
                defaultDecksGroup = new ListViewGroup("decks", "decks");
                decksListView.Groups.Add(defaultDecksGroup);
            }
            else
            {
                defaultDecksGroup = decksListView.Groups["decks"];
            }
            ListViewItem newDeckEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultDecksGroup, Name = result.id };
            // defaultDecksGroup.Items.Add(newDeckEntry);
            decksListView.Items.Add(newDeckEntry);
            MarkDirty();
        }

        public void LegaciesList_Add(object sender, Legacy result)
        {
            Guid guid = Guid.NewGuid();
            Legacy newLegacy = result.Copy();
            newLegacy.filename = "legacies";
            Content.Legacies[guid] = newLegacy;
            ListViewGroup defaultLegaciesGroup;
            if (legaciesListView.Groups["legacies"] == null)
            {
                defaultLegaciesGroup = new ListViewGroup("legacies", "legacies");
                legaciesListView.Groups.Add(defaultLegaciesGroup);
            }
            else
            {
                defaultLegaciesGroup = legaciesListView.Groups["legacies"];
            }
            ListViewItem newLegacyEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultLegaciesGroup, Name = result.id };
            // defaultLegaciesGroup.Items.Add(newLegacyEntry);
            legaciesListView.Items.Add(newLegacyEntry);
            MarkDirty();
        }

        public void EndingsList_Add(object sender, Ending result)
        {
            Guid guid = Guid.NewGuid();
            Ending newEnding = result.Copy();
            newEnding.filename = "endings";
            Content.Endings[guid] = newEnding;
            ListViewGroup defaultEndingsGroup;
            if (endingsListView.Groups["endings"] == null)
            {
                defaultEndingsGroup = new ListViewGroup("endings", "endings");
                endingsListView.Groups.Add(defaultEndingsGroup);
            }
            else
            {
                defaultEndingsGroup = endingsListView.Groups["endings"];
            }
            ListViewItem newEndingEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultEndingsGroup, Name = result.id };
            // defaultEndingsGroup.Items.Add(newEndingEntry);
            endingsListView.Items.Add(newEndingEntry);
            MarkDirty();
        }

        public void VerbsList_Add(object sender, Verb result)
        {
            Guid guid = Guid.NewGuid();
            Verb newVerb = result.Copy();
            newVerb.filename = "verbs";
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
            ListViewItem newVerbEntry = new ListViewItem(result.id) { Tag = guid, Group = defaultVerbsGroup, Name = result.id };
            // defaultVerbsGroup.Items.Add(newVerbEntry);
            verbsListView.Items.Add(newVerbEntry);
            MarkDirty();
        }

        #endregion

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

        #region ListView Key Down events

        private void AspectsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)aspectsListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    AspectViewer av = new AspectViewer(Content.Aspects.Get(guid).Copy(), AspectsList_Assign, aspectsListView.SelectedItems[0]);
                    av.Show();
                }
                else
                {
                    AspectViewer av = new AspectViewer(Content.Aspects.Get(guid).Copy(), null, aspectsListView.SelectedItems[0]);
                    av.Show();
                }
            }
        }

        private void ElementsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)elementsListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    ElementViewer ev = new ElementViewer(Content.Elements.Get(guid).Copy(), ElementsList_Assign, elementsListView.SelectedItems[0]);
                    ev.Show();
                }
                else
                {
                    ElementViewer ev = new ElementViewer(Content.Elements.Get(guid).Copy(), null, elementsListView.SelectedItems[0]);
                    ev.Show();
                }
            }
        }

        private void RecipesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)recipesListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(guid).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(guid).Copy(), null, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
            }
        }

        private void DecksListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)decksListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    DeckViewer dv = new DeckViewer(Content.Decks.Get(guid).Copy(), DecksList_Assign, decksListView.SelectedItems[0]);
                    dv.Show();
                }
                else
                {
                    DeckViewer dv = new DeckViewer(Content.Decks.Get(guid).Copy(), null, decksListView.SelectedItems[0]);
                    dv.Show();
                }
            }
        }

        private void LegaciesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)legaciesListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(guid).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.Recipes.Get(guid).Copy(), null, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
            }
        }

        #endregion
        #region Set Group toolstrip menu item clicked events

        private void SetGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupAspectToolStripMenuItem.Enabled)
            {
                return;
            }

            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = aspectsListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in aspectsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Aspect"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (elementsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        aspectsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (aspectsListView.Groups[newGroup] != null)
                    {
                        aspectsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        aspectsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Aspects.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Aspect", newGroup);
            }
        }

        private void SetGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupElementToolStripMenuItem.Enabled)
            {
                return;
            }

            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = elementsListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in elementsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Element"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        elementsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (elementsListView.Groups[newGroup] != null)
                    {
                        elementsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        elementsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Elements.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Element", newGroup);
            }
        }

        private void SetGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupRecipeToolStripMenuItem.Enabled)
            {
                return;
            }

            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = recipesListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in recipesListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Recipe"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || elementsListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        recipesListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (recipesListView.Groups[newGroup] != null)
                    {
                        recipesListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        recipesListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Recipes.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Recipe", newGroup);
            }
        }

        private void SetGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupDeckToolStripMenuItem.Enabled)
            {
                return;
            }

            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = decksListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in decksListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Deck"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || elementsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        decksListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (decksListView.Groups[newGroup] != null)
                    {
                        decksListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        decksListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Decks.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Deck", newGroup);
            }
        }

        private void SetGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupLegacyToolStripMenuItem.Enabled)
            {
                return;
            }

            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = legaciesListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in legaciesListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Legacy"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || elementsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        legaciesListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (legaciesListView.Groups[newGroup] != null)
                    {
                        legaciesListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        legaciesListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Legacies.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Legacy", newGroup);
            }
        }

        private void SetGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupEndingToolStripMenuItem.Enabled)
            {
                return;
            }

            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = endingsListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in endingsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Ending"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || elementsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || verbsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        endingsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (endingsListView.Groups[newGroup] != null)
                    {
                        endingsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        endingsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Endings.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
            }
        }

        private void SetGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!setGroupVerbToolStripMenuItem.Enabled)
            {
                return;
            }

            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selectedItem = verbsListView.SelectedItems[0];
            string currentGroup = selectedItem.Group?.Name ?? "";
            List<string> groups = new List<string>();
            foreach (ListViewGroup lvg in verbsListView.Groups)
            {
                groups.Add(lvg.Name);
            }
            GroupEditor ge = new GroupEditor(currentGroup, Content.GetRecentGroup("Verb"), groups);
            if (ge.ShowDialog() == DialogResult.OK)
            {
                string newGroup = ge.group;
                if (aspectsListView.Groups[newGroup] != null
                 || elementsListView.Groups[newGroup] != null
                 || recipesListView.Groups[newGroup] != null
                 || legaciesListView.Groups[newGroup] != null
                 || decksListView.Groups[newGroup] != null
                 || endingsListView.Groups[newGroup] != null)
                {
                    MessageBox.Show("That group already exists for another Entity Type.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (GroupExistsAsHidden(newGroup))
                {
                    MessageBox.Show("That group already exists, but is hidden.", "Invalid Group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (newGroup != currentGroup)
                {
                    if (currentGroup != "" && currentGroup != null)
                    {
                        verbsListView.Groups[currentGroup].Items.Remove(selectedItem);
                    }

                    if (verbsListView.Groups[newGroup] != null)
                    {
                        verbsListView.Groups[newGroup].Items.Add(selectedItem);
                    }
                    else
                    {
                        ListViewGroup listViewGroup = new ListViewGroup(newGroup, newGroup);
                        verbsListView.Groups.Add(listViewGroup);
                        listViewGroup.Items.Add(selectedItem);
                        Content.Verbs.Get((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Verb", newGroup);
            }
        }

        #endregion
        #region Use Template toolstrip menu item clicked events

        private void UseTemplateAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Aspect));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Aspect templateAspect = JsonConvert.DeserializeObject<Aspect>(templateJson);
                AspectViewer av = new AspectViewer(templateAspect, AspectsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Element));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Element templateElement = JsonConvert.DeserializeObject<Element>(templateJson);
                ElementViewer av = new ElementViewer(templateElement, ElementsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateRecipeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Recipe));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Recipe templateRecipe = JsonConvert.DeserializeObject<Recipe>(templateJson);
                RecipeViewer av = new RecipeViewer(templateRecipe, RecipesList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Deck));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Deck templateDeck = JsonConvert.DeserializeObject<Deck>(templateJson);
                DeckViewer av = new DeckViewer(templateDeck, DecksList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Legacy));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Legacy templateLegacy = JsonConvert.DeserializeObject<Legacy>(templateJson);
                LegacyViewer av = new LegacyViewer(templateLegacy, LegaciesList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Ending));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Ending templateEnding = JsonConvert.DeserializeObject<Ending>(templateJson);
                EndingViewer av = new EndingViewer(templateEnding, EndingsList_Add, null);
                av.Show();
            }
        }

        private void UseTemplateVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateManager templateManager = new TemplateManager(TemplateManagerMode.SELECTING, typeof(Verb));
            if (templateManager.ShowDialog() == DialogResult.OK)
            {
                string templateJson = templateManager.selectedItem.Tag.ToString();
                Verb templateVerb = JsonConvert.DeserializeObject<Verb>(templateJson);
                VerbViewer av = new VerbViewer(templateVerb, VerbsList_Add, null);
                av.Show();
            }
        }

        private void EndingsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)endingsListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    EndingViewer ev = new EndingViewer(Content.Endings.Get(guid).Copy(), EndingsList_Assign, endingsListView.SelectedItems[0]);
                    ev.Show();
                }
                else
                {
                    EndingViewer ev = new EndingViewer(Content.Endings.Get(guid).Copy(), null, endingsListView.SelectedItems[0]);
                    ev.Show();
                }
            }
        }

        private void VerbsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Guid guid = (Guid)verbsListView.SelectedItems[0].Tag;
                if (editMode)
                {
                    VerbViewer vv = new VerbViewer(Content.Verbs.Get(guid).Copy(), VerbsList_Assign, verbsListView.SelectedItems[0]);
                    vv.Show();
                }
                else
                {
                    VerbViewer vv = new VerbViewer(Content.Verbs.Get(guid).Copy(), null, verbsListView.SelectedItems[0]);
                    vv.Show();
                }
            }
        }
        #endregion
        #region Hide Group toolstrip menu items clicked events

        private void HideGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["aspects"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("aspects", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["elements"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("elements", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["recipes"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("recipes", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["decks"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("decks", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["legacies"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("legacies", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["endings"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("endings", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void HideGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = HideCurrentGroupShortTerm(ListViews["verbs"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("verbs", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private ListViewGroup HideCurrentGroupShortTerm(ListView lv)
        {
            if (lv.SelectedItems.Count < 1)
            {
                return null;
            }

            if(IsDirty)
            {
                MessageBox.Show("Save or discard your unsaved changes, then try again.",
                "You have unsaved changes!",
                MessageBoxButtons.OK);
                return null;
            }
            else if (editMode && MessageBox.Show("You WILL lose any unsaved changes you've made to this group. Are you sure you want to hide it?",
                "Last chance to save!",
                MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return null;
            }

            ListViewGroup group = lv.SelectedItems[0].Group;
            while (group.Items.Count > 0)
            {
                ListViewItem item = group.Items[0];
                group.Items.Remove(item);
                lv.Items.Remove(item);
            }
            return group;
        }

        #endregion

        private void ModViewerTabControl_Load(object sender, EventArgs e)
        {

        }

        private void MarkDirty(bool v)
        {
            IsDirty = v;
            if (MarkDirtyEventHandler != null)
            {
                MarkDirtyEventHandler.Invoke(this, IsDirty);
            }
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
