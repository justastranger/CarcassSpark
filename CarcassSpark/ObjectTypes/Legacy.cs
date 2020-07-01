using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Legacy
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, startdescription, image, fromEnding, startingVerbId, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? availableWithoutEndingMatch, deleted;
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> statusbarelements;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&prepend")]
        public List<string> statusbarelements_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&append")]
        public List<string> statusbarelements_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statusbarelements&remove")]
        public List<string> statusbarelements_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Legacy(string id, string label, string description, string startdescription, string comments,
                      Dictionary<string, int> effects, string image, string fromEnding, bool? availableWithoutEndingMatch,
                      string startingVerbId, List<string> excludesOnEnding,
                      List<string> excludesOnEnding_prepend, List<string> excludesOnEnding_append, List<string> excludesOnEnding_remove,
                      Dictionary<string, int> effects_extend, List<string> effects_remove, List<string> statusbarelements,
                      List<string> statusbarelements_prepend, List<string> statusbarelements_append, List<string> statusbarelements_remove,
                      List<string> extends, bool? deleted)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.startdescription = startdescription;
            this.effects = effects;
            this.effects_extend = effects_extend;
            this.effects_remove = effects_remove;
            this.comments = comments;
            this.image = image;
            this.fromEnding = fromEnding;
            this.extends = extends;
            this.availableWithoutEndingMatch = availableWithoutEndingMatch;
            this.startingVerbId = startingVerbId;
            this.excludesOnEnding = excludesOnEnding;
            this.excludesOnEnding_prepend = excludesOnEnding_prepend;
            this.excludesOnEnding_append = excludesOnEnding_append;
            this.excludesOnEnding_remove = excludesOnEnding_remove;
            this.statusbarelements = statusbarelements;
            this.statusbarelements_prepend = statusbarelements_prepend;
            this.statusbarelements_append = statusbarelements_append;
            this.statusbarelements_remove = statusbarelements_remove;
            this.deleted = deleted;
        }

        public Legacy()
        {

        }

        public string toString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Legacy Copy()
        {
            return new Legacy(id, label, description, startdescription, comments, effects != null ? new Dictionary<string, int>(effects) : null, image, fromEnding, availableWithoutEndingMatch, startingVerbId, excludesOnEnding != null ? new List<string>(excludesOnEnding) : null, excludesOnEnding_prepend != null ? new List<string>(excludesOnEnding_prepend) : null, excludesOnEnding_append != null ? new List<string>(excludesOnEnding_append) : null, excludesOnEnding_remove != null ? new List<string>(excludesOnEnding_remove) : null, effects_extend != null ? new Dictionary<string, int>(effects_extend) : null, effects_remove != null ? new List<string>(effects_remove) : null, statusbarelements != null ? new List<string>(statusbarelements) : null, statusbarelements_prepend != null ? new List<string>(statusbarelements_prepend) : null, statusbarelements_append != null ? new List<string>(statusbarelements_append) : null, statusbarelements_remove != null ? new List<string>(statusbarelements_remove) : null, extends != null ? new List<string>(extends) : null, deleted);
        }
    }
}
