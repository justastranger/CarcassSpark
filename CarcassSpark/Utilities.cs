extern alias CultistSimulator;
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

namespace CarcassSpark
{
    public class Utilities
    {
        public static Dictionary<string, ContentSource> ContentSources = new Dictionary<string, ContentSource>();


        public static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string directoryToVanillaContent = "./cultistsimulator_Data/StreamingAssets/content/core/";
        // This is the root asset bundle that contains references to all the game's assets
        // We'll figure out how to access it eventually to let us view vanilla images without ripping them first
        private static string directoryToVanillaAssets = "./cultistsimulator_Data/globalgamemanagers";

        public static DataGridViewCellStyle DictionaryExtendStyle = new DataGridViewCellStyle();
        public static DataGridViewCellStyle DictionaryRemoveStyle = new DataGridViewCellStyle();
        public static Color ListAppendColor = Color.LimeGreen;
        public static Color ListPrependColor = Color.Aquamarine;
        public static Color ListRemoveColor = Color.Maroon;
        
        static Utilities(){
            DictionaryExtendStyle.BackColor = Color.LimeGreen;
            DictionaryRemoveStyle.BackColor = Color.Maroon;
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
        
        public static Image GetVanillaAspect(string path)
        {
            throw new NotImplementedException("I still haven't figured out how to do this.");
        }

        public static Image GetAspectImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/icons40/aspects/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if(File.Exists(directoryToVanillaContent + "/images/elementArt/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/elementArt/_x.png");
            return null;
        }

        public static Image GetElementImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/elementArt/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/elementArt/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/elementArt/_x.png");
            return null;
        }

        public static Image GetEndingImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/endingArt/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/endingArt/despair.png")) return Image.FromFile(directoryToVanillaContent + "/images/endingArt/despair.png");
            return null;
        }

        public static Image GetLegacyImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/icons100/legacies/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/icons100/legacies/ritual.png")) return Image.FromFile(directoryToVanillaContent + "/images/icons100/legacies/ritual.png");
            return null;
        }

        public static Image GetVerbImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/icons100/verbs/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/icons100/verbs/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/icons100/verbs/_x.png");
            return null;
        }

        public static Image GetCardBackImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/cardBacks/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
            }
            if (File.Exists(directoryToVanillaContent + "/images/cardBacks/_x.png")) return Image.FromFile(directoryToVanillaContent + "/images/cardBacks/_x.png");
            return null;
        }

        public static Image GetBurnImage(string id)
        {
            foreach (ContentSource source in ContentSources.Values)
            {
                string pathToImage = source.currentDirectory + "/images/burnImages/" + id + ".png";
                if (File.Exists(pathToImage))
                {
                    return Image.FromFile(pathToImage);
                }
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
    }
}
