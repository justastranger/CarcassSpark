using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultist_Simulator_Modding_Toolkit
{
    public class Deck
    {
        public string id, label, description, comments, defaultcard;
        public string[] spec; // the actual internal deck
        public bool resetonexhaustion;
        public DrawMessages drawmessages, defaultdrawmessages;

        public Deck(string id, string label, string description, JArray spec, string comments = null,
                    string defaultcard = null, bool resetonexhaustion = false, JToken drawmessages = null,
                    JToken defaultdrawmessages = null)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.spec = spec.ToObject<string[]>();
            this.comments = comments;
            this.defaultcard = defaultcard;
            this.resetonexhaustion = resetonexhaustion;
            if (drawmessages != null) this.drawmessages = drawmessages.ToObject<DrawMessages>();
            if (defaultdrawmessages != null) this.defaultdrawmessages = defaultdrawmessages.ToObject<DrawMessages>();
        }




        public class DrawMessages
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

            [JsonConstructor]
            public DrawMessages(JToken drawmessages)
            {
                this.internalDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(drawmessages));
            }
        }
    }

}
