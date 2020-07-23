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
        public RecipeLink(string id, int? chance, bool? additional, object challenges, Expulsion expulsion)
        {
            this.id = id;
            this.chance = chance;
            this.additional = additional;
            if (challenges != null)
            {
                if (challenges is string) this.challenges = new Dictionary<string, string>() { [challenges as string] = "base" };
                else if (challenges is Dictionary<string, string>)
                {
                    this.challenges = challenges as Dictionary<string, string>;
                }
            }
            this.expulsion = expulsion;
        }

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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public RecipeLink Copy()
        {
            return new RecipeLink(id, chance, additional, challenges != null ? new Dictionary<string, string>(challenges) : null, expulsion != null ? expulsion.Copy() : null);
        }
    }
}
