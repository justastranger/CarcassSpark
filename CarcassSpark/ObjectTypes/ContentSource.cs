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

        public JObject CustomManifest = new JObject();
        // CustomManifest["EditMode"]
        // CustomManifest["AutoSave"]

        public ContentGroup<Aspect> Aspects = new ContentGroup<Aspect>();
        public ContentGroup<Element> Elements = new ContentGroup<Element>();
        public ContentGroup<Recipe> Recipes = new ContentGroup<Recipe>();
        public ContentGroup<Deck> Decks = new ContentGroup<Deck>();
        public ContentGroup<Legacy> Legacies = new ContentGroup<Legacy>();
        public ContentGroup<Ending> Endings = new ContentGroup<Ending>();
        public ContentGroup<Verb> Verbs = new ContentGroup<Verb>();
        public ContentGroup<Culture> Cultures = new ContentGroup<Culture>();
        
        public ContentSource()
        {

        }

        public string GetName()
        {
            if (synopsis != null)
            {
                return synopsis.name;
            }
            else
            {
                return null;
            }
        }
        
        public void SetCustomManifestProperty(string key, object value)
        {
            CustomManifest[key] = JToken.FromObject(value);
        }

        public string GetCustomManifestString(string key)
        {
            if (CustomManifest.ContainsKey(key))
            {
                return CustomManifest[key].ToString();
            }
            else
            {
                return null;
            }
        }

        public bool? GetCustomManifestBool(string key)
        {
            if (CustomManifest.ContainsKey(key))
            {
                return CustomManifest[key].ToObject<bool?>();
            }
            else
            {
                return null;
            }
        }

        public int? GetCustomManifestInt(string key)
        {
            if (CustomManifest.ContainsKey(key))
            {
                return CustomManifest[key].ToObject<int?>();
            }
            else
            {
                return null;
            }
        }

        public List<int> GetCustomManifestListInt(string key)
        {
            if (CustomManifest.ContainsKey(key))
            {
                return CustomManifest[key].ToObject<List<int>>();
            }
            else
            {
                return null;
            }
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

        public string[] GetAllHiddenGroups()
        {
            Dictionary<string, string[]> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            string[] allGroups = new string[] { };
            foreach (string type in hiddenGroups.Keys)
            {
                allGroups.Concat(hiddenGroups[type]);
            }
            return allGroups.Count() > 0 ? allGroups : null;
        }

        public void ResetHiddenGroups(string type)
        {
            Dictionary<string, string[]> hiddenGroups = CustomManifest["hiddenGroups"]?.ToObject<Dictionary<string, string[]>>();
            if (hiddenGroups != null && hiddenGroups.ContainsKey(type))
            {
                hiddenGroups.Remove(type);
                CustomManifest["hiddenGroups"] = JObject.FromObject(hiddenGroups);
            }
        }
    }

    public class ContentGroup<T> where T : IHasGuidAndID
    {
        public Dictionary<Guid, T> GameObjects = new Dictionary<Guid, T>();

        public int Count { get => GameObjects.Count; }
        public Dictionary<Guid, T>.ValueCollection Values { get => GameObjects.Values; }
        public T this[Guid key] { get => GameObjects[key]; set=> GameObjects[key] = value; }

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
            if (Exists(id))
            {
                return GameObjects[id];
            }
            else
            {
                return default(T);
            }
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
