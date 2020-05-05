using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Legacy
    {
        public string id, label, description, startdescription, image, fromEnding, startingVerbId;
        public string[] excludesOnEnding;
        public ElementDictionary effects;
        public bool availableWithoutEndingMatch;

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription,
                      JObject effects, string image, string fromEnding, bool availableWithoutEndingMatch,
                      string startingVerbId = null, JArray excludesOnEnding = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.startdescription = startdescription;
            this.effects = new ElementDictionary(effects);
            this.image = image;
            this.fromEnding = fromEnding;
            this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            this.startingVerbId = startingVerbId;
            if (excludesOnEnding != null) this.excludesOnEnding = excludesOnEnding.ToObject<string[]>();
        }

        public static Legacy getLegacy(string id)
        {
            return MainForm.legaciesList[id];
        }

        public static bool legacyExists(string id)
        {
            return MainForm.legaciesList.ContainsKey(id);
        }
    }
}
