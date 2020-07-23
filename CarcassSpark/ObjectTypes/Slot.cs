using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarcassSpark.ObjectTypes
{
    public class Slot
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, actionId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> required, forbidden;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? greedy, consumes, noanim;
        
        [JsonConstructor]
        public Slot(string id, string label, string description, bool? greedy, bool? consumes, Dictionary<string, int> required, string actionId, Dictionary<string, int> forbidden)
        {
            //necessary
            this.id = id;
            //necessary
            this.label = label;
            // necessary
            this.description = description;
            // necessary
            this.actionId = actionId;
            // optional
            // required.First -> { "funds" : 1 } somehow
            this.required = required;
            // optional
            this.forbidden = forbidden;
            // optional
            this.greedy = greedy;
            this.consumes = consumes;
        }

        public Slot(string id, string label, Dictionary<string, int> required, string description, bool? greedy, bool? consumes, string actionId, Dictionary<string, int> forbidden)
        {
            //necessary
            this.id = id;
            //necessary
            this.label = label;
            // necessary
            this.description = description;
            // necessary
            this.actionId = actionId;
            // necessary
            this.required = required;
            // optional
            this.forbidden = forbidden;
            // optional
            this.greedy = greedy;
            // optional
            this.consumes = consumes;
        }

        public Slot()
        {

        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Slot Copy()
        {
            return new Slot(id, label, description, greedy, consumes, required != null ? new Dictionary<string, int>(required) : null, actionId, forbidden != null ? new Dictionary<string, int>(forbidden) : null);
        }
    }
}
