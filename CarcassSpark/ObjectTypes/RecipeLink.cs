using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassSpark.ObjectTypes
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
        public Dictionary<string, string> challenges; // string aspect ID, string challenge type: "base", "advanced". Default if null: "base"
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Expulsion expulsion;

        [JsonConstructor]
        public RecipeLink(string id, int? chance, bool? additional, Dictionary<string, string> challenges, Expulsion expulsion)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
            this.challenges = challenges;
            this.expulsion = expulsion;
        }

        public RecipeLink()
        {

        }

        public class Expulsion
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Dictionary<string, int> filter;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int? limit;

            [JsonConstructor]
            public Expulsion(Dictionary<string, int> filter, int? limit)
            {
                this.filter = filter;
                this.limit = limit;
            }

            public Expulsion(int limit)
            {
                filter = new Dictionary<string, int>();
                this.limit = limit;
            }
        }
    }
}
