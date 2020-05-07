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
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id, label, description, comments, defaultcard;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> spec; // the actual internal deck
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool resetonexhaustion;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int draws, defaultdraws;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> drawmessages, defaultdrawmessages;

        [JsonConstructor]
        public Deck(JArray spec, string id = null, string label = null, string description = null, string comments = null,
                    string defaultcard = null, bool resetonexhaustion = false, JObject drawmessages = null,
                    JObject defaultdrawmessages = null, int defaultdraws = 1, int draws = 1)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.spec = spec.ToObject<List<string>>();
            this.comments = comments;
            this.defaultcard = defaultcard;
            this.resetonexhaustion = resetonexhaustion;
            this.defaultdraws = defaultdraws;
            this.draws = draws;
            if (drawmessages != null) this.drawmessages = drawmessages.ToObject<Dictionary<string, string>>();
            if (defaultdrawmessages != null) this.defaultdrawmessages = defaultdrawmessages.ToObject<Dictionary<string, string>>();
        }
        
        public Deck()
        {

        }
        
    }

}
