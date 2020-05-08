using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Element
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string icon, id, label, description, comments, decayTo, uniquenessgroup;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> xtriggers;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? animframes, lifetime;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? unique, resaturate;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string[] extends;

        [JsonConstructor]
        public Element(string id, string label, string description, bool? unique,
                       string icon, string comments, JToken aspects,
                       JArray slots, JToken xtriggers, int? animFrames,
                       int? lifetime, string decayTo, string uniquenessgroup, JArray extends, bool? resaturate)
        {
            // necessary
            this.id = id;
            // necessary
            this.label = label;
            // necessary
            this.description = description;
            // not necessary
            if (icon != null) this.icon = icon;
            else this.icon = id;
            // not necessary
            this.comments = comments;
            // not necessary (stay of execution)
            if (aspects != null) this.aspects = aspects.ToObject<Dictionary<string, int>>();
            // not necessary
            if (slots != null) this.slots = slots.ToObject<List<Slot>>();
            // not necessary
            if (xtriggers != null) this.xtriggers = xtriggers.ToObject<Dictionary<string, string>>();
            // not necessary
            if (animFrames.HasValue) this.animframes = animFrames;
            // not necessary
            if (unique.HasValue) this.unique = unique;
            // not necessary
            if (uniquenessgroup != null) this.uniquenessgroup = uniquenessgroup;
            // not necessary
            if (lifetime.HasValue) this.lifetime = lifetime;
            // not necessary
            if (resaturate.HasValue) this.resaturate = resaturate;
            // not necessary, always null when lifetime is
            if (decayTo != null) this.decayTo = decayTo;
            // This is only present in modded elements
            if (extends != null) this.extends = extends.ToObject<string[]>();
        }
        
        public Element(string id, string label, string description,
                       string icon, string comments, Dictionary<string, int> aspects,
                       List<Slot> slots, Dictionary<string, string> xtriggers, string[] extends,
                       string decayTo, int? lifetime, bool? unique, int? animframes,
                       string uniquenessgroup)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            if (icon != null) this.icon = icon;
            else this.icon = id;
            this.comments = comments;
            if (aspects != null) this.aspects = aspects;
            if (slots != null) this.slots = slots;
            if (xtriggers != null) this.xtriggers = xtriggers;
            if (extends != null) this.extends = extends;
            if (decayTo != null) this.decayTo = decayTo;
            if (lifetime.HasValue) this.lifetime = lifetime;
            if (unique.HasValue) this.unique = unique;
            if (animframes.HasValue) this.animframes = animframes;
            if (uniquenessgroup != null) this.uniquenessgroup = uniquenessgroup; ;
        }
        
        public Element()
        {

        }
        
    }
    
}
