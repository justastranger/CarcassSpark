using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
{
    public class Verb
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? atStart, deleted;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slot;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$append")]
        public List<Slot> slot_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$prepend")]
        public List<Slot> slot_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$remove")]
        public List<Slot> slot_remove;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> extends;

        [JsonConstructor]
        public Verb(string id, string label, string description, string comments, List<string> extends, bool? atStart, bool? deleted, List<Slot> slot,
                    List<Slot> slot_prepend, List<Slot> slot_append, List<Slot> slot_remove)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.comments = comments;
            this.extends = extends;
            this.atStart = atStart;
            this.slot = slot;
            this.slot_prepend = slot_prepend;
            this.slot_append = slot_append;
            this.slot_remove = slot_remove;
            this.deleted = deleted;
        }

        public Verb()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Verb Copy()
        {
            return new Verb(id, label, description, comments, extends != null ? new List<string>(extends) : null, atStart, deleted, slots != null ? new List<Slot>(slots) : null, slots_prepend != null ? new List<Slot>(slots_prepend) : null, slots_append != null ? new List<Slot>(slots_append) : null, slots_remove != null ? new List<Slot>(slots_remove) : null);
        }
    }
}
