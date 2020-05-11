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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, startdescription, image, fromEnding, startingVerbId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> excludesOnEnding;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? availableWithoutEndingMatch;

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription,
                      JObject effects, string image, string fromEnding, bool? availableWithoutEndingMatch,
                      string startingVerbId = null, JArray excludesOnEnding = null)
        {
            if (id != null) this.id = id;
            if (label != null) this.label = label;
            if (description != null) this.description = description;
            if (startdescription != null) this.startdescription = startdescription;
            if (effects != null) this.effects = effects.ToObject<Dictionary<string, int>>();
            if (image != null) this.image = image;
            if (fromEnding != null) this.fromEnding = fromEnding;
            if (availableWithoutEndingMatch.HasValue) this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            if (startingVerbId != null) this.startingVerbId = startingVerbId;
            if (excludesOnEnding != null) this.excludesOnEnding = excludesOnEnding.ToObject<List<string>>();
        }

        public Legacy()
        {

        }
    }
}
