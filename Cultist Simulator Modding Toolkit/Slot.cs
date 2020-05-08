using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Slot
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, actionId;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> required, forbidden;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? greedy, consumes;

        [JsonConstructor]
        public Slot(string id, string label, string description, bool? greedy, bool? consumes, JObject required = null, string actionId = null, JObject forbidden = null)
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
            if (required != null) this.required = required.ToObject<Dictionary<string,int>>();
            //if (required.Children().Count() > 1) this.required = required.ToObject<ElementDictionary>();//.First.Value<int>();
            //else
            //{
            //    this.required = new ElementDictionary(((JObject)required.First)., required.First.First.Value<int>());
            //}
            // optional
            if (forbidden != null) this.forbidden = forbidden.ToObject<Dictionary<string, int>>();
            // optional
            this.greedy = greedy;
            this.consumes = consumes;
        }

        public Slot(string id, string label, Dictionary<string, int> required, string description, bool? greedy, bool? consumes, string actionId = null, Dictionary<string, int> forbidden = null)
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
    }
}
