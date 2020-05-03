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
    public partial class MainForm : Form
    {
        private string directoryToContent = "./cultistsimulator_Data/StreamingAssets/content/";
        private string coreContent = "core/";
        private string moddedNewContent = "more/";
        public static string currentModDirectory;

        public static Dictionary<string, Aspect> aspectsList = new Dictionary<string, Aspect>();
        public static Dictionary<string, Element> elementsList = new Dictionary<string, Element>();
        public static Dictionary<string, Recipe> recipesList = new Dictionary<string, Recipe>();
        public static Dictionary<string, Aspect> decksList = new Dictionary<string, Aspect>();
        public static Dictionary<string, Aspect> legaciesList = new Dictionary<string, Aspect>();
        public static Dictionary<string, Aspect> endingsList = new Dictionary<string, Aspect>();
        public static Dictionary<string, Aspect> verbsList = new Dictionary<string, Aspect>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadContentButton_Click(object sender, EventArgs e)
        {
            // clear everything so everything can be reloaded
            aspectListBox.Items.Clear();
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

            foreach (string folder in Directory.EnumerateDirectories(directoryToContent + coreContent))
            {
                foreach ( string file in Directory.EnumerateFiles(folder))
                {
                    //MessageBox.Show(file);
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        reloadFile(fs);
                    }
                }
            }

            using (FileStream aspectsFile = new FileStream(directoryToContent + coreContent + "elements/_aspects.json", FileMode.Open))
            {
                reloadFile(aspectsFile);
            }
        }

        private void newModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            folderBrowserDialog1.ShowDialog();
        }

        private void selectModToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void reloadFile(FileStream file)
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
                            aspectsList.Add(deserializedAspect.id, deserializedAspect);
                            aspectListBox.Items.Add(deserializedAspect.id);
                        } else {
                            Element deserializedElement = element.ToObject<Element>();
                            elementsList.Add(deserializedElement.id, deserializedElement);
                            elementsListBox.Items.Add(deserializedElement.id);
                        }
                    }
                    return;
                case "recipes":
                    foreach (JToken recipe in parsedJToken.First.ToArray())
                    {
                        Recipe deserializedRecipe = recipe.ToObject<Recipe>();
                        recipesList.Add(deserializedRecipe.id, deserializedRecipe);
                        recipesListBox.Items.Add(deserializedRecipe.id);
                    }
                    return;
                case "decks":
                    return;
                case "legacies":
                    return;
                case "endings":
                    return;
                case "verbs":
                    return;
                default:
                    break;
            }
            // JToken[] aspects = JsonConvert.DeserializeObject<JObject>(fileText)["elements"].ToArray();
            foreach (JToken aspect in parsedJToken)
            {
                //Aspect deserializedAspect = aspect.ToObject<Aspect>();
                //aspectsList[deserializedAspect.id] = deserializedAspect;
            }

        }
    }
}