using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class Verb
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool atStart;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Slot> slots;

        [JsonConstructor]
        public Verb(string id, string label, string description, bool atStart, JArray slots = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.atStart = atStart;
            if (slots != null) this.slots = slots.ToObject<List<Slot>>();
        }

        public Verb()
        {

        }
    }
}
