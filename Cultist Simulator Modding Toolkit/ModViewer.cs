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
        public string currentModDirectory;
        public Manifest currentModManifest;
        public bool isVanilla;
        
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
            this.currentModDirectory = location;
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

            foreach (string file in Directory.EnumerateFiles(currentModDirectory, "*.json", SearchOption.AllDirectories))
            {
                //MessageBox.Show(file);
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    if(file.Contains("manifest.json"))
                    {
                        loadManifest(fs);
                    } else {
                        loadFile(fs);
                    }
                }
            }
        }
        
        public void loadManifest(FileStream file)
        {
            string fileText = new StreamReader(file).ReadToEnd();
            currentModManifest = JsonConvert.DeserializeObject<Manifest>(fileText);
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
            return elementsList[id];
        }

        public bool elementExists(string id)
        {
            return elementsList.ContainsKey(id);
        }

        public Deck getDeck(string id)
        {
            return decksList[id];
        }

        public bool deckExists(string id)
        {
            return decksList.ContainsKey(id);
        }
        public Aspect getAspect(string id)
        {
            return aspectsList[id];
        }

        public bool aspectExists(string id)
        {
            return aspectsList.ContainsKey(id);
        }

        public Legacy getLegacy(string id)
        {
            return legaciesList[id];
        }

        public bool legacyExists(string id)
        {
            return legaciesList.ContainsKey(id);
        }

        public Recipe getRecipe(string id)
        {
            return recipesList[id];
        }

        public bool recipeExists(string id)
        {
            return recipesList.ContainsKey(id);
        }
        
        public Ending getEnding(string id)
        {
            return endingsList[id];
        }

        public bool endingExists(string id)
        {
            return endingsList.ContainsKey(id);
        }

        public Verb getVerb(string id)
        {
            return verbsList[id];
        }

        public bool verbExists(string id)
        {
            return verbsList.ContainsKey(id);
        }

        private void elementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedItem == null) return;
            ElementViewer ev = new ElementViewer(elementsList[elementsListBox.SelectedItem.ToString()], this);
            ev.ShowDialog();
        }

        private void aspectListBox_DoubleClick(object sender, EventArgs e)
        {
            if (aspectsListBox.SelectedItem == null) return;
            AspectViewer av = new AspectViewer(aspectsList[aspectsListBox.SelectedItem.ToString()], this);
            av.ShowDialog();
        }

        private void decksListBox_DoubleClick(object sender, EventArgs e)
        {
            if (decksListBox.SelectedItem == null) return;
            DeckViewer dv = new DeckViewer(decksList[decksListBox.SelectedItem.ToString()], this);
            dv.ShowDialog();
        }

        private void recipesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (recipesListBox.SelectedItem == null) return;
            RecipeViewer rv = new RecipeViewer(recipesList[recipesListBox.SelectedItem.ToString()], this);
            rv.ShowDialog();
        }

        private void legaciesListBox_DoubleClick(object sender, EventArgs e)
        {
            LegacyViewer lv = new LegacyViewer(legaciesList[legaciesListBox.SelectedItem.ToString()], this);
            lv.ShowDialog();
        }

        private void endingsListBox_DoubleClick(object sender, EventArgs e)
        {
            EndingViewer ev = new EndingViewer(endingsList[endingsListBox.SelectedItem.ToString()], this);
            ev.ShowDialog();
        }

        private void verbsListBox_DoubleClick(object sender, EventArgs e)
        {
            VerbViewer vv = new VerbViewer(verbsList[verbsListBox.SelectedItem.ToString()], this);
            vv.ShowDialog();
        }

        private void aspectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editManifestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManifestViewer mv = new ManifestViewer(currentModManifest);
            mv.ShowDialog();
            currentModManifest = mv.displayedManifest;
        }
        
        private void saveModToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
    }
}