using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Slot
    {
        public string id, label, description, actionId;
        public ElementDictionary required, forbidden;
        public bool greedy;

        public Slot(string id, string label, ElementDictionary required, string description, string actionId, ElementDictionary forbidden = null, bool greedy = false)
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
            if (greedy) this.greedy = greedy;
        }
    }
}
