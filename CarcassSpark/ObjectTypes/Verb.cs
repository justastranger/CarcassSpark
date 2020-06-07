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
        public string id, label, description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? atStart;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$append")]
        public List<Slot> slots_append;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$prepend")]
        public List<Slot> slots_prepend;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "slots$remove")]
        public List<Slot> slots_remove;

        [JsonConstructor]
        public Verb(string id, string label, string description, bool? atStart, List<Slot> slots,
                    List<Slot> slots_prepend, List<Slot> slots_append, List<Slot> slots_remove)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.atStart = atStart;
            this.slots = slots;
            this.slots_prepend = slots_prepend;
            this.slots_append = slots_append;
            this.slots_remove = slots_remove;
        }

        public Verb()
        {

        }

        public Verb Copy()
        {
            Verb tmp = new Verb();
            tmp.id = id;
            tmp.label = label;
            tmp.description = description;
            tmp.atStart = atStart;
            tmp.slots = slots;
            tmp.slots_prepend = slots_prepend;
            tmp.slots_append = slots_append;
            tmp.slots_remove = slots_remove;
            return tmp;
        }
    }
}
