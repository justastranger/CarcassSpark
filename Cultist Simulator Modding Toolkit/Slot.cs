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
        public string id, label, description, actionId;
        public Required required, forbidden;
        public bool greedy;

        [JsonConstructor]
        public Slot(string id, string label, string description, JObject required = null, string actionId = null, JObject forbidden = null, bool greedy = false)
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
            if (required != null) this.required = new Required(required);
            //if (required.Children().Count() > 1) this.required = required.ToObject<ElementDictionary>();//.First.Value<int>();
            //else
            //{
            //    this.required = new ElementDictionary(((JObject)required.First)., required.First.First.Value<int>());
            //}
            // optional
            if (forbidden != null) this.forbidden = new Required(forbidden);
            // optional
            this.greedy = greedy;
        }

        public Slot(string id, string label, Required required, string description, string actionId = null, Required forbidden = null, bool greedy = false)
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
        }
    }

    public class Required
    {
        public List<Requirement> requirements = new List<Requirement>();

        [JsonConstructor]
        public Required(JObject required)
        { // { { "funds": 1 } } or { {"vitality": 1}, {"influenceheart": 1} }
            foreach (JToken childToken in required.Children())
            {
                //MessageBox.Show(JsonConvert.SerializeObject(childToken));
                Requirement tmp = new Requirement(childToken.Path, childToken.First.ToObject<int>());
                requirements.Add(tmp);
            }
        }

        public class Requirement
        {
            public string id;
            public int amount;

            public Requirement(string id, int amount)
            {
                this.id = id;
                this.amount = amount;
            }
        }
    }
}
