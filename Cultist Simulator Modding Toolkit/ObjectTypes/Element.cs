using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Element
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string icon, id, label, description, comments, decayTo, uniquenessgroup;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$extend")]
        public Dictionary<string, int> aspects_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$remove")]
        public List<string> aspects_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$append")]
        public List<Slot> slots_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$prepend")]
        public List<Slot> slots_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$remove")]
        public List<string> slots_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> xtriggers;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "xtriggers$extend")]
        public Dictionary<string, string> xtriggers_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "xtriggers$remove")]
        public List<string> xtriggers_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? animframes, lifetime;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "animframes$add")]
        public int? animframes_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "animframes$minus")]
        public int? animframes_minus;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "lifetime$add")]
        public int? lifetime_add;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "lifetime$minus")]
        public int? lifetime_minus;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? unique, resaturate;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Element(string id, string label, string description, bool? unique,
                       string icon, string comments, Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspects_remove,
                       List<Slot> slots, List<Slot> slots_prepend, List<Slot> slots_append, List<string> slots_remove,
                       Dictionary<string, string> xtriggers, Dictionary<string, string> xtriggers_extend, List<string> xtriggers_remove, int? animframes, int? animframes_add, int? animframes_minus,
                       int? lifetime, int? lifetime_add, int? lifetime_minus, string decayTo, string uniquenessgroup, List<string> extends, bool? resaturate)
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
            if (aspects != null) this.aspects = aspects;
            if (aspects_extend != null) this.aspects_extend = aspects_extend;
            if (aspects_remove != null) this.aspects_remove = aspects_remove;
            // not necessary
            if (slots != null) this.slots = slots;
            if (slots_prepend != null) this.slots_prepend = slots_prepend;
            if (slots_append != null) this.slots_append = slots_append;
            if (slots_remove != null) this.slots_remove = slots_remove;
            // not necessary
            if (xtriggers != null) this.xtriggers = xtriggers;
            if (xtriggers_extend != null) this.xtriggers_extend = xtriggers_extend;
            if (xtriggers_remove != null) this.xtriggers_remove = xtriggers_remove;
            // not necessary
            if (animframes.HasValue) this.animframes = animframes;
            if (animframes_add.HasValue) this.animframes_add = animframes_add;
            if (animframes_minus.HasValue) this.animframes_minus = animframes_minus;
            // not necessary
            if (unique.HasValue) this.unique = unique;
            // not necessary
            if (uniquenessgroup != null) this.uniquenessgroup = uniquenessgroup;
            // not necessary
            if (lifetime.HasValue) this.lifetime = lifetime;
            if (lifetime_add.HasValue) this.lifetime_add = lifetime_add;
            if (lifetime_minus.HasValue) this.lifetime_minus = lifetime_minus;
            // not necessary
            if (resaturate.HasValue) this.resaturate = resaturate;
            // not necessary, always null when lifetime is
            if (decayTo != null) this.decayTo = decayTo;
            // This is only present in modded elements
            if (extends != null) this.extends = extends;
        }
        
        public Element(string id, string label, string description,
                       string icon, string comments, Dictionary<string, int> aspects,
                       List<Slot> slots, Dictionary<string, string> xtriggers, List<string> extends,
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
