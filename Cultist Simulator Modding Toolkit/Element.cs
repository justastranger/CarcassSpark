using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Element
    {
        public string icon, id, label, description, comments, decayTo, uniquenessgroup;
        public AspectDictionary aspects;
        public List<Slot> slots;
        public XTriggers xtriggers;
        public int animFrames, lifeTime;
        public bool unique;

        [JsonConstructor]
        public Element(string id, string label, string description, bool unique = false,
                       string icon = null, string comments = null, JToken aspects = null,
                       JArray slots = null, JToken xtriggers = null, int animFrames = 0,
                       int lifeTime = 0, string decayTo = null, string uniquenessgroup = null)
        {
            // necessary
            this.id = id;
            // necessary
            this.label = label;
            // necessary
            this.description = description;
            // not necessary
            if (icon != null) this.icon = icon;
            // but still included just in case
            else this.icon = id;
            // not necessary
            this.comments = comments;
            // not necessary (stay of execution)
            this.aspects = new AspectDictionary(aspects);
            // not necessary
            if (slots != null) this.slots = slots.ToObject<List<Slot>>(); //JsonConvert.DeserializeObject<Slot[]>(JsonConvert.SerializeObject(slots));
            // not necessary
            if (xtriggers != null) this.xtriggers = new XTriggers(xtriggers);
            // not necessary
            if (animFrames > 0) this.animFrames = animFrames;
            // not necessary
            if (unique) this.unique = unique;
            // not necessary
            if (uniquenessgroup != null) this.uniquenessgroup = uniquenessgroup;
            // not necessary
            if (lifeTime > 0) this.lifeTime = lifeTime;
            // not necessary, always null when lifeTime is
            if (decayTo != null) this.decayTo = decayTo;
        }
        
        public Element(string id, string label, string description,
                       string icon = null, string comments = null, AspectDictionary aspects = null,
                       JArray slots = null, JToken xtriggers = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            if (icon != null) this.icon = icon;
            else this.icon = id;
            this.comments = comments;
            this.aspects = aspects;
            if (slots != null) this.slots = this.slots = slots.ToObject<List<Slot>>(); // JsonConvert.DeserializeObject<Slot[]>(JsonConvert.SerializeObject(slots));
            if (xtriggers != null) this.xtriggers = new XTriggers(xtriggers);
        }
        
        public Element()
        {

        }

        public class XTriggers
        {
            Dictionary<string, string> internalDictionary;

            public string this[string key]
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

            public Dictionary<string,string> toDictionary()
            {
                return internalDictionary;
            }
        }
    }

    // can be either elements or aspects, only one option is required to fulfill a slot
    // example: {passion:1, glimmering:1, moth:4} to require 1 passion OR 1 glimmering OR 4 moth
    public class ElementDictionary
    {
        Dictionary<string, int> internalDictionary;

        public int this[string key]
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
            internalDictionary = new Dictionary<string, int>();
            internalDictionary[id] = amount;
        }

        [JsonConstructor]
        public ElementDictionary(JObject elementObject)
        {
            //internalDictionary = elementObject.ToObject<Dictionary<string, int>>();
            internalDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(JsonConvert.SerializeObject(elementObject));
        }

        public ElementDictionary()
        {
            internalDictionary = new Dictionary<string, int>();
        }

        public Dictionary<string, int> toDictionary()
        {
            return internalDictionary;
        }

        public string[] getKeys()
        {
            return internalDictionary.Keys.ToArray();
        }

        public int[] getValues()
        {
            return internalDictionary.Values.ToArray();
        }
    }
}
