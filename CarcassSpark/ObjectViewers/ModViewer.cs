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
            }
            saveMod(currentDirectory);
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

        private void decksListBox_DoubleClick(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            DeckViewer dv = new DeckViewer(getDeck(decksListBox.SelectedItem.ToString()), editMode);
            dv.ShowDialog();
        }

        private void recipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            RecipeViewer rv = new RecipeViewer(getRecipe(recipesListBox.SelectedItem.ToString()), editMode);
            rv.ShowDialog();
        }

        private void legaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (legaciesListBox.SelectedItem == null) return;
            LegacyViewer lv = new LegacyViewer(getLegacy(legaciesListBox.SelectedItem.ToString()), editMode);
            lv.ShowDialog();
        }

        private void endingsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (endingsListBox.SelectedItem == null) return;
            EndingViewer ev = new EndingViewer(getEnding(endingsListBox.SelectedItem.ToString()), editMode);
            ev.ShowDialog();
        }

        private void verbsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (verbsListBox.SelectedItem == null) return;
            VerbViewer vv = new VerbViewer(getVerb(verbsListBox.SelectedItem.ToString()), editMode);
            vv.ShowDialog();
        }


        private void elementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            ElementViewer ev = new ElementViewer(getElement(elementsListBox.SelectedItem.ToString()), editMode);
            ev.ShowDialog();
        }

        private void aspectListBox_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            AspectViewer av = new AspectViewer(getAspect(aspectsListBox.SelectedItem.ToString()), editMode);
            av.ShowDialog();
        }

        private void editManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManifestViewer mv = new ManifestViewer(manifest);
            DialogResult dr = mv.ShowDialog();
            if(dr == DialogResult.OK) manifest = mv.displayedManifest;
        }
        
        private void saveMod(object sender, EventArgs e)
        {
            saveMod(currentDirectory);
        }


        private void saveMod(string location)
        {
            ProgressBar.Value = 0;
            ProgressBar.Maximum = 1 + ((aspectsListBox.Items.Count > 0) ? 1 : 0) + ((elementsListBox.Items.Count > 0) ? 1 : 0) + ((recipesListBox.Items.Count > 0) ? 1 : 0) + ((decksListBox.Items.Count > 0) ? 1 : 0) + ((endingsListBox.Items.Count > 0) ? 1 : 0) + ((legaciesListBox.Items.Count > 0) ? 1 : 0) + ((verbsListBox.Items.Count > 0) ? 1 : 0);
            ProgressBar.Visible = true;
            if (!Directory.Exists(location + "/content/"))
            {
                Directory.CreateDirectory(location + "/content/");
            }
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
            string manifestJson = JsonConvert.SerializeObject(manifest, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(location + "/manifest.json", FileMode.OpenOrCreate))))
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
            using (AspectViewer av = new AspectViewer(new Aspect(), true))
            {
                av.ShowDialog();
                if (av.DialogResult == DialogResult.OK && !aspectsList.ContainsKey(av.displayedAspect.id))
                {
                    aspectsList.Add(av.displayedAspect.id, av.displayedAspect);
                    aspectsListBox.Items.Add(av.displayedAspect.id);
                }
            }
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ElementViewer ev = new ElementViewer(new Element(), true))
            {
                ev.ShowDialog();
                if (ev.DialogResult == DialogResult.OK && !elementsList.ContainsKey(ev.displayedElement.id))
                {
                    elementsList.Add(ev.displayedElement.id, ev.displayedElement);
                    elementsListBox.Items.Add(ev.displayedElement.id);
                }
            }
        }

        private void deckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DeckViewer dv = new DeckViewer(new Deck(), true))
            {
                dv.ShowDialog();
                if (dv.DialogResult == DialogResult.OK && !decksList.ContainsKey(dv.displayedDeck.id))
                {
                    decksList.Add(dv.displayedDeck.id, dv.displayedDeck);
                    decksListBox.Items.Add(dv.displayedDeck.id);
                }
            }
        }

        private void legacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LegacyViewer lv = new LegacyViewer(new Legacy(), true))
            {
                lv.ShowDialog();
                if (lv.DialogResult == DialogResult.OK && !legaciesList.ContainsKey(lv.displayedLegacy.id))
                {
                    legaciesList.Add(lv.displayedLegacy.id, lv.displayedLegacy);
                    legaciesListBox.Items.Add(lv.displayedLegacy.id);
                }
            }
        }

        private void endingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (EndingViewer ev = new EndingViewer(new Ending(), true))
            {
                ev.ShowDialog();
                if (ev.DialogResult == DialogResult.OK && !endingsList.ContainsKey(ev.displayedEnding.id))
                {
                    endingsList.Add(ev.displayedEnding.id, ev.displayedEnding);
                    endingsListBox.Items.Add(ev.displayedEnding.id);
                }
            }
        }

        private void verbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (VerbViewer vv = new VerbViewer(new Verb(), true))
            {
                vv.ShowDialog();
                if(vv.DialogResult == DialogResult.OK && !verbsList.ContainsKey(vv.displayedVerb.id))
                {
                    verbsList.Add(vv.displayedVerb.id, vv.displayedVerb);
                    verbsListBox.Items.Add(vv.displayedVerb.id);
                }
            }
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RecipeViewer rv = new RecipeViewer(new Recipe(), true))
            {
                rv.ShowDialog();
                if(rv.DialogResult == DialogResult.OK && !recipesList.ContainsKey(rv.displayedRecipe.id))
                {
                    recipesList.Add(rv.displayedRecipe.id, rv.displayedRecipe);
                    recipesListBox.Items.Add(rv.displayedRecipe.id);
                }
            }
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
                edr.ShowDialog();
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
                edr.ShowDialog();
            }
        }

        private void recipesRequiringThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                // if ((recipe.requirements != null && recipe.requirements.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.requirements[aspectsListBox.SelectedItem.ToString()] > 0) ||
                // (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.extantreqs[aspectsListBox.SelectedItem.ToString()] > 0) ||
                // (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.tablereqs[aspectsListBox.SelectedItem.ToString()] > 0))
                // {
                // tmp.Add(recipe.id, recipe);
                // }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.ShowDialog();
            }
        }

        private void recipesThatProduceThisAspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if (recipe.aspects != null && recipe.aspects.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.aspects[aspectsListBox.SelectedItem.ToString()] > 0)
                {
                    tmp.Add(recipe.id, recipe);
                }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.ShowDialog();
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
                edr.ShowDialog();
            }
        }

        private void elementsThatXtriggerIntoThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (Element element in elementsList.Values)
            {
                // if (element.xtriggers != null && element.xtriggers.ContainsValue(elementsListBox.SelectedItem.ToString()))
                // {
                //  tmp.Add(element.id, element);
                // }
            }
            if (tmp.Count > 0)
            {
                ElementsDictionaryResults edr = new ElementsDictionaryResults(tmp);
                edr.ShowDialog();
            }
        }

        private void recipesThatRequireThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                // if ((recipe.requirements != null && recipe.requirements.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.requirements[elementsListBox.SelectedItem.ToString()] > 0) ||
                // (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.extantreqs[elementsListBox.SelectedItem.ToString()] > 0) ||
                // (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.tablereqs[elementsListBox.SelectedItem.ToString()] > 0))
                // {
                // tmp.Add(recipe.id, recipe);
                // }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.ShowDialog();
            }
        }

        private void recipesThatProduceThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                // if (recipe.effects != null && recipe.effects.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.effects[elementsListBox.SelectedItem.ToString()] > 0)
                // {
                //     tmp.Add(recipe.id, recipe);
                // }
            }
            if (tmp.Count > 0)
            {
                RecipesDictionaryResults rdr = new RecipesDictionaryResults(tmp);
                rdr.ShowDialog();
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
                ddr.ShowDialog();
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
                ldr.ShowDialog();
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
                rdr.ShowDialog();
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
                rdr.ShowDialog();
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
                rdr.ShowDialog();
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
                rdr.ShowDialog();
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
                edr.ShowDialog();
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
            if (confirmDelete(selected) == DialogResult.OK)
            {
                aspectsListBox.Items.Remove(selected);
                aspectsList.Remove(selected);
            }
        }

        private void deleteSelectedElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            string selected = (string)elementsListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.OK)
            {
                elementsListBox.Items.Remove(selected);
                elementsList.Remove(selected);
            }
        }

        private void deleteSelectedRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            string selected = (string)recipesListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.OK)
            {
                recipesListBox.Items.Remove(selected);
                recipesList.Remove(selected);
            }
        }

        private void deleteSelectedDeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            string selected = (string)decksListBox.SelectedItem;
            if (confirmDelete(selected) == DialogResult.OK)
            {
                decksListBox.Items.Remove(selected);
                decksList.Remove(selected);
            }
        }

        private void deleteSelectedLegacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = legaciesListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.OK)
            {
                legaciesListBox.Items.Remove(selected);
                legaciesList.Remove(selected);
            }
        }

        private void deleteSelectedEndingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = endingsListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.OK)
            {
                endingsListBox.Items.Remove(selected);
                endingsList.Remove(selected);
            }
        }

        private void deleteSelectedVerbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = verbsListBox.SelectedItem as string;
            if (confirmDelete(selected) == DialogResult.OK)
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

        public DialogResult confirmDelete(string id)
        {
            if (id == null) return MessageBox.Show("Are you sure you'd like to delete this item?", "Delete Item", MessageBoxButtons.YesNo);
            return MessageBox.Show("Are you sure you'd like to delete " + id + "?", "Delete Item", MessageBoxButtons.YesNo);
        }
    }
}