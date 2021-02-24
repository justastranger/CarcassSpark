using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace CarcassSpark.ObjectTypes
{
    public class ContentSource
    {
        public Guid guid = Guid.NewGuid();

        public string currentDirectory;
        public Synopsis synopsis;

        // CustomManifest["EditMode"]
        // CustomManifest["AutoSave"]
        // CustomManifest["hiddenGroups"]
        // CustomManifest["widths"]
        // CustomManifest["saveWidths"]
        // CustomManifest["recentGroups"]
        // oh man why did I do that to myself

        public ContentGroup<Aspect> Aspects = new ContentGroup<Aspect>("Aspect", "Aspects", "aspects");
        public ContentGroup<Element> Elements = new ContentGroup<Element>("Element", "Elements", "elements");
        public ContentGroup<Recipe> Recipes = new ContentGroup<Recipe>("Recipe", "Recipes", "recipes");
        public ContentGroup<Deck> Decks = new ContentGroup<Deck>("Deck", "Decks", "decks");
        public ContentGroup<Legacy> Legacies = new ContentGroup<Legacy>("Legacy", "Legacies", "legacies");
        public ContentGroup<Ending> Endings = new ContentGroup<Ending>("Ending", "Endings", "endings");
        public ContentGroup<Verb> Verbs = new ContentGroup<Verb>("Verb", "Verbs", "verbs");
        public ContentGroup<Culture> Cultures = new ContentGroup<Culture>("Culture", "Cultures", null);

        public JObject CustomManifest { get; set; } = new JObject();

        public ContentSource()
        {

        }

        public string GetName()
        {
            return synopsis != null ? synopsis.name : null;
        }

        public void SetCustomManifestProperty(string key, object value)
        {
            CustomManifest[key] = JToken.FromObject(value);
        }

        public string GetCustomManifestString(string key)
        {
            return CustomManifest.ContainsKey(key) ? CustomManifest[key].ToString() : null;
        }

        public bool? GetCustomManifestBool(string key)
        {
            return CustomManifest.ContainsKey(key) ? CustomManifest[key].ToObject<bool?>() : null;
        }

        public int? GetCustomManifestInt(string key)
        {
            return CustomManifest.ContainsKey(key) ? CustomManifest[key].ToObject<int?>() : null;
        }

        public List<int> GetCustomManifestListInt(string key)
        {
            return CustomManifest.ContainsKey(key) ? CustomManifest[key].ToObject<List<int>>() : null;
        }

        public void SetRecentGroup(string type, string groupName)
        {
            Dictionary<string, string> recentGroups = CustomManifest["recentGroups"]?.ToObject<Dictionary<string, string>>();
            if (recentGroups == null)
            {
                recentGroups = new Dictionary<string, string>();
            }
            
            recentGroups[type] = groupName;
            CustomManifest["recentGroups"] = JObject.FromObject(recentGroups);
        }

        public string GetRecentGroup(string type)
        {
            Dictionary<string, string> recentGroups = CustomManifest["recentGroups"]?.ToObject<Dictionary<string, string>>();
            return recentGroups != null && recentGroups.ContainsKey(type) ? recentGroups[type] : null;
        }

        public Image GetAspectImage(string id)
        {
            string pathToImage = currentDirectory + "/images/aspects/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaAspectImageExists(id) ? Utilities.GetVanillaAspect(id) : null;
        }

        public Image GetElementImage(string id)
        {
            string pathToImage = currentDirectory + "/images/elements/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaElementImageExists(id) ? Utilities.GetVanillaElement(id) : null;
        }

        public Image GetEndingImage(string id)
        {
            string pathToImage = currentDirectory + "/images/endings/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaEndingImageExists(id) ? Utilities.GetVanillaEnding(id) : null;
        }

        public Image GetLegacyImage(string id)
        {
            string pathToImage = currentDirectory + "/images/legacies/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaLegacyImageExists(id) ? Utilities.GetVanillaLegacy(id) : null;
        }

        public Image GetVerbImage(string id)
        {
            string pathToImage = currentDirectory + "/images/verbs/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaVerbImageExists(id) ? Utilities.GetVanillaVerb(id) : null;
        }

        public Image GetCardBackImage(string id)
        {
            string pathToImage = currentDirectory + "/images/cardbacks/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaCardBackImageExists(id) ? Utilities.GetVanillaCardBack(id) : null;
        }

        public Image GetBurnImage(string id)
        {
            string pathToImage = currentDirectory + "/images/burns/" + id + ".png";
            return File.Exists(pathToImage)
                ? Image.FromFile(pathToImage)
                : Utilities.VanillaBurnImageImageExists(id) ? Utilities.GetVanillaBurnImage(id) : null;
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

        public void SetHiddenGroup(string type, string groupName)
        {
            Dictionary<string, List<string>> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, List<string>>>();
            if (hiddenGroups == null)
            {
                hiddenGroups = new Dictionary<string, List<string>>();
            }

            if (!hiddenGroups.ContainsKey(type))
            {
                hiddenGroups[type] = new List<string>();
            }
            hiddenGroups[type].Add(groupName);
            CustomManifest["hiddenGroups"] = JObject.FromObject(hiddenGroups);
        }

        public string[] GetHiddenGroups(string type)
        {
            Dictionary<string, string[]> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            return hiddenGroups != null && hiddenGroups.ContainsKey(type) ? hiddenGroups[type] : null;
        }

        public string[] GetHiddenGroupsFlattened()
        {
            Dictionary<string, string[]> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            string[] allGroups = new string[] { };
            foreach (string type in hiddenGroups.Keys)
            {
                allGroups.Concat(hiddenGroups[type]);
            }
            return allGroups.Count() > 0 ? allGroups : null;
        }

        public Dictionary<string, List<string>> GetHiddenGroupsDictionary()
        {
            return CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, List<string>>>();
        }

        public List<string> GetHiddenGroupsForType(string type)
        {
            Dictionary<string, List<string>> dict = GetHiddenGroupsDictionary();
            return dict.ContainsKey(type) ? dict[type] : null;
        }

        public void SetHiddenGroupsForType(string type, List<string> groups)
        {
            if (CustomManifest["hiddenGroups"] != null)
            {
                CustomManifest["hiddenGroups"][type] = JArray.FromObject(groups);
            }
            else
            {
                CustomManifest["hiddenGroups"] = JObject.FromObject(new Dictionary<string, List<string>>()
                {
                    { type, groups }
                });
            }
        }

        public void SetHiddenGroups(Dictionary<string, List<string>> hiddenGroups)
        {
            if (hiddenGroups != null && hiddenGroups.Count > 0)
            {
                CustomManifest["hiddenGroups"] = JObject.FromObject(hiddenGroups);
            }
            else if (CustomManifest["hiddenGroups"] != null)
            {
                CustomManifest.Remove("hiddenGroups");
            }
        }

        public bool GetEditMode()
        {
            return CustomManifest["EditMode"] != null ? CustomManifest["EditMode"].ToObject<bool>() : false;
        }

        public void SetEditMode(bool editMode)
        {
            CustomManifest["EditMode"] = editMode;
        }

        public void ResetHiddenGroups(string type)
        {
            Dictionary<string, List<string>> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, List<string>>>();
            if (hiddenGroups != null && hiddenGroups.ContainsKey(type))
            {
                hiddenGroups.Remove(type);
                SetHiddenGroups(hiddenGroups);
            }
        }
    }

    public class ContentGroup<T> where T : IHasGuidAndID
    {
        public Dictionary<Guid, T> GameObjects = new Dictionary<Guid, T>();

        public int Count { get => GameObjects.Count; }
        public Dictionary<Guid, T>.ValueCollection Values { get => GameObjects.Values; }
        public string DisplayName { get; private set; }
        public string DisplayNamePlural { get; private set; }
        public string Filename { get; private set; }

        public T this[Guid key] { get => GameObjects[key]; set=> GameObjects[key] = value; }

        public ContentGroup(string d, string p, string f)
        {
            DisplayName = d;
            Filename = f;
            DisplayNamePlural = p;
        }

        public bool Exists(string id)
        {
            foreach (T ending in GameObjects.Values.Where((go) => go.ID == id))
            {
                return true;
            }
            return false;
        }

        public bool Exists(Guid id)
        {
            return GameObjects.ContainsKey(id);
        }

        public T Get(Guid id)
        {
            return Exists(id) ? GameObjects[id] : default(T);
        }

        public T Get(string id)
        {
            if (Exists(id))
            {
                foreach (T gameOBJ in GameObjects.Values)
                {
                    if (gameOBJ.ID == id)
                    {
                        return gameOBJ;
                    }
                }
            }
            return default(T);
        }

        public void Clear()
        {
            GameObjects.Clear();
        }

        public void Add(Guid guid, T gameObject)
        {
            GameObjects.Add(guid, gameObject);
        }

        public void Remove(Guid guid)
        {
            GameObjects.Remove(guid);
        }

        public Dictionary<Guid, T>.Enumerator GetEnumerator()
        {
            return GameObjects.GetEnumerator();
        }
    }
}
