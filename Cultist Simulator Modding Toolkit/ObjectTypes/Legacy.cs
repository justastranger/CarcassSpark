using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class Legacy
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, startdescription, image, fromEnding, startingVerbId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? availableWithoutEndingMatch;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> excludesOnEnding;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$append")]
        public List<string> excludesOnEnding_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$prepend")]
        public List<string> excludesOnEnding_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "excludesOnEnding$remove")]
        public List<string> excludesOnEnding_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> effects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$extend")]
        public Dictionary<string, int> effects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effects$remove")]
        public List<string> effects_remove;

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription,
                      Dictionary<string, int> effects, string image, string fromEnding, bool? availableWithoutEndingMatch,
                      string startingVerbId, List<string> excludesOnEnding,
                      List<string> excludesOnEnding_prepend, List<string> excludesOnEnding_append, List<string> excludesOnEnding_remove,
                      Dictionary<string, int> effects_extend, List<string> effects_remove)
        {
            if (id != null) this.id = id;
            if (label != null) this.label = label;
            if (description != null) this.description = description;
            if (startdescription != null) this.startdescription = startdescription;
            if (effects != null) this.effects = effects;
            if (effects_extend != null) this.effects_extend = effects_extend;
            if (effects_remove != null) this.effects_remove = effects_remove;
            if (image != null) this.image = image;
            if (fromEnding != null) this.fromEnding = fromEnding;
            if (availableWithoutEndingMatch.HasValue) this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            if (startingVerbId != null) this.startingVerbId = startingVerbId;
            if (excludesOnEnding != null) this.excludesOnEnding = excludesOnEnding;
            if (excludesOnEnding_prepend != null) this.excludesOnEnding_prepend = excludesOnEnding_prepend;
            if (excludesOnEnding_append != null) this.excludesOnEnding_append = excludesOnEnding_append;
            if (excludesOnEnding_remove != null) this.excludesOnEnding_remove = excludesOnEnding_remove;
        }

        public Legacy()
        {

        }
    }
}
