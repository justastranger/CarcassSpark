using AssetStudio;
using CarcassSpark.ObjectTypes;
using CarcassSpark.ObjectViewers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CarcassSpark
{
    public static class Utilities
    {
        public static Dictionary<string, ContentSource> ContentSources = new Dictionary<string, ContentSource>();
        
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string DirectoryToVanillaContent =>
            Settings.settings["GamePath"]?.ToString() != null
                ? Path.Combine(Settings.settings["GamePath"].ToString(), "cultistsimulator_Data\\StreamingAssets\\content\\core\\")
                : "\\cultistsimulator_Data\\StreamingAssets\\content\\core\\";

        // This is the root asset bundle that contains references to all the game's assets
        // We'll figure out how to access it eventually to let us view vanilla images without ripping them first
        private static string _directoryToVanillaAssets = "\\cultistsimulator_Data\\globalgamemanagers";
        private static readonly AssetsManager AssetsManager = new AssetsManager();
        public static Dictionary<string, Sprite> Assets = new Dictionary<string, Sprite>();
        public static ImageList ImageList = new ImageList
        {
            ImageSize = new Size(128, 128)
        };

        public static DataGridViewCellStyle DictionaryExtendStyle = new DataGridViewCellStyle();
        public static DataGridViewCellStyle DictionaryRemoveStyle = new DataGridViewCellStyle();
        public static System.Drawing.Color ListAppendColor = System.Drawing.Color.LimeGreen;
        public static System.Drawing.Color ListPrependColor = System.Drawing.Color.Aquamarine;
        public static System.Drawing.Color ListRemoveColor = System.Drawing.Color.Maroon;

        static Utilities()
        {
            DictionaryExtendStyle.BackColor = System.Drawing.Color.LimeGreen;
            DictionaryRemoveStyle.BackColor = System.Drawing.Color.Maroon;
            AssetsManager.LoadFiles(Settings.settings["GamePath"] + _directoryToVanillaAssets);
            CollectSprites();
        }

        private static void CollectSprites()
        {
            ResourceManager resourceManager = (ResourceManager)AssetsManager.assetsFileList[0].Objects.Find(@object => @object is ResourceManager);
            List<KeyValuePair<string, PPtr<AssetStudio.Object>>> containers = resourceManager.m_Container.ToList();
            foreach (KeyValuePair<string, PPtr<AssetStudio.Object>> keyValuePair in containers)
            {
                string key = keyValuePair.Key;
                PPtr<AssetStudio.Object> valuePointer = keyValuePair.Value;
                if (valuePointer.TryGet(out AssetStudio.Object value))
                {
                    if (value is Sprite sprite)
                    {
                        Assets[key] = sprite;
                        ImageList.Images.Add(key, sprite.GetImage());
                    }
                }
            }
        }

        public static string GetIdType(Guid id)
        {
            return AspectExists(id)
                ? "aspect"
                : ElementExists(id)
                ? "element"
                : RecipeExists(id)
                ? "recipe"
                : DeckExists(id)
                ? "deck"
                : LegacyExists(id)
                ? "legacy" 
                : EndingExists(id) 
                ? "ending" 
                : VerbExists(id) 
                ? "verb" 
                : "unknown";
        }

        public static string GetIdType(string id)
        {
            return AspectExists(id)
                ? "aspect"
                : ElementExists(id)
                ? "element"
                : RecipeExists(id)
                ? "recipe"
                : DeckExists(id)
                ? "deck"
                : LegacyExists(id)
                ? "legacy" 
                : EndingExists(id) 
                ? "ending" 
                : VerbExists(id) 
                ? "verb" 
                : "unknown";
        }

        public static Bitmap GetVanillaAspect(string id)
        {
            try
            {
                string path = "images/aspects/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/elements/_x"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        public static bool VanillaAspectImageExists(string id)
        {
            return Assets.ContainsKey("images/aspects/" + id);
        }

        public static Bitmap GetVanillaElement(string id)
        {
            try
            {
                string path = "images/elements/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/elements/_x"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaElementImageExists(string id)
        {
            return Assets.ContainsKey("images/elements/" + id);
        }

        public static Bitmap GetVanillaEnding(string id)
        {
            try
            {
                string path = "images/endings/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/endings/despair"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaEndingImageExists(string id)
        {
            return Assets.ContainsKey("images/endings/" + id);
        }

        public static Bitmap GetVanillaLegacy(string id)
        {
            try
            {
                string path = "images/legacies/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/legacies/aspirant"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaLegacyImageExists(string id)
        {
            return Assets.ContainsKey("images/legacies/" + id);
        }

        public static Bitmap GetVanillaVerb(string id)
        {
            try
            {
                string path = "images/verbs/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/verbs/_x"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaVerbImageExists(string id)
        {
            return Assets.ContainsKey("images/verbs/" + id);
        }

        public static Bitmap GetVanillaCardBack(string id)
        {
            try
            {
                string path = "images/cardbacks/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/cardbacks/_x"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaCardBackImageExists(string id)
        {
            return Assets.ContainsKey("images/cardbacks/" + id);
        }

        public static Bitmap GetVanillaBurnImage(string id)
        {
            try
            {
                string path = "images/burns/" + id;
                if (Assets.ContainsKey(path))
                {
                    return Assets[path].GetImage();
                }
                else
                {
                    return Assets["images/burns/moon"].GetImage();
                }
            }
            catch (TypeInitializationException)
            {
                MessageBox.Show("Asset Studio's Texture Decoder Library can not be found. Please reinstall Carcass Spark.", "Missing Libraries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool VanillaBurnImageImageExists(string id)
        {
            return Assets.ContainsKey("images/burns/" + id);
        }

        public static bool AspectImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/aspects/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaAspectImageExists(id))
                {
                    return VanillaAspectImageExists(id);
                }
            }
            return false;
        }

        public static Image GetAspectImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.AspectImageExists(id))
                {
                    return source.GetAspectImage(id);
                }
                else if (source.IsVanilla() && VanillaAspectImageExists(id))
                {
                    return GetVanillaAspect(id);
                }
            }
            string defaultImage = DirectoryToVanillaContent + "/images/elements/_x.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool ElementImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/elements/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaElementImageExists(id))
                {
                    return VanillaElementImageExists(id);
                }
            }
            return false;
        }

        public static Image GetElementImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.ElementImageExists(id))
                {
                    return source.GetElementImage(id);
                }
                else if (source.IsVanilla() && VanillaElementImageExists(id))
                {
                    return GetVanillaElement(id);
                }
            }
            string defaultImage = DirectoryToVanillaContent + "/images/elements/_x.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool EndingImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/endings/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaEndingImageExists(id))
                {
                    return VanillaEndingImageExists(id);
                }
            }
            return false;
        }

        public static Image GetEndingImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.EndingImageExists(id))
                {
                    return source.GetEndingImage(id);
                }
                else if (source.IsVanilla() && VanillaEndingImageExists(id))
                {
                    return GetVanillaEnding(id);
                }
            }
            string defaultImage = DirectoryToVanillaContent + "/images/endings/despair.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool LegacyImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/legacies/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaLegacyImageExists(id))
                {
                    return VanillaLegacyImageExists(id);
                }
            }
            return false;
        }

        public static Image GetLegacyImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.LegacyImageExists(id))
                {
                    return source.GetLegacyImage(id);
                }
                else if (source.IsVanilla() && VanillaLegacyImageExists(id))
                {
                    return GetVanillaLegacy(id);
                }
            }
            string defaultImage = DirectoryToVanillaContent + "/images/legacies/ritual.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool VerbImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/verbs/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaVerbImageExists(id))
                {
                    return VanillaVerbImageExists(id);
                }
            }
            return false;
        }

        public static Image GetVerbImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.VerbImageExists(id))
                {
                    return source.GetVerbImage(id);
                }
                else if (source.IsVanilla() && VanillaVerbImageExists(id))
                {
                    return GetVanillaVerb(id);
                }
            }
            string defaultImage = Utilities.DirectoryToVanillaContent + "/images/verbs/_x.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool CardBackImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/cardbacks/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaCardBackImageExists(id))
                {
                    return VanillaCardBackImageExists(id);
                }
            }
            return false;
        }

        public static Image GetCardBackImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.CardBackImageExists(id))
                {
                    return source.GetCardBackImage(id);
                }
                else if (source.IsVanilla() && VanillaCardBackImageExists(id))
                {
                    return GetVanillaCardBack(id);
                }
            }
            string defaultImage = Utilities.DirectoryToVanillaContent + "/images/cardbacks/_x.png";
            return File.Exists(defaultImage) ? Image.FromFile(defaultImage) : null;
        }

        public static bool BurnImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.CurrentDirectory + "/images/burns/" + id + ".png"))
                {
                    return true;
                }
                else if (source.IsVanilla() && VanillaBurnImageImageExists(id))
                {
                    return VanillaBurnImageImageExists(id);
                }
            }
            return false;
        }

        public static Image GetBurnImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.BurnImageExists(id))
                {
                    return source.GetBurnImage(id);
                }

                if (source.IsVanilla() && VanillaBurnImageImageExists(id))
                {
                    return GetVanillaBurnImage(id);
                }
            }
            return null;
        }

        public static bool AspectExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Aspects.Exists(id));
        }

        public static bool AspectExists(string id)
        {
            return ContentSources.Values.Any(source => source.Aspects.Exists(id));
        }

        public static Aspect GetAspect(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Aspects.Exists(id))
                {
                    return source.Aspects.Get(id);
                }
            }
            return null;
        }

        public static Aspect GetAspect(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Aspects.Exists(id))
                {
                    return source.Aspects.GetByName(id);
                }
            }
            return null;
        }

        public static bool ElementExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Elements.Exists(id));
        }

        public static bool ElementExists(string id)
        {
            return ContentSources.Values.Any(source => source.Elements.Exists(id));
        }

        public static Element GetElement(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Elements.Exists(id))
                {
                    return source.Elements.Get(id);
                }
            }
            return null;
        }

        public static Element GetElement(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Elements.Exists(id))
                {
                    return source.Elements.GetByName(id);
                }
            }
            return null;
        }

        public static bool RecipeExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Recipes.Exists(id));
        }

        public static bool RecipeExists(string id)
        {
            return ContentSources.Values.Any(source => source.Recipes.Exists(id));
        }

        public static Recipe GetRecipe(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Recipes.Exists(id))
                {
                    return source.Recipes.Get(id);
                }
            }
            return null;
        }

        public static Recipe GetRecipe(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Recipes.Exists(id))
                {
                    return source.Recipes.GetByName(id);
                }
            }
            return null;
        }

        public static bool DeckExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Decks.Exists(id));
        }

        public static bool DeckExists(string id)
        {
            return ContentSources.Values.Any(source => source.Decks.Exists(id));
        }

        public static Deck GetDeck(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Decks.Exists(id))
                {
                    return source.Decks.Get(id);
                }
            }
            return null;
        }

        public static Deck GetDeck(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Decks.Exists(id))
                {
                    return source.Decks.GetByName(id);
                }
            }
            return null;
        }

        public static bool LegacyExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Legacies.Exists(id));
        }

        public static bool LegacyExists(string id)
        {
            return ContentSources.Values.Any(source => source.Legacies.Exists(id));
        }

        public static Legacy GetLegacy(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Legacies.Exists(id))
                {
                    return source.Legacies.Get(id);
                }
            }
            return null;
        }

        public static Legacy GetLegacy(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Legacies.Exists(id))
                {
                    return source.Legacies.GetByName(id);
                }
            }
            return null;
        }

        public static bool EndingExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Endings.Exists(id));
        }

        public static bool EndingExists(string id)
        {
            return ContentSources.Values.Any(source => source.Endings.Exists(id));
        }

        public static Ending GetEnding(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Endings.Exists(id))
                {
                    return source.Endings.Get(id);
                }
            }
            return null;
        }

        public static Ending GetEnding(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Endings.Exists(id))
                {
                    return source.Endings.GetByName(id);
                }
            }
            return null;
        }

        public static bool VerbExists(Guid id)
        {
            return ContentSources.Values.Any(source => source.Verbs.Exists(id));
        }

        public static bool VerbExists(string id)
        {
            return ContentSources.Values.Any(source => source.Verbs.Exists(id));
        }

        public static Verb GetVerb(Guid id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Verbs[id] != null)
                {
                    return source.Verbs.Get(id);
                }
            }
            return null;
        }

        public static Verb GetVerb(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.Verbs.Exists(id))
                {
                    return source.Verbs.GetByName(id);
                }
            }
            return null;
        }

        #region Get List of all (Type)

        public static List<Aspect> GetAspects()
        {
            return GetGameObjects<Aspect>();
        }

        public static List<Element> GetElements()
        {
            return GetGameObjects<Element>();
        }

        public static List<Recipe> GetRecipes()
        {
            return GetGameObjects<Recipe>();
        }

        public static List<Deck> GetDecks()
        {
            return GetGameObjects<Deck>();
        }

        public static List<Legacy> GetLegacies()
        {
            return GetGameObjects<Legacy>();
        }

        public static List<Ending> GetEndings()
        {
            return GetGameObjects<Ending>();
        }

        public static List<Verb> GetVerbs()
        {
            return GetGameObjects<Verb>();
        }

        public static List<T> GetGameObjects<T>() where T : IGameObject
        {
            Dictionary<Guid, T> tmp = new Dictionary<Guid, T>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<Guid, T> entry in source.GetContentGroup<T>())
                {
                    if (!tmp.ContainsKey(entry.Key))
                    {
                        tmp.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        tmp[entry.Key] = entry.Value;
                    }
                }
            }
            return tmp.Count > 0 ? tmp.Values.ToList() : null;
        }

        #endregion

        public static ContentSource GetContentSource(string name)
        {
            return ContentSources.ContainsKey(name) ? ContentSources[name] : null;
        }

        public static string SerializeObject(object objectToSerialize)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (JsonTextWriter writer = new JsonTextWriter(stringWriter))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;
                jsonSerializer.Serialize(writer, objectToSerialize);
            }
            return stringBuilder.ToString();
        }

        public static IGameObjectViewer GetViewer<T>(T gameObject, EventHandler<T> successCallback) where T : IGameObject
        {
            if (typeof(T) == typeof(Aspect))
            {
                return new AspectViewer(gameObject as Aspect, successCallback as EventHandler<Aspect>, null);
            }
            else if (typeof(T) == typeof(Element))
            {
                return new ElementViewer(gameObject as Element, successCallback as EventHandler<Element>, null);
            }
            else if (typeof(T) == typeof(Recipe))
            {
                return new RecipeViewer(gameObject as Recipe, successCallback as EventHandler<Recipe>, null);
            }
            else if (typeof(T) == typeof(Deck))
            {
                return new DeckViewer(gameObject as Deck, successCallback as EventHandler<Deck>, null);
            }
            else if (typeof(T) == typeof(Legacy))
            {
                return new LegacyViewer(gameObject as Legacy, successCallback as EventHandler<Legacy>, null);
            }
            else if (typeof(T) == typeof(Ending))
            {
                return new EndingViewer(gameObject as Ending, successCallback as EventHandler<Ending>, null);
            }
            else if (typeof(T) == typeof(Verb))
            {
                return new VerbViewer(gameObject as Verb, successCallback as EventHandler<Verb>, null);
            }
            else
            {
                throw new ArgumentOutOfRangeException("No viewer is defined in GetViewer for Game Object " + gameObject.GetType());
            }
        }
    }
}
