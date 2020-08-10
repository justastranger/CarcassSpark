extern alias CultistSimulator;
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
                MessageBox.Show("Failed to load content source.");
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
                aspectsListBox.Items.Clear();
                Content.Aspects.Clear();
                elementsListBox.Items.Clear();
                Content.Elements.Clear();
                recipesListBox.Items.Clear();
                Content.Recipes.Clear();
                decksListBox.Items.Clear();
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
            catch (Exception)
            {
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
            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.synopsis = JsonConvert.DeserializeObject<Synopsis>(JsonConvert.SerializeObject(ht));
            Text = Content.synopsis.name;
        }

        public void LoadCustomManifest(FileStream file)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            Content.CustomManifest = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(ht));
        }

        public void LoadFile(FileStream file, string filePath)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            string newFileText = JsonConvert.SerializeObject(ht, Formatting.Indented);
            if (Settings.settings["saveCleanedVanillaContent"] != null && Settings.settings["saveCleanedVanillaContent"].ToObject<bool>() && isVanilla)
            {
                if (!Directory.Exists("Cleaned Files\\" + filePath.Substring(0, filePath.LastIndexOf('\\'))))
                {
                    Directory.CreateDirectory("Cleaned Files\\" + filePath.Substring(0, filePath.LastIndexOf('\\')));
                }
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open("Cleaned Files\\" + filePath, FileMode.Create))))
                {
                    jtw.WriteRaw(newFileText);
                }
            }
            JToken parsedJToken = JsonConvert.DeserializeObject<JObject>(newFileText).First;
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
                                _ = xtrigger.Name;
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
                                aspectsListBox.Items.Add(deserializedAspect.id);
                            }
                        }
                        else if (element["extends"] != null && Utilities.AspectExists(element["id"].ToString()))
                        {
                            Aspect deserializedAspect = element.ToObject<Aspect>();
                            if (!Content.Aspects.ContainsKey(deserializedAspect.id))
                            {
                                Content.Aspects.Add(deserializedAspect.id, deserializedAspect);
                                aspectsListBox.Items.Add(deserializedAspect.id);
                            }
                        }
                        else
                        {
                            Element deserializedElement = element.ToObject<Element>();
                            if (!Content.Elements.ContainsKey(deserializedElement.id))
                            {
                                Content.Elements.Add(deserializedElement.id, deserializedElement);
                                elementsListBox.Items.Add(deserializedElement.id);
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
                            recipesListBox.Items.Add(deserializedRecipe.id);
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
                            decksListBox.Items.Add(deserializedDeck.id);
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
                default:
                    break;
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
        }

        public void SaveMod()
        {
            SaveMod(Content.currentDirectory);
        }

        public void SaveMod(string location)
        {
            CreateDirectories(location);
            if (aspectsListBox.Items.Count > 0)
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
            if (elementsListBox.Items.Count > 0)
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
            if (recipesListBox.Items.Count > 0)
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
            if (decksListBox.Items.Count > 0)
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
            SaveManifests(location);
        }

        private void SaveManifests(string location)
        {
            string synopsisJson = JsonConvert.SerializeObject(Content.synopsis, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/synopsis.json", FileMode.Create))))
            {
                jtw.WriteRaw(synopsisJson);
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

        private void AspectListBox_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            if (editMode)
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(aspectsListBox.SelectedItem.ToString()), AspectsList_Assign);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(Content.GetAspect(aspectsListBox.SelectedItem.ToString()), null);
                av.Show();
            }
        }

        private void AspectsList_Assign(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result;
            aspectsListBox.Items[aspectsListBox.SelectedIndex] = result.id;
        }

        private void DecksListBox_DoubleClick(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            if (editMode)
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(decksListBox.SelectedItem.ToString()), DecksList_Assign);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(Content.GetDeck(decksListBox.SelectedItem.ToString()), null);
                dv.Show();
            }
        }

        private void DecksList_Assign(object sender, Deck result)
        {
            Content.Decks[result.id] = result;
            decksListBox.Items[decksListBox.SelectedIndex] = result.id;
        }

        private void ElementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            if (editMode)
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(elementsListBox.SelectedItem.ToString()), ElementsList_Assign);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(Content.GetElement(elementsListBox.SelectedItem.ToString()), null);
                ev.Show();
            }
        }

        private void ElementsList_Assign(object sender, Element result)
        {
            Content.Elements[result.id] = result;
            elementsListBox.Items[elementsListBox.SelectedIndex] = result.id;
        }

        private void EndingsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListBox.SelectedItem == null) return;
            if (editMode)
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(endingsListBox.SelectedItem.ToString()), EndingsList_Assign);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(Content.GetEnding(endingsListBox.SelectedItem.ToString()), null);
                ev.Show();
            }
        }

        private void EndingsList_Assign(object sender, Ending result)
        {
            Content.Endings[result.id] = result;
            endingsListBox.Items[endingsListBox.SelectedIndex] = result.id;
        }

        private void LegaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListBox.SelectedItem == null) return;
            if (editMode)
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(legaciesListBox.SelectedItem.ToString()), LegaciesList_Assign);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(Content.GetLegacy(legaciesListBox.SelectedItem.ToString()), null);
                lv.Show();
            }
        }

        private void LegaciesList_Assign(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result;
            legaciesListBox.Items[legaciesListBox.SelectedIndex] = result.id;
        }

        private void RecipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            if (editMode)
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(recipesListBox.SelectedItem.ToString()), RecipesList_Assign);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(Content.GetRecipe(recipesListBox.SelectedItem.ToString()), null);
                rv.Show();
            }
        }

        private void RecipesList_Assign(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result;
            recipesListBox.Items[recipesListBox.SelectedIndex] = result.id;
        }

        private void VerbsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            if (editMode)
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(verbsListBox.SelectedItem.ToString()), VerbsList_Assign);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(Content.GetVerb(verbsListBox.SelectedItem.ToString()), null);
                vv.Show();
            }
        }

        private void VerbsList_Assign(object sender, Verb result)
        {
            Content.Verbs[result.id] = result;
            verbsListBox.Items[verbsListBox.SelectedIndex] = result.id;
        }

        private void AspectsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            aspectsListBox.Items.Clear();
            try
            {
                string[] keysToAdd = SearchKeys(Content.Aspects.Keys.ToList(), aspectsSearchTextBox.Text);
                aspectsListBox.Items.AddRange(keysToAdd);
            }
            catch (Exception)
            {
                aspectsListBox.Items.AddRange(Content.Aspects.Keys.ToArray());
            }
        }

        private void ElementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            elementsListBox.Items.Clear();
            try
            {
                string[] keysToAdd = SearchKeys(Content.Elements.Keys.ToList(), elementsSearchTextBox.Text);
                elementsListBox.Items.AddRange(keysToAdd);
            }
            catch (Exception)
            {
                elementsListBox.Items.AddRange(Content.Elements.Keys.ToArray());
            }
        }

        private void RecipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListBox.Items.Clear();
            try
            {
                string[] keysToAdd = SearchKeys(Content.Recipes.Keys.ToList(), recipesSearchTextBox.Text);
                recipesListBox.Items.AddRange(keysToAdd);
            }
            catch (Exception)
            {
                recipesListBox.Items.AddRange(Content.Recipes.Keys.ToArray());
            }
        }

        private void DecksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            decksListBox.Items.Clear();
            try
            {
                string[] keysToAdd = SearchKeys(Content.Decks.Keys.ToList(), decksSearchTextBox.Text);
                decksListBox.Items.AddRange(keysToAdd);
            }
            catch (Exception)
            {
                decksListBox.Items.AddRange(Content.Decks.Keys.ToArray());
            }
        }

        private void LegaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            legaciesListBox.Items.Clear();
            try
            {
                string[] keysToAdd = SearchKeys(Content.Legacies.Keys.ToList(), legaciesSearchTextBox.Text);
                legaciesListBox.Items.AddRange(keysToAdd);
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
                string[] keysToAdd = SearchKeys(Content.Endings.Keys.ToList(), endingsSearchTextBox.Text);
                endingsListBox.Items.AddRange(keysToAdd);
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
                string[] keysToAdd = SearchKeys(Content.Verbs.Keys.ToList(), verbsSearchTextBox.Text);
                verbsListBox.Items.AddRange(keysToAdd);
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

        private void ElementsWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.aspects != null && element.aspects.ContainsKey(aspectsListBox.SelectedItem.ToString()))
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
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.xtriggers != null && element.xtriggers.ContainsKey(aspectsListBox.SelectedItem.ToString()))
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
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = aspectsListBox.SelectedItem.ToString();
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
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.aspects != null && (recipe.aspects.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.aspects[aspectsListBox.SelectedItem.ToString()] > 0))
                {
                    tmp.Add(recipe.id, recipe);
                }
                else if (recipe.effects != null && (recipe.effects.ContainsKey(aspectsListBox.SelectedItem.ToString()) && Convert.ToInt32(recipe.effects[aspectsListBox.SelectedItem.ToString()]) > 0))
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
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Element element in Content.Elements.Values)
            {
                // TODO foreach (Slot slot in element.slots) if (slot.requirements.contains(id)) tmp.add(element.id, element)
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.Show();
            }
        }

        private void ElementsThatDecayIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.decayTo != null && element.decayTo == elementsListBox.SelectedItem.ToString())
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
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            string id = elementsListBox.SelectedItem.ToString();
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
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = elementsListBox.SelectedItem.ToString();
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
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = elementsListBox.SelectedItem.ToString();
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
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Deck> tmp = new Dictionary<string, Deck>();
            foreach (Deck deck in Content.Decks.Values)
            {
                if (deck.spec != null && deck.spec.Contains(elementsListBox.SelectedItem.ToString()))
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
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Legacy> tmp = new Dictionary<string, Legacy>();
            foreach (Legacy legacy in Content.Legacies.Values)
            {
                if (legacy.effects != null && legacy.effects.ContainsKey(elementsListBox.SelectedItem.ToString()))
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
            if (recipesListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.linked != null) foreach (RecipeLink link in recipe.linked)
                    {
                        if (link.id == recipesListBox.SelectedItem.ToString())
                        {
                            tmp.Add(recipe.id, recipe);
                        }
                    }
                if (recipe.alt != null) foreach (RecipeLink link in recipe.alt)
                    {
                        if (link.id == recipesListBox.SelectedItem.ToString())
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
            if (decksListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.deckeffects != null && recipe.deckeffects.ContainsKey(decksListBox.SelectedItem.ToString()) && recipe.deckeffects[decksListBox.SelectedItem.ToString()] > 0)
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
            if (endingsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.ending != null && recipe.ending == endingsListBox.SelectedItem.ToString())
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
            if (verbsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in Content.Recipes.Values)
            {
                if (recipe.actionId != null && recipe.actionId == verbsListBox.SelectedItem.ToString())
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
            if (verbsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in Content.Elements.Values)
            {
                if (element.slots != null) foreach (Slot slot in element.slots)
                    {
                        if (slot.actionId == verbsListBox.SelectedItem.ToString() && !tmp.ContainsKey(element.id)) tmp.Add(element.id, element);
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
            if (recipesListBox.SelectedItem == null) return;
            Recipe selectedRecipe = Content.GetRecipe(recipesListBox.SelectedItem.ToString());
            RecipeFlowchartViewer rfv = new RecipeFlowchartViewer(selectedRecipe);
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
            if (aspectsListBox.SelectedItem == null) return;
            string selected = (string)aspectsListBox.SelectedItem;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                aspectsListBox.Items.Remove(selected);
                Content.Aspects.Remove(selected);
            }
        }

        private void DeleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            string selected = (string)elementsListBox.SelectedItem;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                elementsListBox.Items.Remove(selected);
                Content.Elements.Remove(selected);
            }
        }

        private void DeleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            string selected = (string)recipesListBox.SelectedItem;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                recipesListBox.Items.Remove(selected);
                Content.Recipes.Remove(selected);
            }
        }

        private void DeleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            string selected = (string)decksListBox.SelectedItem;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                decksListBox.Items.Remove(selected);
                Content.Decks.Remove(selected);
            }
        }

        private void DeleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = legaciesListBox.SelectedItem as string;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                legaciesListBox.Items.Remove(selected);
                Content.Legacies.Remove(selected);
            }
        }

        private void DeleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = endingsListBox.SelectedItem as string;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                endingsListBox.Items.Remove(selected);
                Content.Endings.Remove(selected);
            }
        }

        private void DeleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = verbsListBox.SelectedItem as string;
            if (ConfirmDelete(selected) == DialogResult.Yes)
            {
                verbsListBox.Items.Remove(selected);
                Content.Verbs.Remove(selected);
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

        private void AspectsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                aspectsListBox.SelectedIndex = -1;
                if (aspectsListBox.IndexFromPoint(e.Location) >= 0)
                {
                    aspectsListBox.SelectedIndex = aspectsListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void ElementsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                elementsListBox.SelectedIndex = -1;
                if (elementsListBox.IndexFromPoint(e.Location) >= 0)
                {
                    elementsListBox.SelectedIndex = elementsListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void RecipesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                recipesListBox.SelectedIndex = -1;
                if (recipesListBox.IndexFromPoint(e.Location) >= 0)
                {
                    recipesListBox.SelectedIndex = recipesListBox.IndexFromPoint(e.Location);
                }
            }
        }

        private void DecksListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                decksListBox.SelectedIndex = -1;
                if (decksListBox.IndexFromPoint(e.Location) >= 0)
                {
                    decksListBox.SelectedIndex = decksListBox.IndexFromPoint(e.Location);
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
            Aspect aspectToEdit = Content.GetAspect(aspectsListBox.SelectedItem as string);
            if (aspectToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(aspectToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(aspectToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                Content.Aspects[aspectsListBox.SelectedItem as string] = deserializedAspect;
            }
        }

        private void OpenSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element elementToEdit = Content.GetElement(elementsListBox.SelectedItem as string);
            if (elementToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(elementToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(elementToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                Content.Elements[elementsListBox.SelectedItem as string] = deserializedElement;
            }
        }

        private void OpenSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe recipeToEdit = Content.GetRecipe(recipesListBox.SelectedItem as string);
            if (recipeToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(recipeToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(recipeToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                Content.Recipes[recipesListBox.SelectedItem as string] = deserializedRecipe;
            }
        }

        private void OpenSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck deckToEdit = Content.GetDeck(decksListBox.SelectedItem as string);
            if (deckToEdit == null) return;

            JsonEditor je = new JsonEditor(Utilities.SerializeObject(deckToEdit), true, !editMode);

            //JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(deckToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                Content.Decks[decksListBox.SelectedItem as string] = deserializedDeck;
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
                Content.Legacies[legaciesListBox.SelectedItem as string] = deserializedLegacy;
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
                Content.Endings[endingsListBox.SelectedItem as string] = deserializedEnding;
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
                Content.Verbs[verbsListBox.SelectedItem as string] = deserializedVerb;
            }
        }

        private void DuplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aspect newAspect = Content.Aspects[aspectsListBox.SelectedItem as string].Copy();
            string id = newAspect.id;
            if (aspectsListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (aspectsListBox.Items.Contains(id + tmp.ToString()))
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
            aspectsListBox.Items.Add(newAspect.id);
            Content.Aspects.Add(newAspect.id, newAspect);
        }

        private void DuplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element newElement = Content.Elements[elementsListBox.SelectedItem as string].Copy();
            string id = newElement.id;
            if (elementsListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (elementsListBox.Items.Contains(id + tmp.ToString()))
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
            elementsListBox.Items.Add(newElement.id);
            Content.Elements.Add(newElement.id, newElement);
        }

        private void DuplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe newRecipe = Content.Recipes[recipesListBox.SelectedItem as string].Copy();
            string id = newRecipe.id;
            if (recipesListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (recipesListBox.Items.Contains(id + tmp.ToString()))
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
            recipesListBox.Items.Add(newRecipe.id);
            Content.Recipes.Add(newRecipe.id, newRecipe);
        }

        private void DuplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck newDeck = Content.Decks[decksListBox.SelectedItem as string].Copy();
            string id = newDeck.id;
            if (decksListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (decksListBox.Items.Contains(id + tmp.ToString()))
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
            decksListBox.Items.Add(newDeck.id);
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
            Aspect exportedAspect = Content.GetAspect(aspectsListBox.SelectedItem as string);
            if (exportedAspect == null) return;
            ExportObject(exportedAspect, exportedAspect.id);
        }

        private void ExportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element exportedElement = Content.GetElement(elementsListBox.SelectedItem as string);
            if (exportedElement == null) return;
            ExportObject(exportedElement, exportedElement.id);
        }

        private void ExportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe exportedRecipe = Content.GetRecipe(recipesListBox.SelectedItem as string);
            if (exportedRecipe == null) return;
            ExportObject(exportedRecipe, exportedRecipe.id);
        }

        private void ExportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck exportedDeck = Content.GetDeck(decksListBox.SelectedItem as string);
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
            if (aspectsListBox.SelectedItem == null) return;
            Aspect exportedAspect = Content.GetAspect(aspectsListBox.SelectedItem as string);
            if (exportedAspect == null) return;
            CopyObjectJSONToClipboard(exportedAspect);
        }

        private void CopySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Element exportedElement = Content.GetElement(elementsListBox.SelectedItem as string);
            if (exportedElement == null) return;
            CopyObjectJSONToClipboard(exportedElement);
        }

        private void CopySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            Recipe exportedRecipe = Content.GetRecipe(recipesListBox.SelectedItem as string);
            if (exportedRecipe == null) return;
            CopyObjectJSONToClipboard(exportedRecipe);
        }

        private void CopySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            Deck exportedDeck = Content.GetDeck(decksListBox.SelectedItem as string);
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
        
        public void AspectsList_Add(object sender, Aspect result)
        {
            Content.Aspects[result.id] = result;
            aspectsListBox.Items.Add(result.id);
        }

        public void DecksList_Add(object sender, Deck result)
        {
            Content.Decks[result.id] = result;
            decksListBox.Items.Add(result.id);
        }

        public void ElementsList_Add(object sender, Element result)
        {
            Content.Elements[result.id] = result;
            elementsListBox.Items.Add(result.id);
        }

        public void EndingsList_Add(object sender, Ending result)
        {
            Content.Endings[result.id] = result;
            endingsListBox.Items.Add(result.id);
        }

        public void LegaciesList_Add(object sender, Legacy result)
        {
            Content.Legacies[result.id] = result;
            legaciesListBox.Items.Add(result.id);
        }

        public void RecipesList_Add(object sender, Recipe result)
        {
            Content.Recipes[result.id] = result;
            recipesListBox.Items.Add(result.id);
        }

        public void VerbsList_Add(object sender, Verb result)
        {
            Content.Verbs[result.id] = result;
            verbsListBox.Items.Add(result.id);
        }
    }
}
