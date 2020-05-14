extern alias CultistSimulator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CultistSimulatorModdingToolkit.ObjectTypes;
using CultistSimulatorModdingToolkit.ObjectViewers;

namespace CultistSimulatorModdingToolkit.ObjectViewers
{
    public partial class ModViewer : Form
    {
        public string currentDirectory;
        public Manifest manifest;
        public bool isVanilla, foundManifest, editMode = false;
        
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
            this.currentDirectory = location;
            this.isVanilla = isVanilla;
            toolStrip1.Visible = !this.isVanilla;
            editModeCheckBox.Visible = !this.isVanilla;

            refreshContent();
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

            foreach (string file in Directory.EnumerateFiles(currentDirectory, "*.json", SearchOption.AllDirectories))
            {   
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    if (file.Contains("manifest.json")){
                        loadManifest(fs);
                        foundManifest = true;
                    } else {
                        loadFile(fs);
                    }
                }
            }
            if (!File.Exists(currentDirectory + "/manifest.json") && !isVanilla)
            {
                DialogResult dr = MessageBox.Show("manifest.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    ManifestViewer mv = new ManifestViewer(new Manifest());
                    DialogResult mvdr = mv.ShowDialog();
                    if (mvdr == DialogResult.OK)
                    {
                        manifest = mv.displayedManifest;
                        foundManifest = true;
                    }
                }
            }
        }

        public void loadManifest(FileStream file)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            manifest = JsonConvert.DeserializeObject<Manifest>(JsonConvert.SerializeObject(ht));;
        }

        public void loadFile(FileStream file)
        {

            string fileText = new StreamReader(file).ReadToEnd();
            Hashtable ht = CultistSimulator::SimpleJsonImporter.Import(fileText);
            string newFileText = JsonConvert.SerializeObject(ht);
            JToken parsedJToken = JsonConvert.DeserializeObject<JObject>(newFileText).First;
            string fileType = parsedJToken.Path;
            switch (fileType)
            {
                case "elements":
                    foreach (JToken element in parsedJToken.First.ToArray())
                    {
                        //if (element["isAspect"].ToObject<bool>())
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
                            ObjectTypes.Element deserializedElement = element.ToObject<ObjectTypes.Element>();
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
                        ObjectTypes.Recipe deserializedRecipe = recipe.ToObject<ObjectTypes.Recipe>();
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


        public ObjectTypes.Element getElement(string id)
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

        public ObjectTypes.Recipe getRecipe(string id)
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
        
        private void saveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(currentDirectory + "/content/"))
            {
                Directory.CreateDirectory(currentDirectory + "/content/");
            }
            if (aspectsListBox.Items.Count > 0)
            {
                JObject aspects = new JObject();
                aspects["elements"] = JArray.FromObject(aspectsList.Values);
                string aspectsJson = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/aspects.json", FileMode.Create))))
                {
                    jtw.WriteRaw(aspectsJson);
                }
            }
            if (elementsListBox.Items.Count > 0)
            {
                JObject elements = new JObject();
                elements["elements"] = JArray.FromObject(elementsList.Values);
                string elementsJson = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/elements.json", FileMode.Create))))
                {
                    jtw.WriteRaw(elementsJson);
                }
            }
            if (recipesListBox.Items.Count > 0)
            {
                JObject recipes = new JObject();
                recipes["recipes"] = JArray.FromObject(recipesList.Values);
                string recipesJson = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/recipes.json", FileMode.Create))))
                {
                    jtw.WriteRaw(recipesJson);
                }
            }
            if (decksListBox.Items.Count > 0)
            {
                JObject decks = new JObject();
                decks["decks"] = JArray.FromObject(decksList.Values);
                string decksJson = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/decks.json", FileMode.Create))))
                {
                    jtw.WriteRaw(decksJson);
                }
            }
            if (legaciesListBox.Items.Count > 0)
            {
                JObject legacies = new JObject();
                legacies["legacies"] = JArray.FromObject(legaciesList.Values);
                string legaciesJson = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/legacies.json", FileMode.Create))))
                {
                    jtw.WriteRaw(legaciesJson);
                }
            }
            if (endingsListBox.Items.Count > 0)
            {
                JObject endings = new JObject();
                endings["endings"] = JArray.FromObject(endingsList.Values);
                string endingsJson = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/endings.json", FileMode.Create))))
                {
                    jtw.WriteRaw(endingsJson);
                }
            }
            if (verbsListBox.Items.Count > 0)
            {
                JObject verbs = new JObject();
                verbs["verbs"] = JArray.FromObject(verbsList.Values);
                string verbsJson = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/content/verbs.json", FileMode.Create))))
                {
                    jtw.WriteRaw(verbsJson);
                }
            }
            // TODO uncomment this when it's safe to do so
            string manifestJson = JsonConvert.SerializeObject(manifest, Formatting.Indented);
            using (JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(File.Open(currentDirectory + "/manifest.json", FileMode.OpenOrCreate))))
            {
                jtw.WriteRaw(manifestJson);
            }
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
                if (av.DialogResult == DialogResult.OK)
                {
                    aspectsList.Add(av.displayedAspect.id, av.displayedAspect);
                    aspectsListBox.Items.Add(av.displayedAspect.id);
                }
            }
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ElementViewer ev = new ElementViewer(new ObjectTypes.Element(), true))
            {
                ev.ShowDialog();
                if (ev.DialogResult == DialogResult.OK)
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
                if (dv.DialogResult == DialogResult.OK)
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
                if (lv.DialogResult == DialogResult.OK)
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
                if (ev.DialogResult == DialogResult.OK)
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
                if(vv.DialogResult == DialogResult.OK)
                {
                    verbsList.Add(vv.displayedVerb.id, vv.displayedVerb);
                    verbsListBox.Items.Add(vv.displayedVerb.id);
                }
            }
        }

        private void recipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RecipeViewer rv = new RecipeViewer(new ObjectTypes.Recipe(), true))
            {
                rv.ShowDialog();
                if(rv.DialogResult == DialogResult.OK)
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
                if ((recipe.requirements != null && recipe.requirements.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.requirements[aspectsListBox.SelectedItem.ToString()] > 0) ||
                    (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.extantreqs[aspectsListBox.SelectedItem.ToString()] > 0) ||
                    (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(aspectsListBox.SelectedItem.ToString()) && recipe.tablereqs[aspectsListBox.SelectedItem.ToString()] > 0))
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
                if (element.xtriggers != null && element.xtriggers.ContainsValue(elementsListBox.SelectedItem.ToString()))
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

        private void recipesThatRequireThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if ((recipe.requirements != null && recipe.requirements.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.requirements[elementsListBox.SelectedItem.ToString()] > 0) ||
                    (recipe.extantreqs != null && recipe.extantreqs.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.extantreqs[elementsListBox.SelectedItem.ToString()] > 0) ||
                    (recipe.tablereqs != null && recipe.tablereqs.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.tablereqs[elementsListBox.SelectedItem.ToString()] > 0))
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

        private void recipesThatProduceThisElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (Recipe recipe in recipesList.Values)
            {
                if (recipe.effects != null && recipe.effects.ContainsKey(elementsListBox.SelectedItem.ToString()) && recipe.effects[elementsListBox.SelectedItem.ToString()] > 0)
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

        private void editModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            editMode = editModeCheckBox.Checked;
        }

        private void ModViewer_Shown(object sender, EventArgs e)
        {
            if (isVanilla) return;
            else if (foundManifest) return;
            else Close();
        }
    }
}