using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class ContentSource
    {

        public string currentDirectory;
        public Manifest manifest;

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

        public ContentSource()
        {
            
        }
        
        public string getName()
        {
            if (manifest != null) return manifest.name;
            else return null;
        }

        public Element getElement(string id)
        {
            if (elementExists(id)) return Elements[id];
            else return null;
        }

        public bool elementExists(string id)
        {
            return Elements.ContainsKey(id);
        }

        public Deck getDeck(string id)
        {
            if (deckExists(id)) return Decks[id];
            else return null;
        }

        public bool deckExists(string id)
        {
            return Decks.ContainsKey(id);
        }
        public Aspect getAspect(string id)
        {
            if (aspectExists(id)) return Aspects[id];
            else return null;
        }

        public bool aspectExists(string id)
        {
            return Aspects.ContainsKey(id);
        }

        public Legacy getLegacy(string id)
        {
            if (legacyExists(id)) return Legacies[id];
            else return null;
        }

        public bool legacyExists(string id)
        {
            return Legacies.ContainsKey(id);
        }

        public Recipe getRecipe(string id)
        {
            if (recipeExists(id)) return Recipes[id];
            else return null;
        }

        public bool recipeExists(string id)
        {
            return Recipes.ContainsKey(id);
        }

        public Ending getEnding(string id)
        {
            if (endingExists(id)) return Endings[id];
            else return null;
        }

        public bool endingExists(string id)
        {
            return Endings.ContainsKey(id);
        }

        public Verb getVerb(string id)
        {
            if (verbExists(id)) return Verbs[id];
            else return null;
        }

        public bool verbExists(string id)
        {
            return Verbs.ContainsKey(id);
        }

        public void setCustomManifestProperty(string key, object value)
        {
            CustomManifest[key] = JToken.FromObject(value);
        }

        public string getCustomManifestString(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToString();
            else return null;
        }

        public bool? getCustomManifestBool(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToObject<bool?>();
            else return null;
        }

        public int? getCustomManifestInt(string key)
        {
            if (CustomManifest.ContainsKey(key)) return CustomManifest[key].ToObject<int?>();
            else return null;
        }
    }
}
