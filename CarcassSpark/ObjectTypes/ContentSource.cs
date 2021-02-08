using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class ContentSource
    {
        public Guid guid = Guid.NewGuid();

        public string currentDirectory;
        public Synopsis synopsis;

        public JObject CustomManifest = new JObject();
        // CustomManifest["EditMode"]
        // CustomManifest["AutoSave"]

        public Dictionary<Guid, Aspect> Aspects = new Dictionary<Guid, Aspect>();
        public Dictionary<Guid, Element> Elements = new Dictionary<Guid, Element>();
        public Dictionary<Guid, Recipe> Recipes = new Dictionary<Guid, Recipe>();
        public Dictionary<Guid, Deck> Decks = new Dictionary<Guid, Deck>();
        public Dictionary<Guid, Legacy> Legacies = new Dictionary<Guid, Legacy>();
        public Dictionary<Guid, Ending> Endings = new Dictionary<Guid, Ending>();
        public Dictionary<Guid, Verb> Verbs = new Dictionary<Guid, Verb>();
        public Dictionary<Guid, Culture> Cultures = new Dictionary<Guid, Culture>();

        public ContentSource()
        {
            
        }
        
        public string GetName()
        {
            if (synopsis != null) return synopsis.name;
            else return null;
        }

        public Element GetElement(Guid id)
        {
            if (ElementExists(id)) return Elements[id];
            else return null;
        }

        public Element GetElement(string id)
        {
            if (ElementExists(id))
            {
                foreach (Element element in Elements.Values)
                {
                    if (element.id == id) return element;
                }
            }
            return null;
        }

        public bool ElementExists(Guid id)
        {
            return Elements.ContainsKey(id);
        }

        public Deck GetDeck(Guid id)
        {
            if (DeckExists(id)) return Decks[id];
            else return null;
        }

        public Deck GetDeck(string id)
        {
            if (DeckExists(id))
            {
                foreach (Deck deck in Decks.Values)
                {
                    if (deck.id == id) return deck;
                }
            }
            return null;
        }

        public bool DeckExists(Guid id)
        {
            return Decks.ContainsKey(id);
        }

        public Aspect GetAspect(Guid id)
        {
            if (AspectExists(id)) return Aspects[id];
            else return null;
        }

        public Aspect GetAspect(string id)
        {
            if (AspectExists(id))
            {
                foreach (Aspect aspect in Aspects.Values)
                {
                    if (aspect.id == id) return aspect;
                }
            }
            return null;
        }

        public bool AspectExists(Guid id)
        {
            return Aspects.ContainsKey(id);
        }

        public bool AspectExists(string id)
        {
            foreach (Aspect aspect in Aspects.Values)
            {
                if (aspect.id == id) return true;
            }
            return false;
        }

        public bool ElementExists(string id)
        {
            foreach (Element element in Elements.Values)
            {
                if (element.id == id) return true;
            }
            return false;
        }

        public bool RecipeExists(string id)
        {
            foreach (Recipe recipe in Recipes.Values)
            {
                if (recipe.id == id) return true;
            }
            return false;
        }

        public bool DeckExists(string id)
        {
            foreach (Deck deck in Decks.Values)
            {
                if (deck.id == id) return true;
            }
            return false;
        }

        public bool LegacyExists(string id)
        {
            foreach (Legacy legacy in Legacies.Values)
            {
                if (legacy.id == id) return true;
            }
            return false;
        }

        public bool VerbExists(string id)
        {
            foreach (Verb verb in Verbs.Values)
            {
                if (verb.id == id) return true;
            }
            return false;
        }

        public bool EndingExists(string id)
        {
            foreach (Ending ending in Endings.Values)
            {
                if (ending.id == id) return true;
            }
            return false;
        }

        public Legacy GetLegacy(Guid id)
        {
            if (LegacyExists(id)) return Legacies[id];
            else return null;
        }

        public Legacy GetLegacy(string id)
        {
            if (LegacyExists(id))
            {
                foreach (Legacy legacy in Legacies.Values)
                {
                    if (legacy.id == id) return legacy;
                }
            }
            return null;
        }

        public bool LegacyExists(Guid id)
        {
            return Legacies.ContainsKey(id);
        }

        public Recipe GetRecipe(Guid id)
        {
            if (RecipeExists(id)) return Recipes[id];
            else return null;
        }

        public Recipe GetRecipe(string id)
        {
            if (RecipeExists(id))
            {
                foreach (Recipe recipe in Recipes.Values)
                {
                    if (recipe.id == id) return recipe;
                }
            }
            return null;
        }

        public bool RecipeExists(Guid id)
        {
            return Recipes.ContainsKey(id);
        }

        public Ending GetEnding(Guid id)
        {
            if (EndingExists(id)) return Endings[id];
            else return null;
        }

        public Ending GetEnding(string id)
        {
            if (EndingExists(id))
            {
                foreach (Ending ending in Endings.Values)
                {
                    if (ending.id == id) return ending;
                }
            }
            return null;
        }

        public bool EndingExists(Guid id)
        {
            return Endings.ContainsKey(id);
        }

        public Verb GetVerb(Guid id)
        {
            if (VerbExists(id)) return Verbs[id];
            else return null;
        }

        public Verb GetVerb(string id)
        {
            if (VerbExists(id))
            {
                foreach (Verb verb in Verbs.Values)
                {
                    if (verb.id == id) return verb;
                }
            }
            return null;
        }

        public bool VerbExists(Guid id)
        {
            return Verbs.ContainsKey(id);
        }

        public Culture GetCulture(Guid id)
        {
            if (CultureExists(id)) return Cultures[id];
            else return null;
        }

        public Culture GetCulture(string id)
        {
            if (CultureExists(id))
            {
                foreach (Culture culture in Cultures.Values)
                {
                    if (culture.id == id) return culture;
                }
            }
            return null;
        }

        public bool CultureExists(Guid id)
        {
            return Cultures.ContainsKey(id);
        }

        public bool CultureExists(string id)
        {
            foreach (Culture culture in Cultures.Values)
            {
                if (culture.id == id) return true;
            }
            return false;
        }

        public void SetCustomManifestProperty(string key, object value)
        {
            CustomManifest[key] = JToken.FromObject(value);
        }

        public string GetCustomManifestString(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToString();
            else return null;
        }

        public bool? GetCustomManifestBool(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToObject<bool?>();
            else return null;
        }

        public int? GetCustomManifestInt(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToObject<int?>();
            else return null;
        }

        public List<int> GetCustomManifestListInt(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToObject<List<int>>();
            else return null;
        }

        public void SetRecentGroup(string type, string groupName)
        {
            Dictionary<string, string> recentGroups = CustomManifest["recentGroups"]?.ToObject<Dictionary<string, string>>();
            if (recentGroups != null)
            {
                recentGroups[type] = groupName;
            }
            else
            {
                recentGroups = new Dictionary<string, string>()
                {
                    {type, groupName}
                };
            }
            CustomManifest["recentGroups"] = JObject.FromObject(recentGroups);
        }

        public string GetRecentGroup(string type)
        {
            Dictionary<string, string> recentGroups = CustomManifest["recentGroups"]?.ToObject<Dictionary<string, string>>();
            if (recentGroups != null && recentGroups.ContainsKey(type))
            {
                return recentGroups[type];
            }
            else
            {
                return null;
            }
        }

        public Image GetAspectImage(string id)
        {
            string pathToImage = currentDirectory + "/images/aspects/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaAspectImageExists(id))
            {
                return Utilities.GetVanillaAspect(id);
            }
            return null;
        }

        public Image GetElementImage(string id)
        {
            string pathToImage = currentDirectory + "/images/elements/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaElementImageExists(id))
            {
                return Utilities.GetVanillaElement(id);
            }
            return null;
        }

        public Image GetEndingImage(string id)
        {
            string pathToImage = currentDirectory + "/images/endings/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaEndingImageExists(id))
            {
                return Utilities.GetVanillaEnding(id);
            }
            return null;
        }

        public Image GetLegacyImage(string id)
        {
            string pathToImage = currentDirectory + "/images/legacies/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaLegacyImageExists(id))
            {
                return Utilities.GetVanillaLegacy(id);
            }
            return null;
        }

        public Image GetVerbImage(string id)
        {
            string pathToImage = currentDirectory + "/images/verbs/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaVerbImageExists(id))
            {
                return Utilities.GetVanillaVerb(id);
            }
            return null;
        }

        public Image GetCardBackImage(string id)
        {
            string pathToImage = currentDirectory + "/images/cardbacks/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaCardBackImageExists(id))
            {
                return Utilities.GetVanillaCardBack(id);
            }
            return null;
        }

        public Image GetBurnImage(string id)
        {
            string pathToImage = currentDirectory + "/images/burns/" + id + ".png";
            if (File.Exists(pathToImage))
            {
                return Image.FromFile(pathToImage);
            }
            else if (Utilities.VanillaBurnImageImageExists(id))
            {
                return Utilities.GetVanillaBurnImage(id);
            }
            return null;
        }

        public bool AspectImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/aspects/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool ElementImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/elements/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool EndingImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/endings/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool LegacyImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/legacies/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool VerbImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/verbs/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool CardBackImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/cardbacks/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public bool BurnImageExists(string id)
        {
            string pathToImage = currentDirectory + "/images/burns/" + id + ".png";
            return File.Exists(pathToImage);
        }

        public ContentSource Copy()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<ContentSource>(serializedObject);
        }
    }
}
