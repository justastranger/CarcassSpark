using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarcassSpark.ObjectTypes
{
    public class Element : IGameObject
    {
        [JsonIgnore]
        public string filename;
        [JsonIgnore]
        public Guid guid = Guid.NewGuid();
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string icon, id, label, description, comments, decayTo, uniquenessgroup, inherits, verbicon;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> aspects;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "aspects$add")]
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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "xtriggers$add")]
        public Dictionary<string, List<XTrigger>> xtriggers_extend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "xtriggers$remove")]
        public List<string> xtriggers_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? lifetime;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? unique, resaturate, deleted, isAspect, isHidden, noartneeded;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Induces> induces;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$append")]
        public List<Induces> induces_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$prepend")]
        public List<Induces> induces_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "induces$remove")]
        public List<string> induces_remove;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonIgnore]
        public string Filename { get => this.filename; set => this.filename = value; }
        [JsonIgnore]
        public Guid Guid { get => this.guid; set => this.guid = value; }
        [JsonIgnore]
        public string ID { get => this.id; set => this.id = value; }

        [JsonConstructor]
        public Element(string id, string label, string description, string inherits, bool? unique, bool? deleted,
                       string icon, string comments, Dictionary<string, int> aspects, Dictionary<string, int> aspects_extend, List<string> aspectsRemove,
                       List<Slot> slots, List<Slot> slotsPrepend, List<Slot> slotsAppend, List<string> slotsRemove,
                       Dictionary<string, List<XTrigger>> xtriggers, Dictionary<string, List<XTrigger>> xtriggersExtend, List<string> xtriggersRemove,
                       int? lifetime, string decayTo, string uniquenessgroup, bool? resaturate, List<string> extends)
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
            this.aspects_remove = aspectsRemove;
            // not necessary
            this.slots = slots;
            this.slots_prepend = slotsPrepend;
            this.slots_append = slotsAppend;
            this.slots_remove = slotsRemove;
            // not necessary
            this.xtriggers = xtriggers;
            this.xtriggers_extend = xtriggersExtend;
            this.xtriggers_remove = xtriggersRemove;
            // not necessary
            this.unique = unique;
            this.deleted = deleted;
            // not necessary
            this.uniquenessgroup = uniquenessgroup;
            // not necessary
            this.lifetime = lifetime;
            // not necessary
            this.resaturate = resaturate;
            // not necessary, always null when lifetime is
            this.decayTo = decayTo;
            // This is how element templating is done
            this.inherits = inherits;
            // extends has been transformed into a proper inheritance/templating system, making the above possibly obsolete
            this.extends = extends;
        }

        public Element(string id, string label, string description,
                       string icon, string comments, Dictionary<string, int> aspects,
                       List<Slot> slots, Dictionary<string, List<XTrigger>> xtriggers,
                       string decayTo, int? lifetime, bool? unique,
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
            this.decayTo = decayTo;
            this.lifetime = lifetime;
            this.unique = unique;
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
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Element>(serializedObject);
        }

        Element IGameObject.Copy<Element>()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Element>(serializedObject);
        }

        public bool HasSlots()
        {
            return slots_prepend != null || slots != null || slots_append != null;
        }

        public List<Slot> AllSlotsWhere(Predicate<Slot> predicate)
        {
            List<Slot> OutputSlots = new List<Slot>();
            if (slots_prepend != null)
            {
                OutputSlots.AddRange(slots_prepend.FindAll(predicate));
            }

            if (slots != null)
            {
                OutputSlots.AddRange(slots.FindAll(predicate));
            }

            if (slots_append != null)
            {
                OutputSlots.AddRange(slots_append.FindAll(predicate));
            }

            return OutputSlots;
        }
    }

}
