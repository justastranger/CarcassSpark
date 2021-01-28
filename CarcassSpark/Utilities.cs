using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarcassSpark.ObjectViewers;
using CarcassSpark.ObjectTypes;
using Newtonsoft.Json;
using MindFusion.Layout;
using AssetStudio;

namespace CarcassSpark
{
    public class Utilities
    {
        private const string imagesPathAspects = "images/aspects/";
        private const string imagesPathElements = "images/elements/";
        private const string imagesPathEndings = "images/endings/";
        private const string imagesPathLegacies = "images/legacies/";
        private const string imagesPathVerbs = "images/verbs/";
        private const string imagesPathCardBacks = "images/cardbacks/";
        private const string imagesPathBurnImages = "images/burns/";
        public static Dictionary<string, ContentSource> ContentSources = new Dictionary<string, ContentSource>();


        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string DirectoryToVanillaContent
        {
            get
            {
                if (Settings.settings["GamePath"]?.ToString() != null)
                {
                    return Path.Combine(Settings.settings["GamePath"].ToString(), "cultistsimulator_Data\\StreamingAssets\\content\\core\\");
                }
                else
                {
                    return "\\cultistsimulator_Data\\StreamingAssets\\content\\core\\";
                }
            }
        }
        // This is the root asset bundle that contains references to all the game's assets
        // We'll figure out how to access it eventually to let us view vanilla images without ripping them first
        private static string DirectoryToVanillaAssets = "\\cultistsimulator_Data\\globalgamemanagers";
        private static AssetsManager AssetsManager = new AssetsManager();
        public static Dictionary<string, Sprite> assets = new Dictionary<string, Sprite>();
        public static ImageList ImageList;

        public static DataGridViewCellStyle DictionaryExtendStyle = new DataGridViewCellStyle();
        public static DataGridViewCellStyle DictionaryRemoveStyle = new DataGridViewCellStyle();
        public static System.Drawing.Color ListAppendColor = System.Drawing.Color.LimeGreen;
        public static System.Drawing.Color ListPrependColor = System.Drawing.Color.Aquamarine;
        public static System.Drawing.Color ListRemoveColor = System.Drawing.Color.Maroon;
        
        static Utilities(){
            DictionaryExtendStyle.BackColor = System.Drawing.Color.LimeGreen;
            DictionaryRemoveStyle.BackColor = System.Drawing.Color.Maroon;
            AssetsManager.LoadFiles(Settings.settings["GamePath"].ToString() + DirectoryToVanillaAssets);
            CollectSprites();
            // assetbundles = AssetsManager.assetsFileList.ToDictionary(file => file.fullName, file => file);
            // MessageBox.Show(String.Concat(assets.Keys));
        }

        private static void CollectSprites()
        {
            ResourceManager resourceManager = (ResourceManager)AssetsManager.assetsFileList[0].Objects.Find(@object => @object is ResourceManager);
            List<KeyValuePair<string, PPtr<AssetStudio.Object>>> containers = resourceManager.m_Container.ToList();
            foreach (KeyValuePair<string, PPtr<AssetStudio.Object>> keyValuePair in containers)
            {
                string key = keyValuePair.Key;
                PPtr<AssetStudio.Object> valuePointer = keyValuePair.Value;
                AssetStudio.Object value;
                if (valuePointer.TryGet(out value))
                {
                    if (value is Sprite sprite)
                    {
                        assets[key] = sprite;
                    }
                }
            }
        }

        public static string GetIdType(string id)
        {
            if (AspectExists(id)) return "aspect";
            if (ElementExists(id)) return "element";
            if (RecipeExists(id)) return "recipe";
            if (DeckExists(id)) return "deck";
            if (LegacyExists(id)) return "legacy";
            if (EndingExists(id)) return "ending";
            if (VerbExists(id)) return "verb";
            return "unknown";
        }
        
        public static Bitmap GetVanillaAspect(string id)
        {
            string path = "images/aspects/" + id;
            if (assets.ContainsKey(path))
            {
                return assets[path].GetImage();
            }
            else
            {
                return assets["images/elements/_x"].GetImage();
            }
        }

        public static bool VanillaAspectImageExists(string id)
        {
            return assets.ContainsKey(GetImagesPath(ModItemTypes.ASPECT) + id);
        }

        public static Bitmap GetVanillaElement(string id)
        {
            string path = "images/elements/" + id;
            if (assets.ContainsKey(path))
            {
                return assets[path].GetImage();
            }
            else
            {
                return assets["images/elements/_x"].GetImage();
            }
        }

        public static bool VanillaElementImageExists(string id)
        {
            return assets.ContainsKey(GetImagesPath(ModItemTypes.ELEMENT) + id);
        }

        public static Bitmap GetVanillaEnding(string id)
        {
            string path = "images/endings/" + id;
            if (assets.ContainsKey(path))
            {
                return assets[path].GetImage();
            }
            else
            {
                return assets["images/endings/despair"].GetImage();
            }
        }

        public static bool VanillaEndingImageExists(string id)
        {
            return assets.ContainsKey(GetImagesPath(ModItemTypes.ENDING) + id);
        }

        public static Bitmap GetVanillaLegacy(string id)
        {
            string path = "images/legacies/" + id;
            if (assets.ContainsKey(path))
            {
                return assets[path].GetImage();
            }
            else
            {
                return assets["images/legacies/aspirant"].GetImage();
            }
        }

        public static bool VanillaLegacyImageExists(string id)
        {
            return assets.ContainsKey(GetImagesPath(ModItemTypes.LEGACY) + id);
        }

        public static Bitmap GetVanillaVerb(string id)
        {
            string path = "images/verbs/" + id;
            if (assets.ContainsKey(path))
            {
                return assets[path].GetImage();
            }
            else
            {
                return assets["images/verbs/_x"].GetImage();
            }
        }

        public static bool VanillaVerbImageExists(string id)
        {
            return assets.ContainsKey(GetImagesPath(ModItemTypes.VERB) + id);
        }

        public static Bitmap GetVanillaCardBack(string id)
        {
            string path = "images/cardbacks/" + id;
            if (assets.ContainsKey(id))
            {
                return assets[id].GetImage();
            }
            else
            {
                return assets["images/cardbacks/_x"].GetImage();
            }
        }

        public static bool VanillaCardBackImageExists(string id)
        {
            return assets.ContainsKey(imagesPathCardBacks + id);
        }

        public static Bitmap GetVanillaBurnImage(string id)
        {
            string path = "images/burns/" + id;
            if (assets.ContainsKey(id))
            {
                return assets[id].GetImage();
            }
            else
            {
                return assets["images/burns/moon"].GetImage();
            }
        }

        public static bool VanillaBurnImageImageExists(string id)
        {
            return assets.ContainsKey(imagesPathBurnImages + id);
        }

        public static bool AspectImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/aspects/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaAspectImageExists(id)) return VanillaAspectImageExists(id);
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
                else if (source.GetName() == "Vanilla" && VanillaAspectImageExists(id)) return GetVanillaAspect(id);
            }
            string defaultImage = DirectoryToVanillaContent + "/images/elements/_x.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool ElementImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/elements/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaElementImageExists(id)) return VanillaElementImageExists(id);
            }
            return false;
        }

        public static Image GetElementImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.ElementImageExists(id)) return source.GetElementImage(id);
                else if (source.GetName() == "Vanilla" && VanillaElementImageExists(id)) return GetVanillaElement(id);
            }
            string defaultImage = DirectoryToVanillaContent + "/images/elements/_x.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool EndingImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/endings/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaEndingImageExists(id)) return VanillaEndingImageExists(id);
            }
            return false;
        }

        public static Image GetEndingImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.EndingImageExists(id)) return source.GetEndingImage(id);
                else if (source.GetName() == "Vanilla" && VanillaEndingImageExists(id)) return GetVanillaEnding(id);
            }
            string defaultImage = DirectoryToVanillaContent + "/images/endings/despair.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool LegacyImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/legacies/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaLegacyImageExists(id)) return VanillaLegacyImageExists(id);
            }
            return false;
        }

        public static Image GetLegacyImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.LegacyImageExists(id)) return source.GetLegacyImage(id);
                else if (source.GetName() == "Vanilla" && VanillaLegacyImageExists(id)) return GetVanillaLegacy(id);
            }
            string defaultImage = DirectoryToVanillaContent + "/images/legacies/ritual.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool VerbImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/verbs/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaVerbImageExists(id)) return VanillaVerbImageExists(id);
            }
            return false;
        }

        public static Image GetVerbImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.VerbImageExists(id)) return source.GetVerbImage(id);
                else if (source.GetName() == "Vanilla" && VanillaVerbImageExists(id)) return GetVanillaVerb(id);
            }
            string defaultImage = Utilities.DirectoryToVanillaContent + "/images/verbs/_x.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool CardBackImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/cardbacks/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaCardBackImageExists(id)) return VanillaCardBackImageExists(id);
            }
            return false;
        }

        public static Image GetCardBackImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.CardBackImageExists(id)) return source.GetCardBackImage(id);
                else if (source.GetName() == "Vanilla" && VanillaCardBackImageExists(id)) return GetVanillaCardBack(id);
            }
            string defaultImage = Utilities.DirectoryToVanillaContent + "/images/cardbacks/_x.png";
            if (File.Exists(defaultImage))
                return Image.FromFile(defaultImage);
            return null;
        }

        public static bool BurnImageExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (File.Exists(source.currentDirectory + "/images/burns/" + id + ".png"))
                {
                    return true;
                }
                else if (source.GetName() == "Vanilla" && VanillaBurnImageImageExists(id)) return VanillaBurnImageImageExists(id);
            }
            return false;
        }

        public static Image GetBurnImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.BurnImageExists(id)) return source.GetBurnImage(id);
                if (source.GetName() == "Vanilla" && VanillaBurnImageImageExists(id)) return GetVanillaBurnImage(id);
            }
            return null;
        }

        public static bool AspectExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.AspectExists(id)) return true;
            }
            return false;
        }

        public static Aspect GetAspect(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetAspect(id) != null)
                {
                    return source.GetAspect(id);
                }
            }
            return null;
        }

        public static bool ElementExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.ElementExists(id)) return true;
            }
            return false;
        }

        public static Element GetElement(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetElement(id) != null)
                {
                    return source.GetElement(id);
                }
            }
            return null;
        }

        public static bool RecipeExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.RecipeExists(id)) return true;
            }
            return false;
        }

        public static Recipe GetRecipe(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetRecipe(id) != null)
                {
                    return source.GetRecipe(id);
                }
            }
            return null;
        }

        public static bool DeckExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.RecipeExists(id)) return true;
            }
            return false;
        }

        public static Deck GetDeck(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetDeck(id) != null)
                {
                    return source.GetDeck(id);
                }
            }
            return null;
        }

        public static bool LegacyExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.LegacyExists(id)) return true;
            }
            return false;
        }

        public static Legacy GetLegacy(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetLegacy(id) != null)
                {
                    return source.GetLegacy(id);
                }
            }
            return null;
        }

        public static bool EndingExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.EndingExists(id)) return true;
            }
            return false;
        }

        public static Ending GetEnding(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetEnding(id) != null)
                {
                    return source.GetEnding(id);
                }
            }
            return null;
        }

        public static bool VerbExists(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.VerbExists(id)) return true;
            }
            return false;
        }

        public static Verb GetVerb(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                if (source.GetVerb(id) != null)
                {
                    return source.GetVerb(id);
                }
            }
            return null;
        }


        public static List<Aspect> GetAspects()
        {
            Dictionary<string, Aspect> tmp = new Dictionary<string, Aspect>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Aspect> AspectEntry in source.Aspects)
                {
                    if (!tmp.ContainsKey(AspectEntry.Key)) tmp.Add(AspectEntry.Key, AspectEntry.Value);
                    else tmp[AspectEntry.Key] = AspectEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Aspect>();
            else return null;
        }

        public static List<Element> GetElements()
        {
            Dictionary<string, Element> tmp = new Dictionary<string, Element>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Element> ElementEntry in source.Elements)
                {
                    if (!tmp.ContainsKey(ElementEntry.Key)) tmp.Add(ElementEntry.Key, ElementEntry.Value);
                    else tmp[ElementEntry.Key] = ElementEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Element>();
            else return null;
        }

        public static List<Recipe> GetRecipes()
        {
            Dictionary<string, Recipe> tmp = new Dictionary<string, Recipe>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Recipe> RecipeEntry in source.Recipes)
                {
                    if (!tmp.ContainsKey(RecipeEntry.Key)) tmp.Add(RecipeEntry.Key, RecipeEntry.Value);
                    else tmp[RecipeEntry.Key] = RecipeEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Recipe>();
            else return null;
        }

        public static List<Deck> GetDecks()
        {
            Dictionary<string, Deck> tmp = new Dictionary<string, Deck>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Deck> DeckEntry in source.Decks)
                {
                    if (!tmp.ContainsKey(DeckEntry.Key)) tmp.Add(DeckEntry.Key, DeckEntry.Value);
                    else tmp[DeckEntry.Key] = DeckEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Deck>();
            else return null;
        }

        public static List<Legacy> GetLegacies()
        {
            Dictionary<string, Legacy> tmp = new Dictionary<string, Legacy>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Legacy> LegacyEntry in source.Legacies)
                {
                    if (!tmp.ContainsKey(LegacyEntry.Key)) tmp.Add(LegacyEntry.Key, LegacyEntry.Value);
                    else tmp[LegacyEntry.Key] = LegacyEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Legacy>();
            else return null;
        }

        public static List<Ending> GetEndings()
        {
            Dictionary<string, Ending> tmp = new Dictionary<string, Ending>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Ending> EndingEntry in source.Endings)
                {
                    if (!tmp.ContainsKey(EndingEntry.Key)) tmp.Add(EndingEntry.Key, EndingEntry.Value);
                    else tmp[EndingEntry.Key] = EndingEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Ending>();
            else return null;
        }

        public static List<Verb> GetVerbs()
        {
            Dictionary<string, Verb> tmp = new Dictionary<string, Verb>();
            foreach (ContentSource source in ContentSources.Values)
            {
                foreach (KeyValuePair<string, Verb> VerbEntry in source.Verbs)
                {
                    if (!tmp.ContainsKey(VerbEntry.Key)) tmp.Add(VerbEntry.Key, VerbEntry.Value);
                    else tmp[VerbEntry.Key] = VerbEntry.Value;
                }
            }
            if (tmp.Count > 0) return tmp.Values.ToList<Verb>();
            else return null;
        }

        public static ContentSource GetContentSource(string name)
        {
            if (ContentSources.ContainsKey(name))
            {
                return ContentSources[name];
            }
            else
            {
                return null;
            }
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

        private static string GetImagesPath(ModItemTypes itemType)
        {
            switch (itemType)
            {
            case ModItemTypes.ASPECT:
                return imagesPathAspects;
            case ModItemTypes.ELEMENT:
                return imagesPathElements;
            case ModItemTypes.LEGACY:
                return imagesPathLegacies;
            case ModItemTypes.ENDING:
                return imagesPathEndings;
            case ModItemTypes.VERB:
                return imagesPathVerbs;
            default:
                throw new NotImplementedException();
            }
        }

        public enum ModItemTypes
        {
            ASPECT,
            ELEMENT,
            RECIPE,
            DECK,
            LEGACY,
            ENDING,
            VERB,
            UNKNOWN,
            CULTURE
        }
    }
}
