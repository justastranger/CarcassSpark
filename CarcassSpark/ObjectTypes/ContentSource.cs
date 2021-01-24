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

        public string currentDirectory;
        public Synopsis synopsis;

        public JObject CustomManifest = new JObject();
        // CustomManifest["EditMode"]
        // CustomManifest["AutoSave"]

        public Dictionary<string, Aspect> Aspects = new Dictionary<string, Aspect>();
        public Dictionary<string, Element> Elements = new Dictionary<string, Element>();
        public Dictionary<string, Recipe> Recipes = new Dictionary<string, Recipe>();
        public Dictionary<string, Deck> Decks = new Dictionary<string, Deck>();
        public Dictionary<string, Legacy> Legacies = new Dictionary<string, Legacy>();
        public Dictionary<string, Ending> Endings = new Dictionary<string, Ending>();
        public Dictionary<string, Verb> Verbs = new Dictionary<string, Verb>();
        public Dictionary<string, Culture> Cultures = new Dictionary<string, Culture>();

        public ContentSource()
        {
            
        }
        
        public string GetName()
        {
            if (synopsis != null) return synopsis.name;
            else return null;
        }

        public Element GetElement(string id)
        {
            if (ElementExists(id)) return Elements[id];
            else return null;
        }

        public bool ElementExists(string id)
        {
            return Elements.ContainsKey(id);
        }

        public Deck GetDeck(string id)
        {
            if (DeckExists(id)) return Decks[id];
            else return null;
        }

        public bool DeckExists(string id)
        {
            return Decks.ContainsKey(id);
        }
        public Aspect GetAspect(string id)
        {
            if (AspectExists(id)) return Aspects[id];
            else return null;
        }

        public bool AspectExists(string id)
        {
            return Aspects.ContainsKey(id);
        }

        public Legacy GetLegacy(string id)
        {
            if (LegacyExists(id)) return Legacies[id];
            else return null;
        }

        public bool LegacyExists(string id)
        {
            return Legacies.ContainsKey(id);
        }

        public Recipe GetRecipe(string id)
        {
            if (RecipeExists(id)) return Recipes[id];
            else return null;
        }

        public bool RecipeExists(string id)
        {
            return Recipes.ContainsKey(id);
        }

        public Ending GetEnding(string id)
        {
            if (EndingExists(id)) return Endings[id];
            else return null;
        }

        public bool EndingExists(string id)
        {
            return Endings.ContainsKey(id);
        }

        public Verb GetVerb(string id)
        {
            if (VerbExists(id)) return Verbs[id];
            else return null;
        }

        public bool VerbExists(string id)
        {
            return Verbs.ContainsKey(id);
        }

        public Culture GetCulture(string id)
        {
            if (CultureExists(id)) return Cultures[id];
            else return null;
        }

        public bool CultureExists(string id)
        {
            return Cultures.ContainsKey(id);
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
