using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultistSimulatorModdingToolkit.ObjectTypes
{
    public class RecipeLink
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? chance;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? additional;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> challenges;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Expulsion expulsion;

        [JsonConstructor]
        public RecipeLink(string id, int? chance, bool? additional, JObject challenges = null, JObject expulsion = null)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
            if (challenges != null)
            {
                this.challenges = challenges.ToObject<Dictionary<string, string>>();
            }
            if (expulsion != null)
            {
                this.expulsion = expulsion.ToObject<Expulsion>();
            }
        }

        public RecipeLink()
        {

        }

        public class Expulsion
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Dictionary<string, int> filter;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int limit;

            [JsonConstructor]
            public Expulsion(Dictionary<string, int> filter, int? limit)
            {
                this.filter = filter;
                if (limit.HasValue) this.limit = limit.Value;
            }

            public Expulsion(int limit)
            {
                filter = new Dictionary<string, int>();
                this.limit = limit;
            }
        }
    }
}
