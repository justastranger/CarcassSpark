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

namespace CarcassSpark.ObjectViewers
{
    public partial class ModViewer : Form
    {
        public string currentDirectory;
        public Manifest manifest;
        public bool isVanilla, editMode = false;
        
        public Dictionary<string, Aspect> aspectsList = new Dictionary<string, Aspect>();
        public Dictionary<string, Element> elementsList = new Dictionary<string, Element>();
        public Dictionary<string, Recipe> recipesList = new Dictionary<string, Recipe>();
        public Dictionary<string, Deck> decksList = new Dictionary<string, Deck>();
        public Dictionary<string, Legacy> legaciesList = new Dictionary<string, Legacy>();
        public Dictionary<string, Ending> endingsList = new Dictionary<string, Ending>();
        public Dictionary<string, Verb> verbsList = new Dictionary<string, Verb>();

        public ModViewer(string location, bool isVanilla)
        {
            InitializeComponent();
            currentDirectory = location;
            this.isVanilla = isVanilla;
            setEditingMode(!isVanilla);
            saveFileDialog.InitialDirectory = currentDirectory;
            openFileDialog.InitialDirectory = currentDirectory;
            refreshContent();
        }

        public ModViewer(bool newMod, string location)
        {
            InitializeComponent();
            currentDirectory = location;
            if (newMod)
            {
                createManifest();
                editMode = true;
            }
            refreshContent();
        }

        public ModViewer(string location)
        {
            InitializeComponent();
            currentDirectory = location;
            refreshContent();
        }

        void setEditingMode(bool editing)
        {
            editMode = editing;
            toggleEditModeToolStripMenuItem.Checked = editing;
            toolStrip1.Visible = editing;
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
            toggleEditModeToolStripMenuItem.Checked = editing;
            /* Uncomment to hide the Open Selected <Object>'s JSON outside of edit mode
            openSelectedAspectsJSONToolStripMenuItem.Visible = editing;
            openSelectedDecksJSONToolStripMenuItem.Visible = editing;
            openSelectedElementsJSONToolStripMenuItem.Visible = editing;
            openSelectedEndingsJSONToolStripMenuItem.Visible = editing;
            openSelectedLegacysJSONToolStripMenuItem.Visible = editing;
            openSelectedRecipesJSONToolStripMenuItem.Visible = editing;
            openSelectedVerbsJSONToolStripMenuItem.Visible = editing;
            */
        }

        void refreshContent()
        {
            aspectsListBox.Items.Clear();
            aspectsList.Clear();
            elementsListBox.Items.Clear();
            elementsList.Clear();
            recipesListBox.Items.Clear();
            recipesList.Clear();
            decksListBox.Items.Clear();
            decksList.Clear();
            legaciesListBox.Items.Clear();
            legaciesList.Clear();
            endingsListBox.Items.Clear();
            endingsList.Clear();
            verbsListBox.Items.Clear();
            verbsList.Clear();
            if (!isVanilla) checkForManifest();
            if (isVanilla) foreach (string file in Directory.EnumerateFiles(currentDirectory, "*.json", SearchOption.AllDirectories))
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    loadFile(fs, file);
                }
            }
            else if (Directory.Exists(currentDirectory + "\\content\\")) foreach (string file in Directory.EnumerateFiles(currentDirectory + "\\content\\", "*.json", SearchOption.AllDirectories))
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    loadFile(fs, file);
                }
            }
        }

        public void createManifest()
        {
            ManifestViewer mv = new ManifestViewer(new Manifest());
            DialogResult mvdr = mv.ShowDialog();
            if (mvdr == DialogResult.OK)
            {
                manifest = mv.displayedManifest;
                saveMod(currentDirectory);
            }
        }

        public void checkForManifest()
        {
            string manifestPath = currentDirectory + "/manifest.json";
            if (File.Exists(currentDirectory + "/manifest.json"))
            {
                using (FileStream fs = new FileStream(manifestPath, FileMode.Open))
                {
                    loadManifest(fs);
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("manifest.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                createManifest();
            }
        }

        public void loadManifest(FileStream file)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            manifest = JsonConvert.DeserializeObject<Manifest>(JsonConvert.SerializeObject(ht));
            Text = manifest.name;
        }

        public void loadFile(FileStream file, string filePath)
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
                                string catalyst = xtrigger.Name;
                                if (xtrigger.Value as JArray != null) xtrigger.Value = xtrigger.Value as JArray;
                                else if (xtrigger.Value as JObject != null) xtrigger.Value = new JArray(xtrigger.Value);
                                else if (xtrigger.Value.Value<string>() != null) xtrigger.Value = JArray.FromObject(new List<XTrigger>() { new XTrigger(xtrigger.Value.Value<string>()) });
                            }
                        }

                        if (element["isAspect"] != null)
                        {
                            Aspect deserializedAspect = element.ToObject<Aspect>();
                            if (!aspectsList.ContainsKey(deserializedAspect.id))
                            {
                                aspectsList.Add(deserializedAspect.id, deserializedAspect);
                                aspectsListBox.Items.Add(deserializedAspect.id);
                            }
                        }
                        else if (element["extends"] != null && Utilities.aspectExists(element["id"].ToString()))
                        {
                            Aspect deserializedAspect = element.ToObject<Aspect>();
                            if (!aspectsList.ContainsKey(deserializedAspect.id))
                            {
                                aspectsList.Add(deserializedAspect.id, deserializedAspect);
                                aspectsListBox.Items.Add(deserializedAspect.id);
                            }
                        }
                        else
                        {
                            Element deserializedElement = element.ToObject<Element>();
                            if (!elementsList.ContainsKey(deserializedElement.id))
                            {
                                elementsList.Add(deserializedElement.id, deserializedElement);
                                elementsListBox.Items.Add(deserializedElement.id);
                            }
                        }
                    }
                    return;
                case "recipes":
                    foreach (JToken recipe in parsedJToken.First.ToArray())
                    {
                        // if (recipe["extantreqs"] as JObject != null) foreach (JProperty extantreq in recipe["extantreqs"])
                        // {
                        //     if (extantreq.Value.Value<string>() == "-") extantreq.Value = "-1";
                        // }
                        Recipe deserializedRecipe = recipe.ToObject<Recipe>();
                        if (!recipesList.ContainsKey(deserializedRecipe.id))
                        {
                            recipesList.Add(deserializedRecipe.id, deserializedRecipe);
                            recipesListBox.Items.Add(deserializedRecipe.id);
                        }
                    }
                    return;
                case "decks":
                    foreach (JToken deck in parsedJToken.First.ToArray())
                    {
                        Deck deserializedDeck = deck.ToObject<Deck>();
                        if (!decksList.ContainsKey(deserializedDeck.id))
                        {
                            decksList.Add(deserializedDeck.id, deserializedDeck);
                            decksListBox.Items.Add(deserializedDeck.id);
                        }
                    }
                    return;
                case "legacies":
                    foreach (JToken legacy in parsedJToken.First.ToArray())
                    {
                        Legacy deserializedLegacy = legacy.ToObject<Legacy>();
                        if (!legaciesList.ContainsKey(deserializedLegacy.id))
                        {
                            legaciesList.Add(deserializedLegacy.id, deserializedLegacy);
                            legaciesListBox.Items.Add(deserializedLegacy.id);
                        }
                    }
                    return;
                case "endings":
                    foreach(JToken ending in parsedJToken.First.ToArray())
                    {
                        Ending deserializedEnding = ending.ToObject<Ending>();
                        if (!endingsList.ContainsKey(deserializedEnding.id))
                        {
                            endingsList.Add(deserializedEnding.id, deserializedEnding);
                            endingsListBox.Items.Add(deserializedEnding.id);
                        }
                    }
                    return;
                case "verbs":
                    foreach(JToken verb in parsedJToken.First.ToArray())
                    {
                        Verb deserializedVerb = verb.ToObject<Verb>();
                        if (!verbsList.ContainsKey(deserializedVerb.id))
                        {
                            verbsList.Add(deserializedVerb.id, deserializedVerb);
                            verbsListBox.Items.Add(deserializedVerb.id);
                        }
                    }
                    return;
                default:
                    break;
            }
        }


        public Element getElement(string id)
        {
            if (elementExists(id)) return elementsList[id];
            else return null;
        }

        public bool elementExists(string id)
        {
            return elementsList.ContainsKey(id);
        }

        public Deck getDeck(string id)
        {
            if (deckExists(id)) return decksList[id];
            else return null;
        }

        public bool deckExists(string id)
        {
            return decksList.ContainsKey(id);
        }
        public Aspect getAspect(string id)
        {
            if (aspectExists(id)) return aspectsList[id];
            else return null;
        }

        public bool aspectExists(string id)
        {
            return aspectsList.ContainsKey(id);
        }

        public Legacy getLegacy(string id)
        {
            if (legacyExists(id)) return legaciesList[id];
            else return null;
        }

        public bool legacyExists(string id)
        {
            return legaciesList.ContainsKey(id);
        }

        public Recipe getRecipe(string id)
        {
            if (recipeExists(id)) return recipesList[id];
            else return null;
        }

        public bool recipeExists(string id)
        {
            return recipesList.ContainsKey(id);
        }
        
        public Ending getEnding(string id)
        {
            if (endingExists(id)) return endingsList[id];
            else return null;
        }

        public bool endingExists(string id)
        {
            return endingsList.ContainsKey(id);
        }

        public Verb getVerb(string id)
        {
            if (verbExists(id)) return verbsList[id];
            else return null;
        }

        public bool verbExists(string id)
        {
            return verbsList.ContainsKey(id);
        }

        private void aspectListBox_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            if (editMode)
            {
                AspectViewer av = new AspectViewer(getAspect(aspectsListBox.SelectedItem.ToString()), aspectsList_Assign);
                av.Show();
            }
            else
            {
                AspectViewer av = new AspectViewer(getAspect(aspectsListBox.SelectedItem.ToString()), null);
                av.Show();
            }
        }

        private void aspectsList_Assign(object sender, Aspect result)
        {
            aspectsList[result.id] = result;
            aspectsListBox.Items[aspectsListBox.SelectedIndex] = result.id;
            // aspectsListBox.SelectedItem = result.id;
        }

        private void decksListBox_DoubleClick(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            if (editMode)
            {
                DeckViewer dv = new DeckViewer(getDeck(decksListBox.SelectedItem.ToString()), decksList_Assign);
                dv.Show();
            }
            else
            {
                DeckViewer dv = new DeckViewer(getDeck(decksListBox.SelectedItem.ToString()), null);
                dv.Show();
            }
        }

        private void decksList_Assign(object sender, Deck result)
        {
            decksList[result.id] = result;
            decksListBox.Items[decksListBox.SelectedIndex] = result.id;
        }

        private void elementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            if (editMode)
            {
                ElementViewer ev = new ElementViewer(getElement(elementsListBox.SelectedItem.ToString()), elementsList_Assign);
                ev.Show();
            }
            else
            {
                ElementViewer ev = new ElementViewer(getElement(elementsListBox.SelectedItem.ToString()), null);
                ev.Show();
            }
        }

        private void elementsList_Assign(object sender, Element result)
        {
            elementsList[result.id] = result;
            elementsListBox.Items[elementsListBox.SelectedIndex] = result.id;
        }

        private void endingsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListBox.SelectedItem == null) return;
            if (editMode)
            {
                EndingViewer ev = new EndingViewer(getEnding(endingsListBox.SelectedItem.ToString()), endingsList_Assign);
                ev.Show();
            }
            else
            {
                EndingViewer ev = new EndingViewer(getEnding(endingsListBox.SelectedItem.ToString()), null);
                ev.Show();
            }
        }

        private void endingsList_Assign(object sender, Ending result)
        {
            endingsList[result.id] = result;
            endingsListBox.Items[endingsListBox.SelectedIndex] = result.id;
        }

        private void legaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListBox.SelectedItem == null) return;
            if (editMode)
            {
                LegacyViewer lv = new LegacyViewer(getLegacy(legaciesListBox.SelectedItem.ToString()), legaciesList_Assign);
                lv.Show();
            }
            else
            {
                LegacyViewer lv = new LegacyViewer(getLegacy(legaciesListBox.SelectedItem.ToString()), null);
                lv.Show();
            }
        }

        private void legaciesList_Assign(object sender, Legacy result)
        {
            legaciesList[result.id] = result;
            legaciesListBox.Items[legaciesListBox.SelectedIndex] = result.id;
        }

        private void recipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            if (editMode)
            {
                RecipeViewer rv = new RecipeViewer(getRecipe(recipesListBox.SelectedItem.ToString()), recipesList_Assign);
                rv.Show();
            }
            else
            {
                RecipeViewer rv = new RecipeViewer(getRecipe(recipesListBox.SelectedItem.ToString()), null);
                rv.Show();
            }
        }

        private void recipesList_Assign(object sender, Recipe result)
        {
            recipesList[result.id] = result;
            recipesListBox.Items[recipesListBox.SelectedIndex] = result.id;
        }

        private void verbsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            if (editMode)
            {
                VerbViewer vv = new VerbViewer(getVerb(verbsListBox.SelectedItem.ToString()), verbsList_Assign);
                vv.Show();
            }
            else
            {
                VerbViewer vv = new VerbViewer(getVerb(verbsListBox.SelectedItem.ToString()), null);
                vv.Show();
            }
        }

        private void verbsList_Assign(object sender, Verb result)
        {
            verbsList[result.id] = result;
            verbsListBox.Items[verbsListBox.SelectedIndex] = result.id;
        }

        private void editManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManifestViewer mv = new ManifestViewer(manifest);
            DialogResult dr = mv.ShowDialog();
            if (dr == DialogResult.OK)
            {
                manifest = mv.displayedManifest;
                saveMod(currentDirectory);
            }
        }
        
        private void saveMod(object sender, EventArgs e)
        {
            
            saveMod(currentDirectory);
        }

        private void createDirectories(string modLocation)
        {
            if (!Directory.Exists(modLocation + "/content/"))
            {
                Directory.CreateDirectory(modLocation + "/content/");
            }
            if (!Directory.Exists(modLocation + "/images/elementart/"))
            {
                Directory.CreateDirectory(modLocation + "/images/elementart/");
            }
            if (!Directory.Exists(modLocation + "/images/endingart/"))
            {
                Directory.CreateDirectory(modLocation + "/images/endingart/");
            }
            if (!Directory.Exists(modLocation + "/images/icons40/aspects/"))
            {
                Directory.CreateDirectory(modLocation + "/images/icons40/aspects/");
            }
            if (!Directory.Exists(modLocation + "/images/icons100/legacies/"))
            {
                Directory.CreateDirectory(modLocation + "/images/icons100/legacies/");
            }
            if (!Directory.Exists(modLocation + "/images/icons100/verbs/"))
            {
                Directory.CreateDirectory(modLocation + "/images/icons100/verbs/");
            }
            if (!Directory.Exists(modLocation + "/images/statusbaricons/"))
            {
                Directory.CreateDirectory(modLocation + "/images/statusbaricons/");
            }
        }


        private void saveMod(string location)
        {
            ProgressBar.Value = 0;
            ProgressBar.Maximum = 1 + ((aspectsListBox.Items.Count > 0) ? 1 : 0) + ((elementsListBox.Items.Count > 0) ? 1 : 0) + ((recipesListBox.Items.Count > 0) ? 1 : 0) + ((decksListBox.Items.Count > 0) ? 1 : 0) + ((endingsListBox.Items.Count > 0) ? 1 : 0) + ((legaciesListBox.Items.Count > 0) ? 1 : 0) + ((verbsListBox.Items.Count > 0) ? 1 : 0);
            ProgressBar.Visible = true;
            createDirectories(location);
            if (aspectsListBox.Items.Count > 0)
            {
                JObject aspects = new JObject();
                aspects["elements"] = JArray.FromObject(aspectsList.Values);
                string aspectsJson = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/aspects.json", FileMode.Create))))
                {
                    jtw.WriteRaw(aspectsJson);
                }
                ProgressBar.PerformStep();
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
                JObject elements = new JObject();
                elements["elements"] = JArray.FromObject(elementsList.Values);
                string elementsJson = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/elements.json", FileMode.Create))))
                {
                    jtw.WriteRaw(elementsJson);
                }
                ProgressBar.PerformStep();
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
                JObject recipes = new JObject();
                recipes["recipes"] = JArray.FromObject(recipesList.Values);
                string recipesJson = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/recipes.json", FileMode.Create))))
                {
                    jtw.WriteRaw(recipesJson);
                }
                ProgressBar.PerformStep();
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
                JObject decks = new JObject();
                decks["decks"] = JArray.FromObject(decksList.Values);
                string decksJson = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/decks.json", FileMode.Create))))
                {
                    jtw.WriteRaw(decksJson);
                }
                ProgressBar.PerformStep();
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
                JObject legacies = new JObject();
                legacies["legacies"] = JArray.FromObject(legaciesList.Values);
                string legaciesJson = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/legacies.json", FileMode.Create))))
                {
                    jtw.WriteRaw(legaciesJson);
                }
                ProgressBar.PerformStep();
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
                JObject endings = new JObject();
                endings["endings"] = JArray.FromObject(endingsList.Values);
                string endingsJson = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/endings.json", FileMode.Create))))
                {
                    jtw.WriteRaw(endingsJson);
                }
                ProgressBar.PerformStep();
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
                JObject verbs = new JObject();
                verbs["verbs"] = JArray.FromObject(verbsList.Values);
                string verbsJson = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/content/verbs.json", FileMode.Create))))
                {
                    jtw.WriteRaw(verbsJson);
                }
                ProgressBar.PerformStep();
            }
            else
            {
                if (File.Exists(location + "/content/verbs.json"))
                {
                    File.Delete(location + "/content/verbs.json");
                }
            }
            string manifestJson = JsonConvert.SerializeObject(manifest, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/manifest.json", FileMode.Create))))
            {
                jtw.WriteRaw(manifestJson);
            }
            ProgressBar.PerformStep();
            ProgressBar.Visible = false;
        }

        private void ModViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilities.currentMods.Remove(this);
        }

        private void aspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AspectViewer av = new AspectViewer(new Aspect(), aspectsList_Add);
            av.Show();
        }

        private void aspectsList_Add(object sender, Aspect result)
        {
            aspectsList[result.id] = result;
            aspectsListBox.Items.Add(result.id);
        }

        private void deckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckViewer dv = new DeckViewer(new Deck(), decksList_Add);
            dv.Show();
        }

        private void decksList_Add(object sender, Deck result)
        {
            decksList[result.id] = result;
            decksListBox.Items.Add(result.id);
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementViewer ev = new ElementViewer(new Element(), elementsList_Add);
            ev.Show();
        }

        private void elementsList_Add(object sender, Element result)
        {
            elementsList[result.id] = result;
            elementsListBox.Items.Add(result.id);
        }

        private void endingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(new Ending(), endingsList_Add);
            ev.Show();
        }

        private void endingsList_Add(object sender, Ending result)
        {
            endingsList[result.id] = result;
            endingsListBox.Items.Add(result.id);
        }

        private void legacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(new Legacy(), legaciesList_Add);
            lv.Show();
        }

        private void legaciesList_Add(object sender, Legacy result)
        {
            legaciesList[result.id] = result;
            legaciesListBox.Items.Add(result.id);
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeViewer rv = new RecipeViewer(new Recipe(), recipesList_Add);
            rv.Show();
        }

        private void recipesList_Add(object sender, Recipe result)
        {
            recipesList[result.id] = result;
            recipesListBox.Items.Add(result.id);
        }

        private void verbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(new Verb(), verbsList_Add);
            vv.Show();
        }

        private void verbsList_Add(object sender, Verb result)
        {
            verbsList[result.id] = result;
            verbsListBox.Items.Add(result.id);
        }

        private void reloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshContent();
        }

        private void summonGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SummonCreator sc = new SummonCreator();
            sc.ShowDialog();
            if(sc.DialogResult == DialogResult.OK)
            {
                elementsListBox.Items.Add(sc.baseSummon.id);
                elementsList.Add(sc.baseSummon.id, sc.baseSummon);

                elementsListBox.Items.Add(sc.preSummon.id);
                elementsList.Add(sc.preSummon.id, sc.preSummon);

                recipesListBox.Items.Add(sc.startSummon.id);
                recipesList.Add(sc.startSummon.id, sc.startSummon);

                recipesListBox.Items.Add(sc.succeedSummon.id);
                recipesList.Add(sc.succeedSummon.id, sc.succeedSummon);
            }
        }

        private void aspetsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            aspectsListBox.Items.Clear();
            foreach (string id in aspectsList.Keys.ToList())
            {
                if (id.Contains(aspetsSearchTextBox.Text))
                {
                    aspectsListBox.Items.Add(id);
                }
            }
        }

        private void elementsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            elementsListBox.Items.Clear();
            foreach (string id in elementsList.Keys.ToList())
            {
                if (id.Contains(elementsSearchTextBox.Text))
                {
                    elementsListBox.Items.Add(id);
                }
            }
        }

        private void recipesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            recipesListBox.Items.Clear();
            foreach (string id in recipesList.Keys.ToList())
            {
                if (id.Contains(recipesSearchTextBox.Text))
                {
                    recipesListBox.Items.Add(id);
                }
            }
        }

        private void decksSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            decksListBox.Items.Clear();
            foreach (string id in decksList.Keys.ToList())
            {
                if (id.Contains(decksSearchTextBox.Text))
                {
                    decksListBox.Items.Add(id);
                }
            }
        }

        private void legaciesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            legaciesListBox.Items.Clear();
            foreach (string id in legaciesList.Keys.ToList())
            {
                if (id.Contains(legaciesSearchTextBox.Text))
                {
                    legaciesListBox.Items.Add(id);
                }
            }
        }

        private void endingsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            endingsListBox.Items.Clear();
            foreach (string id in endingsList.Keys.ToList())
            {
                if (id.Contains(endingsSearchTextBox.Text))
                {
                    endingsListBox.Items.Add(id);
                }
            }
        }

        private void verbsSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            verbsListBox.Items.Clear();
            foreach (string id in verbsList.Keys.ToList())
            {
                if (id.Contains(verbsSearchTextBox.Text))
                {
                    verbsListBox.Items.Add(id);
                }
            }
        }

        private void elementsWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in elementsList.Values)
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

        private void elementsThatReactWithThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in elementsList.Values)
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

        private void recipesRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = aspectsListBox.SelectedItem.ToString();
            foreach (Recipe recipe in recipesList.Values)
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

        private void recipesThatProduceThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if (recipe.aspects != null &&(recipe.aspects.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.aspects[aspectsListBox.SelectedItem.ToString()] > 0))
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

        private void elementsThatDecayIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in elementsList.Values)
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

        private void elementsThatXtriggerIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            string id = elementsListBox.SelectedItem.ToString();
            foreach (Element element in elementsList.Values)
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

        private void recipesThatRequireThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = elementsListBox.SelectedItem.ToString();
            foreach (Recipe recipe in recipesList.Values)
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

        private void recipesThatProduceThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            string id = elementsListBox.SelectedItem.ToString();
            foreach (Recipe recipe in recipesList.Values)
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

        private void decksThatContainThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Deck> tmp = new Dictionary<string, Deck>();
            foreach (Deck deck in decksList.Values)
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

        private void legaciesThatStartWithThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Legacy> tmp = new Dictionary<string, Legacy>();
            foreach (Legacy legacy in legaciesList.Values)
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

        private void recipesThatLinkToThisRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if (recipe.linked != null) foreach (RecipeLink link in recipe.linked)
                {
                    if (link.id == recipesListBox.SelectedItem.ToString())
                    {
                        tmp.Add(recipe.id, recipe);
                    }
                }
                if (recipe.alternativerecipes != null) foreach (RecipeLink link in recipe.alternativerecipes)
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

        private void recipesThatDrawFromThisDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if (recipe.deckeffect != null && recipe.deckeffect.ContainsKey(decksListBox.SelectedItem.ToString()) && recipe.deckeffect[decksListBox.SelectedItem.ToString()] > 0)
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

        private void recipesThatCauseThisEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (endingsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
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

        private void recipesThatUseThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
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

        private void elementsWithSlotsForThisVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in elementsList.Values)
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

        private void viewAsFlowchartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            Recipe selectedRecipe = Utilities.getRecipe(recipesListBox.SelectedItem.ToString());
            RecipeFlowchartViewer rfv = new RecipeFlowchartViewer(selectedRecipe);
            rfv.Show();
        }

        private void saveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToFolderBrowserDialog.SelectedPath = currentDirectory;
            if (saveToFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                saveMod(saveToFolderBrowserDialog.SelectedPath);
            }
        }

        private void toggleAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleAutosaveToolStripMenuItem.Checked = !toggleAutosaveToolStripMenuItem.Checked;
            autosaveTimer.Enabled = toggleAutosaveToolStripMenuItem.Enabled;
        }

        private void autosaveTimer_Tick(object sender, EventArgs e)
        {
            saveMod(currentDirectory);
        }
        
        private void deleteSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            string selected = (string)aspectsListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                aspectsListBox.Items.Remove(selected);
                aspectsList.Remove(selected);
            }
        }

        private void deleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            string selected = (string)elementsListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                elementsListBox.Items.Remove(selected);
                elementsList.Remove(selected);
            }
        }

        private void deleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            string selected = (string)recipesListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                recipesListBox.Items.Remove(selected);
                recipesList.Remove(selected);
            }
        }

        private void deleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            string selected = (string)decksListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                decksListBox.Items.Remove(selected);
                decksList.Remove(selected);
            }
        }

        private void deleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = legaciesListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                legaciesListBox.Items.Remove(selected);
                legaciesList.Remove(selected);
            }
        }

        private void deleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = endingsListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                endingsListBox.Items.Remove(selected);
                endingsList.Remove(selected);
            }
        }

        private void deleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = verbsListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.Yes)
            {
                verbsListBox.Items.Remove(selected);
                verbsList.Remove(selected);
            }
        }

        private void ModViewer_Shown(object sender, EventArgs e)
        {
            if (isVanilla) return;
            else if (manifest != null) return;
            else Close();
        }
        
        public void deleted(string id)
        {
            MessageBox.Show(id + "has been deleted.");
        }

        private void aspectsListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void elementsListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void recipesListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void decksListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void legaciesListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void endingsListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void verbsListBox_MouseDown(object sender, MouseEventArgs e)
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

        private void toggleEditModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleEditModeToolStripMenuItem.Checked = !toggleEditModeToolStripMenuItem.Checked;
            editMode = toggleEditModeToolStripMenuItem.Checked;
        }

        private void duplicateSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aspect newAspect = aspectsList[aspectsListBox.SelectedItem as string].Copy();
            string id = newAspect.id;
            if (aspectsListBox.Items.Contains(id + "_1"))
            {
                id += "_";
                int tmp = 1;
                while (aspectsListBox.Items.Contains(id+tmp.ToString()))
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
            aspectsList.Add(newAspect.id, newAspect);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element newElement = elementsList[elementsListBox.SelectedItem as string].Copy();
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
            elementsList.Add(newElement.id, newElement);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe newRecipe = recipesList[recipesListBox.SelectedItem as string].Copy();
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
            recipesList.Add(newRecipe.id, newRecipe);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck newDeck = decksList[decksListBox.SelectedItem as string].Copy();
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
            decksList.Add(newDeck.id, newDeck);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy newLegacy = legaciesList[legaciesListBox.SelectedItem as string].Copy();
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
            legaciesList.Add(newLegacy.id, newLegacy);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending newEnding = endingsList[endingsListBox.SelectedItem as string].Copy();
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
            endingsList.Add(newEnding.id, newEnding);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void duplicateSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb newVerb = verbsList[verbsListBox.SelectedItem as string].Copy();
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
            verbsList.Add(newVerb.id, newVerb);
            saveMod(currentDirectory);
            refreshContent();
        }

        private void imageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageImporter ii = new ImageImporter();
            if (ii.ShowDialog() == DialogResult.OK)
            {
                switch (ii.displayedImageType.ToLower())
                {
                    case "aspect":
                        File.Copy(ii.displayedImagePath, currentDirectory + "\\images\\icons40\\aspects\\" + ii.displayedFileName, true);
                        break;
                    case "element":
                        File.Copy(ii.displayedImagePath, currentDirectory + "\\images\\elementArt\\" + ii.displayedFileName, true);
                        break;
                    case "ending":
                        File.Copy(ii.displayedImagePath, currentDirectory + "\\images\\endingArt\\" + ii.displayedFileName, true);
                        break;
                    case "legacy":
                        File.Copy(ii.displayedImagePath, currentDirectory + "\\images\\icons100\\legacies\\" + ii.displayedFileName, true);
                        break;
                    case "verb":
                        File.Copy(ii.displayedImagePath, currentDirectory + "\\images\\icons100\\verbs\\" + ii.displayedFileName, true);
                        break;
                }
                MessageBox.Show("Imported " + ii.displayedImageType + " image.");
            }
        }

        private void exportSelectedAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aspect exportedAspect = getAspect(aspectsListBox.SelectedItem as string);
            if (exportedAspect == null) return;
            exportObject(exportedAspect, exportedAspect.id);
        }

        private void exportSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element exportedElement = getElement(elementsListBox.SelectedItem as string);
            if (exportedElement == null) return;
            exportObject(exportedElement, exportedElement.id);
        }

        private void exportSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe exportedRecipe = getRecipe(recipesListBox.SelectedItem as string);
            if (exportedRecipe == null) return;
            exportObject(exportedRecipe, exportedRecipe.id);
        }

        private void exportSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck exportedDeck = getDeck(decksListBox.SelectedItem as string);
            if (exportedDeck == null) return;
            exportObject(exportedDeck, exportedDeck.id);
        }

        private void exportSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy exportedLegacy = getLegacy(legaciesListBox.SelectedItem as string);
            if (exportedLegacy == null) return;
            exportObject(exportedLegacy, exportedLegacy.id);
        }

        private void exportSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending exportedEnding = getEnding(endingsListBox.SelectedItem as string);
            if (exportedEnding == null) return;
            exportObject(exportedEnding, exportedEnding.id);
        }

        private void exportSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getVerb(verbsListBox.SelectedItem as string) == null) return;
            Verb exportedVerb = getVerb(verbsListBox.SelectedItem as string);
            exportObject(exportedVerb, exportedVerb.id);
        }

        private void exportObject(object objectToExport, string id)
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

        private void aspectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(new StreamReader(openFileDialog.OpenFile()).ReadToEnd());
                    if (aspectsListBox.Items.Contains(deserializedAspect.id))
                    {
                        MessageBox.Show("Aspect already exists, overwriting.");
                    }
                    else
                    {
                        aspectsListBox.Items.Add(deserializedAspect.id);
                    }
                    aspectsList[deserializedAspect.id] = deserializedAspect;
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
                    if (elementsListBox.Items.Contains(deserializedElement.id))
                    {
                        MessageBox.Show("Element already exists, overwriting.");
                    }
                    else
                    {
                        elementsListBox.Items.Add(deserializedElement.id);
                    }
                    elementsList[deserializedElement.id] = deserializedElement;
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
                    if (recipesListBox.Items.Contains(deserializedRecipe.id))
                    {
                        MessageBox.Show("Recipe already exists, overwriting.");
                    }
                    else
                    {
                        recipesListBox.Items.Add(deserializedRecipe.id);
                    }
                    recipesList[deserializedRecipe.id] = deserializedRecipe;
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
                    if (decksListBox.Items.Contains(deserializedDeck.id))
                    {
                        MessageBox.Show("Deck already exists, overwriting.");
                    }
                    else
                    {
                        decksListBox.Items.Add(deserializedDeck.id);
                    }
                    decksList[deserializedDeck.id] = deserializedDeck;
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
                    if (legaciesListBox.Items.Contains(deserializedLegacy.id))
                    {
                        MessageBox.Show("Legacy already exists, overwriting.");
                    }
                    else
                    {
                        legaciesListBox.Items.Add(deserializedLegacy.id);
                    }
                    legaciesList[deserializedLegacy.id] = deserializedLegacy;
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
                    if (endingsListBox.Items.Contains(deserializedEnding.id))
                    {
                        MessageBox.Show("Ending already exists, overwriting.");
                    }
                    else
                    {
                        endingsListBox.Items.Add(deserializedEnding.id);
                    }
                    endingsList[deserializedEnding.id] = deserializedEnding;
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
                    if (verbsListBox.Items.Contains(deserializedVerb.id))
                    {
                        MessageBox.Show("Verb already exists, overwriting.");
                    }
                    else
                    {
                        verbsListBox.Items.Add(deserializedVerb.id);
                    }
                    verbsList[deserializedVerb.id] = deserializedVerb;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deserializing Verb");
                }
            }
        }

        public void copyObjectJSONToClipboard(object objectToExport)
        {
            string JSON = JsonConvert.SerializeObject(objectToExport, Formatting.Indented);
            Clipboard.SetText(JSON);
        }

        private void copySelectedAspectJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aspect exportedAspect = getAspect(aspectsListBox.SelectedItem as string);
            if (exportedAspect == null) return;
            copyObjectJSONToClipboard(exportedAspect);
        }

        private void copySelectedElementJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element exportedElement = getElement(elementsListBox.SelectedItem as string);
            if (exportedElement == null) return;
            copyObjectJSONToClipboard(exportedElement);
        }

        private void copySelectedRecipeJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe exportedRecipe = getRecipe(recipesListBox.SelectedItem as string);
            if (exportedRecipe == null) return;
            copyObjectJSONToClipboard(exportedRecipe);
        }

        private void copySelectedDeckJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck exportedDeck = getDeck(decksListBox.SelectedItem as string);
            if (exportedDeck == null) return;
            copyObjectJSONToClipboard(exportedDeck);
        }

        private void copySelectedLegacyJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy exportedLegacy = getLegacy(legaciesListBox.SelectedItem as string);
            if (exportedLegacy == null) return;
            copyObjectJSONToClipboard(exportedLegacy);
        }

        private void copySelectedEndingJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending exportedEnding = getEnding(endingsListBox.SelectedItem as string);
            if (exportedEnding == null) return;
            copyObjectJSONToClipboard(exportedEnding);
        }

        private void copySelectedVerbJSONToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb exportedVerb = getVerb(verbsListBox.SelectedItem as string);
            if (exportedVerb == null) return;
            copyObjectJSONToClipboard(exportedVerb);
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
                        aspectsList[deserializedAspect.id] = deserializedAspect;
                        if (!aspectsListBox.Items.Contains(deserializedAspect.id))
                        {
                            aspectsListBox.Items.Add(deserializedAspect.id);
                        }
                        break;
                    case "Element":
                        Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                        elementsList[deserializedElement.id] = deserializedElement;
                        if (!elementsListBox.Items.Contains(deserializedElement.id))
                        {
                            elementsListBox.Items.Add(deserializedElement.id);
                        }
                        break;
                    case "Recipe":
                        Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                        recipesList[deserializedRecipe.id] = deserializedRecipe;
                        if (!recipesListBox.Items.Contains(deserializedRecipe.id))
                        {
                            recipesListBox.Items.Add(deserializedRecipe.id);
                        }
                        break;
                    case "Deck":
                        Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                        decksList[deserializedDeck.id] = deserializedDeck;
                        if (!decksListBox.Items.Contains(deserializedDeck.id))
                        {
                            decksListBox.Items.Add(deserializedDeck.id);
                        }
                        break;
                    case "Legacy":
                        Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                        legaciesList[deserializedLegacy.id] = deserializedLegacy;
                        if (!legaciesListBox.Items.Contains(deserializedLegacy.id))
                        {
                            legaciesListBox.Items.Add(deserializedLegacy.id);
                        }
                        break;
                    case "Ending":
                        Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                        endingsList[deserializedEnding.id] = deserializedEnding;
                        if (!endingsListBox.Items.Contains(deserializedEnding.id))
                        {
                            endingsListBox.Items.Add(deserializedEnding.id);
                        }
                        break;
                    case "Verb":
                        Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                        verbsList[deserializedVerb.id] = deserializedVerb;
                        if (!verbsListBox.Items.Contains(deserializedVerb.id))
                        {
                            verbsListBox.Items.Add(deserializedVerb.id);
                        }
                        break;
                    default:
                        MessageBox.Show("I'm not sure what you selected or how, but that was an invalid choice.", "Unknown Object Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
        }

        private void editSelectedAspectsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aspect aspectToEdit = getAspect(aspectsListBox.SelectedItem as string);
            if (aspectToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(aspectToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Aspect deserializedAspect = JsonConvert.DeserializeObject<Aspect>(je.objectText);
                aspectsList[aspectsListBox.SelectedItem as string] = deserializedAspect;
            }
        }

        private void editSelectedElementsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Element elementToEdit = getElement(elementsListBox.SelectedItem as string);
            if (elementToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(elementToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Element deserializedElement = JsonConvert.DeserializeObject<Element>(je.objectText);
                elementsList[elementsListBox.SelectedItem as string] = deserializedElement;
            }
        }

        private void editSelectedRecipesJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Recipe recipeToEdit = getRecipe(recipesListBox.SelectedItem as string);
            if (recipeToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(recipeToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Recipe deserializedRecipe = JsonConvert.DeserializeObject<Recipe>(je.objectText);
                recipesList[recipesListBox.SelectedItem as string] = deserializedRecipe;
            }
        }

        private void editSelectedDecksJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deck deckToEdit = getDeck(decksListBox.SelectedItem as string);
            if (deckToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(deckToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Deck deserializedDeck = JsonConvert.DeserializeObject<Deck>(je.objectText);
                decksList[decksListBox.SelectedItem as string] = deserializedDeck;
            }
        }

        private void editSelectedLegacysJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Legacy legacyToEdit = getLegacy(legaciesListBox.SelectedItem as string);
            if (legacyToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(legacyToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Legacy deserializedLegacy = JsonConvert.DeserializeObject<Legacy>(je.objectText);
                legaciesList[legaciesListBox.SelectedItem as string] = deserializedLegacy;
            }
        }

        private void editSelectedEndingsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ending endingToEdit = getEnding(endingsListBox.SelectedItem as string);
            if (endingToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(endingToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Ending deserializedEnding = JsonConvert.DeserializeObject<Ending>(je.objectText);
                endingsList[endingsListBox.SelectedItem as string] = deserializedEnding;
            }
        }

        private void editSelectedVerbsJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verb verbToEdit = getVerb(verbsListBox.SelectedItem as string);
            if (verbToEdit == null) return;
            JsonEditor je = new JsonEditor(JsonConvert.SerializeObject(verbToEdit, Formatting.Indented), true, !editMode);
            if (je.ShowDialog() == DialogResult.OK)
            {
                Verb deserializedVerb = JsonConvert.DeserializeObject<Verb>(je.objectText);
                verbsList[verbsListBox.SelectedItem as string] = deserializedVerb;
            }
        }

        public DialogResult confirmDelete(string id)
        {
            if (id == null) return MessageBox.Show("Are you sure you'd like to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
            return MessageBox.Show("Are you sure you'd like to delete " + id + "?", "Delete Item", MessageBoxButtons.YesNo);
        }
    }
}