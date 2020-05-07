using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    public partial class ModViewer : Form
    {
        public string currentDirectory;
        public Manifest manifest;
        public bool isVanilla, foundManifest;
        
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
            
            this.foundManifest = false;
            foreach (string file in Directory.EnumerateFiles(currentDirectory, "*.json", SearchOption.AllDirectories))
            {
                //MessageBox.Show(file);
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    if(file.Contains("manifest.json"))
                    {
                        loadManifest(fs);
                        foundManifest = true;
                    } else {
                        loadFile(fs);
                    }
                }
            }
            if (!foundManifest && !isVanilla)
            {
                DialogResult dr = MessageBox.Show("manifest.json not found in selected directory, are you creating a new mod?", "No Manifest", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes)
                {
                    ManifestViewer mv = new ManifestViewer(new Manifest("", "", "", "", ""));
                    mv.ShowDialog();
                    manifest = mv.displayedManifest;
                    foundManifest = true;
                }
            }
        }
        
        public void loadManifest(FileStream file)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            manifest = JsonConvert.DeserializeObject<Manifest>(fileText);
        }

        public void loadFile(FileStream file)
        {

            string fileText = new StreamReader(file).ReadToEnd();
            JToken parsedJToken = JsonConvert.DeserializeObject<JToken>(fileText).First;
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
                            if(!aspectsList.ContainsKey(deserializedAspect.id))
                            {
                                aspectsList.Add(deserializedAspect.id, deserializedAspect);
                                aspectsListBox.Items.Add(deserializedAspect.id);
                            }
                        } else {
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

        private void elementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            ElementViewer ev = new ElementViewer(getElement(elementsListBox.SelectedItem.ToString()), false);
            ev.ShowDialog();
        }

        private void aspectListBox_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            AspectViewer av = new AspectViewer(getAspect(aspectsListBox.SelectedItem.ToString()), false);
            av.ShowDialog();
        }

        private void decksListBox_DoubleClick(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            DeckViewer dv = new DeckViewer(getDeck(decksListBox.SelectedItem.ToString()), false);
            dv.ShowDialog();
        }

        private void recipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            RecipeViewer rv = new RecipeViewer(getRecipe(recipesListBox.SelectedItem.ToString()), false);
            rv.ShowDialog();
        }

        private void legaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(getLegacy(legaciesListBox.SelectedItem.ToString()), false);
            lv.ShowDialog();
        }

        private void endingsListBox_DoubleClick(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(getEnding(endingsListBox.SelectedItem.ToString()), false);
            ev.ShowDialog();
        }

        private void verbsListBox_DoubleClick(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(getVerb(verbsListBox.SelectedItem.ToString()), false);
            vv.ShowDialog();
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
            using (ElementViewer ev = new ElementViewer(new Element(), true))
            {
                ev.ShowDialog();
                if (ev.DialogResult == DialogResult.OK)
                {
                    elementsList.Add(ev.displayedElement.id, ev.displayedElement);
                    elementsListBox.Items.Add(ev.displayedElement.id);
                }
            }
        }

        private void editManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManifestViewer mv = new ManifestViewer(manifest);
            mv.ShowDialog();
            manifest = mv.displayedManifest;
        }
        
        private void saveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(currentDirectory + "/content/"))
            {
                Directory.CreateDirectory(currentDirectory + "/content/");
            }

            JObject aspects = new JObject();
            aspects["elements"] = JArray.FromObject(aspectsList.Values);
            string aspectsJson = JsonConvert.SerializeObject(aspects, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_aspects.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(aspectsJson);
                jtw.Close();
                
            }
            JObject elements = new JObject();
            elements["elements"] = JArray.FromObject(elementsList.Values);
            string elementsJson = JsonConvert.SerializeObject(elements, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_elements.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(elementsJson);
                jtw.Close();
            }
            JObject recipes = new JObject();
            recipes["recipes"] = JArray.FromObject(recipesList.Values);
            string recipesJson = JsonConvert.SerializeObject(recipes, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_recipes.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(recipesJson);
                jtw.Close();
            }
            JObject decks = new JObject();
            decks["decks"] = JArray.FromObject(decksList.Values);
            string decksJson = JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_decks.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(decksJson);
                jtw.Close();
            }
            JObject legacies = new JObject();
            legacies["legacies"] = JArray.FromObject(legaciesList.Values);
            string legaciesJson = JsonConvert.SerializeObject(legacies, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_legacies.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(legaciesJson);
                jtw.Close();
            }
            JObject endings = new JObject();
            endings["endings"] = JArray.FromObject(endingsList.Values);
            string endingsJson = JsonConvert.SerializeObject(endings, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_endings.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(endingsJson);
                jtw.Close();
            }
            JObject verbs = new JObject();
            verbs["verbs"] = JArray.FromObject(verbsList.Values);
            string verbsJson = JsonConvert.SerializeObject(verbs, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            using (FileStream fs = File.Open(currentDirectory + "/content/" + manifest.name + "_verbs.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(verbsJson);
                jtw.Close();
            }
            // TODO uncomment this when it's safe to do so
            string manifestJson = JsonConvert.SerializeObject(manifest, Formatting.Indented);
            using (FileStream fs = File.Open(currentDirectory + "/manifest.json", FileMode.OpenOrCreate))
            {
                JsonTextWriter jtw = new JsonTextWriter(new StreamWriter(fs));
                jtw.WriteRaw(manifestJson);
                jtw.Close();
            }
        }

        private void ModViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utilities.currentMods.Remove(this);
        }

        private void ModViewer_Shown(object sender, EventArgs e)
        {
            if (isVanilla) return;
            else if (this.foundManifest) return;
            else this.Close();
        }
    }
}