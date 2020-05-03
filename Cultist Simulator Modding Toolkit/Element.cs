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
        public XTriggers xtriggers;

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
            if (slots != null) this.slots = JsonConvert.DeserializeObject<Slot[]>(JsonConvert.SerializeObject(slots));
            if (xtriggers != null) this.xtriggers = new XTriggers(xtriggers);
        }

        

        public class Slot
        {
            public string id, label, description, actionId;
            public ElementDictionary required, forbidden;

            public Slot(string id, string label, ElementDictionary required, string description, string actionId, ElementDictionary forbidden = null)
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
            }
        }

        public class XTriggers : Dictionary<string, string>
        {
            Dictionary<string, string> internalDictionary;

            new public string this[string key]
            {
                get
                {
                    return internalDictionary[key];
                }
                set
                {
                    internalDictionary[key] = value;
                }
            }

            public XTriggers(JToken xtriggers)
            {
                this.internalDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(xtriggers));
            }

            public XTriggers(string aspectID, string newElement)
            {
                this.internalDictionary = new Dictionary<string, string>();
                this.internalDictionary[aspectID] = newElement;
            }
        }
    }

    // can be either elements or aspects, only one option is required to fulfill a slot
    // example: {passion:1, glimmering:1, moth:4} to require 1 passion OR 1 glimmering OR 4 moth
    public class ElementDictionary : Dictionary<string, int>
    {
        Dictionary<string, int> internalDictionary;

        new public int this[string key]
        {
            get
            {
                return internalDictionary[key];
            }
            set
            {
                internalDictionary[key] = value;
            }
        }

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
