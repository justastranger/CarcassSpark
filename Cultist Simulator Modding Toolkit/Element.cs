using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    class Element
    {
        public string icon, id, label, description, comments;
        public AspectDictionary aspects;
        public Slot[] slots;
        public JToken xtriggers;

        [JsonConstructor]
        public Element(string id = null, string label = null, string description = null,
                       string icon = null, string comments = null, JToken aspects = null,
                       JArray slots = null, JToken xtriggers = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            if (icon != null) this.icon = icon;
            else this.icon = id;
            this.comments = comments;
            this.aspects = new AspectDictionary(aspects);

        }

        // Just like ElementDictionary, except aspect IDs only
        // example: {lantern: 4, tool: 1, auctionable: 2} to require 4 lantern AND 1 tool AND 2 auctionable
        public class AspectDictionary : Dictionary<string, int>
        {
            Dictionary<string, int> internalDictionary;

            public AspectDictionary(JToken aspects)
            {
                this.internalDictionary = JsonConvert.DeserializeObject<ElementDictionary>(JsonConvert.SerializeObject(aspects));
            }

            public AspectDictionary(string id, int amount)
            {
                this.internalDictionary = new Dictionary<string, int>();
                this.internalDictionary[id] = amount;
            }
        }

        public class Slot
        {
            public string id, label, description, actionId;
            public ElementDictionary required;

            public Slot(string id, string label, ElementDictionary required, string description, string actionId)
            {

            }
        }
    }

    // can be either elements or aspects, only one option is required to fulfill a slot
    // example: {passion:1, glimmering:1, moth:4} to require 1 passion OR 1 glimmering OR 4 moth
    public class ElementDictionary : Dictionary<string, int>
    {
        Dictionary<string, int> internalDictionary;

        public ElementDictionary(string id, int amount)
        {
            this.internalDictionary = new Dictionary<string, int>();
            this.internalDictionary[id] = amount;
        }

        [JsonConstructor]
        public ElementDictionary(JToken elementObject)
        {
            this.internalDictionary = JsonConvert.DeserializeObject<ElementDictionary>(JsonConvert.SerializeObject(elementObject));
        }
    }
}
