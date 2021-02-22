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

        public Dictionary<string, ListView> ListViews = new Dictionary<string, ListView>();

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
            if (!isVanilla)
            {
                // If there is no synopsis, try to create one. If no synopsis ends up loaded or created, return false so the tab can be canceled
                if (!CheckForSynopsis())
                {
                    return false;
                }
                if (Directory.Exists(Content.currentDirectory + "\\content\\"))
                {
                    foreach (string file in Directory.EnumerateFiles(Content.currentDirectory + "\\content\\", "*.json", SearchOption.AllDirectories))
                    {
                        using (FileStream fs = new FileStream(file, FileMode.Open))
                        {
                            LoadFile(fs, file);
                        }
                    }
                    // mod loaded successfully
                    MarkDirty(false);
                    return true;
                }
            }
            else
            {
                Content.synopsis = new Synopsis("Vanilla", "Weather Factory", null, "Content from Cultist Simulator", null);
                foreach (string file in Directory.EnumerateFiles(Content.currentDirectory, "*.json", SearchOption.AllDirectories))
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        LoadFile(fs, file);
                    }
                }
                return true;
            }
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
            Content.CustomManifest = JsonConvert.DeserializeObject<JObject>(new StreamReader(file).ReadToEnd());
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
                
                Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                if (hiddenGroups != null && fileName != null && hiddenGroups.ContainsKey(fileType) && hiddenGroups[fileType] != null)
                {
                    isGroupHidden = hiddenGroups[fileType].Contains(fileName);
                } // */

                switch (fileType)
                {
                    case "elements":
                        foreach (JToken element in parsedJToken.First.ToArray())
                        {
                            if (element["xtriggers"] != null)
                            {
                                foreach (JProperty xtrigger in element["xtriggers"])
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
                                        xtrigger.Value = JArray.FromObject(new List<XTrigger>() { new XTrigger(xtrigger.Value.Value<string>()) });
                                    }
                                }
                            }

                            if (element["isAspect"] != null)
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                Content.Aspects.Add(deserializedAspect.guid, deserializedAspect);
                                if (!isGroupHidden)
                                {
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.guid,
                                        Name = deserializedAspect.id
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                    if (aspectsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        aspectsListView.Groups.Add(listViewGroup);
                                        aspectLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        aspectLVI.Group = aspectsListView.Groups[fileName];
                                    }
                                }
                                deserializedAspect.filename = fileName;
                            }
                            else if (element["extends"] != null && Utilities.AspectExists(element["id"].ToString()))
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                Content.Aspects.Add(deserializedAspect.guid, deserializedAspect);
                                if (!isGroupHidden)
                                {
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.guid,
                                        Name = deserializedAspect.id
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                    if (aspectsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        aspectsListView.Groups.Add(listViewGroup);
                                        aspectLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        aspectLVI.Group = aspectsListView.Groups[fileName];
                                    }
                                }
                                deserializedAspect.filename = fileName;
                            }
                            else
                            {
                                Element deserializedElement = element.ToObject<Element>();
                                Content.Elements.Add(deserializedElement.guid, deserializedElement);
                                if (!isGroupHidden)
                                {
                                    ListViewItem elementLVI = new ListViewItem(deserializedElement.id)
                                    {
                                        Tag = deserializedElement.guid,
                                        Name = deserializedElement.id
                                    };
                                    elementsListView.Items.Add(elementLVI);
                                    if (elementsListView.Groups[fileName] == null)
                                    {
                                        ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                        elementsListView.Groups.Add(listViewGroup);
                                        elementLVI.Group = listViewGroup;
                                    }
                                    else
                                    {
                                        elementLVI.Group = elementsListView.Groups[fileName];
                                    }
                                }
                                deserializedElement.filename = fileName;
                            }
                        }
                        return;
                    case "recipes":
                        foreach (JToken recipe in parsedJToken.First.ToArray())
                        {
                            Recipe deserializedRecipe = recipe.ToObject<Recipe>();
                            Content.Recipes.Add(deserializedRecipe.guid, deserializedRecipe);
                            if (!isGroupHidden)
                            {
                                ListViewItem recipeLVI = new ListViewItem(deserializedRecipe.id)
                                {
                                    Tag = deserializedRecipe.guid,
                                    Name = deserializedRecipe.id
                                };
                                recipesListView.Items.Add(recipeLVI);
                                if (recipesListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    recipesListView.Groups.Add(listViewGroup);
                                    recipeLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    recipeLVI.Group = recipesListView.Groups[fileName];
                                }
                            }
                            deserializedRecipe.filename = fileName;
                        }
                        return;
                    case "decks":
                        foreach (JToken deck in parsedJToken.First.ToArray())
                        {
                            Deck deserializedDeck = deck.ToObject<Deck>();
                            Content.Decks.Add(deserializedDeck.guid, deserializedDeck);
                            if (!isGroupHidden)
                            {
                                ListViewItem deckLVI = new ListViewItem(deserializedDeck.id)
                                {
                                    Tag = deserializedDeck.guid,
                                    Name = deserializedDeck.id
                                };
                                decksListView.Items.Add(deckLVI);
                                if (decksListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    decksListView.Groups.Add(listViewGroup);
                                    deckLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    deckLVI.Group = decksListView.Groups[fileName];
                                }
                            }
                            deserializedDeck.filename = fileName;
                        }
                        return;
                    case "legacies":
                        foreach (JToken legacy in parsedJToken.First.ToArray())
                        {
                            Legacy deserializedLegacy = legacy.ToObject<Legacy>();
                            Content.Legacies.Add(deserializedLegacy.guid, deserializedLegacy);
                            if (!isGroupHidden)
                            {
                                ListViewItem legacyLVI = new ListViewItem(deserializedLegacy.id)
                                {
                                    Tag = deserializedLegacy.guid,
                                    Name = deserializedLegacy.id
                                };
                                legaciesListView.Items.Add(legacyLVI);
                                if (legaciesListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    legaciesListView.Groups.Add(listViewGroup);
                                    legacyLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    legacyLVI.Group = legaciesListView.Groups[fileName];
                                }
                            }
                            deserializedLegacy.filename = fileName;
                        }
                        return;
                    case "endings":
                        foreach (JToken ending in parsedJToken.First.ToArray())
                        {
                            Ending deserializedEnding = ending.ToObject<Ending>();
                            Content.Endings.Add(deserializedEnding.guid, deserializedEnding);
                            if (!isGroupHidden)
                            {
                                ListViewItem endingLVI = new ListViewItem(deserializedEnding.id)
                                {
                                    Tag = deserializedEnding.guid,
                                    Name = deserializedEnding.id
                                };
                                endingsListView.Items.Add(endingLVI);
                                if (endingsListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    endingsListView.Groups.Add(listViewGroup);
                                    endingLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    endingLVI.Group = endingsListView.Groups[fileName];
                                }
                            }
                            deserializedEnding.filename = fileName;
                        }
                        return;
                    case "verbs":
                        foreach (JToken verb in parsedJToken.First.ToArray())
                        {
                            Verb deserializedVerb = verb.ToObject<Verb>();
                            Content.Verbs.Add(deserializedVerb.guid, deserializedVerb);
                            if (!isGroupHidden)
                            {
                                ListViewItem verbLVI = new ListViewItem(deserializedVerb.id)
                                {
                                    Tag = deserializedVerb.guid,
                                    Name = deserializedVerb.id
                                };
                                verbsListView.Items.Add(verbLVI);
                                if (verbsListView.Groups[fileName] == null)
                                {
                                    ListViewGroup listViewGroup = new ListViewGroup(fileName, fileName);
                                    verbsListView.Groups.Add(listViewGroup);
                                    verbLVI.Group = listViewGroup;
                                }
                                else
                                {
                                    verbLVI.Group = verbsListView.Groups[fileName];
                                }
                            }
                            deserializedVerb.filename = fileName;
                        }
                        return;
                    case "cultures":
                        foreach (JToken culture in parsedJToken.First.ToArray())
                        {
                            Culture deserializedCulture = culture.ToObject<Culture>();
                            Content.Cultures.Add(deserializedCulture.guid, deserializedCulture);
                        }
                        return;
                    default:
                        break;
                }
            }
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
            if (Content.Aspects.Count > 0)
            {
                Dictionary<string, List<Aspect>> sortedAspects = new Dictionary<string, List<Aspect>>();
                
                foreach (Aspect aspect in Content.Aspects.Values)
                {
                    if (!sortedAspects.ContainsKey(aspect.filename))
                    {
                        sortedAspects[aspect.filename] = new List<Aspect>();
                    }
                    sortedAspects[aspect.filename].Add(aspect);
                }

                foreach (KeyValuePair<string, List<Aspect>> keyValuePair in sortedAspects)
                {
                    JObject aspects = new JObject
                    {
                        ["elements"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedAspects = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedAspects);
                    }
                }
            }
            if (Content.Elements.Count > 0)
            {
                Dictionary<string, List<Element>> sortedElements = new Dictionary<string, List<Element>>();

                foreach (Element element in Content.Elements.Values)
                {
                    if (!sortedElements.ContainsKey(element.filename))
                    {
                        sortedElements[element.filename] = new List<Element>();
                    }
                    sortedElements[element.filename].Add(element);
                }

                foreach (KeyValuePair<string, List<Element>> keyValuePair in sortedElements)
                {
                    JObject elements = new JObject
                    {
                        ["elements"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedElements = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedElements);
                    }
                }
            }
            if (Content.Recipes.Count > 0)
            {
                Dictionary<string, List<Recipe>> sortedRecipes = new Dictionary<string, List<Recipe>>();

                foreach (Recipe recipe in Content.Recipes.Values)
                {
                    if (!sortedRecipes.ContainsKey(recipe.filename))
                    {
                        sortedRecipes[recipe.filename] = new List<Recipe>();
                    }
                    sortedRecipes[recipe.filename].Add(recipe);
                }

                foreach (KeyValuePair<string, List<Recipe>> keyValuePair in sortedRecipes)
                {
                    JObject recipes = new JObject
                    {
                        ["recipes"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedRecipes = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedRecipes);
                    }
                }
            }
            if (decksListView.Items.Count > 0)
            {
                Dictionary<string, List<Deck>> sortedDecks = new Dictionary<string, List<Deck>>();

                foreach (Deck deck in Content.Decks.Values)
                {
                    if (!sortedDecks.ContainsKey(deck.filename))
                    {
                        sortedDecks[deck.filename] = new List<Deck>();
                    }
                    sortedDecks[deck.filename].Add(deck);
                }

                foreach (KeyValuePair<string, List<Deck>> keyValuePair in sortedDecks)
                {
                    JObject decks = new JObject
                    {
                        ["decks"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedDecks = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedDecks);
                    }
                }
            }
            if (legaciesListView.Items.Count > 0)
            {
                Dictionary<string, List<Legacy>> sortedLegacies = new Dictionary<string, List<Legacy>>();

                foreach (Legacy legacy in Content.Legacies.Values)
                {
                    if (!sortedLegacies.ContainsKey(legacy.filename))
                    {
                        sortedLegacies[legacy.filename] = new List<Legacy>();
                    }
                    sortedLegacies[legacy.filename].Add(legacy);
                }

                foreach (KeyValuePair<string, List<Legacy>> keyValuePair in sortedLegacies)
                {
                    JObject legacies = new JObject
                    {
                        ["legacies"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedLegacies = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedLegacies);
                    }
                }
            }
            if (endingsListView.Items.Count > 0)
            {
                Dictionary<string, List<Ending>> sortedEndings = new Dictionary<string, List<Ending>>();

                foreach (Ending ending in Content.Endings.Values)
                {
                    if (!sortedEndings.ContainsKey(ending.filename))
                    {
                        sortedEndings[ending.filename] = new List<Ending>();
                    }
                    sortedEndings[ending.filename].Add(ending);
                }

                foreach (KeyValuePair<string, List<Ending>> keyValuePair in sortedEndings)
                {
                    JObject endings = new JObject
                    {
                        ["endings"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedEndings = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedEndings);
                    }
                }
            }
            if (verbsListView.Items.Count > 0)
            {
                Dictionary<string, List<Verb>> sortedVerbs = new Dictionary<string, List<Verb>>();

                foreach (Verb verb in Content.Verbs.Values)
                {
                    if (!sortedVerbs.ContainsKey(verb.filename))
                    {
                        sortedVerbs[verb.filename] = new List<Verb>();
                    }
                    sortedVerbs[verb.filename].Add(verb);
                }

                foreach (KeyValuePair<string, List<Verb>> keyValuePair in sortedVerbs)
                {
                    JObject verbs = new JObject
                    {
                        ["verbs"] = JArray.FromObject(keyValuePair.Value)
                    };
                    string serializedVerbs = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/" + keyValuePair.Key + ".json", FileMode.Create))))
                    {
                        jtw.WriteRaw(serializedVerbs);
                    }
                }
            }
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

        private void SaveType<T>(Dictionary<Guid, T> contentDict, string typeName, string location) where T : IGameObject
        {
            if (contentDict.Count > 0)
            {
                Dictionary<string, List<T>> sortedGameObjects = new Dictionary<string, List<T>>();

                foreach (T gameObject in contentDict.Values)
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
                        [typeName] = JArray.FromObject(keyValuePair.Value)
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

        public void SaveCustomManifest(string location)
        {
            if (isVanilla)
            {
                return;
            }

            if (Content.CustomManifest.Count > 0)
            {
                string CustomManifestJson = JsonConvert.SerializeObject(Content.CustomManifest, Formatting.Indented);
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
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), AspectsList_Assign, aspectsListView.SelectedItems[0]);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), null, aspectsListView.SelectedItems[0]);
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
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), DecksList_Assign, decksListView.SelectedItems[0]);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), null, decksListView.SelectedItems[0]);
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
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), ElementsList_Assign, elementsListView.SelectedItems[0]);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), null, elementsListView.SelectedItems[0]);
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
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), EndingsList_Assign, endingsListView.SelectedItems[0]);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), null, endingsListView.SelectedItems[0]);
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
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), LegaciesList_Assign, legaciesListView.SelectedItems[0]);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), null, legaciesListView.SelectedItems[0]);
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
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null, recipesListView.SelectedItems[0]);
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
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), VerbsList_Assign, verbsListView.SelectedItems[0]);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), null, verbsListView.SelectedItems[0]);
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

        private void AspectsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            aspectsListView.BeginUpdate();
            if (aspectsSearchTextBox.Text != "")
            {
                Aspect[] aspectsToAdd = SearchAspects(Content.Aspects.Values.ToList(), aspectsSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                aspectsListView.Items.Clear();
                foreach (Aspect aspect in aspectsToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("aspects")) ? hiddenGroups["aspects"].Contains(aspect.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (aspectsListView.Groups[aspect.filename] == null)
                        {
                            group = new ListViewGroup(aspect.filename, aspect.filename);
                        }
                        else
                        {
                            group = aspectsListView.Groups[aspect.filename];
                        }
                        ListViewItem item = new ListViewItem(aspect.id) { Tag = aspect.guid, Group = group, Name = aspect.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                aspectsListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                aspectsListView.Items.Clear();
                foreach (Aspect aspect in Content.Aspects.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("aspects")) ? hiddenGroups["aspects"].Contains(aspect.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (aspectsListView.Groups[aspect.filename] == null)
                        {
                            group = new ListViewGroup(aspect.filename, aspect.filename);
                        }
                        else
                        {
                            group = aspectsListView.Groups[aspect.filename];
                        }
                        ListViewItem item = new ListViewItem(aspect.id) { Tag = aspect.guid, Group = group, Name = aspect.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                aspectsListView.Items.AddRange(items.ToArray());
            }
            aspectsListView.EndUpdate();
        }

        private void ElementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            elementsListView.BeginUpdate();
            if (elementsSearchTextBox.Text != "")
            {
                Element[] elementsToAdd = SearchElements(Content.Elements.Values.ToList(), elementsSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                elementsListView.Items.Clear();
                foreach (Element element in elementsToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("elements")) ? hiddenGroups["elements"].Contains(element.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (elementsListView.Groups[element.filename] == null)
                        {
                            group = new ListViewGroup(element.filename, element.filename);
                        }
                        else
                        {
                            group = elementsListView.Groups[element.filename];
                        }
                        ListViewItem item = new ListViewItem(element.id) { Tag = element.guid, Group = group, Name = element.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                elementsListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                elementsListView.Items.Clear();
                foreach (Element element in Content.Elements.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("elements")) ? hiddenGroups["elements"].Contains(element.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (elementsListView.Groups[element.filename] == null)
                        {
                            group = new ListViewGroup(element.filename, element.filename);
                        }
                        else
                        {
                            group = elementsListView.Groups[element.filename];
                        }
                        ListViewItem item = new ListViewItem(element.id) { Tag = element.guid, Group = group, Name = element.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                elementsListView.Items.AddRange(items.ToArray());
            }
            elementsListView.EndUpdate();
        }

        private void RecipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListView.BeginUpdate();
            if (recipesSearchTextBox.Text != "")
            {
                Recipe[] recipesToAdd = SearchRecipes(Content.Recipes.Values.ToList(), recipesSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                recipesListView.Items.Clear();
                foreach (Recipe recipe in recipesToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("recipes")) ? hiddenGroups["recipes"].Contains(recipe.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (recipesListView.Groups[recipe.filename] == null)
                        {
                            group = new ListViewGroup(recipe.filename, recipe.filename);
                        }
                        else
                        {
                            group = recipesListView.Groups[recipe.filename];
                        }
                        ListViewItem item = new ListViewItem(recipe.id) { Tag = recipe.guid, Group = group, Name = recipe.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                recipesListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                recipesListView.Items.Clear();
                foreach (Recipe recipe in Content.Recipes.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("recipes")) ? hiddenGroups["recipes"].Contains(recipe.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (recipesListView.Groups[recipe.filename] == null)
                        {
                            group = new ListViewGroup(recipe.filename, recipe.filename);
                        }
                        else
                        {
                            group = recipesListView.Groups[recipe.filename];
                        }
                        ListViewItem item = new ListViewItem(recipe.id) { Tag = recipe.guid, Group = group, Name = recipe.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                recipesListView.Items.AddRange(items.ToArray());
            }
            recipesListView.EndUpdate();
        }

        private void DecksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            decksListView.BeginUpdate();
            if (decksSearchTextBox.Text != "")
            {
                Deck[] decksToAdd = SearchDecks(Content.Decks.Values.ToList(), decksSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                decksListView.Items.Clear();
                foreach (Deck deck in decksToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("decks")) ? hiddenGroups["decks"].Contains(deck.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (decksListView.Groups[deck.filename] == null)
                        {
                            group = new ListViewGroup(deck.filename, deck.filename);
                        }
                        else
                        {
                            group = decksListView.Groups[deck.filename];
                        }
                        ListViewItem item = new ListViewItem(deck.id) { Tag = deck.guid, Group = group, Name = deck.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                decksListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                decksListView.Items.Clear();
                foreach (Deck deck in Content.Decks.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("decks")) ? hiddenGroups["decks"].Contains(deck.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (decksListView.Groups[deck.filename] == null)
                        {
                            group = new ListViewGroup(deck.filename, deck.filename);
                        }
                        else
                        {
                            group = decksListView.Groups[deck.filename];
                        }
                        ListViewItem item = new ListViewItem(deck.id) { Tag = deck.guid, Group = group, Name = deck.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                decksListView.Items.AddRange(items.ToArray());
            }
            decksListView.EndUpdate();
        }

        private void LegaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            legaciesListView.BeginUpdate();
            if (legaciesSearchTextBox.Text != "")
            {
                Legacy[] legaciesToAdd = SearchLegacies(Content.Legacies.Values.ToList(), legaciesSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                legaciesListView.Items.Clear();
                foreach (Legacy legacy in legaciesToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("legacies")) ? hiddenGroups["legacies"].Contains(legacy.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (legaciesListView.Groups[legacy.filename] == null)
                        {
                            group = new ListViewGroup(legacy.filename, legacy.filename);
                        }
                        else
                        {
                            group = legaciesListView.Groups[legacy.filename];
                        }
                        ListViewItem item = new ListViewItem(legacy.id) { Tag = legacy.guid, Group = group, Name = legacy.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                legaciesListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                legaciesListView.Items.Clear();
                foreach (Legacy legacy in Content.Legacies.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("legacies")) ? hiddenGroups["legacies"].Contains(legacy.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (legaciesListView.Groups[legacy.filename] == null)
                        {
                            group = new ListViewGroup(legacy.filename, legacy.filename);
                        }
                        else
                        {
                            group = legaciesListView.Groups[legacy.filename];
                        }
                        ListViewItem item = new ListViewItem(legacy.id) { Tag = legacy.guid, Group = group, Name = legacy.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                legaciesListView.Items.AddRange(items.ToArray());
            }
            legaciesListView.EndUpdate();
        }

        private void EndingsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            endingsListView.BeginUpdate();
            if (endingsSearchTextBox.Text != "")
            {
                Ending[] endingsToAdd = SearchEndings(Content.Endings.Values.ToList(), endingsSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                endingsListView.Items.Clear();
                foreach (Ending ending in endingsToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("endings")) ? hiddenGroups["endings"].Contains(ending.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (endingsListView.Groups[ending.filename] == null)
                        {
                            group = new ListViewGroup(ending.filename, ending.filename);
                        }
                        else
                        {
                            group = endingsListView.Groups[ending.filename];
                        }
                        ListViewItem item = new ListViewItem(ending.id) { Tag = ending.guid, Group = group, Name = ending.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                endingsListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                endingsListView.Items.Clear();
                foreach (Ending ending in Content.Endings.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("endings")) ? hiddenGroups["endings"].Contains(ending.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (endingsListView.Groups[ending.filename] == null)
                        {
                            group = new ListViewGroup(ending.filename, ending.filename);
                        }
                        else
                        {
                            group = endingsListView.Groups[ending.filename];
                        }
                        ListViewItem item = new ListViewItem(ending.id) { Tag = ending.guid, Group = group, Name = ending.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                endingsListView.Items.AddRange(items.ToArray());
            }
            endingsListView.EndUpdate();
        }

        private void VerbsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            verbsListView.BeginUpdate();
            if (verbsSearchTextBox.Text != "")
            {
                Verb[] verbsToAdd = SearchVerbs(Content.Verbs.Values.ToList(), verbsSearchTextBox.Text);
                List<ListViewItem> items = new List<ListViewItem>();
                verbsListView.Items.Clear();
                foreach (Verb verb in verbsToAdd)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("verbs")) ? hiddenGroups["verbs"].Contains(verb.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (verbsListView.Groups[verb.filename] == null)
                        {
                            group = new ListViewGroup(verb.filename, verb.filename);
                        }
                        else
                        {
                            group = verbsListView.Groups[verb.filename];
                        }
                        ListViewItem item = new ListViewItem(verb.id) { Tag = verb.guid, Group = group, Name = verb.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                verbsListView.Items.AddRange(items.ToArray());
            }
            else
            {
                List<ListViewItem> items = new List<ListViewItem>();
                verbsListView.Items.Clear();
                foreach (Verb verb in Content.Verbs.Values)
                {
                    Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
                    bool isGroupHidden = (hiddenGroups != null && hiddenGroups.ContainsKey("verbs")) ? hiddenGroups["verbs"].Contains(verb.filename) : false;
                    if (!isGroupHidden)
                    {
                        ListViewGroup group;
                        if (verbsListView.Groups[verb.filename] == null)
                        {
                            group = new ListViewGroup(verb.filename, verb.filename);
                        }
                        else
                        {
                            group = verbsListView.Groups[verb.filename];
                        }
                        ListViewItem item = new ListViewItem(verb.id) { Tag = verb.guid, Group = group, Name = verb.id };
                        // group.Items.Add(item);
                        items.Add(item);
                    }
                }
                verbsListView.Items.AddRange(items.ToArray());
            }
            verbsListView.EndUpdate();
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
            RecipeFlowchartViewer rfv = new RecipeFlowchartViewer(Content.GetRecipe(id).Copy());
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

        private void OpenSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)aspectsListView.SelectedItems[0].Tag;
            Aspect aspectToEdit = Content.GetAspect(guid);
            if (aspectToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(aspectToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                if (!deserializedAspect.Equals(aspectToEdit))
                {
                    // Content.Aspects.Remove(aspectsListView.SelectedItems[0].Tag.ToString());
                    deserializedAspect.filename = aspectsListView.SelectedItems[0].Group.Name;
                    Content.Aspects[guid] = deserializedAspect;
                    aspectsListView.SelectedItems[0].Text = deserializedAspect.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Aspects[aspectsListView.SelectedItems[0].Tag.ToString()] = deserializedAspect.Copy();
                }
            }
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)elementsListView.SelectedItems[0].Tag;
            Element elementToEdit = Content.GetElement(guid);
            if (elementToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(elementToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                if (!deserializedElement.Equals(elementToEdit))
                {
                    // Content.Elements.Remove(elementsListView.SelectedItems[0].Tag.ToString());
                    deserializedElement.filename = elementsListView.SelectedItems[0].Group.Name;
                    Content.Elements[guid] = deserializedElement;
                    elementsListView.SelectedItems[0].Text = deserializedElement.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Elements[elementsListView.SelectedItems[0].Tag.ToString()] = deserializedElement.Copy();
                }
            }
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)recipesListView.SelectedItems[0].Tag;
            Recipe recipeToEdit = Content.GetRecipe(guid);
            if (recipeToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(recipeToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                if (!deserializedRecipe.Equals(recipeToEdit))
                {
                    // Content.Recipes.Remove(recipesListView.SelectedItems[0].Tag.ToString());
                    deserializedRecipe.filename = recipesListView.SelectedItems[0].Group.Name;
                    Content.Recipes[guid] = deserializedRecipe;
                    recipesListView.SelectedItems[0].Text = deserializedRecipe.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Recipes[recipesListView.SelectedItems[0].Tag.ToString()] = deserializedRecipe.Copy();
                }
            }
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)decksListView.SelectedItems[0].Tag;
            Deck deckToEdit = Content.GetDeck(guid);
            if (deckToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(deckToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                if (!deserializedDeck.Equals(deckToEdit))
                {
                    // Content.Decks.Remove(decksListView.SelectedItems[0].Tag.ToString());
                    deserializedDeck.filename = decksListView.SelectedItems[0].Group.Name;
                    Content.Decks[guid] = deserializedDeck;
                    decksListView.SelectedItems[0].Text = deserializedDeck.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Decks[decksListView.SelectedItems[0].Tag.ToString()] = deserializedDeck.Copy();
                }
            }
        }

        private void OpenSelectedLegacysJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)legaciesListView.SelectedItems[0].Tag;
            Legacy legacyToEdit = Content.GetLegacy(guid);
            if (legacyToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(legacyToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                if (!deserializedLegacy.Equals(legacyToEdit))
                {
                    // Content.Legacies.Remove(legaciesListView.SelectedItems[0].Tag.ToString());
                    deserializedLegacy.filename = legaciesListView.SelectedItems[0].Group.Name;
                    Content.Legacies[guid] = deserializedLegacy;
                    legaciesListView.SelectedItems[0].Text = deserializedLegacy.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Legacies[legaciesListView.SelectedItems[0].Tag.ToString()] = deserializedLegacy.Copy();
                }
            }
        }

        private void OpenSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)endingsListView.SelectedItems[0].Tag;
            Ending endingToEdit = Content.GetEnding(guid);
            if (endingToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(endingToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                if (!deserializedEnding.Equals(endingToEdit))
                {
                    // Content.Endings.Remove(endingsListView.SelectedItems[0].Tag.ToString());
                    deserializedEnding.filename = endingsListView.SelectedItems[0].Group.Name;
                    Content.Endings[guid] = deserializedEnding;
                    endingsListView.SelectedItems[0].Text = deserializedEnding.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Endings[endingsListView.SelectedItems[0].Tag.ToString()] = deserializedEnding.Copy();
                }
            }
        }

        private void OpenSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Guid guid = (Guid)verbsListView.SelectedItems[0].Tag;
            Verb verbToEdit = Content.GetVerb(guid);
            if (verbToEdit == null)
            {
                return;
            }

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(verbToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                if (!deserializedVerb.Equals(verbToEdit))
                {
                    // Content.Verbs.Remove(verbsListView.SelectedItems[0].Tag.ToString());
                    deserializedVerb.filename = verbsListView.SelectedItems[0].Group.Name;
                    Content.Verbs[guid] = deserializedVerb;
                    verbsListView.SelectedItems[0].Text = deserializedVerb.id;
                    MarkDirty();
                }
                else
                {
                    // Content.Verbs[verbsListView.SelectedItems[0].Tag.ToString()] = deserializedVerb.Copy();
                }
            }
        }

        private void DuplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedAspectToolStripMenuItem.Enabled)
            {
                return;
            }

            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = aspectsListView.SelectedItems[0].Group;
            Aspect newAspect = Content.Aspects[(Guid)aspectsListView.SelectedItems[0].Tag].Copy();
            string id = newAspect.id;
            if (aspectsListView.Items.ContainsKey(id))
            {
                id += "_";
                int tmp = 1;
                while (aspectsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newAspect.id = id;
            newAspect.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newAspect.id) { Tag = newGuid, Group = group, Name = newAspect.id };
            aspectsListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Aspects.Add(newGuid, newAspect);
            MarkDirty();
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedElementToolStripMenuItem.Enabled)
            {
                return;
            }

            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = elementsListView.SelectedItems[0].Group;
            Element newElement = Content.Elements[(Guid)elementsListView.SelectedItems[0].Tag].Copy();
            string id = newElement.id;
            if (elementsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (elementsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newElement.id = id;
            newElement.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newElement.id) { Tag = newGuid, Group = group, Name = newElement.id };
            elementsListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Elements.Add(newGuid, newElement);
            MarkDirty();
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedRecipeToolStripMenuItem.Enabled)
            {
                return;
            }

            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = recipesListView.SelectedItems[0].Group;
            Recipe newRecipe = Content.Recipes[(Guid)recipesListView.SelectedItems[0].Tag].Copy();
            string id = newRecipe.id;
            if (recipesListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (recipesListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newRecipe.id = id;
            newRecipe.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newRecipe.id) { Tag = newGuid, Group = group, Name = newRecipe.id };
            recipesListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Recipes.Add(newGuid, newRecipe);
            MarkDirty();
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedDeckToolStripMenuItem.Enabled)
            {
                return;
            }

            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = decksListView.SelectedItems[0].Group;
            Deck newDeck = Content.Decks[(Guid)decksListView.SelectedItems[0].Tag].Copy();
            string id = newDeck.id;
            if (decksListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (decksListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newDeck.id = id;
            newDeck.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newDeck.id) { Tag = newGuid, Group = group, Name = newDeck.id };
            decksListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Decks.Add(newGuid, newDeck);
            MarkDirty();
        }

        private void DuplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedLegacyToolStripMenuItem.Enabled)
            {
                return;
            }

            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = legaciesListView.SelectedItems[0].Group;
            Legacy newLegacy = Content.Legacies[(Guid)legaciesListView.SelectedItems[0].Tag].Copy();
            string id = newLegacy.id;
            if (legaciesListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (legaciesListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newLegacy.id = id;
            newLegacy.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newLegacy.id) { Tag = newGuid, Group = group, Name = newLegacy.id };
            legaciesListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Legacies.Add(newGuid, newLegacy);
            MarkDirty();
        }

        private void DuplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedEndingToolStripMenuItem.Enabled)
            {
                return;
            }

            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = endingsListView.SelectedItems[0].Group;
            Ending newEnding = Content.Endings[(Guid)endingsListView.SelectedItems[0].Tag].Copy();
            string id = newEnding.id;
            if (endingsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (endingsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newEnding.id = id;
            newEnding.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newEnding.id) { Tag = newGuid, Group = group, Name = newEnding.id };
            endingsListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Endings.Add(newGuid, newEnding);
            MarkDirty();
        }

        private void DuplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!duplicateSelectedVerbToolStripMenuItem.Enabled)
            {
                return;
            }

            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewGroup group = verbsListView.SelectedItems[0].Group;
            Verb newVerb = Content.Verbs[(Guid)verbsListView.SelectedItems[0].Tag].Copy();
            string id = newVerb.id;
            if (verbsListView.Items.ContainsKey(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (verbsListView.Items.ContainsKey(id + tmp.ToString()))
                {
                    tmp += 1;
                }
                id += tmp.ToString();
            }
            else
            {
                id += "_1";
            }
            newVerb.id = id;
            newVerb.filename = group.Name;
            Guid newGuid = Guid.NewGuid();
            ListViewItem newItem = new ListViewItem(newVerb.id) { Tag = newGuid, Group = group, Name = newVerb.id };
            verbsListView.Items.Add(newItem);
            // group.Items.Add(newItem);
            Content.Verbs.Add(newGuid, newVerb);
            MarkDirty();
        }

        private void ExportSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Aspect exportedAspect = Content.GetAspect((Guid)aspectsListView.SelectedItems[0].Tag);
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

            Element exportedElement = Content.GetElement((Guid)elementsListView.SelectedItems[0].Tag);
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

            Recipe exportedRecipe = Content.GetRecipe((Guid)recipesListView.SelectedItems[0].Tag);
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

            Deck exportedDeck = Content.GetDeck((Guid)decksListView.SelectedItems[0].Tag);
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

            Legacy exportedLegacy = Content.GetLegacy((Guid)legaciesListView.SelectedItems[0].Tag);
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

            Ending exportedEnding = Content.GetEnding((Guid)endingsListView.SelectedItems[0].Tag);
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

            Verb exportedVerb = Content.GetVerb((Guid)verbsListView.SelectedItems[0].Tag);
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

        private void CopySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Aspect exportedAspect = Content.GetAspect((Guid)aspectsListView.SelectedItems[0].Tag);
            if (exportedAspect == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedAspect);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Element exportedElement = Content.GetElement((Guid)elementsListView.SelectedItems[0].Tag);
            if (exportedElement == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedElement);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Recipe exportedRecipe = Content.GetRecipe((Guid)recipesListView.SelectedItems[0].Tag);
            if (exportedRecipe == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedRecipe);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1)
            {
                return;
            }

            Deck exportedDeck = Content.GetDeck((Guid)decksListView.SelectedItems[0].Tag);
            if (exportedDeck == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedDeck);
        }

        private void CopySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListView.SelectedItems.Count < 1)
            {
                return;
            }

            Legacy exportedLegacy = Content.GetLegacy((Guid)legaciesListView.SelectedItems[0].Tag);
            if (exportedLegacy == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedLegacy);
        }

        private void CopySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Ending exportedEnding = Content.GetEnding((Guid)endingsListView.SelectedItems[0].Tag);
            if (exportedEnding == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedEnding);
        }

        private void CopySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListView.SelectedItems.Count < 1)
            {
                return;
            }

            Verb exportedVerb = Content.GetVerb((Guid)verbsListView.SelectedItems[0].Tag);
            if (exportedVerb == null)
            {
                return;
            }

            CopyObjectJSONToClipboard(exportedVerb);
        }

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
                    AspectViewer av = new AspectViewer(Content.GetAspect(guid).Copy(), AspectsList_Assign, aspectsListView.SelectedItems[0]);
                    av.Show();
                }
                else
                {
                    AspectViewer av = new AspectViewer(Content.GetAspect(guid).Copy(), null, aspectsListView.SelectedItems[0]);
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
                    ElementViewer ev = new ElementViewer(Content.GetElement(guid).Copy(), ElementsList_Assign, elementsListView.SelectedItems[0]);
                    ev.Show();
                }
                else
                {
                    ElementViewer ev = new ElementViewer(Content.GetElement(guid).Copy(), null, elementsListView.SelectedItems[0]);
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
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(guid).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(guid).Copy(), null, recipesListView.SelectedItems[0]);
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
                    DeckViewer dv = new DeckViewer(Content.GetDeck(guid).Copy(), DecksList_Assign, decksListView.SelectedItems[0]);
                    dv.Show();
                }
                else
                {
                    DeckViewer dv = new DeckViewer(Content.GetDeck(guid).Copy(), null, decksListView.SelectedItems[0]);
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
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(guid).Copy(), RecipesList_Assign, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(guid).Copy(), null, recipesListView.SelectedItems[0]);
                    rv.Show();
                }
            }
        }

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
                        Content.GetAspect((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetElement((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetRecipe((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetDeck((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetLegacy((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetEnding((Guid)selectedItem.Tag).filename = newGroup;
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
                        Content.GetVerb((Guid)selectedItem.Tag).filename = newGroup;
                    }
                    MarkDirty();
                }
                Content.SetRecentGroup("Verb", newGroup);
            }
        }

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
                    EndingViewer ev = new EndingViewer(Content.GetEnding(guid).Copy(), EndingsList_Assign, endingsListView.SelectedItems[0]);
                    ev.Show();
                }
                else
                {
                    EndingViewer ev = new EndingViewer(Content.GetEnding(guid).Copy(), null, endingsListView.SelectedItems[0]);
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
                    VerbViewer vv = new VerbViewer(Content.GetVerb(guid).Copy(), VerbsList_Assign, verbsListView.SelectedItems[0]);
                    vv.Show();
                }
                else
                {
                    VerbViewer vv = new VerbViewer(Content.GetVerb(guid).Copy(), null, verbsListView.SelectedItems[0]);
                    vv.Show();
                }
            }
        }

        private void hideGroupAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["aspects"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("aspects", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["elements"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("elements", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["recipes"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("recipes", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["decks"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("decks", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["legacies"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("legacies", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["endings"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("endings", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private void hideGroupVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewGroup group = hideCurrentGroupShortTerm(ListViews["verbs"]);
            if (group==null) { return; }
            Content.SetHiddenGroup("verbs", group.Name);
            SaveCustomManifest(Content.currentDirectory);
        }

        private ListViewGroup hideCurrentGroupShortTerm(ListView lv)
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
            IsDirty = true;
            if (MarkDirtyEventHandler != null)
            {
                MarkDirtyEventHandler.Invoke(this, IsDirty);
            }
        }

        public bool GroupExistsAsHidden(string newGroup)
        {
            bool isGroupHidden = false;
            Dictionary<string, string[]> hiddenGroups = Content.CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            if (hiddenGroups != null)
            {
                foreach (string key in hiddenGroups.Keys)
                {
                    isGroupHidden |= hiddenGroups[key].Contains(newGroup) || hiddenGroups[key].Contains(newGroup + ".json");
                }
            }
            return isGroupHidden;
        }
    }
}
