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
        
        public string GetName()
        {
            if (manifest != null) return manifest.name;
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
    }
}
