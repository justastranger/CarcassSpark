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
        public string icon, id, label, description, comments, decayTo, uniquenessgroup, inherits, verbicon;
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
        public Dictionary<string, List<XTrigger>> xtriggers;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "xtriggers$extend")]
        public Dictionary<string, List<XTrigger>> xtriggers_extend;
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
        public bool? unique, resaturate, deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Induces> induces;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$append")]
        public List<Induces> induces_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$prepend")]
        public List<Induces> induces_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$remove")]
        public List<string> induces_remove;

        [JsonConstructor]
        public Element(string id, string label, string description, string inherits, bool? unique, bool? deleted,
                       string icon, string comments, Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspects_remove,
                       List<Slot> slots, List<Slot> slots_prepend, List<Slot> slots_append, List<string> slots_remove,
                       Dictionary<string, List<XTrigger>> xtriggers, Dictionary<string, List<XTrigger>> xtriggers_extend, List<string> xtriggers_remove, int? animframes, int? animframes_add, int? animframes_minus,
                       int? lifetime, int? lifetime_add, int? lifetime_minus, string decayTo, string uniquenessgroup, List<string> extends, bool? resaturate)
        {
            // necessary
            this.id = id;
            // necessary
            this.label = label;
            // necessary
            this.description = description;
            // not necessary
            this.icon = icon;
            // else this.icon = id;
            // not necessary
            this.comments = comments;
            // not necessary (stay of execution)
            this.aspects = aspects;
            this.aspects_extend = aspects_extend;
            this.aspects_remove = aspects_remove;
            // not necessary
            this.slots = slots;
            this.slots_prepend = slots_prepend;
            this.slots_append = slots_append;
            this.slots_remove = slots_remove;
            // not necessary
            this.xtriggers = xtriggers;
            this.xtriggers_extend = xtriggers_extend;
            this.xtriggers_remove = xtriggers_remove;
            // not necessary
            this.animframes = animframes;
            this.animframes_add = animframes_add;
            this.animframes_minus = animframes_minus;
            // not necessary
            this.unique = unique;
            this.deleted = deleted;
            // not necessary
            this.uniquenessgroup = uniquenessgroup;
            // not necessary
            this.lifetime = lifetime;
            this.lifetime_add = lifetime_add;
            this.lifetime_minus = lifetime_minus;
            // not necessary
            this.resaturate = resaturate;
            // not necessary, always null when lifetime is
            this.decayTo = decayTo;
            // This is only present in modded elements
            this.extends = extends;
            // This is how element templating is done
            this.inherits = inherits;
        }
        
        public Element(string id, string label, string description,
                       string icon, string comments, Dictionary<string, int> aspects,
                       List<Slot> slots, Dictionary<string, List<XTrigger>> xtriggers, List<string> extends,
                       string decayTo, int? lifetime, bool? unique, int? animframes,
                       string uniquenessgroup, string inherits)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.icon = icon;
            this.comments = comments;
            this.aspects = aspects;
            this.slots = slots;
            this.xtriggers = xtriggers;
            this.extends = extends;
            this.decayTo = decayTo;
            this.lifetime = lifetime;
            this.unique = unique;
            this.animframes = animframes;
            this.uniquenessgroup = uniquenessgroup;
            this.inherits = inherits;
        }
        
        public Element()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Element Copy()
        {
            return new Element(id, label, description, inherits, unique, deleted, icon, comments, aspects != null ? new Dictionary<string, int>(aspects) : null, aspects_extend != null ? new Dictionary<string, int>(aspects_extend) : null, aspects_remove != null ? new List<string>(aspects_remove) : null, slots != null ? new List<Slot>(slots) : null, slots_prepend != null ? new List<Slot>(slots_prepend) : null, slots_append != null ? new List<Slot>(slots_append) : null, slots_remove != null ? new List<string>(slots_remove) : null, xtriggers != null ? new Dictionary<string, List<XTrigger>>(xtriggers) : null, xtriggers_extend != null ? new Dictionary<string, List<XTrigger>>(xtriggers_extend) : null, xtriggers_remove != null ? new List<string>(xtriggers_remove) : null, animframes, animframes_add, animframes_minus, lifetime, lifetime_add, lifetime_minus, decayTo, uniquenessgroup, extends != null ? new List<string>(extends) : null, resaturate);
        }
    }
    
}
