using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    class Verb
    {
        public string id, label, description;
        public bool atStart;
        public Slot[] slots;

        [JsonConstructor]
        public Verb(string id, string label, string description, bool atStart, JArray slots = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.atStart = atStart;
            if (slots != null) this.slots = slots.ToObject<Slot[]>();
        }
    }
}
