using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CarcassSpark.DictionaryViewers;
using CarcassSpark.Flowchart;
using CarcassSpark.Tools;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections;
using MindFusion.Json;

namespace CarcassSpark.ObjectViewers
{
    public partial class ModViewerTabControl : UserControl
    {
        public bool isVanilla, editMode, valid = false;

        public ContentSource Content = new ContentSource();
        
        public ModViewerTabControl(string location, bool isVanilla, bool newMod)
        {
            InitializeComponent();
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
                    valid = false;
                    return;
                }
            }
            // if loading content is successful
            if (LoadContent())
            {
                SetEditingMode(Content.GetCustomManifestBool("EditMode") ?? !isVanilla);
                Utilities.ContentSources.Add(isVanilla ? "Vanilla" : Content.synopsis.name, Content);
                valid = true;
            }
            else
            {
                valid = false;
                // MessageBox.Show("Failed to load content source.");
            }
        }

        public void SetEditingMode(bool editing)
        {
            editMode = editing;
            deleteSelectedAspectToolStripMenuItem.Visible = editing;
            deleteSelectedDeckToolStripMenuItem.Visible = editing;
            deleteSelectedElementToolStripMenuItem.Visible = editing;
            deleteSelectedEndingToolStripMenuItem.Visible = editing;
            deleteSelectedLegacyToolStripMenuItem.Visible = editing;
            deleteSelectedRecipeToolStripMenuItem.Visible = editing;
            deleteSelectedVerbToolStripMenuItem.Visible = editing;
            duplicateSelectedAspectToolStripMenuItem.Visible = editing;
            duplicateSelectedDeckToolStripMenuItem.Visible = editing;
            duplicateSelectedElementToolStripMenuItem.Visible = editing;
            duplicateSelectedEndingToolStripMenuItem.Visible = editing;
            duplicateSelectedLegacyToolStripMenuItem.Visible = editing;
            duplicateSelectedRecipeToolStripMenuItem.Visible = editing;
            duplicateSelectedVerbToolStripMenuItem.Visible = editing;
        }

        public bool LoadContent()
        {
            try
            {
                aspectsListView.Items.Clear();
                Content.Aspects.Clear();
                aspectsListView.Items.Clear();
                Content.Elements.Clear();
                recipesListView.Items.Clear();
                Content.Recipes.Clear();
                decksListView.Items.Clear();
                Content.Decks.Clear();
                legaciesListBox.Items.Clear();
                Content.Legacies.Clear();
                endingsListBox.Items.Clear();
                Content.Endings.Clear();
                verbsListBox.Items.Clear();
                Content.Verbs.Clear();
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
                        return true;
                    }
                    return false;
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
                MessageBox.Show(e.Message, "Content Source Loading Failed");
                return false;
            }
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
                    if (CreateSynopsis()) return true;
                    // otherwise return false so I can abort the creation of the tab
                    else return false;
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
            if (fileText != "" && fileText != null)
            {
                JToken parsedJToken = JsonConvert.DeserializeObject<JObject>(fileText).First;
                string fileType = parsedJToken.Path;
                switch (fileType)
                {
                    case "elements":
                        foreach (JToken element in parsedJToken.First.ToArray())
                        {
                            if (element["xtriggers"] != null)
                            {
                                foreach (JProperty xtrigger in element["xtriggers"])
                                {
                                    if (xtrigger.Value as JArray != null) xtrigger.Value = xtrigger.Value as JArray;
                                    else if (xtrigger.Value as JObject != null) xtrigger.Value = new JArray(xtrigger.Value);
                                    else if (xtrigger.Value.Value<string>() != null) xtrigger.Value = JArray.FromObject(new List<XTrigger>() { new XTrigger(xtrigger.Value.Value<string>()) });
                                }
                            }

                            if (element["isAspect"] != null)
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                if (!Content.Aspects.ContainsKey(deserializedAspect.id))
                                {
                                    Content.Aspects.Add(deserializedAspect.id, deserializedAspect);
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.GetHashCode()
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                }
                            }
                            else if (element["extends"] != null && Utilities.AspectExists(element["id"].ToString()))
                            {
                                Aspect deserializedAspect = element.ToObject<Aspect>();
                                if (!Content.Aspects.ContainsKey(deserializedAspect.id))
                                {
                                    Content.Aspects.Add(deserializedAspect.id, deserializedAspect);
                                    ListViewItem aspectLVI = new ListViewItem(deserializedAspect.id)
                                    {
                                        Tag = deserializedAspect.GetHashCode()
                                    };
                                    aspectsListView.Items.Add(aspectLVI);
                                }
                            }
                            else
                            {
                                Element deserializedElement = element.ToObject<Element>();
                                if (!Content.Elements.ContainsKey(deserializedElement.id))
                                {
                                    Content.Elements.Add(deserializedElement.id, deserializedElement);
                                    ListViewItem elementLVI = new ListViewItem(deserializedElement.id)
                                    {
                                        Tag = deserializedElement.GetHashCode()
                                    };
                                    elementsListView.Items.Add(elementLVI);
                                }
                            }
                        }
                        return;
                    case "recipes":
                        foreach (JToken recipe in parsedJToken.First.ToArray())
                        {
                            Recipe deserializedRecipe = recipe.ToObject<Recipe>();
                            if (!Content.Recipes.ContainsKey(deserializedRecipe.id))
                            {
                                Content.Recipes.Add(deserializedRecipe.id, deserializedRecipe);
                                ListViewItem recipeLVI = new ListViewItem(deserializedRecipe.id)
                                {
                                    Tag = deserializedRecipe.GetHashCode()
                                };
                                recipesListView.Items.Add(recipeLVI);
                            }
                        }
                        return;
                    case "decks":
                        foreach (JToken deck in parsedJToken.First.ToArray())
                        {
                            Deck deserializedDeck = deck.ToObject<Deck>();
                            if (!Content.Decks.ContainsKey(deserializedDeck.id))
                            {
                                Content.Decks.Add(deserializedDeck.id, deserializedDeck);
                                ListViewItem deckLVI = new ListViewItem(deserializedDeck.id)
                                {
                                    Tag = deserializedDeck.GetHashCode()
                                };
                                decksListView.Items.Add(deckLVI);
                            }
                        }
                        return;
                    case "legacies":
                        foreach (JToken legacy in parsedJToken.First.ToArray())
                        {
                            Legacy deserializedLegacy = legacy.ToObject<Legacy>();
                            if (!Content.Legacies.ContainsKey(deserializedLegacy.id))
                            {
                                Content.Legacies.Add(deserializedLegacy.id, deserializedLegacy);
                                legaciesListBox.Items.Add(deserializedLegacy.id);
                            }
                        }
                        return;
                    case "endings":
                        foreach (JToken ending in parsedJToken.First.ToArray())
                        {
                            Ending deserializedEnding = ending.ToObject<Ending>();
                            if (!Content.Endings.ContainsKey(deserializedEnding.id))
                            {
                                Content.Endings.Add(deserializedEnding.id, deserializedEnding);
                                endingsListBox.Items.Add(deserializedEnding.id);
                            }
                        }
                        return;
                    case "verbs":
                        foreach (JToken verb in parsedJToken.First.ToArray())
                        {
                            Verb deserializedVerb = verb.ToObject<Verb>();
                            if (!Content.Verbs.ContainsKey(deserializedVerb.id))
                            {
                                Content.Verbs.Add(deserializedVerb.id, deserializedVerb);
                                verbsListBox.Items.Add(deserializedVerb.id);
                            }
                        }
                        return;
                    case "cultures":
                        foreach (JToken culture in parsedJToken.First.ToArray())
                        {
                            Culture deserializedCulture = culture.ToObject<Culture>();
                            if (!Content.Cultures.ContainsKey(deserializedCulture.id))
                            {
                                Content.Cultures.Add(deserializedCulture.id, deserializedCulture);
                            }
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

        public void SaveMod()
        {
            SaveMod(Content.currentDirectory);
        }

        public void SaveMod(string location)
        {
            CreateDirectories(location);
            if (aspectsListView.Items.Count > 0)
            {
                JObject aspects = new JObject
                {
                    ["elements"] = JArray.FromObject(Content.Aspects.Values)
                };
                string aspectsJson = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/aspects.json", FileMode.Create))))
                {
                    jtw.WriteRaw(aspectsJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/aspects.json"))
                {
                    File.Delete(location + "/content/aspects.json");
                }
            }
            if (elementsListView.Items.Count > 0)
            {
                JObject elements = new JObject
                {
                    ["elements"] = JArray.FromObject(Content.Elements.Values)
                };
                string elementsJson = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/elements.json", FileMode.Create))))
                {
                    jtw.WriteRaw(elementsJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/elements.json"))
                {
                    File.Delete(location + "/content/elements.json");
                }
            }
            if (recipesListView.Items.Count > 0)
            {
                JObject recipes = new JObject
                {
                    ["recipes"] = JArray.FromObject(Content.Recipes.Values)
                };
                string recipesJson = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/recipes.json", FileMode.Create))))
                {
                    jtw.WriteRaw(recipesJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/recipes.json"))
                {
                    File.Delete(location + "/content/recipes.json");
                }
            }
            if (decksListView.Items.Count > 0)
            {
                JObject decks = new JObject
                {
                    ["decks"] = JArray.FromObject(Content.Decks.Values)
                };
                string decksJson = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/decks.json", FileMode.Create))))
                {
                    jtw.WriteRaw(decksJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/decks.json"))
                {
                    File.Delete(location + "/content/decks.json");
                }
            }
            if (legaciesListBox.Items.Count > 0)
            {
                JObject legacies = new JObject
                {
                    ["legacies"] = JArray.FromObject(Content.Legacies.Values)
                };
                string legaciesJson = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/legacies.json", FileMode.Create))))
                {
                    jtw.WriteRaw(legaciesJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/legacies.json"))
                {
                    File.Delete(location + "/content/legacies.json");
                }
            }
            if (endingsListBox.Items.Count > 0)
            {
                JObject endings = new JObject
                {
                    ["endings"] = JArray.FromObject(Content.Endings.Values)
                };
                string endingsJson = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/endings.json", FileMode.Create))))
                {
                    jtw.WriteRaw(endingsJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/endings.json"))
                {
                    File.Delete(location + "/content/endings.json");
                }
            }
            if (verbsListBox.Items.Count > 0)
            {
                JObject verbs = new JObject
                {
                    ["verbs"] = JArray.FromObject(Content.Verbs.Values)
                };
                string verbsJson = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/verbs.json", FileMode.Create))))
                {
                    jtw.WriteRaw(verbsJson);
                }
            }
            else
            {
                if (File.Exists(location + "/content/verbs.json"))
                {
                    File.Delete(location + "/content/verbs.json");
                }
            }
            if (Content.Cultures.Count > 0)
            {
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
            else
            {
                if (File.Exists(location + "/content/cultures.json"))
                {
                    File.Delete(location + "/content/cultures.json");
                }
            }
            SaveManifests(location);
        }

        public void SaveManifests(string location)
        {
            if (isVanilla) return;
            string synopsisJson = JsonConvert.SerializeObject(Content.synopsis, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/synopsis.json", FileMode.Create))))
            {
                jtw.WriteRaw(synopsisJson);
            }
            SaveCustomManifest(location);
        }

        private void SaveCustomManifest(string location)
        {
            if (isVanilla) return;
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
            if (aspectsListView.SelectedItems.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            if (editMode)
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), AspectsList_Assign);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), null);
                av.Show();
            }
        }

        private void AspectsList_Assign(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result.Copy();
            if (aspectsListView.Items[aspectsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Aspects.Remove(aspectsListView.Items[aspectsListView.SelectedIndices[0]].ToString());
                aspectsListView.Items[aspectsListView.SelectedIndices[0]].Text = result.id;
                aspectsListView.Items[aspectsListView.SelectedIndices[0]].Tag = result.GetHashCode();
            }
        }

        private void DecksListView_DoubleClick(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            string id = decksListView.SelectedItems[0].Text;
            if (editMode)
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), DecksList_Assign);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), null);
                dv.Show();
            }
        }

        private void DecksList_Assign(object sender, Deck result)
        {
            Content.Decks[result.id] = result.Copy();
            if (decksListView.Items[decksListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Decks.Remove(decksListView.Items[decksListView.SelectedIndices[0]].ToString());
                decksListView.Items[decksListView.SelectedIndices[0]].Text = result.id;
                decksListView.Items[decksListView.SelectedIndices[0]].Tag = result.GetHashCode();
            }
        }

        private void ElementsListView_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            if (editMode)
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), ElementsList_Assign);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), null);
                ev.Show();
            }
        }

        private void ElementsList_Assign(object sender, Element result)
        {
            Content.Elements[result.id] = result.Copy();
            if (elementsListView.Items[elementsListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Elements.Remove(elementsListView.Items[elementsListView.SelectedIndices[0]].ToString());
                elementsListView.Items[elementsListView.SelectedIndices[0]].Text = result.id;
                elementsListView.Items[elementsListView.SelectedIndices[0]].Tag = result.GetHashCode();
            }
        }

        private void EndingsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (!(endingsListBox.SelectedItem is string id)) return;
            if (editMode)
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), EndingsList_Assign);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), null);
                ev.Show();
            }
        }

        private void EndingsList_Assign(object sender, Ending result)
        {
            Content.Endings[result.id] = result.Copy();
            if (endingsListBox.Items[endingsListBox.SelectedIndex].ToString() != result.id)
            {
                Content.Endings.Remove(endingsListBox.Items[endingsListBox.SelectedIndex].ToString());
                endingsListBox.Items[endingsListBox.SelectedIndex] = result.id;
            }
        }

        private void LegaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (!(legaciesListBox.SelectedItem is string id)) return;
            if (editMode)
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), LegaciesList_Assign);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(id).Copy(), null);
                lv.Show();
            }
        }

        private void LegaciesList_Assign(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result.Copy();
            if (legaciesListBox.Items[legaciesListBox.SelectedIndex].ToString() != result.id)
            {
                Content.Legacies.Remove(legaciesListBox.Items[legaciesListBox.SelectedIndex].ToString());
                legaciesListBox.Items[legaciesListBox.SelectedIndex] = result.id;
            }
        }

        private void RecipesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            if (editMode)
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                rv.Show();
            }
        }

        private void RecipesList_Assign(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result.Copy();
            if (recipesListView.Items[recipesListView.SelectedIndices[0]].Text != result.id)
            {
                Content.Recipes.Remove(recipesListView.Items[recipesListView.SelectedIndices[0]].ToString());
                recipesListView.Items[recipesListView.SelectedIndices[0]].Text = result.id;
                recipesListView.Items[recipesListView.SelectedIndices[0]].Tag = result.GetHashCode();
            }
        }

        private void VerbsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (!(verbsListBox.SelectedItem is string id)) return;
            if (editMode)
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), VerbsList_Assign);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), null);
                vv.Show();
            }
        }

        private void VerbsList_Assign(object sender, Verb result)
        {
            Content.Verbs[result.id] = result.Copy();
            if (verbsListBox.Items[verbsListBox.SelectedIndex].ToString() != result.id)
            {
                Content.Verbs.Remove(verbsListBox.Items[verbsListBox.SelectedIndex].ToString());
                verbsListBox.Items[verbsListBox.SelectedIndex] = result.id;
            }
        }

        private void AspectsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            aspectsListView.Items.Clear();
            try
            {
                Aspect[] aspectsToAdd = SearchAspects(Content.Aspects.Values.ToList(), aspectsSearchTextBox.Text);
                aspectsListView.Items.AddRange(aspectsToAdd.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
            catch (Exception)
            {
                aspectsListView.Items.AddRange(Content.Aspects.Values.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
        }

        private void ElementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            elementsListView.Items.Clear();
            try
            {
                Element[] elementsToAdd = SearchElements(Content.Elements.Values.ToList(), elementsSearchTextBox.Text);
                elementsListView.Items.AddRange(elementsToAdd.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
            catch (Exception)
            {
                elementsListView.Items.AddRange(Content.Elements.Values.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
        }

        private void RecipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListView.Items.Clear();
            try
            {
                Recipe[] recipesToAdd = SearchRecipes(Content.Recipes.Values.ToList(), recipesSearchTextBox.Text);
                recipesListView.Items.AddRange(recipesToAdd.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
            catch (Exception)
            {
                recipesListView.Items.AddRange(Content.Recipes.Values.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
        }

        private void DecksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            decksListView.Items.Clear();
            try
            {
                Deck[] decksToAdd = SearchDecks(Content.Decks.Values.ToList(), decksSearchTextBox.Text);
                decksListView.Items.AddRange(decksToAdd.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
            catch (Exception)
            {
                decksListView.Items.AddRange(Content.Decks.Values.Select(a => new ListViewItem(a.id) { Tag = a.GetHashCode() }).ToArray());
            }
        }

        private void LegaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            legaciesListBox.Items.Clear();
            try
            {
                Legacy[] legaciesToAdd = SearchLegacies(Content.Legacies.Values.ToList(), legaciesSearchTextBox.Text);
                legaciesListBox.Items.AddRange(legaciesToAdd.Select(a => a.id).ToArray());
            }
            catch (Exception)
            {
                legaciesListBox.Items.AddRange(Content.Legacies.Keys.ToArray());
            }
        }

        private void EndingsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            endingsListBox.Items.Clear();
            try
            {
                Ending[] endingsToAdd = SearchEndings(Content.Endings.Values.ToList(), endingsSearchTextBox.Text);
                endingsListBox.Items.AddRange(endingsToAdd.Select(a => a.id).ToArray());
            }
            catch (Exception)
            {
                endingsListBox.Items.AddRange(Content.Endings.Keys.ToArray());
            }
        }

        private void VerbsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            verbsListBox.Items.Clear();
            try
            {
                Verb[] verbsToAdd = SearchVerbs(Content.Verbs.Values.ToList(), verbsSearchTextBox.Text);
                verbsListBox.Items.AddRange(verbsToAdd.Select(a => a.id).ToArray());
            }
            catch (Exception)
            {
                verbsListBox.Items.AddRange(Content.Verbs.Keys.ToArray());
            }
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
            Regex regex = new Regex(searchPattern);
            return (from aspect in aspectsList
                    where (aspect.id != null && regex.IsMatch(aspect.id))
                       || (aspect.label != null && regex.IsMatch(aspect.label))
                       || (aspect.description != null && regex.IsMatch(aspect.description))
                       || (aspect.comments != null && regex.IsMatch(aspect.comments))
                    select aspect).ToArray();
        }

        private Element[] SearchElements(List<Element> elementsList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from element in elementsList
                    where (element.id != null && regex.IsMatch(element.id))
                       || (element.label != null && regex.IsMatch(element.label))
                       || (element.id != null && regex.IsMatch(element.id))
                       || (element.comments != null && regex.IsMatch(element.comments))
                    select element).ToArray();
        }

        private Recipe[] SearchRecipes(List<Recipe> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from recipe in recipesList
                    where (recipe.id != null && regex.IsMatch(recipe.id))
                       || (recipe.label != null && regex.IsMatch(recipe.label)) 
                       || (recipe.description != null && regex.IsMatch(recipe.description))
                       || (recipe.startdescription != null && regex.IsMatch(recipe.startdescription))
                       || (recipe.comments != null && regex.IsMatch(recipe.comments))
                    select recipe).ToArray();
        }

        private Deck[] SearchDecks(List<Deck> decksList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from deck in decksList
                    where (deck.id != null && regex.IsMatch(deck.id))
                       || (deck.label != null && regex.IsMatch(deck.label))
                       || (deck.description != null && regex.IsMatch(deck.description))
                       || (deck.comments != null && regex.IsMatch(deck.comments))
                    select deck).ToArray();
        }

        private Legacy[] SearchLegacies(List<Legacy> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from legacy in recipesList
                    where (legacy.id != null && regex.IsMatch(legacy.id))
                       || (legacy.label != null && regex.IsMatch(legacy.label))
                       || (legacy.description != null && regex.IsMatch(legacy.description))
                       || (legacy.startdescription != null && regex.IsMatch(legacy.startdescription))
                       || (legacy.comments != null && regex.IsMatch(legacy.comments))
                    select legacy).ToArray();
        }

        private Ending[] SearchEndings(List<Ending> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from ending in recipesList
                    where (ending.id != null && regex.IsMatch(ending.id))
                       || (ending.label != null && regex.IsMatch(ending.label))
                       || (ending.description != null && regex.IsMatch(ending.description))
                       || (ending.comments != null && regex.IsMatch(ending.comments))
                    select ending).ToArray();
        }

        private Verb[] SearchVerbs(List<Verb> recipesList, string searchPattern)
        {
            Regex regex = new Regex(searchPattern);
            return (from verb in recipesList
                    where (verb.id != null && regex.IsMatch(verb.id))
                       || (verb.label != null && regex.IsMatch(verb.label))
                       || (verb.description != null && regex.IsMatch(verb.description))
                       || (verb.comments != null && regex.IsMatch(verb.comments))
                    select verb).ToArray();
        }

        private void ElementsWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.Items.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.aspects != null && element.aspects.ContainsKey(id))
                {
                    tmp.Add(element.id, element);
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
            if (aspectsListView.Items.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null && element.xtriggers.ContainsKey(id))
                {
                    tmp.Add(element.id, element);
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
            if (aspectsListView.Items.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null)
                {
                    if (recipe.requirements.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.requirements.ContainsValue(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.extantreqs.ContainsValue(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                    else if (recipe.tablereqs.ContainsValue(id)) tmp.Add(recipe.id, recipe);
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
            if (aspectsListView.Items.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.aspects != null && (recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0))
                {
                    tmp.Add(recipe.id, recipe);
                }
                else if (recipe.effects != null && (recipe.effects.ContainsKey(id) && Convert.ToInt32(recipe.effects[id]) > 0))
                {
                    tmp.Add(recipe.id, recipe);
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
            if (aspectsListView.Items.Count < 1) return;
            string id = aspectsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                // TODO foreach (Slot slot in element.slots) if (slot.requirements.contains(id)) tmp.add(element.id, element)
                foreach (Slot slot in element.slots)
                {
                    if (slot.required.ContainsKey(id)) tmp.Add(element.id, element);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.decayTo == id)
                {
                    tmp.Add(element.id, element);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null)
                {
                    foreach (KeyValuePair<string, List<XTrigger>> xtrigger in element.xtriggers)
                    {
                        foreach (XTrigger xtriggereffect in xtrigger.Value)
                        {
                            if (xtriggereffect.id == id) tmp.Add(element.id, element);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.requirements != null)
                {
                    if (recipe.requirements.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.extantreqs != null)
                {
                    if (recipe.extantreqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
                }
                else if (recipe.tablereqs != null)
                {
                    if (recipe.tablereqs.ContainsKey(id)) tmp.Add(recipe.id, recipe);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.effects != null && (recipe.effects.ContainsKey(id) || recipe.effects.ContainsValue(id)))
                {
                    tmp.Add(recipe.id, recipe);
                }
                else if (recipe.aspects != null && (recipe.aspects.ContainsKey(id) && recipe.aspects[id] > 0))
                {
                    tmp.Add(recipe.id, recipe);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Deck> tmp = new Dictionary<string, Deck>();
            foreach (Deck deck in Content.Decks.Values)
            {
                if (deck.spec != null && deck.spec.Contains(id))
                {
                    tmp.Add(deck.id, deck);
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
            if (elementsListView.Items.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            Dictionary<string, Legacy> tmp = new Dictionary<string, Legacy>();
            foreach (Legacy legacy in Content.Legacies.Values)
            {
                if (legacy.effects != null && legacy.effects.ContainsKey(id))
                {
                    tmp.Add(legacy.id, legacy);
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
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.linked != null) foreach (RecipeLink link in recipe.linked)
                    {
                        if (link.id == id)
                        {
                            tmp.Add(recipe.id, recipe);
                        }
                    }
                if (recipe.alt != null) foreach (RecipeLink link in recipe.alt)
                    {
                        if (link.id == id)
                        {
                            tmp.Add(recipe.id, recipe);
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
            if (decksListView.SelectedItems.Count < 1) return;
            string id = decksListView.SelectedItems[0].Text;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.deckeffects != null && recipe.deckeffects.ContainsKey(id) && recipe.deckeffects[id] > 0)
                {
                    tmp.Add(recipe.id, recipe);
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
            if (!(endingsListBox.SelectedItem is string id)) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.ending != null && recipe.ending == id)
                {
                    tmp.Add(recipe.id, recipe);
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
            if (!(verbsListBox.SelectedItem is string id)) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.actionId != null && recipe.actionId == id)
                {
                    tmp.Add(recipe.id, recipe);
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
            if (!(verbsListBox.SelectedItem is string id)) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.slots != null) foreach (Slot slot in element.slots)
                    {
                        if (slot.actionId == id) tmp[element.id] = element;
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
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
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
            if (!(aspectsListView.SelectedItems[0] is ListViewItem listViewItem)) return;
            if (ConfirmDelete(listViewItem.Text) == DialogResult.Yes)
            {
                aspectsListView.Items.Remove(listViewItem);
                Content.Aspects.Remove(listViewItem.Text);
            }
        }

        private void DeleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            string id = elementsListView.SelectedItems[0].Text;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                elementsListView.Items.RemoveByKey(id);
                Content.Elements.Remove(id);
            }
        }

        private void DeleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            string id = recipesListView.SelectedItems[0].Text;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                recipesListView.Items.RemoveByKey(id);
                Content.Recipes.Remove(id);
            }
        }

        private void DeleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            string id = decksListView.SelectedItems[0].Text;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                decksListView.Items.RemoveByKey(id);
                Content.Decks.Remove(id);
            }
        }

        private void DeleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(legaciesListBox.SelectedItem is string id)) return;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                legaciesListBox.Items.Remove(id);
                Content.Legacies.Remove(id);
            }
        }

        private void DeleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(endingsListBox.SelectedItem is string id)) return;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                endingsListBox.Items.Remove(id);
                Content.Endings.Remove(id);
            }
        }

        private void DeleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(verbsListBox.SelectedItem is string id)) return;
            if (ConfirmDelete(id) == DialogResult.Yes)
            {
                verbsListBox.Items.Remove(id);
                Content.Verbs.Remove(id);
            }
        }

        public DialogResult ConfirmDelete(string id)
        {
            if (id == null) return MessageBox.Show("Are you sure you'd like to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
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
                Point point = decksListView.PointToClient(Cursor.Position);
                if (decksListView.GetItemAt(point.X, point.Y) is ListViewItem listViewItem)
                {
                    listViewItem.Selected = true;
                }
            }
        }

        private void LegaciesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                legaciesListBox.SelectedIndex = -1;
                if (legaciesListBox.IndexFromPoint(e.Location) >= 0)
                {
                    legaciesListBox.SelectedIndex = legaciesListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void EndingsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                endingsListBox.SelectedIndex = -1;
                if (endingsListBox.IndexFromPoint(e.Location) >= 0)
                {
                    endingsListBox.SelectedIndex = endingsListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void VerbsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                verbsListBox.SelectedIndex = -1;
                if (verbsListBox.IndexFromPoint(e.Location) >= 0)
                {
                    verbsListBox.SelectedIndex = verbsListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void OpenSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect aspectToEdit = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (aspectToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(aspectToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(aspectToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                if (deserializedAspect.id != aspectsListView.SelectedItems[0].Text)
                {
                    Content.Aspects.Remove(aspectsListView.SelectedItems[0].Text);
                    Content.Aspects[deserializedAspect.id] = deserializedAspect.Copy();
                    aspectsListView.SelectedItems[0].Text = deserializedAspect.id;
                    aspectsListView.SelectedItems[0].Tag = deserializedAspect.GetHashCode();
                }
                else
                {
                    Content.Aspects[aspectsListView.SelectedItems[0].Text] = deserializedAspect.Copy();
                }
            }
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element elementToEdit = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (elementToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(elementToEdit), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                // Content.Elements[elementsListBox.SelectedItem as string] = deserializedElement;
                if (deserializedElement.id != elementsListView.SelectedItems[0].Text)
                {
                    Content.Elements.Remove(elementsListView.SelectedItems[0].Text);
                    Content.Elements[deserializedElement.id] = deserializedElement.Copy();
                    elementsListView.SelectedItems[0].Text = deserializedElement.id;
                    elementsListView.SelectedItems[0].Tag = deserializedElement.GetHashCode();
                }
                else
                {
                    Content.Elements[elementsListView.SelectedItems[0].Text] = deserializedElement.Copy();
                }
            }
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe recipeToEdit = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (recipeToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(recipeToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(recipeToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                // Content.Recipes[recipesListBox.SelectedItem as string] = deserializedRecipe;
                if (deserializedRecipe.id != recipesListView.SelectedItems[0].Text)
                {
                    Content.Recipes.Remove(recipesListView.SelectedItems[0].Text);
                    Content.Recipes[deserializedRecipe.id] = deserializedRecipe.Copy();
                    recipesListView.SelectedItems[0].Text = deserializedRecipe.id;
                    recipesListView.SelectedItems[0].Tag = deserializedRecipe.GetHashCode();
                }
                else
                {
                    Content.Recipes[recipesListView.SelectedItems[0].Text] = deserializedRecipe.Copy();
                }
            }
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck deckToEdit = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (deckToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(deckToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(deckToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                // Content.Decks[decksListBox.SelectedItem as string] = deserializedDeck;
                if (deserializedDeck.id != decksListView.SelectedItems[0].Text)
                {
                    Content.Decks.Remove(decksListView.SelectedItems[0].Text);
                    Content.Decks[deserializedDeck.id] = deserializedDeck.Copy();
                    decksListView.SelectedItems[0].Text = deserializedDeck.id;
                    decksListView.SelectedItems[0].Tag = deserializedDeck.GetHashCode();
                }
                else
                {
                    Content.Decks[decksListView.SelectedItems[0].Text] = deserializedDeck.Copy();
                }
            }
        }

        private void OpenSelectedLegacysJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy legacyToEdit = Content.GetLegacy(legaciesListBox.SelectedItem as string);
            if (legacyToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(legacyToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(legacyToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                // Content.Legacies[legaciesListBox.SelectedItem as string] = deserializedLegacy;
                if (deserializedLegacy.id != legaciesListBox.SelectedItem.ToString())
                {
                    Content.Legacies.Remove(legaciesListBox.SelectedItem as string);
                    Content.Legacies[deserializedLegacy.id] = deserializedLegacy;
                    legaciesListBox.SelectedItem = deserializedLegacy.id;
                }
                else
                {
                    Content.Legacies[legaciesListBox.SelectedItem as string] = deserializedLegacy;
                }
            }
        }

        private void OpenSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending endingToEdit = Content.GetEnding(endingsListBox.SelectedItem as string);
            if (endingToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(endingToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(endingToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                // Content.Endings[endingsListBox.SelectedItem as string] = deserializedEnding;
                if (deserializedEnding.id != endingsListBox.SelectedItem.ToString())
                {
                    Content.Endings.Remove(endingsListBox.SelectedItem as string);
                    Content.Endings[deserializedEnding.id] = deserializedEnding;
                    endingsListBox.SelectedItem = deserializedEnding.id;
                }
                else
                {
                    Content.Endings[endingsListBox.SelectedItem as string] = deserializedEnding;
                }
            }
        }

        private void OpenSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb verbToEdit = Content.GetVerb(verbsListBox.SelectedItem as string);
            if (verbToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(verbToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(verbToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                // Content.Verbs[verbsListBox.SelectedItem as string] = deserializedVerb;
                if (deserializedVerb.id != verbsListBox.SelectedItem.ToString())
                {
                    Content.Verbs.Remove(verbsListBox.SelectedItem as string);
                    Content.Verbs[deserializedVerb.id] = deserializedVerb;
                    verbsListBox.SelectedItem = deserializedVerb.id;
                }
                else
                {
                    Content.Verbs[verbsListBox.SelectedItem as string] = deserializedVerb;
                }
            }
        }

        private void DuplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect newAspect = Content.Aspects[aspectsListView.SelectedItems[0].Text].Copy();
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
            aspectsListView.Items.Add(new ListViewItem(newAspect.id) { Tag = newAspect.GetHashCode() });
            Content.Aspects.Add(newAspect.id, newAspect.Copy());
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element newElement = Content.Elements[elementsListView.SelectedItems[0].Text].Copy();
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
            elementsListView.Items.Add(new ListViewItem(newElement.id) { Tag = newElement.GetHashCode() });
            Content.Elements.Add(newElement.id, newElement.Copy());
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe newRecipe = Content.Recipes[recipesListView.SelectedItems[0].Text].Copy();
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
            recipesListView.Items.Add(new ListViewItem(newRecipe.id) { Tag = newRecipe.GetHashCode() });
            Content.Recipes.Add(newRecipe.id, newRecipe);
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck newDeck = Content.Decks[decksListView.SelectedItems[0].Text].Copy();
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
            decksListView.Items.Add(new ListViewItem(newDeck.id) { Tag = newDeck.GetHashCode() });
            Content.Decks.Add(newDeck.id, newDeck);
        }

        private void DuplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy newLegacy = Content.Legacies[legaciesListBox.SelectedItem as string].Copy();
            string id = newLegacy.id;
            if (legaciesListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (legaciesListBox.Items.Contains(id + tmp.ToString()))
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
            legaciesListBox.Items.Add(newLegacy.id);
            Content.Legacies.Add(newLegacy.id, newLegacy);
        }

        private void DuplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending newEnding = Content.Endings[endingsListBox.SelectedItem as string].Copy();
            string id = newEnding.id;
            if (endingsListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (endingsListBox.Items.Contains(id + tmp.ToString()))
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
            endingsListBox.Items.Add(newEnding.id);
            Content.Endings.Add(newEnding.id, newEnding);
        }

        private void DuplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb newVerb = Content.Verbs[verbsListBox.SelectedItem as string].Copy();
            string id = newVerb.id;
            if (verbsListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (verbsListBox.Items.Contains(id + tmp.ToString()))
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
            verbsListBox.Items.Add(newVerb.id);
            Content.Verbs.Add(newVerb.id, newVerb);
        }

        private void ExportSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect exportedAspect = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (exportedAspect == null) return;
            ExportObject(exportedAspect, exportedAspect.id);
        }

        private void ExportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element exportedElement = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (exportedElement == null) return;
            ExportObject(exportedElement, exportedElement.id);
        }

        private void ExportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe exportedRecipe = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (exportedRecipe == null) return;
            ExportObject(exportedRecipe, exportedRecipe.id);
        }

        private void ExportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck exportedDeck = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (exportedDeck == null) return;
            ExportObject(exportedDeck, exportedDeck.id);
        }

        private void ExportSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy exportedLegacy = Content.GetLegacy(legaciesListBox.SelectedItem as string);
            if (exportedLegacy == null) return;
            ExportObject(exportedLegacy, exportedLegacy.id);
        }

        private void ExportSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending exportedEnding = Content.GetEnding(endingsListBox.SelectedItem as string);
            if (exportedEnding == null) return;
            ExportObject(exportedEnding, exportedEnding.id);
        }

        private void ExportSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Content.GetVerb(verbsListBox.SelectedItem as string) == null) return;
            Verb exportedVerb = Content.GetVerb(verbsListBox.SelectedItem as string);
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
            // string JSON = JsonConvert.SerializeObject(objectToExport, Formatting.Indented);
            Clipboard.SetText(Utilities.SerializeObject(objectToExport));
        }

        private void CopySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListView.SelectedItems.Count < 1) return;
            Aspect exportedAspect = Content.GetAspect(aspectsListView.SelectedItems[0].Text);
            if (exportedAspect == null) return;
            CopyObjectJSONToClipboard(exportedAspect);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            Element exportedElement = Content.GetElement(elementsListView.SelectedItems[0].Text);
            if (exportedElement == null) return;
            CopyObjectJSONToClipboard(exportedElement);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            Recipe exportedRecipe = Content.GetRecipe(recipesListView.SelectedItems[0].Text);
            if (exportedRecipe == null) return;
            CopyObjectJSONToClipboard(exportedRecipe);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            Deck exportedDeck = Content.GetDeck(decksListView.SelectedItems[0].Text);
            if (exportedDeck == null) return;
            CopyObjectJSONToClipboard(exportedDeck);
        }

        private void CopySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legaciesListBox.SelectedItem == null) return;
            Legacy exportedLegacy = Content.GetLegacy(legaciesListBox.SelectedItem as string);
            if (exportedLegacy == null) return;
            CopyObjectJSONToClipboard(exportedLegacy);
        }

        private void CopySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListBox.SelectedItem == null) return;
            Ending exportedEnding = Content.GetEnding(endingsListBox.SelectedItem as string);
            if (exportedEnding == null) return;
            CopyObjectJSONToClipboard(exportedEnding);
        }

        private void CopySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            Verb exportedVerb = Content.GetVerb(verbsListBox.SelectedItem as string);
            if (exportedVerb == null) return;
            CopyObjectJSONToClipboard(exportedVerb);
        }

        private void NewAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspectViewer av = new AspectViewer(new Aspect(), AspectsList_Add);
            av.Show();
        }

        private void NewElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), ElementsList_Add);
            ev.Show();
        }

        private void NewRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), RecipesList_Add);
            rv.Show();
        }

        private void NewDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), DecksList_Add);
            dv.Show();
        }

        private void NewLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), LegaciesList_Add);
            lv.Show();
        }

        private void NewEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), EndingsList_Add);
            ev.Show();
        }

        private void NewVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), VerbsList_Add);
            vv.Show();
        }

        public void AspectsList_Add(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result.Copy();
            aspectsListView.Items.Add(new ListViewItem(result.id) { Tag = result.GetHashCode() });
        }

        public void ElementsList_Add(object sender, Element result)
        {
            Content.Elements[result.id] = result;
            elementsListView.Items.Add(new ListViewItem(result.id) { Tag = result.GetHashCode() });
        }

        public void RecipesList_Add(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result;
            elementsListView.Items.Add(new ListViewItem(result.id) { Tag = result.GetHashCode() });
        }

        public void DecksList_Add(object sender, Deck result)
        {
            Content.Decks[result.id] = result;
            decksListView.Items.Add(new ListViewItem(result.id) { Tag = result.GetHashCode() });
        }

        public void LegaciesList_Add(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result;
            legaciesListBox.Items.Add(result.id);
        }

        public void EndingsList_Add(object sender, Ending result)
        {
            Content.Endings[result.id] = result;
            endingsListBox.Items.Add(result.id);
        }

        public void VerbsList_Add(object sender, Verb result)
        {
            Content.Verbs[result.id] = result;
            verbsListBox.Items.Add(result.id);
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
            if (aspectsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = aspectsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), AspectsList_Assign);
                    av.Show();
                }
                else
                {
                    AspectViewer av = new AspectViewer(Content.GetAspect(id).Copy(), null);
                    av.Show();
                }
            }
        }

        private void ElementsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (elementsListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = elementsListView.SelectedItems[0].Text;
                if (editMode)
                {
                    ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), ElementsList_Assign);
                    ev.Show();
                }
                else
                {
                    ElementViewer ev = new ElementViewer(Content.GetElement(id).Copy(), null);
                    ev.Show();
                }
            }
        }

        private void RecipesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (recipesListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = recipesListView.SelectedItems[0].Text;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                    rv.Show();
                }
            }
        }

        private void DecksListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (decksListView.SelectedItems.Count < 1) return;
            if (e.KeyCode == Keys.Enter)
            {
                string id = decksListView.SelectedItems[0].Text;
                if (editMode)
                {
                    DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), DecksList_Assign);
                    dv.Show();
                }
                else
                {
                    DeckViewer dv = new DeckViewer(Content.GetDeck(id).Copy(), null);
                    dv.Show();
                }
            }
        }

        private void LegaciesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!(legaciesListBox.SelectedItem is string id)) return;
                if (editMode)
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), RecipesList_Assign);
                    rv.Show();
                }
                else
                {
                    RecipeViewer rv = new RecipeViewer(Content.GetRecipe(id).Copy(), null);
                    rv.Show();
                }
            }
        }

        private void EndingsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!(endingsListBox.SelectedItem is string id)) return;
                if (editMode)
                {
                    EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), EndingsList_Assign);
                    ev.Show();
                }
                else
                {
                    EndingViewer ev = new EndingViewer(Content.GetEnding(id).Copy(), null);
                    ev.Show();
                }
            }
        }

        private void VerbsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!(verbsListBox.SelectedItem is string id)) return;
                if (editMode)
                {
                    VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), VerbsList_Assign);
                    vv.Show();
                }
                else
                {
                    VerbViewer vv = new VerbViewer(Content.GetVerb(id).Copy(), null);
                    vv.Show();
                }
            }
        }


    }
}
